using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public float speed = 5f;
    private int direction = 1; // 1 = right, -1 = left
    private float screenHalfWidth;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D missing from cloud!"); 

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
    
    void FixedUpdate()
    {
        // Move the cloud using physics
        Vector2 newPos = rb.position + Vector2.right * speed * direction * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        // Bounce back at edges
        if (rb.position.x > screenHalfWidth)
            direction = -1;
        else if (rb.position.x < -screenHalfWidth)
            direction = 1;
    }
    
}
