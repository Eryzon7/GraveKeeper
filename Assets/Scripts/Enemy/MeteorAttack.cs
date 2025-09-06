using UnityEngine;
using System.Collections;

public class MeteorAttack : MonoBehaviour
{
    public GameObject telegraphPrefab;
    public GameObject meteorPrefab;

    public float spawnRadius;
    public float meteorDelay;
    public int meteorCount;

    public IEnumerator MeteorShower()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            // Pick random position around boss
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 targetPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            // Spawn telegraph circle
            GameObject telegraph = Instantiate(telegraphPrefab, targetPos, Quaternion.identity);

            // Wait before meteor falls
            yield return new WaitForSeconds(meteorDelay);

            // Spawn meteor at the telegraph’s spot
            GameObject meteor = Instantiate(meteorPrefab, targetPos + Vector3.up * 10f, Quaternion.identity);
            Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Shoot meteor straight down
                rb.linearVelocity = Vector2.down * 15f;
            }

            // Destroy telegraph after impact
            Destroy(telegraph);

            // Little delay before spawning next meteor (optional)
            yield return new WaitForSeconds(0.2f);
        }
    }
}
