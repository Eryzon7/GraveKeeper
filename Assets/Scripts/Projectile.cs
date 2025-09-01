using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;


    private PlayerStats OwnerStats;
    private Vector2 direction;

    

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    public void SetOwner(PlayerStats stats)
    {
        OwnerStats = stats;
    }

    void Update()
    {
        // Move using the prefab’s rotation
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Drops"))
            return;
        
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth zombie = other.GetComponent<EnemyHealth>();
            if (zombie != null)
            {
                zombie.TakeDamage(OwnerStats.attackDamage);
            }
        }
        Destroy(gameObject);
    }
}
