using Code.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private Tape _tape;

    private bool _hasStartedWorking = false;
    public bool HasStartedWorking => _hasStartedWorking;

    public static Action OnGameStarted;
    public static Action OnStopTape;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindSceneReference;
    }

    private void FindSceneReference(Scene scene, LoadSceneMode loadSceneMode)
    {
        _tape = FindFirstObjectByType<Tape>();
    }

    public Tape GetTape() 
    {
        return _tape;
    }

    public void StartWorking() 
    {
        _hasStartedWorking = true;
        OnGameStarted?.Invoke();
    }

    public void StopTape() 
    {
        OnStopTape?.Invoke();
    }
}
