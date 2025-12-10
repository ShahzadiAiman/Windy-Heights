// HighScoreDisplay.cs
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public GameObject highScoreEntryPrefab; // prefab with TMP texts
    public Transform contentParent; // where entries will be spawned

    void Start()
    {
        DisplayScores();
    }

    void DisplayScores()
    {
        Debug.Log("Prefab is: " + highScoreEntryPrefab);

        var highscores = HighscoreHandler.Instance.highscoreList;
        Debug.Log("Highscores count: " + highscores.Count);

        foreach (var hs in highscores)
        {
            Debug.Log("Spawning: " + hs.playerName + " - " + hs.points);

            GameObject entryGO = Instantiate(highScoreEntryPrefab, contentParent);

            TextMeshProUGUI[] texts = entryGO.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log("TMP Count in prefab: " + texts.Length);

            if (texts.Length >= 2)
            {
                texts[0].text = hs.playerName;
                texts[1].text = hs.points.ToString();
            }
        }
    }
}
