using UnityEngine;

public interface ICensorItem
{
    CensorItemType Type { get; }
    void ApplyAction();
}
