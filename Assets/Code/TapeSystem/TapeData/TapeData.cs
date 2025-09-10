using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TapeData", menuName = "Scriptable Objects/TapeData")]
public class TapeData : ScriptableObject
{
    public List<FrameData> Frames = new();
    public bool Invulnerable;
    public int MinGoodChoices;
    public float InitialSpeed;
    public float SpawnOffset;
    public float SpawnRate;
}
