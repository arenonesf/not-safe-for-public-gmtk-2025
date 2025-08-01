using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private TapeData _tapeData;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private Transform _startPosition;
    private float _speed;
    public float TapeSpeed => _speed;
    private List<GameObject> _frames = new();
    private CountdownTimer _timer;
    private int _currentFrame = 0;
    public Transform StartPosition => _startPosition;

    void Start()
    {
        _timer = new CountdownTimer(_tapeData.SpawnRate);
        //_timer.OnTimerStart += () => Debug.Log("Timer Started");
        //_timer.OnTimerStop += () => Debug.Log("Timer Ended");
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
                _timer.Reset(_tapeData.SpawnRate);
                _timer.Start();
            }               
        }
    }

    private void InitFrames()
    {
        _speed = _tapeData.InitialSpeed;

        for (int i = 0; i < _tapeData.Frames.Count; i++)
        {
            Vector3 spawnPosition = _startPosition.position - _tapeData.SpawnOffset * i * Vector3.right;
            var gameobject = Instantiate(_framePrefab, spawnPosition, Quaternion.identity);
            gameobject.GetComponent<Frame>().InitFrame(_tapeData.Frames[i], this);
            gameobject.SetActive(false);
            _frames.Add(gameobject);   
        }
        Loops.Instance.RegistrerFrame(_frames);
        _timer.Start();
    }

    private void InitTape()
    {
        _speed = _tapeData.InitialSpeed;
    }

    private void SetActiveFrame(int index)
    {
        _frames[index].SetActive(true);
        _currentFrame++;
    }

    public Frame GetReturnFirstFrame()
    {
        return _frames[^1].GetComponent<Frame>();
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }
}
