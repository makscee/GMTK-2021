using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeBlock : Block
{
    public ShapeConnectionCheckerBase checkerBase;
    public GameObject connectionDot;
    public HashSet<ShapeBlock> allBlocks;
    public List<Bind> additionalBinds = new List<Bind>();
    public static ShapeBlock Create(Vector2 position)
    {
        return Instantiate(Prefabs.Instance.shapeBlock, position, Quaternion.identity).GetComponent<ShapeBlock>();
    }
    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        BindMatrix.AddBind(this, MouseBind.Get(), Vector2.zero, 10);
        var str = GlobalConfig.Instance.shapesBindStr;
        foreach (var block in allBlocks)
        {
            block.checkerBase.gameObject.SetActive(true);
            block.connectionDot.SetActive(false);

            var adjacentBinds = new List<Bind>(BindMatrix.GetAllAdjacentBinds(block));
            foreach (var bind in adjacentBinds.Where(bind => bind.Strength == str))
                bind.Break();
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        BindMatrix.RemoveBind(this, MouseBind.Get());

        foreach (var block in allBlocks)
        {
            block.checkerBase.TryToConnect();
            block.checkerBase.gameObject.SetActive(false);
            block.connectionDot.SetActive(true);
        }
    }
}