using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackedShape : BindableMonoBehavior, IBindHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vein vein;
    BindVisual _bv;
    public void OnBind(Bind bind)
    {
        
    }

    public void OnUnbind(Bind bind)
    {
        if (bind.Second is Vein)
        {
            ShapeBuilder.CreateShape(Random.Range(3, 8), transform.position);
            foreach (var adjacentBind in BindMatrix.GetAllAdjacentBinds(this).ToArray()) adjacentBind.Break();
            // BindMatrix.GetBind(this, MouseBind.Get())?.Break();
            Destroy(gameObject);
            vein.CurrentShapes--;
        }
    }
    
    

    public static PackedShape Create(Vein vein)
    {
        var halfSize = vein.transform.localScale.x / 2;
        var offset = new Vector2(Random.Range(-halfSize, halfSize), Random.Range(-halfSize, halfSize));
        var ps = Instantiate(Prefabs.Instance.packedShape, vein.GetPosition() - offset, Quaternion.identity).GetComponent<PackedShape>();
        var b = BindMatrix.AddBind(ps, vein, offset, GlobalConfig.Instance.packedShapeBindStr, GlobalConfig.Instance.packedShapeMaxDistance);
        var bv = BindVisual.Create(b.First, b.Second, GlobalConfig.Instance.packedShapeBindColor);
        bv.offset = b.Offset;
        b.visual = bv;
        ps._bv = bv;
        ps.vein = vein;
        return ps;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var b = BindMatrix.AddBind(this, MouseBind.Get(), Vector2.zero, GlobalConfig.Instance.mouseBindStr);
        b.visual = BindVisual.Create(b.First, b.Second, GlobalConfig.Instance.mouseBindColor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BindMatrix.GetBind(this, MouseBind.Get())?.Break();
    }

    public void SetEnabled(bool value)
    {
        gameObject.SetActive(value);
        _bv.gameObject.SetActive(value);
    }
}