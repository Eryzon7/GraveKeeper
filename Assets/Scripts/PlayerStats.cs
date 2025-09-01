using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerStats : MonoBehaviour
{
    public float attackDamage = 3;
    public float attackSpeed = 1f;
    public float attackDelay = 1f;
    public int weaponPierce = 1;
    public float moveSpeed = 5f;
    public float maxHealth = 100f;
    public float currentHealth;

    public void ApplyPowerUp(PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUp.PowerUpType.AttackDamage:
                attackDamage += powerUp.value;
                break;
            case PowerUp.PowerUpType.AttackSpeed:
                attackSpeed += powerUp.value;
                attackDelay = 1 / attackSpeed;
                break;
            case PowerUp.PowerUpType.Pierce:
                weaponPierce += Mathf.RoundToInt(powerUp.value);
                break;
            case PowerUp.PowerUpType.MoveSpeed:
                moveSpeed *= ((powerUp.value / 100) + 1);
                break;
            case PowerUp.PowerUpType.MaxHealth:
                maxHealth += powerUp.value;
                currentHealth += powerUp.value; // heal when max goes up
                break;
        }

        Debug.Log("Applied powerup: " + powerUp.powerUpName);
    }
}
