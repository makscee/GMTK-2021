using System.Collections.Generic;
using UnityEngine;

public class BindableMonoBehavior : MonoBehaviour, IBindable
{
    public bool Movable = true;

    protected Vector2 Velocity;
    public Vector2 DesiredVelocity;
    protected virtual void FixedUpdate()
    {
        if (!Movable) return;
        var delta = Time.deltaTime;
        var maxSpeedChange = GlobalConfig.Instance.bindMaxAcc * delta;
        
        var brokenBinds = new List<Bind>();
        foreach (var bind in BindMatrix.GetAllAdjacentBinds(this))
        {
            if (bind.Strength == 0) continue;
            var v = (bind.GetTarget(this) - GetPosition()) / GlobalConfig.Instance.bindTargetDiv;
            var f = v * (bind.Strength * GlobalConfig.Instance.bindStrMult);
            if (bind.MaxLengthReached()) brokenBinds.Add(bind);
            DesiredVelocity += f;
        }

        foreach (var bind in brokenBinds) bind.Break();
        // if (!IsAnchored)
        // {
        //     const float radius = 0.1f;
        //     const float speed = 1f;
        //     var t = Time.time;
        //     DesiredVelocity += new Vector2(Mathf.Cos(t * speed) * radius, Mathf.Sin(t * speed) * radius);
        // }
        
        Velocity = Vector2.MoveTowards(Velocity, DesiredVelocity, maxSpeedChange);
        transform.position += (Vector3)Velocity * delta;
        DesiredVelocity = Vector2.zero;
    }

    public Vector2 GetPosition()
    {
        if (!_destroyed)
            return transform.position;
        return Vector2.zero;
    }

    public virtual bool IsAnchor()
    {
        return false;
    }

    public bool IsAnchored { get; set; }

    public bool Used { get; set; }

    bool _destroyed;
    bool _isAnchored;

    protected virtual void OnDestroy()
    {
        _destroyed = true;
    }
}