using System;
using System.Linq;
using UnityEngine;

public class ShapeBlockConnectionChecker : MonoBehaviour
{
    [SerializeField] ShapeConnectionCheckerBase connectionCheckerBase;
    public int dir;
    public GameObject dot;
    public BindVisual previewBind;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("ConnectionDot")) return;
        dot = other.gameObject;
        var b = dot.GetComponentInParent<Block>();
        if (b == null || b.occupiedSides[(dir + 2) % 4])
        {
            dot = null;
            return;
        }
        if (previewBind != null)
            previewBind.Destroy();
        previewBind = BindVisual.Create(dot.GetComponentInParent<Block>(), connectionCheckerBase.block, GlobalConfig.Instance.bindPreview);
        previewBind.gameObject.SetActive(_previewEnabled);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("ConnectionDot")) return;
        if (previewBind != null)
            previewBind.Destroy();
        dot = null;
    }

    bool _previewEnabled = true;
    public void SetPreviewEnabled(bool value)
    {
        _previewEnabled = value;
        if (previewBind != null)
        {
            previewBind.gameObject.SetActive(value);
        }
    }
}