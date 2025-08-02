using Code.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private Tape _tape;

    private bool _hasStartedWorking = false;
    public bool HasStartedWorking => _hasStartedWorking;

    private int _numOfPlayer = 0000;

    private FSM _fsm;
    //private Notebook _notebook;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindSceneReference;
    }
    private void Awake()
    {
        _fsm = new();

        var stop = new State_Stop();
        var running = new State_Running();

        void At(IState from, IState to, Func<bool> condition) => _fsm.AddTransition((IState)from, (IState)to, condition);

        /*Func<bool> NotebookOpen() => () => _notebook.NotebookIsOpen;
        Func<bool> NotebookClosed() => () => !_notebook.NotebookIsOpen;

        At(stop, running, NotebookClosed());
        At(running, stop, NotebookOpen());*/

        _fsm.SetState(stop);
    }

    private void Update()
    {
        _fsm.OnUpdate();
    }

    private void FindSceneReference(Scene scene, LoadSceneMode loadSceneMode)
    {
        _tape = FindFirstObjectByType<Tape>();
        Debug.Log(_tape);
    }

    public Tape GetTape() 
    {
        return _tape;
    }

    /*public void StartWorking() 
    {
        _hasStartedWorking = true;
        OnGameStarted?.Invoke();
    }

    public void StopTape() 
    {
        OnStopTape?.Invoke();
    }*/

    public string GetName()
    {
        return _numOfPlayer.ToString();
    }

    public void PlayerLose()
    {
        ++_numOfPlayer;
        RestartGame();
    }

    private void RestartGame()
    {
        Loops.Instance.ResetMistakes();
        SceneManager.LoadScene(1);
    }

    public void ChangeScene()
    {
        if (SceneManager.sceneCount > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }
}
