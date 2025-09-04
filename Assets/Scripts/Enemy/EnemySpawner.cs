using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int monstersPerWave = 5;

    public int ActiveEnemies => activeEnemies.Count;

    private List<GameObject> activeEnemies = new List<GameObject>();

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < monstersPerWave; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1.3f, 1.3f), Random.Range(-1.3f, 1.3f), 0);
            GameObject monster = Instantiate(monsterPrefab, transform.position + offset, Quaternion.identity);
            activeEnemies.Add(monster);

            // remove it when it dies
            monster.GetComponent<EnemyHealth>().OnDeath += () => { activeEnemies.Remove(monster); };

            yield return new WaitForSeconds(Random.Range(0.1f, 1.2f)); // spawn delay
        }
    }
}