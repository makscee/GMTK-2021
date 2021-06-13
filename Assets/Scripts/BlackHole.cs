using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHole : BindableMonoBehavior
{
    [SerializeField] Player player;

    [SerializeField] bool consumedPlayer;

    void Update()
    {
        if (!consumedPlayer) CheckAsteroids();
        if (consumedPlayer ||
            !((player.transform.position - transform.position).magnitude < transform.localScale.x / 2f * 1.1f)) return;
        
        consumedPlayer = true;
        SharedObjects.instance.inputHandler.isEnabled = false;
        SharedObjects.instance.gameManager.gameEnded = true;
        player.currentInputDir = Vector2.zero;
        SectorMap.DisableAll();
        SharedObjects.instance.soundsPlayer.FadeOutBgMusic();
        var i = 0;
        var delay = GlobalConfig.Instance.blackHoleBindsDuration / player.connectedBlocks.Count;
        foreach (var block in player.connectedBlocks)
        {
            block.EnableColliders(false);
        }
        foreach (var block in player.connectedBlocks)
        {
            Animator.Invoke(() =>
            {
                var b = BindMatrix.AddBind(this, block, Vector2.zero, GlobalConfig.Instance.blackHoleStr);
                b.visual = BindVisual.Create(b.First, b.Second, GlobalConfig.Instance.blackHoleBindColor);
            }).In(i * delay);
            i++;
        }

        var scale = transform.localScale;
        Animator.Invoke(() =>
        {
            Animator.Interpolate(1f, 0f, 3f).PassValue(v => transform.localScale = scale * v)
                .Type(InterpolationType.Square).WhenDone(
                    () =>
                    {
                        BindMatrix.RemoveAllBinds(this);
                        Destroy(gameObject);
                        SharedObjects.instance.endScreen.Enable();
                        SharedObjects.instance.inputHandler.isEnabled = true;
                        foreach (var block in player.connectedBlocks)
                        {
                            block.EnableColliders(true);
                        }
                        SharedObjects.instance.soundsPlayer.PlayGameOverSound();
                    });
        }).In(i * delay);
    }

    float _untilNextAsteroid = 5f;
    void CheckAsteroids()
    {
        _untilNextAsteroid -= Time.deltaTime;
        if (_untilNextAsteroid < 0f)
        {
            var part = (player.transform.position - transform.position).magnitude / (transform.position).magnitude;
            _untilNextAsteroid = Mathf.Lerp(GlobalConfig.Instance.asteroidMinDelay,
                GlobalConfig.Instance.asteroidMaxDelay, part);
            for (var i = 0; i < Random.Range(GlobalConfig.Instance.asteroidMinAmount, GlobalConfig.Instance.asteroidMaxAmount); i++)
                Asteroid.Create();
        }
        
    }
}