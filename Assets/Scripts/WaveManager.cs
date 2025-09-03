using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public GameObject spawnerPrefab;

    public GameObject minX;
    public GameObject maxX;
    public GameObject minY;
    public GameObject maxY;

    public int numberOfSpawners = 5; // how many to spawn at start
    public List<GameObject> spawners = new List<GameObject>();
    public float timeBetweenWaves = 5f; // extra rest time after wave ends

    private int spawnersDone = 0;

    public int currentWave { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // make sure only one exists
            return;
        }

        Instance = this;
    }

    void Start()
    {
        SpawnAllSpawners();
        StartCoroutine(WaveLoop());
    }

    private void SpawnAllSpawners()
    {
        for (int i = 0; i < numberOfSpawners; i++)
        {
            float randomX = Random.Range(minX.transform.position.x, maxX.transform.position.x);
            float randomY = Random.Range(minY.transform.position.y, maxY.transform.position.y);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

            GameObject chest = Instantiate(spawnerPrefab, spawnPos, Quaternion.identity);
            spawners.Add(chest);
        }
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            currentWave++;
            Debug.Log("Wave " + currentWave + " starting!");

            // Trigger all graveyards
            foreach (var spawner in spawners)
            {
                var spawnerScript = spawner.GetComponent<MonsterSpawner>();
                if (spawnerScript != null)
                {
                    spawnerScript.monstersPerWave = currentWave * 3;
                    spawnerScript.StartWave();
                }
            }

            // Wait until all enemies from all spawners are dead
            yield return new WaitUntil(() => AllEnemiesDefeated());

            Debug.Log("Wave " + currentWave + " ended!");

            // Short break before next wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private bool AllEnemiesDefeated()
    {
        spawnersDone = 0;
        foreach (var spawner in spawners)
        {
            var spawnerScript = spawner.GetComponent<MonsterSpawner>();
            if (spawnerScript.ActiveEnemies == 0)
            {
                spawnersDone++;
            }
        }
        if (spawnersDone == spawners.Count)
        {
            return true;
        }
        return false;
    }
}