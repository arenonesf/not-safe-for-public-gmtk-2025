using Code.Core;
using System;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private Tape _tape;
    public static Action OnBeginPlay;
    public static Action OnBeginNextRound;
    private int _censorWorkerCount = 0;
    public int CensorWorkerCount => _censorWorkerCount;

    protected override void Awake()
    {
        base.Awake();
        _tape = FindFirstObjectByType<Tape>();
    }

    private void Start()
    {
        LoopsManager.OnSuccessCensoringTheTape += PrepareForNextRound;
        LoopsManager.OnExceedingMistakes += GameOver;
    }

    private void OnDisable()
    {
        LoopsManager.OnSuccessCensoringTheTape -= PrepareForNextRound;
        LoopsManager.OnExceedingMistakes -= GameOver;
    }

    private void GameOver()
    {
        _censorWorkerCount++;
    }

    private void PrepareForNextRound()
    {
        Debug.Log("NEXT ROUND BABY!");
        OnBeginNextRound?.Invoke();
    }

    public Tape GetTape()
    {
        return _tape;
    }

    public void BeginTapeRunning()
    {
        OnBeginPlay?.Invoke();
    }
}
