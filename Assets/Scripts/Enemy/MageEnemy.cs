using UnityEngine;
using System.Collections;

public class EnemyMage : EnemyActions
{
    public GameObject magicCirclePrefab;
    public float attackRange = 8f;
    public float attackCooldown = 3f;
    public float spellArea = 2f;
    public float castingTime = 1f;

    private float cooldownTimer;
    private bool isCasting = false;

    public override void TryAttack()
    {
        StartCoroutine(Cast());
    }

    private void OnDrawGizmosSelected()
    {
        // Draw explosion radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spellArea);
    }

    public override void FixedUpdate()
    {
        if (isCasting)
            return; // Stop moving if about to explode

        base.FixedUpdate();
    }

    private IEnumerator Cast()
    {
        isCasting = true;

        // Optional: flash red or play warning animation here
        //animation

        yield return new WaitForSeconds(castingTime);

        // Damage all players in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.position, spellArea);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // Example: if players have a Health script
                var health = hit.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(enemyStats.attackDamage);
                }
            }
        }
    }
}
