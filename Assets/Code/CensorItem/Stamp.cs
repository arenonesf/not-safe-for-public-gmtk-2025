using System;
using UnityEngine;

public class Stamp : MonoBehaviour, ICensorItem
{
    private readonly CensorItemType _type = CensorItemType.Stamp;
    public CensorItemType Type => _type;

    public void ApplyAction(Frame frame)
    {
        GameObject gameObject = frame.gameObject;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        Debug.Log("Stamp Applied");
    }
}
