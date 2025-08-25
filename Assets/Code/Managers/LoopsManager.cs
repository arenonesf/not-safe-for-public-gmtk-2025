using Code.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoopsManager : MonoBehaviour
{
    [SerializeField] private int totalMistakes = 5;
    [SerializeField] private float tapeSpeedMultiplier = 1.25f;

    [SerializeField] private Stamp _stamp;
    [SerializeField] private Scissors _scissors;
    [SerializeField] private Pen _pen;
    [SerializeField] private GameObject _gameoverCard;
    private Collider2D _stampCollider;
    private Collider2D _penCollider;
    private Collider2D _scissorsCollider;

    private int _currentRoundIndex = 0;
    private int _totalMistakesCount;
    private int _roundMistakesCount;
    private int _currentRightChoicesCount;
    private Tape _tape;
    private List<Frame> _frames = new();
    private EndOfTapeTrigger _endOfTapeTrigger;
    private CensorItemSelector _censorItemSelector;
    
    public static Action OnExceedingMistakes;

    private void Awake()
    {
        _stampCollider = _stamp.GetComponent<Collider2D>();
        _stampCollider.enabled = false;
        _penCollider = _pen.GetComponent<Collider2D>();
        _penCollider.enabled = false;
        _scissorsCollider = _scissors.GetComponent<Collider2D>();
        _scissorsCollider.enabled = false;
        _endOfTapeTrigger = FindAnyObjectByType<EndOfTapeTrigger>();
        _censorItemSelector = FindAnyObjectByType<CensorItemSelector>();
    }

    private void OnEnable()
    {
        GameManager.OnBeginPlay += StartTape;
        _endOfTapeTrigger.OnFrameWithError += AddErrorToCount;
    }

    void Start()
    {
        _stamp.enabled = false;
        _pen.enabled = false;
        _scissors.enabled = false;
        _tape = GameManager.Instance.GetTape();
        _endOfTapeTrigger = FindFirstObjectByType<EndOfTapeTrigger>();
        _totalMistakesCount = 0;
    }

    public List<Frame> GetFrames() 
    {
        return _frames;
    }

    public void ClearFrames() 
    {
        _frames.Clear();
    }

    private void StartTape()
    {
        _stamp.enabled = true;
        _pen.enabled = true;
        _scissors.enabled = true;
        _stampCollider.enabled = true;
        _penCollider.enabled = true;
        _scissorsCollider.enabled = true;

        Debug.Log($"TAPE START. Current round: {_currentRoundIndex + 1}");
        if (_tape.PreviousSpeed != 0)
            _tape.SetSpeed( _tape.PreviousSpeed);
        else
            _tape.SetSpeed(_tape.CurrentTapeData.InitialSpeed);
        Debug.Log($"Start Speed {_tape.TapeSpeed}");
        AudioEvents.Instance.PlayStartRound();
        AudioEvents.Instance.PlayStartTape();
    }
    private void StopTape() 
    {
        _stampCollider.enabled = false;
        _penCollider.enabled = false;
        _scissorsCollider.enabled = false;
        _stamp.enabled = false;
        _pen.enabled = false;
        _scissors.enabled = false;
        
        AudioEvents.Instance.PlayStopTape();
        _tape.SetSpeed(0);
    }

    private void PrepareForNextRound()
    {
        Debug.Log($"Exit Speed {_tape.TapeSpeed}");
        StopTape();
        ResetErrorsAndRightChoices();
        _tape.TryStartNextRound();
    }

    private void ResetErrorsAndRightChoices()
    {
        _roundMistakesCount = 0;
        _currentRightChoicesCount = 0;
    }

    public void RegisterFrame(Frame frame)
    {
        frame.OnErrorCensoring += AddErrorToCount;
        frame.OnRightChoice += AddRightChoiceToCount;
        _frames.Add(frame);
    }

    public void DeregisterFrames()
    {
        foreach(var frame in _frames)
        {
            frame.OnErrorCensoring -= AddErrorToCount;
            frame.OnRightChoice -= AddRightChoiceToCount;
        }
    }

    private void AddRightChoiceToCount(Frame frame)
    {
        Debug.Log("ADDING RIGHT CHOICE");
        _currentRightChoicesCount++;
        CheckIfWonRound();
    }
        
    private void AddErrorToCount(Frame frame)
    {
        CameraShakeManager.Instance.CameraShakeOnError();
        _totalMistakesCount++;
        _roundMistakesCount++;
        CheckIfWonRound();
        if (_totalMistakesCount < totalMistakes)
        {
            // KEEP PLAYING BUT MORE DIFFICULT
            AddSpeedToTape();
        }
    }

    public void CheckIfWonRound()
    {
        Debug.Log($"Round Mistakes: {_roundMistakesCount}");
        if (_currentRightChoicesCount == _tape.CurrentTapeData.MinGoodChoices)
        {
            // GAME WIN
            if (_roundMistakesCount == 0 && _tape.TapeSpeed > _tape.CurrentTapeData.InitialSpeed) 
            {
                _tape.SetPreviousSpeed(_tape.TapeSpeed / tapeSpeedMultiplier);
            }
            else
                _tape.SetPreviousSpeed(_tape.TapeSpeed);
            PrepareForNextRound();
        }
        else if (_totalMistakesCount >= totalMistakes )
        {
            // GAME OVER
            StopTape();            
            CameraShakeManager.Instance.CameraShakeGameOver();
            OnExceedingMistakes?.Invoke();
            _gameoverCard.SetActive(true);
            _censorItemSelector.ResetCursorToDefault();
        }
    }

    private void AddSpeedToTape()
    {
        var newSpeed = _tape.TapeSpeed * tapeSpeedMultiplier;
        _tape.SetSpeed(newSpeed);
    }

    public int GetCurrentRound() => _currentRoundIndex;

    public void UpdateNextRoundIndex() 
    {
        _currentRoundIndex++;
    }

    public void Reset()
    {
        _currentRoundIndex = 0;
        _roundMistakesCount = 0;
        _totalMistakesCount = 0;
        _currentRightChoicesCount = 0;
    }

    private void OnDisable()
    {
        GameManager.OnBeginPlay -= StartTape;
        _endOfTapeTrigger.OnFrameWithError -= AddErrorToCount;
    }

    public bool GetGameIsOver()
    {
        return _gameoverCard.activeSelf;
    }
}
