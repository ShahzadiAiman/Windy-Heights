using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(float balloonSize)
    {
        // Smaller balloon → higher score
        int points = Mathf.RoundToInt(100 / balloonSize);
        score += points;
        UpdateScoreText();

        LoadNextLevel();
    }

    public void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(nextIndex);
        else
            Debug.Log("Game complete!");
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
