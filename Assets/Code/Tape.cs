using System;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private List<FrameData> _frameDataList = new();
    [SerializeField] private Transform _StartPosition;
    public float TapeSpeed => _speed;
    private List<GameObject> _frames = new();
    
    void Start()
    {
        InitFrames();
    }

    private void InitFrames()
    {
        for (int i = 0; i < _frameDataList.Count; i++)
        {
            var gameobject = Instantiate(_framePrefab, _StartPosition.position, Quaternion.identity);
            gameobject.GetComponent<Frame>().InitFrame(_frameDataList[i], this);
            gameobject.SetActive(false);
            _frames.Add(gameobject);   
        }
    }
}
