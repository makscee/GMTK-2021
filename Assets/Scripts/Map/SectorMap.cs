using System;
using System.Collections.Generic;
using UnityEngine;

public static class SectorMap
{
    static Dictionary<string, Sector> _createdSectors = new Dictionary<string, Sector>();
    static float SectorSize => GlobalConfig.Instance.sectorSize;

    static int _lastCheckedX = int.MinValue, _lastCheckedY;
    public static void CheckPlayerPosition(Vector2 pos)
    {
        if (SharedObjects.instance.gameManager.gameEnded) return;
        var x = Mathf.FloorToInt(pos.x / SectorSize);
        var y = Mathf.FloorToInt(pos.y / SectorSize);
        
        if (x == _lastCheckedX && y == _lastCheckedY) return;
        _lastCheckedX = x;
        _lastCheckedY = y;

        for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
                InitSector(x + i, y + j);
        foreach (var sector in _createdSectors.Values)
                sector.SetEnabled(Mathf.Abs(sector.x - x) < 3 && Mathf.Abs(sector.y - y) < 3);
    }

    static void InitSector(int x, int y)
    {
        var key = GetKey(x, y);
        if (_createdSectors.ContainsKey(key)) return;
        _createdSectors.Add(key, new Sector(x, y));
    }

    static string GetKey(Vector2 pos)
    {
        return
            $"{Mathf.Floor(pos.x / SectorSize)}_{Mathf.Floor(pos.y / SectorSize)}";
    }
    static string GetKey(int x, int y)
    {
        return $"{x}_{y}";
    }

    public static void DisableAll()
    {
        foreach (var sector in _createdSectors.Values)
        {
            sector.SetEnabled(false);
        }
    }

    public static void Clear()
    {
        _createdSectors.Clear();
        _lastCheckedX = int.MinValue;
    }
}