using UnityEngine;
using System;
using NUnit.Framework;

public class EnemyHealth : MonoBehaviour
{
 
    private EnemyStats enemyStats;
    

    // Event that other scripts (like MonsterSpawner) can subscribe to
    public event Action OnDeath;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.currentHealth = enemyStats.maxHealth;
    }

    public void TakeDamage(float amount)
    {
        enemyStats.currentHealth -= amount;
        Console.WriteLine("--damage taken--");

        if (enemyStats.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Console.WriteLine("--died--");
        OnDeath?.Invoke();  // fire event to notify listeners
        Destroy(gameObject);
    }
}
