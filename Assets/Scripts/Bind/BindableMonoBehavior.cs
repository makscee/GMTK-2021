using UnityEngine;

public class BindableMonoBehavior : MonoBehaviour, IBindable
{
    public bool Movable = true;

    protected Vector2 Velocity;
    public Vector2 DesiredVelocity;
    public const float MaxAcceleration = 300;
    protected virtual void FixedUpdate()
    {
        if (!Movable) return;
        var delta = Time.deltaTime;
        var maxSpeedChange = MaxAcceleration * delta;
        
        foreach (var bind in BindMatrix.GetAllAdjacentBinds(this))
        {
            if (bind.Strength == 0) continue;
            var v = (bind.GetTarget(this) - GetPosition()) / 2;
            var f = v * (bind.Strength * Bind.StrengthMultiplier);

            DesiredVelocity += f;
        }
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