using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private int enemiesHit = 0;

    private PlayerStats OwnerStats;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    public virtual void SetOwner(PlayerStats stats)
    {
        OwnerStats = stats;
    }

    void Update()
    {
        // Move using the prefab’s rotation
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Drops") || other.CompareTag("Chests"))
            return;
        
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth zombie = other.GetComponent<EnemyHealth>();
            if (zombie != null)
            {
                enemiesHit++;
                zombie.TakeDamage(OwnerStats.attackDamage);
                AggroSystem bossAggro = FindFirstObjectByType<AggroSystem>();
                bossAggro.AddThreat(OwnerStats.gameObject, OwnerStats.attackDamage);
            }
        }
        if (OwnerStats.weaponPierce <= enemiesHit)
        Destroy(gameObject);
    }
}
