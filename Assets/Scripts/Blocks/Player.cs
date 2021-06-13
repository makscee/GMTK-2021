using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Block
{
    public Vector2 currentInputDir;
    public HashSet<ShapeBlock> connectedBlocks = new HashSet<ShapeBlock>();
    
    public void InputDir(Vector2 dir)
    {
        currentInputDir = dir;
    }

    public bool pulseNextFrame;
    float _sinceLastPulse;
    void Update()
    {
        if (pulseNextFrame)
        {
            _sinceLastPulse = 0f;
            pulseNextFrame = false;

            var used = new Dictionary<ShapeBlock, int>();
            var q = new Queue<ShapeBlock>();
            var depth = -1;
            FillQueueWithAdjacent(q, used, this, depth);

            while (q.Count > 0)
            {
                var b = q.Dequeue();
                depth = used[b];
                FillQueueWithAdjacent(q, used, b, depth);
            }

            foreach (var block in connectedBlocks.Where(block => !used.ContainsKey(block)))
                if (block != null) block.SeparateSelf();
            connectedBlocks.Clear();
            foreach (var pair in used)
            {
                pair.Key.Blink(GlobalConfig.Instance.blinkDelay * pair.Value);
                connectedBlocks.Add(pair.Key);
                if (pair.Key.thruster != null)
                    pair.Key.thruster.p = this;
            }
        }
        if (_sinceLastPulse > GlobalConfig.Instance.pulseDelay)
        {
            pulseNextFrame = true;
        }
        SectorMap.CheckPlayerPosition(GetPosition());
    }

    static void FillQueueWithAdjacent(Queue<ShapeBlock> q, Dictionary<ShapeBlock, int> used, Block block, int depth)
    {
        foreach (var bind in BindMatrix.GetAllAdjacentBinds(block))
        {
            ShapeBlock neighbour;
            if (bind.First != block)
                neighbour = bind.First as ShapeBlock;
            else neighbour = bind.Second as ShapeBlock;
            if (neighbour == null) continue;
            if (used.ContainsKey(neighbour))
                continue;
            q.Enqueue(neighbour);
            used.Add(neighbour, depth + 1);
        }
    }
}