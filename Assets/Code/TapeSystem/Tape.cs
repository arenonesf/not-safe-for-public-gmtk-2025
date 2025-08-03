using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private TapeData _tapeData;
    public TapeData TapeData => _tapeData;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private Transform _startPosition;
    private float _speed;
    public float TapeSpeed => _speed;
    private List<GameObject> _frames = new();
    public List<GameObject> Frames => _frames;
    private CountdownTimer _timer;
    private int _currentFrame = 0;
    public Transform StartPosition => _startPosition;

    private void Awake()
    {
        _speed = 0f;    
    }

    void Start()
    {
        _timer = new CountdownTimer(_tapeData.SpawnRate);

        //_timer.OnTimerStart += () => Debug.Log("Timer Started");
        //_timer.OnTimerStop += () => Debug.Log("Timer Ended");

        InitFrames();
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
        _speed = 0;

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

    public Vector3 GetLastFramePosition()
    {
        return _frames[^1].transform.position;
    }

    public float GetTapeDataOffset()
    {
        return _tapeData.SpawnOffset;
    }

    private void OnDestroy()
    {
        _timer.Dispose();
    }
}
