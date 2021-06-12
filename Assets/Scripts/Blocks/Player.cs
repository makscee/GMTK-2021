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
}