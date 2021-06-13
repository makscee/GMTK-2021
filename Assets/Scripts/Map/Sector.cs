using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sector
{
    static float SectorSize => GlobalConfig.Instance.sectorSize;
    
    List<Vein> _veins = new List<Vein>();
    public readonly int x, y;

    public Sector(int x, int y)
    {
        var veins = Random.Range(GlobalConfig.Instance.sectorVeinMin, GlobalConfig.Instance.sectorVeinMax);
        for (var i = 0; i < veins; i++)
        {
            var pos = new Vector2(Random.Range((x - 0.5f) * SectorSize, (x + 0.5f) * SectorSize),
                Random.Range((y - 0.5f) * SectorSize, (y + 0.5f) * SectorSize));
            var tier = 0;
            var t = Random.Range(0f, 1f);
            if (t < 0.05f) tier = 2;
            else if (t < 0.15f) tier = 1;
            _veins.Add(Vein.Create(tier, pos));
        }

        this.x = x;
        this.y = y;
    }

    bool _enabled = true;
    public void SetEnabled(bool value)
    {
        if (_enabled == value) return;

        foreach (var vein in _veins.Where(vein => vein != null))
        {
            vein.SetEnabled(value);
        }
        _enabled = value;
    }
}