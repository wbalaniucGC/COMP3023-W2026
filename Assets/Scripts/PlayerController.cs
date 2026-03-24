using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float moveSpeed = 5.0f;

    [Header("Jump Settings")]
    public float jumpForce = 5.0f;      // The force of the jump
    public LayerMask groundLayer;       // Checking to see if I'm standing on the ground.
    public LayerMask enemyLayer;        // Checking to see if I'm standing on top of an enemy.
    public Transform groundCheck;       // Position of my ground check
    public float groundCheckRadius;     // Size of my ground check.


    private Vector2 moveInput;
    private Rigidbody2D rBody;
    private Animator anim;
    private bool jumpTriggered = false;
    private bool isGrounded = false;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        anim.SetBool("isGrounded", isGrounded);

        // NEW: Check for stomp every frame we are falling
        if (rBody.linearVelocity.y < -0.1f)
        {
            CheckForStomp();
        }
    }

    private void CheckForStomp()
    {
        // Reuse your existing groundCheck transform to look for enemies
        Collider2D hitEnemy = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, enemyLayer);

        if (hitEnemy != null)
        {
            Enemy enemyScript = hitEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                // Reset vertical velocity for a consistent bounce
                rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
                rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                // Trigger the enemy damage (which now talks to GameManager)
                enemyScript.TakeDamage();
            }
        }
    }

    private void FixedUpdate()
    {
        float moveX = moveInput.x * moveSpeed;
        rBody.linearVelocity = new Vector2(moveX, rBody.linearVelocity.y);

        // Jump logic
        if(jumpTriggered && isGrounded)
        {
            // Reset my vertical velocity first to ensure consistent jump heights
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Animator logic
        anim.SetFloat("yVelocity", rBody.linearVelocity.y);
        anim.SetFloat("xVelocity", Mathf.Abs(rBody.linearVelocity.x));

        // Handle sprite flips
        if(moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Since CheckForStomp handles the top, this only handles side hits
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Call the static GameManager to reduce lives
            GameManager.RemoveLife();
            Debug.Log("Hit from side! Lives: " + GameManager.PlayerLives);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpTriggered = true;
        }
        else if (context.canceled)
        {
            jumpTriggered = false;
        }
    }
}
