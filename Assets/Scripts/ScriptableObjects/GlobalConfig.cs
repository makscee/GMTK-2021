using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "ScriptableObjects/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    [SerializeField] public int thrusterBindStr, shapeBlockBindStr, shapesBindStr, shapesWeakenedBindStr, veinStaticBindStr, packedShapeBindStr, mouseBindStr, blackHoleStr;
    [SerializeField] public Color bindBlock, bindPreview, bindShape;
    [SerializeField, Range(2, 6)] public float bindStrMult;
    [SerializeField, Range(200, 600)] public float bindMaxAcc;
    [SerializeField, Range(1f, 9f)] public float bindTargetDiv;
    [SerializeField] public float blinkDelay, blinkDuration, pulseDelay;
    [SerializeField] public float thrusterChance, packedShapeMaxDistance;
    [SerializeField] public Color mouseBindColor, packedShapeBindColor, blackHoleBindColor;
    [SerializeField] public float asteroidAngleSpread, asteroidSpawnDistance, asteroidSpeed;

    [SerializeField] public float sectorSize, viewDistance, minMarkerDistance;
    [SerializeField] public int sectorVeinMin, sectorVeinMax;
    [SerializeField] public float markerDistance, blackHoleBindsDuration;
    [SerializeField] public float asteroidMaxDelay, asteroidMinDelay, camFollowSpeed;
    [SerializeField] public int asteroidMinAmount, asteroidMaxAmount;
    
    public static GlobalConfig Instance => GetInstance();

    static GlobalConfig _instanceCache;
    static GlobalConfig GetInstance()
    {
        if (_instanceCache == null)
            _instanceCache = Resources.Load<GlobalConfig>("GlobalConfig");
        return _instanceCache;
    }

    public static Action onValidate;

    void OnValidate()
    {
        onValidate?.Invoke();
    }
}