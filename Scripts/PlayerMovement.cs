using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class kitescript : MonoBehaviour
{
    public float speed = 5f;  
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private Collider2D col;

    // ⭐ ADDED: So movement freezes during pushback
    private bool isPushedBack = false;

    // ⭐ ADDED: Settings for pushback
    public float pushDistance = 1f;
    public float pushDuration = 0.25f;
    //--

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
        // ⭐ ADDED: Stop WASD while being pushed back
        if (isPushedBack) return;

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
        // ⭐ ADDED: Freeze movement during pushback
        if (isPushedBack) return;

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

    // ⭐ ADDED: Cloud collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            StartCoroutine(PushBackEffect());
        }
    }

    // ⭐ ADDED: Pushback animation
    
    private IEnumerator PushBackEffect()
    {
        isPushedBack = true;

        Vector3 startPos = transform.position;

        // --- STEP 1: small downward push ---
        float downDistance1 = 2f;  // smaller push
        float duration1 = 0.30f;
        for (float t = 0; t < duration1; t += Time.deltaTime)
        {
            transform.position = startPos + Vector3.down * downDistance1 * (t / duration1);
            yield return null;
        }

        // --- STEP 2: smooth, gentle loose curl ---
        Vector3[] curlPoints = new Vector3[]
        {
            new Vector3(-0.4f, 0.15f),
            new Vector3(-0.2f, 0.25f),
            new Vector3(0f, 0.3f),
            new Vector3(0.2f, 0.25f),
            new Vector3(0.4f, 0.15f),
            new Vector3(0.2f, 0f),
            new Vector3(0f, -0.1f),
            new Vector3(-0.2f, 0f)
        };

        Vector3 curlStart = transform.position;
        for (int i = 0; i < curlPoints.Length - 1; i++)
        {
            Vector3 p0 = curlStart + curlPoints[i];
            Vector3 p1 = curlStart + curlPoints[i + 1];
            float segmentDuration = 0.04f;

            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime / segmentDuration;
                transform.position = Vector3.Lerp(p0, p1, t);
                yield return null;
            }
        }

        // --- STEP 3: small downward push ---
        Vector3 finalStart = transform.position;
        float downDistance2 = 2f;  // smaller push
        float duration2 = 0.30f;
        for (float t = 0; t < duration2; t += Time.deltaTime)
        {
            transform.position = finalStart + Vector3.down * downDistance2 * (t / duration2);
            yield return null;
        }

        isPushedBack = false;
    }







}
