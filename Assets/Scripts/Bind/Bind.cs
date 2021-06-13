using System;
using UnityEngine;

public class Bind
{
    public BindVisual visual;
    public Bind(IBindable fist, IBindable second, Vector2 offset, int strength, float maxLength = -1f)
    {
        First = fist;
        Second = second;
        Offset = offset;
        Strength = strength;
        MaxLength = maxLength;
    }

    public readonly IBindable First, Second;
    
    // first -> second
    public readonly Vector2 Offset;

    public int Strength;
    public float MaxLength;

    public bool Used(IBindable obj)
    {
        return obj == First || obj == Second;
    }

    public Vector2 GetTarget(IBindable self)
    {
        if (First != self && Second != self) throw new Exception("Trying to get target for object that is not part of the bind");
        Vector2 target;
        var firstPos = First.GetPosition();
        var secondPos = Second.GetPosition();
        if (First == self) target = secondPos - Offset;
        else target = firstPos + Offset;
        return target;
    }

    public bool MaxLengthReached()
    {
        return MaxLength != -1f && (First.GetPosition() - Second.GetPosition() + Offset).magnitude > MaxLength;
    }

    public void DestroyVisual()
    {
        if (visual != null)
            visual.Destroy();
    }

    public void Break()
    {
        BindMatrix.RemoveBind(First, Second);
        DestroyVisual();
    }
}