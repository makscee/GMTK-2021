using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : BindableMonoBehavior, IDragHandler, IBeginDragHandler, IEndDragHandler, IBindHandler
{
    public bool[] occupiedSides = new bool[4]; 
    public virtual void OnDrag(PointerEventData eventData)
    {
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
    }


    public void OnBind(Bind bind)
    {
        if (bind.First is Block && bind.Second is Block)
        {
            var offset = bind.Offset;
            var dir = new Vector2Int((int) offset.x, (int) offset.y);
            if (bind.Second == this) dir *= -1;
            occupiedSides[Utils.DirFromCoords(dir)] = true;
        }
    }

    public void OnUnbind(Bind bind)
    {
        if (bind.First is Block && bind.Second is Block)
        {
            var offset = bind.Offset;
            var dir = new Vector2Int((int) offset.x, (int) offset.y);
            if (bind.Second == this) dir *= -1;
            occupiedSides[Utils.DirFromCoords(dir)] = false;
        }
    }
}