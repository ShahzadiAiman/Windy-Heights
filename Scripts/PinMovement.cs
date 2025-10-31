using UnityEngine;

public class PinMovement : MonoBehaviour
{
    public float speed = 8f; 

    private Vector3 direction = Vector3.up;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
        
        // Simple collision check
        Collider2D hit = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size * 0.9f, 0f, LayerMask.GetMask("Default"));
        if (hit != null && hit.CompareTag("Cloud"))
        {
            Destroy(gameObject); // stops pin when it hits a cloud
        }
    }
    

    
    public void SetDirection(Vector3 dir)
    {
        
        direction = dir.normalized;
    }
}
