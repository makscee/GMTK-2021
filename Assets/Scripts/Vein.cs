using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vein : BindableMonoBehavior
{
    [SerializeField] int minShapes, maxShapes;
    List<PackedShape> _packedShapes = new List<PackedShape>();
    int _currentShapes;
    Marker _marker;

    public int CurrentShapes
    {
        get => _currentShapes;
        set
        {
            _currentShapes = value;
            if (value == 0)
            {
                Destroy();
            }
        }
    }

    void Start()
    {
        BindMatrix.AddBind(this, StaticAnchor.Create(GetPosition()), Vector2.zero, GlobalConfig.Instance.veinStaticBindStr);
        CreatePackedShapes();
    }

    void CreatePackedShapes()
    {
        for (var i = 0; i < Random.Range(minShapes, maxShapes); i++)
            _packedShapes.Add(PackedShape.Create(this));
        CurrentShapes = _packedShapes.Count;
    }

    public static Vein Create(int tier, Vector2 pos)
    {
        GameObject prefab = null;
        switch (tier)
        {
            case 0: 
                prefab = Prefabs.Instance.vein5;
                break;
            case 1: 
                prefab = Prefabs.Instance.vein10;
                break;
            case 2: 
                prefab = Prefabs.Instance.vein20;
                break;
        }

        var v = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Vein>();
        v._marker = Marker.Create(SharedObjects.instance.player.transform, v.transform);
        return v;
    }

    public void SetEnabled(bool value)
    {
        gameObject.SetActive(value);
        foreach (var packedShape in _packedShapes)
        {
            if (packedShape == null) continue;
            packedShape.SetEnabled(value);
        }
        _marker.SetEnabled(value);
    }

    void Destroy()
    {
        Destroy(gameObject);
        _marker.Destroy();
    }
}