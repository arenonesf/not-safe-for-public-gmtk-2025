using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void GameScene()
    {
        SceneManager.LoadScene("NSFPGame");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
