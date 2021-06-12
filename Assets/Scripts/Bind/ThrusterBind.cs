using UnityEngine;

public class ThrusterBind : IBindable
{
    Bind _bind;
    Thruster _thruster;

    public ThrusterBind(Thruster t)
    {
        _thruster = t;
    }

    public Vector2 GetPosition()
    {
        return (Vector2) _thruster.transform.position + _thruster.GetOffsetPosition();
    }

    public bool IsAnchor()
    {
        return true;
    }

    public bool IsAnchored
    {
        get => true;
        set { }
    }

    public bool Used { get; set; }
}