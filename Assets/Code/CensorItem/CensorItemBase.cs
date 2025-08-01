using UnityEngine;

public abstract class CensorItemBase : MonoBehaviour, ICensorItem
{
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
}
