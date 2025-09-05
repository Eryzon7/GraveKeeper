using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab
    public Transform firePoint;         // Where the projectiles spawn
    public int projectileCount = 3;     // Number of projectiles in shotgun
    public float spreadAngle = 30f;     // Total spread in degrees
    public float projectileSpeed = 10f;

    void attackSet(int move)
    {
        switch (move)
        {
            case 1:
                //basic attack 
                //ShotgunAttack(target);
                break;
            case 2:
                //aoe patern attack1
                break;
            case 3:
                //aoe patern attack 2
                break;
            case 4:
                //cloud chase
                break;
            case 5:
                //circle nuke
                break;
        }
    }

    public void ShotgunAttack(Transform target)
    {
        // Direction from boss to player
        Vector2 direction = (target.position - firePoint.position).normalized;

        // Calculate starting angle
        float startAngle = -spreadAngle / 2f;

        // Angle increment between projectiles
        float angleIncrement = spreadAngle / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = startAngle + i * angleIncrement;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + currentAngle);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotation);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = rotation * Vector2.right * projectileSpeed;
            }
        }
    }
}
