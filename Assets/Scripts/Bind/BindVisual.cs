using System;
using UnityEngine;

public class BindVisual : MonoBehaviour
{
    public IBindable first, second;
    [SerializeField] SpriteRenderer sr;
    public Vector2 offset = Vector2.zero;

    const float MinWidth = 0.03f, MaxWidth = 0.2f;
    const float MaxLength = 3;

    float _widthMult = 1f;
    
    public static BindVisual Create(IBindable first, IBindable second, Color c, float widthMultiplier = 1f)
    {        
        var bindVisual = Instantiate(Prefabs.Instance.bindVisual).GetComponent<BindVisual>();
        bindVisual.first = first;
        bindVisual.second = second;
        bindVisual.sr.color = c;
        bindVisual._widthMult = widthMultiplier;
    
        return bindVisual;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    // void Update()
    // {
    //     var vec = first.position - second.position;
    //     vec.z = 0;
    //     
    //     var angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90;
    //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //     
    //     if (vec.magnitude < length) return;
    //     transform.position += vec.normalized * (vec.magnitude - length);
    // }
    void Update()
    {
        var firstPosition = first.GetPosition();
        var dir = (second.GetPosition() - firstPosition - offset) / 2;
        transform.position = firstPosition + dir;
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var length = dir.magnitude * 2;
        var width = Mathf.Lerp(MaxWidth, MinWidth, length / MaxLength) * _widthMult;
        transform.localScale = new Vector3(width, length);
    }
}