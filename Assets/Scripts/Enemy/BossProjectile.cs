using UnityEngine;

public class BossProjectile : Projectile
{
    private EnemyStats enemyStats;

    public void SetOwner(EnemyStats stats)
    {
        enemyStats = stats;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Drops") || other.CompareTag("Chests"))
            return;

       

        if (other.CompareTag("Player"))
        {
            Debug.Log($"Projectile hit: {other.name}");
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                Debug.Log("Projectile damaging player!");
                player.TakeDamage(enemyStats.attackDamage);
            }
        }
    }
}
