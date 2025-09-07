using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    private float lastAttackTime = -Mathf.Infinity;


    private EnemyStats enemyStats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;

        enemyStats.currentHealth = enemyStats.maxHealth;
    }

    public virtual void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);


        if (distance > enemyStats.attackRange)
        {
            rb.MovePosition(rb.position + direction * enemyStats.moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            TryAttack();
        }

        // Flip left/right
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        else
            transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
    }

    public virtual void TryAttack()
    {
        if (Time.time >= lastAttackTime + enemyStats.attackDelay)
        {
            lastAttackTime = Time.time;
            player.GetComponent<PlayerHealth>()?.TakeDamage(enemyStats.attackDamage);

            Debug.Log("Zombie attacked player!");
            // here you can trigger attack animation too
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // amount of damage
            }
        }
    }
}
