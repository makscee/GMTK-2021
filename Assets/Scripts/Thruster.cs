using System;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public Player p;
    public Block block;
    [SerializeField] ParticleSystem particles;

    void Start()
    {
        BindMatrix.AddBind(block, new ThrusterBind(this), Vector2.zero, GlobalConfig.Instance.thrusterBindStr);
    }

    public Vector2 GetOffsetPosition()
    {
        if (p != null)
        {
            var dir = p.currentInputDir;
            if (dir == Vector2.zero)
            {
                particles.Stop();
                return dir;
            }

            particles.Play();
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            particles.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return dir;
        }
        else
        {
            particles.Stop();
        }
        return Vector2.zero;
    }
}