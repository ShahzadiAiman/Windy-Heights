using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] HighscoreHandler highscoreHandler;
    
    public static GameManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    [Header("Player Data")]
    public int score = 0;
    public string playerName;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // Title screen
        {
            ResetData();
        }

        if (scoreText == null)
        {
            GameObject found = GameObject.FindWithTag("ScoreText");
            if (found != null)
                scoreText = found.GetComponent<TextMeshProUGUI>();
        }
        UpdateScoreText();
    }

    public void AddScore(float balloonScale)    
    {
        int points = Mathf.RoundToInt(10f / balloonScale);
        score += points;
        UpdateScoreText();

        // Load next level after a short delay
        LoadNextLevelWithDelay(0.3f);
    }


    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }


    public void RestartLevel()
    {

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
        
    }
  

    public void LoadNextLevel()
    {

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("🎉 Game complete! Restarting to Title...");

            //save score
            HighscoreHandler.Instance.AddHighscoreIfPossible(
                new HighscoreElement(GameManager.Instance.playerName, GameManager.Instance.score)
            );
            
            //return to title
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene("HighScores"); 

        }
    }



    public void LoadNextLevelWithDelay(float delay)
    {
        StartCoroutine(LoadNextSceneDelayed(delay));
    }

    private System.Collections.IEnumerator LoadNextSceneDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }


    // Optional reset
    public void ResetData()
    {
        score = 0;
        playerName = "Player1";
        UpdateScoreText();
    }
    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("Player name set to: " + playerName);
    }

}
