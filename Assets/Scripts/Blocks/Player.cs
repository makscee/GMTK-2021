using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Block
{
    public List<Thruster> thrusters;
    public Vector2 currentInputDir;
    
    public void InputDir(Vector2 dir)
    {
        currentInputDir = dir;
    }
    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        BindMatrix.AddBind(this, MouseBind.Get(), Vector2.zero, 10);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        BindMatrix.RemoveBind(this, MouseBind.Get());
    }
}