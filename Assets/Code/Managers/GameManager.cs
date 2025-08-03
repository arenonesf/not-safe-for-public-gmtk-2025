using Code.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    private Tape _tape;
    public static Action OnOpenConditions;
    public static Action OnOpenNextCard;
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
        SceneManager.sceneLoaded += ActiveCard;
    }

    private void OnDisable()
    {
        LoopsManager.OnSuccessCensoringTheTape -= PrepareForNextRound;
        LoopsManager.OnExceedingMistakes -= GameOver;
        SceneManager.sceneLoaded -= ActiveCard;
    }


    private void ActiveCard(Scene scene, LoadSceneMode load)
    {
        Debug.Log("Entra o que");
        if (scene.name == "NSFPGame")
        {
            MenuManager.Instance.PassToCardCondition();
            Debug.Log("Entracño");

        }
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
    
    public void OpenCardOfConditions()
    {
        OnOpenConditions?.Invoke();
    }

    public void OpenNextCard()
    {
        OnOpenNextCard?.Invoke();
    }
}
