using UnityEngine;

public class Pen : CensorItemBase
{
    private readonly CensorItemType _type = CensorItemType.Pen;
    public override CensorItemType Type => _type;

    protected override void ApplyEffect(Frame frame)
    {
        frame.ActivateCensoredPenImage();
        Debug.Log("Pen Applied");
    }
}
