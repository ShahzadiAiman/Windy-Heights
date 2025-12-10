using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // for back buttons
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene("HighScores"); // for back buttons
    }
    
    
}
