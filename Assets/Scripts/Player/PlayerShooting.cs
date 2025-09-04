using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject shovelPrefab;   // Assign in inspector
    public Transform firePoint;       // Assign in inspector

    private PlayerStats playerStats;
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + playerStats.attackDelay)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
    }

    void Shoot()
    {
        // Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mousePos - firePoint.position);

        // Rotate projectile
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90f);

        // Spawn projectile
        GameObject shovel = Instantiate(shovelPrefab, firePoint.position, rotation);
        Projectile proj = shovel.GetComponent<Projectile>();

        // Apply direction
        proj.SetDirection(shootDir);

        // Apply player stats
        proj.SetOwner(playerStats);
    }
}