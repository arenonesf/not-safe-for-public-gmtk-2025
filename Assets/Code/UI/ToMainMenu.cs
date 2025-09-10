using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
   
    public void GoToMainMenu()
    {
        AudioEvents.Instance.StopAllAudioEvents();
        SceneManager.LoadScene("MainMenu");
    }
}
