using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform follow;
    void Update()
    {
        var v = follow.transform.position;
        v.z = transform.position.z;
        transform.position = v;
    }
}