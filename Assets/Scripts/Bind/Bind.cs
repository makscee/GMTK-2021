using System;
using UnityEngine;

public class Bind
{
    public BindVisual visual;
    public Bind(IBindable fist, IBindable second, Vector2 offset, int strength)
    {
        First = fist;
        Second = second;
        Offset = offset;
        Strength = strength;
    }

    public readonly IBindable First, Second;
    
    // first -> second
    public readonly Vector2 Offset;

    public readonly int Strength;
    public const int StrengthMultiplier = 3;

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
        var selfPos = self.GetPosition();
        if (First == self) target = secondPos - Offset;
        else target = firstPos + Offset;
        return target;
    }

    public void Break()
    {
        BindMatrix.RemoveBind(First, Second);
        if (visual != null)
            visual.Destroy();
    }
}