using UnityEngine;
using System.Collections;

public class SpiralAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed;
    private EnemyStats enemyStats;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    public IEnumerator Spiral(float duration, float fireRate)
    {
        float angle = 0f;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            // calculate direction from angle
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 dir = new Vector2(dirX, dirY).normalized;

            // spawn projectile
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            BossProjectile bp = proj.GetComponent<BossProjectile>();
            if (bp != null)
            {
                bp.SetOwner(enemyStats);
            }

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            Debug.Log($"Angle: {angle}, Dir: {dir}, Speed: {projectileSpeed}");
            rb.linearVelocity = dir * projectileSpeed; // 

            // optional: make projectile face direction
            proj.transform.right = dir;

            // increment angle smoothly
            angle += 360f * fireRate; // one full spin per second
            yield return new WaitForSeconds(fireRate);
        }
    }
}
