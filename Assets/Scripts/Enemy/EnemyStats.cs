using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float attackDamage = 3;
    public float attackSpeed = 1f;
    public float attackDelay = 1f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;
    public float maxHealth = 3f;
    public float currentHealth;

    public int goldValue = 3;
    public int goldGrowth = 2;


    public void levelUp()
    {
      // levels up stats with amount of waves done
    }
}
