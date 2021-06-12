using System;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public Player p;
    public Block block;

    void Start()
    {
        BindMatrix.AddBind(block, new ThrusterBind(this), Vector2.zero, GlobalConfig.Instance.thrusterBindStr);
    }

    public Vector2 GetOffsetPosition()
    {
        if (p != null)
            return p.currentInputDir;
        return Vector2.zero;
    }
}