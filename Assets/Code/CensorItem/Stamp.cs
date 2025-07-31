using System;
using UnityEngine;

public class Stamp : MonoBehaviour, ICensorItem
{
    private readonly CensorItemType _type = CensorItemType.Stamp;
    public CensorItemType Type => _type;

    public void ApplyAction()
    {
        Debug.Log("Stamp Applied");
    }
}
