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
    private int _currentMistakesCount;
    private int _currentRightChoicesCount;
    private Tape _tape;
    private List<Frame> _frames = new();
    private EndOfTapeTrigger _endOfTapeTrigger;
    
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
        _currentMistakesCount = 0;
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
        //MenuManager.Instance.SetCardOfRound();
        _tape.SetSpeed(_tape.CurrentTapeData.InitialSpeed);
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
        _tape.SetPreviousSpeed(_tape.TapeSpeed);
        Debug.Log($"Current Speed {_tape.TapeSpeed}");
        StopTape();
        ResetErrorsAndRightChoices();
        _tape.TryStartNextRound();
    }

    private void ResetErrorsAndRightChoices()
    {
        _currentMistakesCount = 0;
        _currentRightChoicesCount = 0;
    }

    public void RegistrerFrames(List<GameObject> frames)
    {
        foreach (var frame in frames)
        {
            var frameComponent = frame.GetComponent<Frame>();

            frameComponent.OnErrorCensoring += AddErrorToCount;
            frameComponent.OnRightChoice += AddRightChoiceToCount;
            _frames.Add(frameComponent);
        }
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
        _currentMistakesCount++;
        CheckIfWonRound();
    }

    public void TapeRunnedCompletely()
    {
        Debug.Log($"The tape ended. Total mistakes: {_currentMistakesCount}");

        if(_currentMistakesCount < totalMistakes)
        {
            // KEEP PLAYING BUT MORE DIFFICULT
            Debug.Log($"Total errors: {_currentMistakesCount}. Player keeps playing but with increased speed.");
            AddSpeedToTape();
        }
    }

    public void CheckIfWonRound()
    {
        if (_currentRightChoicesCount == _tape.CurrentTapeData.MinGoodChoices)
        {
            // GAME WIN
            Debug.Log("Player has nailed it.");
            PrepareForNextRound();
        }
        else if (_currentMistakesCount >= totalMistakes )
        {
            // GAME OVER
            Debug.Log("GameOver!!!");
            StopTape();            
            CameraShakeManager.Instance.CameraShakeGameOver();
            OnExceedingMistakes?.Invoke();
            _gameoverCard.SetActive(true);
        }
    }

    private void AddSpeedToTape()
    {
        var newSpeed = _tape.TapeSpeed * tapeSpeedMultiplier;
        _tape.SetSpeed(newSpeed);
        Debug.Log($"New speed: {newSpeed}");
    }

    public int GetCurrentRound() => _currentRoundIndex;

    public void UpdateNextRoundIndex() 
    {
        _currentRoundIndex++;
    }

    public void Reset()
    {
        _currentRoundIndex = 0;
        _currentMistakesCount = 0;
        _currentRightChoicesCount = 0;
    }

    private void OnDisable()
    {
        GameManager.OnBeginPlay -= StartTape;
        _endOfTapeTrigger.OnFrameWithError -= AddErrorToCount;
    }
}
