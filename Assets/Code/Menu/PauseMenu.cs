using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    //Variable Pause UI
    [SerializeField] private GameObject PauseUI;

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
        if (_keyIsPressed && SceneManager.GetActiveScene().name != "MainMenu") {
            Toggle();
            _keyIsPressed = false;
        }
    }

    //For can see the Pause UI and for can't see 
    public void Toggle() {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf) {
            Time.timeScale = 0f;
            isPauseMenuActive = true;
        }
        else {
            Time.timeScale = 1f;
            isPauseMenuActive = false;
        }
    }

    //For return to menu
    public void ToMainMenu(string scene) 
    {
        AudioEvents.Instance.StopAllAudioEvents();
        LoopsManager.Instance.Reset();
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