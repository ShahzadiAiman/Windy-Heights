using UnityEngine;

public class yellowballoonscript : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1; // 1 = right, -1 = left
    private float screenHalfWidth;
    void Start()
    {
        // Calculate half the screen width in world units
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the sprite horizontally
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        // Bounce back at edges
        if (transform.position.x > screenHalfWidth)
            direction = -1;  // go left
        else if (transform.position.x < -screenHalfWidth)
            direction = 1;   // go right
        
    }
}
