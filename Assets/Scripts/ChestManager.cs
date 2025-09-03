using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject chestPrefab;

    public GameObject minX;
    public GameObject maxX;
    public GameObject minY;
    public GameObject maxY;

    private int opendChestAmount = 0;

    public int numberOfChests = 5; // how many to spawn at start
    public List<GameObject> spawnedChests = new List<GameObject>();

    public static ChestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnAllChests();
    }

    private void SpawnAllChests()
    {
        for (int i = 0; i < numberOfChests; i++)
        {
            float randomX = Random.Range(minX.transform.position.x, maxX.transform.position.x);
            float randomY = Random.Range(minY.transform.position.y, maxY.transform.position.y);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

            GameObject chest = Instantiate(chestPrefab, spawnPos, Quaternion.identity);
            spawnedChests.Add(chest);
        }
    }

    public void CheckChestOpend()
    {
        foreach (GameObject chest in spawnedChests)
        {
            if (chest != null) // chest still exists
            {
                Chest chestScript = chest.GetComponent<Chest>();
                if (chestScript != null && chestScript.isOpened)
                { 
                    opendChestAmount += 1;
                }
            }
        }
    }

    public void ChestPriceUpdate(int baseChestPrice)
    {
        CheckChestOpend();
        int newChesthPrice = Mathf.RoundToInt(baseChestPrice * Mathf.Pow(1.5f, opendChestAmount));
        opendChestAmount = 0;
        foreach (GameObject chest in spawnedChests)
        {
            if (chest != null) // chest still exists
            {
                Chest chestScript = chest.GetComponent<Chest>();
                if (chestScript != null && !chestScript.isOpened)
                {
                    chestScript.chestPrice = newChesthPrice;
                    chestScript.promptUI.text = "$" + newChesthPrice;
                }
            }
        }
    }
}