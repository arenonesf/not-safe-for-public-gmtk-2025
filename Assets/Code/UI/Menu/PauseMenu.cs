using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    //Variable Pause UI
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private LoopsManager _loopsManager;
    private CensorItemSelector _censorItemSelector;

    //Variable for to confirm if is active
    public static bool isPauseMenuActive;
    private bool _keyIsPressed = false;

    private void Start() {
        isPauseMenuActive = false;
        ClickController.OnEscapeButtonPress += ActivePauseMenu;
    }

    private void OnDisable()
    {
        ClickController.OnEscapeButtonPress -= ActivePauseMenu;
    }

    private void Update() {
        if (_keyIsPressed && SceneManager.GetActiveScene().name != "MainMenu" && !_loopsManager.GetGameIsOver()) {
            Toggle();
            _keyIsPressed = false;
        }
    }

    //For can see the Pause UI and for can't see 
    public void Toggle() {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf) {
            
            _loopsManager.StopTape(false);
            Time.timeScale = 0f;

            if (_censorItemSelector == null)
                _censorItemSelector = FindAnyObjectByType<CensorItemSelector>();

            _censorItemSelector.ResetCursorToDefault();

            isPauseMenuActive = true;
        }
        else {
            Time.timeScale = 1f;
            _loopsManager.ContinueTape();
            isPauseMenuActive = false;
        }
    }

    //For return to menu
    public void ToMainMenu(string scene) 
    {
        AudioEvents.Instance.StopAllAudioEvents();
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPauseMenuActive = false;
        SceneManager.LoadScene(scene);
    }

    private void ActivePauseMenu()
    {
        _keyIsPressed = true;
    }
}