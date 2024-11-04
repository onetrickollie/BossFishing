using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Access Sprite Renderer for flipping
    }

    private void Update()
    {
        // Capture movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Prioritize horizontal movement over vertical when both keys are pressed
        if (Mathf.Abs(movement.x) > 0 && Mathf.Abs(movement.y) > 0)
        {
            movement.y = 0; // Prioritize horizontal movement
        }

        // Set animator parameters
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Flip the sprite based on horizontal movement
        if (movement.x > 0)
        {
            spriteRenderer.flipX = true; // Face right
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = false; // Face left
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
