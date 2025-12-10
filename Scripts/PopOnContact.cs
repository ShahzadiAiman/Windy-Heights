using UnityEngine;


public class Popping : MonoBehaviour
{
    [Header("Sound Effect")]
    public AudioClip popSound;
    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            if (popSound != null)
                AudioSource.PlayClipAtPoint(popSound, transform.position); // Play pop sound at contact point

            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
                balloon.Pop();

            Destroy(gameObject, 0.1f);
        }
    }
}
