using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
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
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            activeEnemies.Add(monster);

            // remove it when it dies
            monster.GetComponent<EnemyHealth>().OnDeath += () => { activeEnemies.Remove(monster); };

            yield return new WaitForSeconds(0.5f); // spawn delay
        }
    }
}