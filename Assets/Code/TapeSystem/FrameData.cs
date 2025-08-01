using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameData", menuName = "Scriptable Objects/FrameData")]
public class FrameData : ScriptableObject
{
    public string FrameName;
    public Sprite FrameImage;
    public Sprite CensoredPenImage;
    public List<CensorItemType> FrameCensorType = new();
}
