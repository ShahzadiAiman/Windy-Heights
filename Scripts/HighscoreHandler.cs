// HighscoreHandler.cs
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    public static HighscoreHandler Instance;

    public List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    [SerializeField] public int maxCount = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        highscoreList.Clear();

        for (int i = 0; i < maxCount; i++)
        {
            string name = PlayerPrefs.GetString("Player_Name" + i, "Empty");
            int points = PlayerPrefs.GetInt("Player_Points" + i, 0);

            highscoreList.Add(new HighscoreElement(name, points));
        }

        Debug.Log("HighScores loaded. Count: " + highscoreList.Count);
    }

    private void SaveHighscores()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i < highscoreList.Count)
            {
                PlayerPrefs.SetString("Player_Name" + i, highscoreList[i].playerName);
                PlayerPrefs.SetInt("Player_Points" + i, highscoreList[i].points);
            }
        }

        PlayerPrefs.Save(); // make sure to save immediately
        Debug.Log("HighScores saved.");
    }

    public void AddHighscoreIfPossible(HighscoreElement element)
    {
        highscoreList.Add(element);

        // Sort descending by score
        highscoreList.Sort((a, b) => b.points.CompareTo(a.points));

        // Trim list to max count
        if (highscoreList.Count > maxCount)
            highscoreList.RemoveAt(highscoreList.Count - 1);

        SaveHighscores();
    }
}
