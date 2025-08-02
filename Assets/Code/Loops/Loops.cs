using Code.Core;
using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Loops : PersistentSingleton<Loops>
{
    [SerializeField] private int _maxMistakes;
    [SerializeField] private float _countdownTime = 1f;
    private int _mistakes = 0;
    private int _currentMistakes = 0;
    private int _currentRightChoices = 0;
    private List<Frame> _frames = new();
    private CountdownTimer _timer;   
    private Tape _tape;
    public float _prevSpeed;

    void Start()
    {
        _tape = GameManager.instance.GetTape();
        _timer = new CountdownTimer(_countdownTime);
        _timer.Reset();
        _timer.OnTimerStart += () => Debug.Log($"El timer de {gameObject.name} ha empezado");
        _prevSpeed = _tape.TapeData.InitialSpeed;
    }

    public void SetFinishLoop()
    {
        StopLoop();
    }

    private void StopLoop()
    {
        /*if (!_notebook.NotebookIsOpen)
        {*/
            //_prevSpeed = _tape.TapeSpeed;
            //_tape.SetSpeed(0);
        //}
        _timer.Start();
        GameManager.instance.ChangeToTapeRunning(false);
    }

    private void Update()
    {
        if (_timer.IsFinished/*&& !_notebook.NotebookIsOpen*/)
        {
            CheckErrorInFrames();
            CheckChangeSpeed();
            if (!GameManager.instance.HasLosed)
                GameManager.instance.ChangeToTapeRunning(true);
            _timer.Stop();
            _timer.Reset();
        }
    }

    public void CheckChangeSpeed()
    {
        _mistakes += _currentMistakes;
        Debug.Log($"Player has made {_mistakes} mistakes in total.");
        if (_maxMistakes <= _mistakes)
            GameManager.Instance.PlayerLose();
        else
        {
            if (_currentMistakes > 0)
                _tape.SetSpeed(_prevSpeed * 1.25f);
            else
                _tape.SetSpeed(_prevSpeed);

        }

        if (_currentMistakes == 0)
            GameManager.Instance.ChangeScene();

        _currentMistakes = 0;
    }

    public void RegistrerFrame(List<GameObject> frames)
    {
        foreach (var frame in frames)
        {
            var frameComponent = frame.GetComponent<Frame>();

            frameComponent.OnErrorCensoring += OnFrameCensorError;
            frameComponent.OnRightChoice += CheckIfWinRound;
            _frames.Add(frameComponent);
        }
    }

    private void OnFrameCensorError(Frame frame)
    {
        _mistakes++;
        Debug.Log($"Player has failed {_mistakes} times.");
    }

    private void OnDisable()
    {
        foreach (var frame in _frames)
        {
            frame.OnErrorCensoring -= OnFrameCensorError;
            frame.OnRightChoice -= CheckIfWinRound;
        }
    }

    public void CheckErrorInFrames()
    {
        foreach (var frame in _frames)
        {
            if (!frame.CheckFrame())
                _currentMistakes++;
        }
    }

    public void CheckIfWinRound(Frame frame) 
    {
        _currentRightChoices++;
        Debug.Log($"Player has made {_currentRightChoices} right choices in this round.");
        if (_currentRightChoices >= _tape.TapeData.MinGoodChoices)
        {
            Debug.Log("Player has won the round!");
            //GameManager.Instance.ChangeScene();
        }
    }

    public void ResetMistakes()
    {
        _currentRightChoices = 0;
        _currentMistakes = 0;
        _mistakes = 0;
    }
}
