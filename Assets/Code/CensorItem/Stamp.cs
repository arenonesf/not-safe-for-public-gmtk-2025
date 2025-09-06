using System;
using UnityEngine;

public class Stamp : CensorItemBase
{
    [SerializeField] private GameObject _stampedSprite;
    private readonly CensorItemType _type = CensorItemType.Stamp;

    public override CensorItemType Type => _type;

    protected override void ApplyEffect(Frame frame)
    {
        GameObject gameObject = frame.gameObject;
        GameObject stamp = Instantiate(_stampedSprite, gameObject.transform.position, Quaternion.identity);
        stamp.transform.SetParent(gameObject.transform);
        frame.AddCensorToList(_type);
    }
}
