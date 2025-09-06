using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab
    public Transform firePoint;         // Where the projectiles spawn
    public int projectileCount = 3;     // Number of projectiles in shotgun
    public float spreadAngle = 30f;     // Total spread in degrees
    public float projectileSpeed = 10f;

    private AggroSystem aggro;

    private void Start()
    {
        aggro = GetComponent<AggroSystem>();
    }

    public void Shotgun()
    {
        GameObject target = aggro.GetHighestThreatTarget();
        if (target == null) return;

        Vector2 direction = (target.transform.position - firePoint.position).normalized;

        float startAngle = -spreadAngle / 2f;
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = startAngle + i * angleIncrement;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + currentAngle);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = rotation * Vector2.right * projectileSpeed;
        }
    }
}
