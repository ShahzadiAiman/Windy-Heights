using UnityEngine;
using UnityEngine.UI;
using TMPro; // << IMPORT THIS

public class SelectKiteColor : MonoBehaviour
{
    public TMP_Dropdown colorDropdown;

    void Start()
    {
        int savedColor = PlayerPrefs.GetInt("PlayerColor", 0);
        colorDropdown.value = savedColor;
    }

    public void SaveColorChoice(int value)
    {
        PlayerPrefs.SetInt("PlayerColor", value);
        PlayerPrefs.Save();
    }
}
