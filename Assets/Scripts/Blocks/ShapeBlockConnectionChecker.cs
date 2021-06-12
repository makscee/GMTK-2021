using System;
using UnityEngine;

public class ShapeBlockConnectionChecker : MonoBehaviour
{
    [SerializeField] ShapeConnectionCheckerBase connectionCheckerBase;
    public int dir;
    public GameObject dot;
    BindVisual _previewBind;

    void OnTriggerEnter2D(Collider2D other)
    {
        dot = other.gameObject;
        if (_previewBind != null)
            _previewBind.Destroy();
        _previewBind = BindVisual.Create(dot.transform, connectionCheckerBase.transform, GlobalConfig.Instance.bindPreview);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (_previewBind != null)
            _previewBind.Destroy();
        dot = null;
    }
}