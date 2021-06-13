using System;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public Transform player, target;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer[] toHide;
    [SerializeField] bool isBlackHole;

    Vector3 _initialScale;
    void Start()
    {
        var targetSr = target.GetComponent<SpriteRenderer>();
        sr.color = targetSr.color;
        _initialScale = transform.localScale;
    }

    void Update()
    {
        var from = player.position;
        var to = target.position;
        var dist = (to - from).magnitude; 
        if (!isBlackHole && (dist > GlobalConfig.Instance.viewDistance || dist < GlobalConfig.Instance.minMarkerDistance))
        {
            SetHidden(true);
            return;
        }

        transform.localScale =
            Vector3.Lerp(_initialScale, _initialScale * 3, 1f - dist / GlobalConfig.Instance.viewDistance);

        if (isBlackHole && dist < GlobalConfig.Instance.minMarkerDistance)
        {
            Destroy();
            return;
        }
        SetHidden(false);
        
        transform.position = from +
                             (to - from).normalized * (GlobalConfig.Instance.markerDistance * (isBlackHole ? 1.5f : 1f));
        var dir = (to - from) / 2;
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    bool _hidden = true;
    public void SetHidden(bool value)
    {
        if (_hidden == value) return;
        foreach (var spriteRenderer in toHide)
        {
            spriteRenderer.enabled = !value;
        }

        _hidden = value;
    }

    public void SetEnabled(bool value)
    {
        gameObject.SetActive(value);
    }

    public static Marker Create(Transform player, Transform target)
    {
        var marker = Instantiate(Prefabs.Instance.marker).GetComponent<Marker>();
        marker.player = player;
        marker.target = target;
        return marker;
    }
}