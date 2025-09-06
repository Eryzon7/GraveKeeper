using UnityEngine;
using System.Collections;

public class SpiralAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float projectileSpeed;


    public IEnumerator Spiral(float duration, float fireRate)
    {
        float angle = 0f;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 dir = new Vector2(dirX, dirY);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = dir * projectileSpeed;

            angle += 15f; // smaller = smoother spiral
            yield return new WaitForSeconds(fireRate);
        }
    }
}
