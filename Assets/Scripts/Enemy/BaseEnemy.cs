using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public EnemyStats stats;
    public EnemyHealth health;
    public EnemyActions actions;
    public EnemyDrops drops;

    protected virtual void Awake()
    {
        stats = GetComponent<EnemyStats>();
        health = GetComponent<EnemyHealth>();
        actions = GetComponent<EnemyActions>();
        drops = GetComponent<EnemyDrops>();

        // Tell health who owns it (so it can call Die)
        health.OnDeath += HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        drops.DropCoins();
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
}
