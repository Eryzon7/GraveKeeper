using UnityEngine;

public class EnemyMage : EnemyActions
{
    public GameObject magicCirclePrefab;
    public float attackRange = 8f;
    public float attackCooldown = 3f;

    private float cooldownTimer;


    public override void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        cooldownTimer -= Time.deltaTime;

        if (distance <= attackRange && cooldownTimer <= 0)
        {
            TryAttack();
            cooldownTimer = attackCooldown;
        }
    }

    public override void TryAttack()
    {
        // Spawn the magic circle instantly at the player’s current position
        Instantiate(magicCirclePrefab, player.position, Quaternion.identity);
    }
}
