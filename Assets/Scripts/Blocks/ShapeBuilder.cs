using System.Collections.Generic;
using UnityEngine;

public class ShapeBuilder
{
    public static void CreateShape(int blocks, Vector2 position)
    {
        var blocksMatrix = new ShapeBlock[15][];
        for (var index = 0; index < 15; index++)
        {
            blocksMatrix[index] = new ShapeBlock[15];
        }

        var pos = new Vector2Int(5, 5);
        for (var i = 0; i < blocks; i++)
        {
            var block = ShapeBlock.Create(position);
            blocksMatrix[pos.x][pos.y] = block;
            var freeTiles = new List<Vector2Int>();
            foreach (var dir in Utils.AllDirs)
            {
                var neighbour = blocksMatrix[pos.x + dir.x][pos.y + dir.y];
                if (neighbour == null)
                {
                    freeTiles.Add(dir);
                    continue;
                }
                BindMatrix.AddBind(block, neighbour, dir, GlobalConfig.Instance.shapeBlockBindStr);
            }

            if (freeTiles.Count == 0) break;
            pos += freeTiles[Random.Range(0, freeTiles.Count)];
        }
    }
}