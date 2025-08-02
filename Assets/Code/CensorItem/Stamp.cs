using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stamp : CensorItemBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _stampedSprite;
    private readonly CensorItemType _type = CensorItemType.Stamp;
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
        GameObject stamp = Instantiate(_stampedSprite, gameObject.transform.position, Quaternion.identity);
        stamp.transform.SetParent(gameObject.transform);
        frame.AddCensorToList(_type);
        Debug.Log("Stamp Applied");
    }
}
