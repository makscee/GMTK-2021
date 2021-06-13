using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeBlock : Block
{
    public ShapeConnectionCheckerBase checkerBase;
    public GameObject connectionDot;
    public HashSet<ShapeBlock> allBlocks;
    public Thruster thruster;
    int _overlapping;
    bool _connected;
    [SerializeField] SpriteRenderer sr;
    Color _initialColor;

    public int Overlapping
    {
        get => _overlapping;
        set
        {
            _overlapping = value;
            if (_overlapping == 0 && allBlocks.All(b => b._overlapping == 0))
            {
                foreach (var block in allBlocks)
                    block.checkerBase.SetPreviewsEnabled(true);
            } else if (_overlapping == 1)
            {
                foreach (var block in allBlocks)
                    block.checkerBase.SetPreviewsEnabled(false);
            }
        }
    }

    void Awake()
    {
        _initialColor = sr.color;
        SetConnected(false);
    }

    public static ShapeBlock Create(Vector2 position, bool thruster = false)
    {
        return Instantiate(thruster ? Prefabs.Instance.shapeThrusterBlock : Prefabs.Instance.shapeBlock, position,
            Quaternion.identity).GetComponent<ShapeBlock>();
    }

    public void Separate()
    {
        if (_connected)
            SharedObjects.instance.player.pulseNextFrame = true;
        SetConnected(false);
        var str = GlobalConfig.Instance.shapesBindStr;
        foreach (var block in allBlocks)
        {
            block.SetConnected(false);
            var adjacentBinds = new List<Bind>(BindMatrix.GetAllAdjacentBinds(block));
            foreach (var bind in adjacentBinds.Where(bind => bind.Strength == str))
                bind.Break();
        }
    }

    public void SeparateSelf()
    {
        var str = GlobalConfig.Instance.shapesBindStr;
        var adjacentBinds = new List<Bind>(BindMatrix.GetAllAdjacentBinds(this));
        foreach (var bind in adjacentBinds.Where(bind => bind.Strength == str))
            bind.Break();
        SetConnected(false);
    }

    public void Blink(float delay)
    {
        Animator.Interpolate(Color.white, _initialColor, GlobalConfig.Instance.blinkDuration)
            .PassValue(c => sr.color = c)
            .Delay(delay).NullCheck(gameObject);
    }
    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // if (SharedObjects.instance.gameManager.gameEnded) return;
        var b = BindMatrix.AddBind(this, MouseBind.Get(), Vector2.zero, GlobalConfig.Instance.mouseBindStr);
        b.visual = BindVisual.Create(b.First, b.Second, GlobalConfig.Instance.mouseBindColor);
        foreach (var block in allBlocks)
        {
            block.checkerBase.gameObject.SetActive(true);
            block.connectionDot.SetActive(false);
            Separate();
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // if (SharedObjects.instance.gameManager.gameEnded) return;
        BindMatrix.GetBind(this, MouseBind.Get())?.Break();

        foreach (var block in allBlocks)
        {
            if (block.checkerBase.TryToConnect())
            {
                SharedObjects.instance.player.pulseNextFrame = true;
                SetConnected(true);
            }
        }

        foreach (var block in allBlocks)
        {
            block.checkerBase.gameObject.SetActive(false);
            block.connectionDot.SetActive(_connected);
            block.SetConnected(_connected);
        }
    }

    public void SetConnected(bool value)
    {
        _connected = value;
        sr.color = _initialColor / 1.5f;
        if (!value && thruster != null)
            thruster.p = null;
    }

    void Destroy()
    {
        foreach (var bind in BindMatrix.GetAllAdjacentBinds(this).ToArray()) bind.Break();
        checkerBase.Destroy();
        Destroy(gameObject);
    }

    public void Explode()
    {
        SharedObjects.instance.soundsPlayer.PlayExplosionSound();
        ParticleExplosion.Create(transform.position);
        Separate();
        foreach (var block in allBlocks)
        {
            block.Destroy();
        }
    }

    public void EnableColliders(bool value)
    {
        GetComponent<Collider2D>().enabled = value;
        foreach (var col in GetComponentsInChildren<Collider2D>()) col.enabled = value;
    }
}