using Code.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerOld : PersistentSingleton<GameManagerOld>
{
    private Tape _tape;

    private bool _changeToTapeRunning = false;

    private int _numOfPlayer = 0000;

    private FSM _fsm;

    private bool _hasLosed = false;

    public bool HasLosed => _hasLosed;

    //private Notebook _notebook;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += FindSceneReference;
    }
    private void Start()
    {
        _fsm = new();

        var stop = new State_Stop(_tape);
        var running = new State_Running(_tape);

        void At(IState from, IState to, Func<bool> condition) => _fsm.AddTransition((IState)from, (IState)to, condition);

        /*Func<bool> NotebookOpen() => () => _notebook.NotebookIsOpen;
        Func<bool> NotebookClosed() => () => !_notebook.NotebookIsOpen;*/
        Func<bool> StartWorking() => () => _changeToTapeRunning;
        Func<bool> StopWorking() => () => !_changeToTapeRunning;

        At(stop, running, StartWorking());
        At(running, stop, StopWorking());
        /*At(stop, running, NotebookClosed());
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
    }

    public Tape GetTape() 
    {
        return _tape;
    }

    public void ChangeToTapeRunning(bool wantToRun) 
    {
        _changeToTapeRunning = wantToRun;
    }

    /*
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
        _hasLosed = true;
        ++_numOfPlayer;
        RestartGame();
    }

    private void RestartGame()
    {
        //LoopsManager.Instance.ResetMistakes();
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
