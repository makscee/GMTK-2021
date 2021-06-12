using UnityEngine;

public class ShapeBlock : Block
{
    public static ShapeBlock Create(Vector2 position)
    {
        return Instantiate(Prefabs.Instance.shapeBlock, position, Quaternion.identity).GetComponent<ShapeBlock>();
    }
}