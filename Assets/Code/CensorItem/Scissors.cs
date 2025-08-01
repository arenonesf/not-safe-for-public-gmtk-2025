using Code.Core.UnityUtils.Persistence;
using UnityEngine;

public class Scissors : MonoBehaviour, ICensorItem
{
    private readonly CensorItemType _type = CensorItemType.Scissors;
    public CensorItemType Type => _type;

    public void ApplyAction(Frame frame)
    {
        GameObject gameObject = frame.gameObject;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
        Debug.Log("Scissors Applied");
    }
}
