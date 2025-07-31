using UnityEngine;

[CreateAssetMenu(fileName = "FrameData", menuName = "Scriptable Objects/FrameData")]
public class FrameData : ScriptableObject
{
    public string FrameName;
    public Sprite FrameImage;
    public FrameCensorType FrameCensorType;
}
