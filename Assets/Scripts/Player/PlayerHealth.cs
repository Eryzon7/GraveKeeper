using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthSlider; // assign in inspector
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerStats.currentHealth = playerStats.maxHealth;
        healthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.currentHealth;
    }

    public void Heal(int amount)
    {
        playerStats.currentHealth = Mathf.Min(playerStats.currentHealth + amount, playerStats.maxHealth);
        healthSlider.value = playerStats.currentHealth;
        Debug.Log("Healed! Current HP: " + playerStats.currentHealth);
    }

    public void TakeDamage(float damage)
    {
        playerStats.currentHealth -= damage;
        if (playerStats.currentHealth < 0) playerStats.currentHealth = 0;
        {
            healthSlider.value = playerStats.currentHealth;
        }

        if (playerStats.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add respawn, game over, or disable player here
        gameObject.SetActive(false);
    }
}
