using Code.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoopsManager : PersistentSingleton<LoopsManager>
{
    [SerializeField] private int totalMistakes = 5;
    [SerializeField] private float tapeSpeedMultiplier = 1.25f;

    [SerializeField] private Stamp _stamp;
    [SerializeField] private Scissors _scissors;
    [SerializeField] private Pen _pen;

    private int _currentMistakesCount;
    private int _currentRightChoicesCount;
    private Tape _tape;
    private List<Frame> _frames = new();
    private EndOfTapeTrigger _endOfTapeTrigger;
    
    public static Action OnSuccessCensoringTheTape;
    public static Action OnExceedingMistakes;
    
    void Start()
    {
        _stamp.GetComponent<Collider2D>().enabled = false;
        _pen.GetComponent<Collider2D>().enabled = false;
        _scissors.GetComponent<Collider2D>().enabled = false;
        _stamp.enabled = false;
        _pen.enabled = false;
        _scissors.enabled = false;
        _tape = GameManager.Instance.GetTape();
        _endOfTapeTrigger = FindFirstObjectByType<EndOfTapeTrigger>();
        _currentMistakesCount = 0;
        GameManager.OnBeginPlay += StartTape;
        GameManager.OnBeginNextRound += PrepareForNextRound;
        _endOfTapeTrigger.OnFrameWithError += AddErrorToCount;
    }

    private void StartTape()
    {
        _stamp.GetComponent<Collider2D>().enabled = true;
        _pen.GetComponent<Collider2D>().enabled = true;
        _scissors.GetComponent<Collider2D>().enabled = true;
        _stamp.enabled = true;
        _pen.enabled = true;
        _scissors.enabled = true;      

        Debug.Log($"TAPE START. Current round: {_tape.GetCurrentRound() + 1}");
        //MenuManager.Instance.SetCardOfRound();
        if (_tape.PreviousSpeed == 0f)
        {
            _tape.SetSpeed(_tape.CurrentTapeData.InitialSpeed);
        }
        else
        {
            _tape.SetSpeed(_tape.PreviousSpeed);
        }
        AudioEvents.Instance.PlayStartRound();
        AudioEvents.Instance.PlayStartTape();
    }
    private void StopTape() 
    {
        _stamp.GetComponent<Collider2D>().enabled = false;
        _pen.GetComponent<Collider2D>().enabled = false;
        _scissors.GetComponent<Collider2D>().enabled = false;
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

    public void RegistrerFrame(List<GameObject> frames)
    {
        foreach (var frame in frames)
        {
            var frameComponent = frame.GetComponent<Frame>();

            frameComponent.OnErrorCensoring += AddErrorToCount;
            frameComponent.OnRightChoice += AddRightChoiceToCount;
            _frames.Add(frameComponent);
        }
    }

    public void DeregisterFrames(List<GameObject> frames)
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
            OnSuccessCensoringTheTape?.Invoke();
            return;
        }
        else if (_currentMistakesCount >= totalMistakes )
        {
            // GAME OVER
            StopTape();
            MenuManager.Instance.RestartCardToShow();
            CameraShakeManager.Instance.CameraShakeGameOver();
            OnExceedingMistakes?.Invoke();
        }
    }

    private void AddSpeedToTape()
    {
        var newSpeed = _tape.TapeSpeed * tapeSpeedMultiplier;
        _tape.SetSpeed(newSpeed);
        Debug.Log($"New speed: {newSpeed}");
    }

    private void OnDisable()
    {
        GameManager.OnBeginPlay -= StartTape;
        GameManager.OnBeginNextRound -= PrepareForNextRound;
        _endOfTapeTrigger.OnFrameWithError -= AddErrorToCount;
    }
}
