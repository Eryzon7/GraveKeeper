using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private PlayerStats playerStats;
    private Vector2 movement;

    private bool facingRight = true; // Track current facing direction

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Flip character if needed
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerStats.moveSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip X scale
        transform.localScale = scale;
    }
}