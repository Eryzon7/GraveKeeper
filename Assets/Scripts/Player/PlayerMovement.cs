using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private PlayerStats playerStats;
    private Vector2 movement;

    private bool facingRight = true; // Track current facing direction

    [Header("Dash Settings")]
    public float dashSpeed = 15f;       // How fast you dash
    public float dashDuration = 0.2f;   // How long the dash lasts
    public float dashCooldown = 1f;     // Cooldown time between dashes

    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDashTime;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!isDashing)
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

            // Dash input
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + movement * dashSpeed * Time.fixedDeltaTime);

            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.MovePosition(rb.position + movement * playerStats.moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip X scale
        transform.localScale = scale;
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // Optional: you could add a small effect here (particles, trail, sound)
    }
}
