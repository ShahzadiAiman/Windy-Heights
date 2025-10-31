using UnityEngine;
using UnityEngine.InputSystem;

public class kitescript : MonoBehaviour
{
    public float speed = 5f;  
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private Collider2D col;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        Debug.LogError("Rigidbody2D not found on " + gameObject.name);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        col = GetComponent<Collider2D>();

        if (rb == null) Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        if (col == null) Debug.LogError("Collider2D not found on " + gameObject.name);
    
    }

    void Update()
    {
        moveInput = Vector2.zero;

        //Get current keyboard
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        //this checks with key was pressed
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            moveInput.y += 1; //move up
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            moveInput.y -= 1; //move down
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            moveInput.x -= 1; //move left 
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            moveInput.x += 1; //move right

        //normalize for faster diagonal movement
        moveInput = moveInput.normalized;

        //flip the sprite when changing directions
        if (moveInput.x > 0.01f) sr.flipX = false;
        else if (moveInput.x < -0.01f) sr.flipX = true;

        // --- Collision check before moving ---
        Vector2 moveAmount = moveInput * speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(rb.position, rb.GetComponent<Collider2D>().bounds.size * 0.9f, 0f, moveAmount, moveAmount.magnitude, LayerMask.GetMask("Default"));
        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + moveAmount); // Move only if no collision
        }
        // --- End collision check ---

    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + moveInput * speed * Time.fixedDeltaTime;

        Camera cam = Camera.main;

        if (cam != null)
        {
            float vertExtent = cam.orthographicSize;
            float horzExtent = vertExtent * cam.aspect;

            //Define screen bounds
            float leftBound = -horzExtent;
            float rightBound = horzExtent;
            float bottomBound = -vertExtent;
            float topBound = vertExtent;

            // Clamp player position inside screen bounds
            newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);
            newPos.y = Mathf.Clamp(newPos.y, bottomBound, topBound);
        }
        // Move the Rigidbody2D to the new position
        rb.MovePosition(newPos);
    }

}
