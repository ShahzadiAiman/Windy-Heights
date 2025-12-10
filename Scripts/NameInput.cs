using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    public TMP_InputField input;

    public void OnNameChanged()
    {
        GameManager.Instance.playerName = input.text;
    }
}
