using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private List<FrameData> _frameDataList = new();
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _spawnOffset;
    [SerializeField] private float _spawnRate;
    public float TapeSpeed => _speed;
    private List<GameObject> _frames = new();
    private CountdownTimer _timer;
    private int _currentFrame = 0;
    public Transform StartPosition => _startPosition;

    void Start()
    {
        _timer = new CountdownTimer(_spawnRate);
        _timer.OnTimerStart += () => Debug.Log("Timer Started");
        _timer.OnTimerStop += () => Debug.Log("Timer Ended");
        InitFrames();
    }

    private void OnDestroy()
    {
        _timer.Dispose();
    }

    private void Update()
    {
        if (_timer.IsFinished && _currentFrame < _frames.Count)
        {
            SetActiveFrame(_currentFrame);

            if (_currentFrame != _frames.Count)
            {
                _timer.Reset(_spawnRate);
                _timer.Start();
            }               
        }
    }

    private void InitFrames()
    {
        for (int i = 0; i < _frameDataList.Count; i++)
        {
            Vector3 spawnPosition = _startPosition.position + Vector3.right * i * _spawnOffset;
            var gameobject = Instantiate(_framePrefab, spawnPosition, Quaternion.identity);
            gameobject.GetComponent<Frame>().InitFrame(_frameDataList[i], this);
            gameobject.SetActive(false);
            _frames.Add(gameobject);   
        }
        _timer.Start();
    }

    private void SetActiveFrame(int index)
    {
        _frames[index].SetActive(true);
        _currentFrame++;
    }
}
