using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CensorItemBase : MonoBehaviour, ICensorItem, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Sprite HoveredSprite;
    [SerializeField] private float _hoverMoveAmount;
    [SerializeField] private float _hoverMoveDuration;
    protected SpriteRenderer _spriteRenderer;
    protected Sprite _defaultSprite;
    public abstract CensorItemType Type { get; }
    private Vector3 _initialPosition;


    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialPosition = transform.position;
        _defaultSprite = _spriteRenderer.sprite;
    }

    public virtual void TryApplyCensorToolToFrame(Frame frame)
    {
        if (frame.CanApplyCensor(Type))
        {
            ApplyEffect(frame);
            frame.AddCensorToList(Type);
            Debug.Log($"{Type} Applied");
            AudioEvents.Instance.PlayCensorItemSound(Type);
        }
        else
            AudioEvents.Instance.PlayError();
    }

    // Default behavior - override in subclasses if needed
    protected abstract void ApplyEffect(Frame frame);

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        _spriteRenderer.sprite = HoveredSprite;
        transform.DOMoveY(_initialPosition.y + _hoverMoveAmount, _hoverMoveDuration);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        _spriteRenderer.sprite = _defaultSprite;
        transform.DOMoveY(_initialPosition.y, _hoverMoveDuration);
    }
}
