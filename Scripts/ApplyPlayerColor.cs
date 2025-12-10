using UnityEngine;

public class ApplyPlayerColor : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color originalColor;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color; // whatever color your kite originally is
    }

    void Start()
    {
        int colorChoice = PlayerPrefs.GetInt("PlayerColor", 0);
        Debug.Log("ApplyPlayerColor: colorChoice = " + colorChoice);

        if (colorChoice == 0)
        {
            sprite.color = originalColor;
        }
        else if (colorChoice == 1)
        {
            sprite.color = new Color(1f, 0.4f, 0.4f); //red

        }
        else if (colorChoice == 2)
        {
            sprite.color = new Color(1f, 0.4f, 0.7f); // purple
        }
        else if (colorChoice == 3)
        {
            sprite.color = new Color(0.7f, 1f, 0.8f); //green
        }
        
    }
}
