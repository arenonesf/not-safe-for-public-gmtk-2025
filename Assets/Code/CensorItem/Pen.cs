using UnityEngine;

public class Pen : MonoBehaviour, ICensorItem
{
    private readonly CensorItemType _type = CensorItemType.Pen;
    public CensorItemType Type => _type;

    public void ApplyAction(Frame frame)
    {
        Debug.Log("Pen Applied");
    }
}
