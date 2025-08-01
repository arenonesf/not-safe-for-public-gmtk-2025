using Code.Core;
using Code.Core.Timer;
using System.Collections.Generic;
using UnityEngine;

public class Loops : PersistentSingleton<Loops>
{
    [SerializeField] private int _maxMistakes;
    private int _mistakes = 0;
    private int _currentMistakes = 0;
    private List<Frame> _frames = new();
    private CountdownTimer _timer;   
    private Tape _tape;
    private float _prevSpeed;
   

    void Start()
    {
        _tape = GetComponent<Tape>();
        _timer = new CountdownTimer(1f);
        _timer.Reset();
        _timer.OnTimerStart += () => Debug.Log($"El timer de {gameObject.name} ha empezado");
        _prevSpeed = _tape.TapeSpeed;
    }

    public void SetFinishLoop()
    {
        StopLoop();
    }

    private void StopLoop()
    {
        _prevSpeed = _tape.TapeSpeed;
        _tape.SetSpeed(0);
        _timer.Start();
    }

    private void Update()
    {
        if (_timer.IsFinished)
        {
            _timer.Stop();
            _timer.Reset();
            CheckErrorInFrames();
            CheckChangeSpeed();
        }

    }

    private void CheckChangeSpeed()
    {
        _mistakes += _currentMistakes;
        Debug.Log($"Player has made {_mistakes} mistakes in total.");
        if (_maxMistakes <= _mistakes)
            Debug.Log("Perdiste");
        else
        {
            if (_currentMistakes > 0)
                _tape.SetSpeed(_prevSpeed * 1.25f);
            else
                _tape.SetSpeed(_prevSpeed);

        }
        _currentMistakes = 0;
    }

    public void RegistrerFrame(List<GameObject> frames)
    {
        foreach (var frame in frames)
        {
            var frameComponent = frame.GetComponent<Frame>();

            frameComponent.OnErrorCensoring += OnFrameCensorError;
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
        }
    }

    private void CheckErrorInFrames()
    {
        foreach (var frame in _frames)
        {
            if (!frame.CheckFrame())
                _currentMistakes++;
        }
    }
}
