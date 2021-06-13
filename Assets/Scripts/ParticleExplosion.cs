using UnityEngine;

public class ParticleExplosion
{
    public static void Create(Vector2 pos)
    {
        var go = Object.Instantiate(Prefabs.Instance.explosion, pos, Quaternion.identity);
        Object.Destroy(go, 5);
    }
}