using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System.Collections;


public class ExplodingZombie : EnemyActions
{
    public float explosionRange = 2.5f;
    public float explosionDelay = .3f;
    public float explosionDamage = 30f;

    private bool isExploding = false;
    

    public override void TryAttack()
    {
        StartCoroutine(Explode());
    }

    private void OnDrawGizmosSelected()
    {
        // Draw explosion radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    public override void FixedUpdate()
    {
        if (isExploding)
            return; // Stop moving if about to explode

        base.FixedUpdate();
    }

    private IEnumerator Explode()
    {
        isExploding = true;

        // Optional: flash red or play warning animation here
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
        }

        yield return new WaitForSeconds(explosionDelay);

        // Damage all players in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // Example: if players have a Health script
                var health = hit.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(explosionDamage);
                }
            }
        }

        // Destroy self
        Destroy(gameObject);
    }
}
