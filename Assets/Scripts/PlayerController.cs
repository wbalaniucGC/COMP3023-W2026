using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    private Vector2 moveInput;
    private Rigidbody2D rBody;
    private Animator anim;
    private bool jumpTriggered = false;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveX = moveInput.x * moveSpeed;
        rBody.linearVelocity = new Vector2(moveX, rBody.linearVelocity.y);

        // Jump logic
        if(jumpTriggered)
        {
            // Reset my vertical velocity first to ensure consistent jump heights
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Animator logic
        anim.SetFloat("xVelocity", Mathf.Abs(rBody.linearVelocity.x));

        // Handle sprite flips
        if(moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            jumpTriggered = true;
        }
        else if(context.canceled)
        {
            jumpTriggered = false;
        }
    }
}
