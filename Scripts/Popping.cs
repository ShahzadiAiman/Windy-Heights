using UnityEngine;

public class Popping : MonoBehaviour
{
    public AudioClip popSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            if (audioSource != null && popSound != null)
            {
                
                AudioSource.PlayClipAtPoint(popSound, Camera.main.transform.position);

            }

            // Let the balloon handle its own pop logic
            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
                balloon.Pop();

            // Destroy the pin
            Destroy(gameObject, 0.05f);
        }
    }
}
