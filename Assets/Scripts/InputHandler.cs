using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputHandler : MonoBehaviour
{
    public bool isEnabled = true;
    void Update()
    {
        if (!isEnabled) return;
        SharedObjects.instance.player.InputDir(GetDirection());
        if (Input.GetMouseButtonDown(1))
        {
            ShapeBuilder.CreateShape(Random.Range(3, 8), SharedObjects.instance.cam.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    static Vector2 GetDirection()
    {
        var dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir += Vector2.down;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
        }

        return dir.normalized;
    }
}