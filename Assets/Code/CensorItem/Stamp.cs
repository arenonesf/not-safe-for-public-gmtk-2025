using System;
using UnityEngine;

public class Stamp : MonoBehaviour, ICensorItem
{
    [SerializeField] private GameObject _stampedSprite;
    private readonly CensorItemType _type = CensorItemType.Stamp;
    public CensorItemType Type => _type;

    public void ApplyAction(Frame frame)
    {
        if (frame.CanApplyCensor(_type))
        {
            GameObject gameObject = frame.gameObject;
            GameObject stamp = Instantiate(_stampedSprite, gameObject.transform.position, Quaternion.identity);
            stamp.transform.SetParent(gameObject.transform);
            frame.AddCensorToList(_type);
            Debug.Log("Stamp Applied");
        }
        
    }
}
