using System;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    private Tape _tape;
    private FrameData _frameData;
    public FrameData FrameData => _frameData;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _penCensoredSpriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private List<CensorItemType> _censorsApplied = new();
    public Action<Frame> OnErrorCensoring;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (_tape == null) return;
        _rigidbody2D.MovePosition(_rigidbody2D.position + _tape.TapeSpeed * Time.fixedDeltaTime * Vector2.right);
    }

    public void InitFrame(FrameData data, Tape tape)
    {
        _tape = tape;
        _frameData = data;
        _spriteRenderer.sprite = data.FrameImage;
        _penCensoredSpriteRenderer.sprite = data.CensoredPenImage;
        _boxCollider2D.size = _spriteRenderer.bounds.size;
        _boxCollider2D.offset = _spriteRenderer.bounds.center - transform.position;
    }

    public void ReturnToStartPosition() 
    {
        _rigidbody2D.position = _tape.StartPosition.position;
    }

    public void AddCensorToList(CensorItemType type)
    {
        _censorsApplied.Add(type);
    }

    public bool CanApplyCensor(CensorItemType type)
    {
        if (!_frameData.FrameCensorType.Contains(type))
        {
            OnErrorCensoring?.Invoke(this);
            return false;
        }

        bool canApply = !_censorsApplied.Contains(type);
        return canApply;
    }

    public void ActivateCensoredPenImage()
    {
        _penCensoredSpriteRenderer.gameObject.SetActive(true);
    }

    public bool CheckFrame()
    {
        foreach (var censor in _censorsApplied)
        {
            if (!_frameData.FrameCensorType.Contains(censor)) return false;
        }

        return true;
    }
}
