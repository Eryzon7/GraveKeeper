using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public MonsterSpawner[] spawners;
    public float timeBetweenWaves = 5f; // extra rest time after wave ends

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
        StartCoroutine(WaveLoop());
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
                spawner.monstersPerWave = currentWave * 3; // scale number of zombies
                spawner.StartWave();
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
        foreach (var spawner in spawners)
        {
            if (spawner.ActiveEnemies > 0)
                return false;
        }
        return true;
    }
}