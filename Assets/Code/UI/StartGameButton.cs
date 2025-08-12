using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    private Button _buttonComponent;

    private void Awake()
    {
        _buttonComponent = GetComponent<Button>();
    }

    private void Start()
    {
        _buttonComponent.onClick.AddListener(StartTape);
    }

    private void OnDisable()
    {
        _buttonComponent.onClick.RemoveAllListeners();
    }

    public void StartTape()
    {
        GameManager.Instance.BeginTapeRunning();
        menuManager.SetCardOfRound();
    }
}
