using System;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    [SerializeField] ShapeBlock shapeBlock;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlockOverlap"))
        {
            shapeBlock.Overlapping++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlockOverlap"))
        {
            shapeBlock.Overlapping--;
        }
    }
}