using System;
using UnityEngine;

public class BindVisual : MonoBehaviour
{
    public Transform first, second;
    [SerializeField] SpriteRenderer sr;

    const float MinWidth = 0.03f, MaxWidth = 0.2f;
    const float MaxLength = 3;
    
    public static BindVisual Create(Transform first, Transform second, Color c)
    {        
        var bindVisual = Instantiate(Prefabs.Instance.bindVisual).GetComponent<BindVisual>();
        bindVisual.first = first;
        bindVisual.second = second;
        bindVisual.sr.color = c;
    
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
        var firstPosition = first.position;
        var dir = (second.position - firstPosition) / 2;
        transform.position = firstPosition + dir;
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var length = dir.magnitude * 2 - 1f / 3;
        var width = Mathf.Lerp(MaxWidth, MinWidth, length / MaxLength);
        transform.localScale = new Vector3(width, length);
    }
}