using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioSource source3;

    [SerializeField] AudioSource bgMusicSource;

    [SerializeField] AudioClip[] connectSounds;
    [SerializeField] AudioClip[] explosionSounds;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip asteroidFly;

    int connectPlayCnt;

    void Update()
    {
        for (var i = 0; i < connectPlayCnt; i++)
        {
            Animator.Invoke(() =>
            {
                source.PlayOneShot(connectSounds[Random.Range(0, connectSounds.Length)]);
            }).In(i * 0.08f);
        }

        connectPlayCnt = 0;
    }

    public void PlayConnectSound()
    {
        connectPlayCnt++;
    }

    public void PlayExplosionSound()
    {
        source3.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);
        Animator.Interpolate(0f, 1f, 2f).PassValue(v =>
        {
            source2.volume = v;
            bgMusicSource.volume = v;
        });
    }

    public void FadeOutBgMusic()
    {
        Animator.Interpolate(1f, 0f, 4f).PassValue(v => bgMusicSource.volume = v);
    }

    public void PlayGameOverSound()
    {
        source1.PlayOneShot(gameOverSound);
    }

    public void PlayAsteroidFlySound()
    {
        source2.PlayOneShot(asteroidFly);
    }
}