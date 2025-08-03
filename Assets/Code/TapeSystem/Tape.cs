using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private List<TapeData> _tapeRounds;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private Transform _startPosition;

    private int _currentRoundIndex = 0;
    private float _speed;
    private float _previousSpeed;
    public float PreviousSpeed => _previousSpeed;
    private CountdownTimer _timer;
    private int _currentFrame = 0;
    private List<GameObject> _frames = new();

    public float TapeSpeed => _speed;
    public List<GameObject> Frames => _frames;
    public Transform StartPosition => _startPosition;
    public TapeData CurrentTapeData => _tapeRounds[_currentRoundIndex];

    private void Awake()
    {
        _speed = 0f;
    }

    private void Start()
    {
        if (_tapeRounds == null || _tapeRounds.Count == 0)
        {
            Debug.LogError("No TapeData rounds assigned.");
            return;
        }

        _timer = new CountdownTimer(CurrentTapeData.SpawnRate);
        InitFrames();
    }

    private void Update()
    {
        if (_timer.IsFinished && _currentFrame < _frames.Count)
        {
            SetActiveFrame(_currentFrame);

            if (_currentFrame != _frames.Count)
            {
                _timer.Reset(CurrentTapeData.SpawnRate);
                _timer.Start();
            }
        }
    }

    private void InitFrames()
    {
        _speed = 0;
        _frames.Clear();
        _currentFrame = 0;

        for (int i = 0; i < CurrentTapeData.Frames.Count; i++)
        {
            Vector3 spawnPosition = _startPosition.position - CurrentTapeData.SpawnOffset * i * Vector3.right;
            var gameobject = Instantiate(_framePrefab, spawnPosition, Quaternion.identity);
            gameobject.GetComponent<Frame>().InitFrame(CurrentTapeData.Frames[i], this);
            gameobject.SetActive(false);
            _frames.Add(gameobject);
        }

        LoopsManager.Instance.RegistrerFrame(_frames);
        _timer.Reset(CurrentTapeData.SpawnRate);
        _timer.Start();
    }

    private void SetActiveFrame(int index)
    {
        _frames[index].SetActive(true);
        _currentFrame++;
    }

    public void TryStartNextRound()
    {
        _currentRoundIndex++;

        if (_currentRoundIndex < _tapeRounds.Count)
        {
            LoopsManager.Instance.DeregisterFrames(_frames);
            MenuManager.Instance.PassToNextCard();
            foreach (var frame in _frames)
            {
                Destroy(frame);
            }

            _frames.Clear();
            InitFrames();
        }
        else
        {
            Debug.Log("All tape rounds completed.");
        }
    }

    public void SetPreviousSpeed(float speed) => _previousSpeed = speed;
    public Frame GetLastFrameInTape() => _frames[^1].GetComponent<Frame>();
    public void SetSpeed(float value) => _speed = value;
    public Vector3 GetLastFramePosition() => _frames[^1].transform.position;
    public float GetTapeDataOffset() => CurrentTapeData.SpawnOffset;
    public int GetCurrentRound() => _currentRoundIndex;

    private void OnDestroy() => _timer.Dispose();
}
