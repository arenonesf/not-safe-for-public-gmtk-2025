using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    [SerializeField] private List<TapeData> _tapeRounds;
    [SerializeField] private GameObject _framePrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private LoopsManager _loopsManager;

    private float _speed;
    private float _previousSpeed;
    public float PreviousSpeed => _previousSpeed;

    public float TapeSpeed => _speed;
    public Transform StartPosition => _startPosition;
    public TapeData CurrentTapeData => _tapeRounds[_loopsManager.GetCurrentRound()];

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

        InitFrames();
    }

    private void InitFrames()
    {
        _speed = 0;
        _loopsManager.ClearFrames();

        for (int i = 0; i < CurrentTapeData.Frames.Count; i++)
        {
            Vector3 spawnPosition = _startPosition.position - CurrentTapeData.SpawnOffset * i * Vector3.right;
            var gameobject = Instantiate(_framePrefab, spawnPosition, Quaternion.identity);
            var frameComponent = gameobject.GetComponent<Frame>();
            frameComponent.InitFrame(CurrentTapeData.Frames[i], this);
            
            _loopsManager.RegisterFrame(frameComponent);
        }
    }

    public void TryStartNextRound()
    {
        _loopsManager.UpdateNextRoundIndex();

        if (_loopsManager.GetCurrentRound() < _tapeRounds.Count)
        {
            _loopsManager.DeregisterFrames();
            GameManager.Instance.GetMenuManager().PassToNextCard();
            foreach (var frame in _loopsManager.GetFrames())
            {
                Destroy(frame.gameObject);
            }

            _loopsManager.ClearFrames();
            InitFrames();
        }
        else
        {
            Debug.Log("All tape rounds completed.");
        }
    }

    public void SetPreviousSpeed(float speed) => _previousSpeed = speed;

    public Frame GetLastFrameInTape()
    {
        for (int i = _loopsManager.GetFrames().Count - 1; i >= 0; i--)
        {
            var go = _loopsManager.GetFrames()[i];
            if (!go) { _loopsManager.GetFrames().RemoveAt(i); continue; } // remove destroyed
            if (go.TryGetComponent<Frame>(out var frame) && frame)
                return frame;
        }
        
        return null;
    }

    public void SetSpeed(float value) => _speed = value;
    public Vector3 GetLastFramePosition() => _loopsManager.GetFrames()[^1].transform.position;
    public float GetTapeDataOffset() => CurrentTapeData.SpawnOffset;
}
