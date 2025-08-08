using Code.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    private Tape _tape;
    private MenuManager _menuManager;
    public static Action OnBeginPlay;
    private int _censorWorkerCount = 0;
    public int CensorWorkerCount => _censorWorkerCount;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += FindGameplayReferences;
    }

    private void Start()
    {
        LoopsManager.OnExceedingMistakes += GameOver;        
    }

    private void OnDisable()
    {
        LoopsManager.OnExceedingMistakes -= GameOver;
        SceneManager.sceneLoaded -= FindGameplayReferences;
    }

    private void FindGameplayReferences(Scene scene, LoadSceneMode load) 
    {
        _tape = FindFirstObjectByType<Tape>();
        _menuManager = FindFirstObjectByType<MenuManager>();
        OpenCardOfConditions(scene);
    }

    private void OpenCardOfConditions(Scene scene)
    {
        if (scene.name == "NSFPGame")
            _menuManager.PassToCardCondition();
    }

    private void GameOver()
    {
        _censorWorkerCount++;
    }

    public Tape GetTape()
    {
        return _tape;
    }

    public MenuManager GetMenuManager() 
    {
        return _menuManager;
    }

    public void BeginTapeRunning()
    {
        OnBeginPlay?.Invoke();
    }
}
