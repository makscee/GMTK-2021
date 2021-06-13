using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Transform path;

    public Vector2 dir;

    Transform _player;
    void Start()
    {
        transform.localRotation = Quaternion.FromToRotation(Vector2.up, dir);
        _player = SharedObjects.instance.player.transform;
    }

    bool _soundPlayed;
    void FixedUpdate()
    {
        transform.position += (Vector3) dir * (Time.fixedDeltaTime * GlobalConfig.Instance.asteroidSpeed);
        var playerDist = (_player.position - transform.position).magnitude;
        if (playerDist > GlobalConfig.Instance.asteroidSpawnDistance * 1.05f) Destroy(gameObject);
        if (!_soundPlayed && playerDist < 15)
        {
            SharedObjects.instance.soundsPlayer.PlayAsteroidFlySound();
            _soundPlayed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            other.GetComponent<ShapeBlock>().Explode();
            Explode();
        } else if (other.CompareTag("PlayerBlock"))
        {
            GameManager.Restart();
        }
    }

    void Explode()
    {
        ParticleExplosion.Create(transform.position);
        Destroy(gameObject);
    }

    public static void Create()
    {
        var playerPos = SharedObjects.instance.player.GetPosition();
        var a = Instantiate(Prefabs.Instance.asteroid,
                playerPos + Random.insideUnitCircle.normalized * (GlobalConfig.Instance.asteroidSpawnDistance * Random.Range(0.8f, 1.2f)),
                Quaternion.identity)
            .GetComponent<Asteroid>();
        var spread = GlobalConfig.Instance.asteroidAngleSpread;
        var aDir = (playerPos - (Vector2) a.transform.position).normalized.Rotate(Random.Range(-spread, spread));
        a.dir = aDir;
    }
}