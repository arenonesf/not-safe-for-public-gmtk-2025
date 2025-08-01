using UnityEngine;

public class Scissors : CensorItemBase
{
    private readonly CensorItemType _type = CensorItemType.Scissors;
    public override CensorItemType Type => _type;

    protected override void ApplyEffect(Frame frame)
    {
        GameObject gameObject = frame.gameObject;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
        Debug.Log("Scissors Applied");
    }
}
