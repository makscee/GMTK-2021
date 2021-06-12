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

        var pos = new Vector2Int(7, 7);
        var allBlocks = new HashSet<ShapeBlock>();
        for (var i = 0; i < blocks; i++)
        {
            var block = ShapeBlock.Create(position);
            allBlocks.Add(block);
            blocksMatrix[pos.x][pos.y] = block;
            var freeTiles = new List<Vector2Int>();
            for (var dirInt = 0; dirInt < 4; dirInt++)
            {
                var dir = Utils.CoordsFromDirInt(dirInt);
                var neighbour = blocksMatrix[pos.x + dir.x][pos.y + dir.y];
                if (neighbour == null)
                {
                    freeTiles.Add(dir);
                    continue;
                }
                BindMatrix.AddBind(block, neighbour, dir, GlobalConfig.Instance.shapeBlockBindStr);
                BindVisual.Create(block.transform, neighbour.transform, GlobalConfig.Instance.bindBlock);
                block.checkerBase.DisableDir(dirInt);
                neighbour.checkerBase.DisableDir((dirInt + 2) % 4);
            }

            if (freeTiles.Count == 0) break;
            pos += freeTiles[Random.Range(0, freeTiles.Count)];
        }

        foreach (var block in allBlocks)
        {
            block.allBlocks = allBlocks;
        }
    }
}