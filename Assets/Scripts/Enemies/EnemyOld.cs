using UnityEngine;

public class EnemyOld : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float patrolDistance = 3f;

    private Vector2 startPosition;
    private int direction = -1;  // -1 = right , -1 = left
    private SpriteRenderer sRend;

    private void Awake()
    {
        startPosition = transform.position;         // Stores the initial position
        sRend = GetComponent<SpriteRenderer>();     // Caches a reference to the SpriteRenderer
    }

    private void Update()
    {
        // Calculate the movement this frame
        float movement = direction * speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

        // Check if we've reached the patrol boundary
        // (Current position) - (Start Position) >= patrolDistance
        // Flip is true
        if(Mathf.Abs(transform.position.x - startPosition.x) >= patrolDistance)
        {
            Flip();
        }
    }

    private void Flip()
    {
        direction *= -1;    // Flip direction
        sRend.flipX = (direction == 1);      // Flip sprite visual

        // Potential bug that we will fix if needed. 
        // Graphic bug. Stuck in flip-loop
        // Reset position slightliy to prevent getting stuck in the flip-loop.
        float resetX = startPosition.x + (patrolDistance * (direction * -1));
        transform.position = new Vector3(resetX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // You hit the player
            Debug.Log("Player Hit! Hahahahah");
        }
    }

    private void OnDrawGizmos() // OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Determine the start point for our Gizmo
        Vector3 center = Application.isPlaying ? (Vector3)startPosition : transform.position;

        // Draw a line representing the full patrol path
        Vector3 leftPoint = center + Vector3.left * patrolDistance;
        Vector3 rightPoint = center + Vector3.right * patrolDistance;

        Gizmos.DrawLine(leftPoint, rightPoint);
    }
}
