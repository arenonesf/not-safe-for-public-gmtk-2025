using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scissors : CensorItemBase
{
    private readonly CensorItemType _type = CensorItemType.Scissors;
    public override CensorItemType Type => _type;
    private Transform _transform;
    private Vector3 _initialPosition;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _initialPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        _spriteRenderer.sprite = HoveredTool;
        Debug.Log($"{gameObject.name} hover");
        _transform.DOMoveY(-3f, 0.075f);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        _spriteRenderer.sprite = DefaultTool;
        transform.DOMoveY(_initialPosition.y, 0.075f);
    }

    protected override void ApplyEffect(Frame frame)
    {
        GameObject gameObject = frame.gameObject;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
        Debug.Log("Scissors Applied");
    }
}
