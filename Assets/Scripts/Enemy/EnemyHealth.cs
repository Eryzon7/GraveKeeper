using UnityEngine;
using System;
using NUnit.Framework;

public class EnemyHealth : MonoBehaviour
{
 
    private EnemyStats enemyStats;
    private EnemyDrops drops;

    // Event that other scripts (like MonsterSpawner) can subscribe to
    public event Action OnDeath;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        drops = GetComponent<EnemyDrops>();
        enemyStats.currentHealth = enemyStats.maxHealth;
    }

    public void TakeDamage(float amount)
    {
        enemyStats.currentHealth -= amount;

        if (enemyStats.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        drops.DropCoins();
        OnDeath?.Invoke();  // fire event to notify listeners
        Destroy(gameObject);
    }
}
