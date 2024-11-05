using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Only load position if it was saved and it's not the initial load
        if (GameManager.Instance != null && GameManager.Instance.playerPosition != Vector3.zero)
        {
            transform.position = GameManager.Instance.playerPosition;
        }
    }

    private void Update()
    {
        // Capture movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters for animations
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Handle flipping based on horizontal movement
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
        // Move the player using Rigidbody for consistent movement speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
