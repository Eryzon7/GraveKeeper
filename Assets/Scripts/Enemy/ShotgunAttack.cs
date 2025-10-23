using UnityEngine;
using System.Collections;

public class ShotgunAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab
    public Transform firePoint;         // Where the projectiles spawn
    public int projectileCount = 3;     // Number of projectiles in shotgun
    public float spreadAngle = 30f;     // Total spread in degrees
    public float projectileSpeed = 10f;

    private AggroSystem aggro;
    private EnemyStats enemyStats;

    private void Start()
    {
        aggro = GetComponent<AggroSystem>();
        enemyStats = GetComponent<EnemyStats>();
    }

    public IEnumerator Shotgun()
    {
        GameObject target = aggro.GetHighestThreatTarget();
        if (target == null) yield break;

        // Base direction toward target
        Vector2 baseDirection = (target.transform.position - firePoint.position).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - spreadAngle / 2f;
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = startAngle + i * angleIncrement;
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);
            BossProjectile bp = proj.GetComponent<BossProjectile>();
            if (bp != null)
                bp.SetOwner(enemyStats);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 shootDir = rotation * Vector2.right; // This gives each projectile its own direction
                rb.linearVelocity = shootDir * projectileSpeed;
            }
        }
    }

}

