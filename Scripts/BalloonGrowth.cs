using UnityEngine;

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize = 3f;

    private GameManager gameManager;
    private bool isPopped = false;

    void Start()
    {
        // Modern replacement for FindObjectOfType
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Grow the balloon every second
        InvokeRepeating(nameof(Grow), 1f, 1f);
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growRate;

        if (transform.localScale.x >= maxSize)
        {
            CancelInvoke(nameof(Grow));
            // When too big → no points, restart level
            if (gameManager != null)
                gameManager.RestartLevel();
            else
                Debug.LogWarning("GameManager not found!");
            Destroy(gameObject);
        }
    }
    
    public void Pop()
    {
        if (isPopped) return; // <- Prevent double pops
        isPopped = true;
    
        CancelInvoke(nameof(Grow));

        // 🔊 Play pop sound if available
        AudioSource audioSource = Object.FindFirstObjectByType<AudioSource>();
        if (audioSource != null)
            audioSource.Play();

        if (gameManager != null)
            gameManager.AddScore(transform.localScale.x);

        // Start the pop animation instead of instantly destroying
        StartCoroutine(PopAnimation());
    }

    // New coroutine at the bottom of your class
    private System.Collections.IEnumerator PopAnimation()
    {
        float duration = 0.3f; // how long the pop lasts
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // shrink to nothing
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Shrink
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            // Spin
            transform.Rotate(0f, 0f, 360f * Time.deltaTime / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        Destroy(gameObject); // remove the balloon after animation
    }

}
