using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CensorItemBase : MonoBehaviour, ICensorItem, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite HoveredTool;
    public Sprite DefaultTool;
    public abstract CensorItemType Type { get; }

    public virtual void TryApplyCensorToolToFrame(Frame frame)
    {
        if (frame.CanApplyCensor(Type))
        {
            ApplyEffect(frame);
            frame.AddCensorToList(Type);
            Debug.Log($"{Type} Applied");
        }
    }

    // Default: do nothing.
    protected abstract void ApplyEffect(Frame frame);

    // Default pointer hover behavior – override in subclasses if needed
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} hovered (default behavior)");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} hover exit (default behavior)");
    }
}
