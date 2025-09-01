using UnityEngine;

public class EnemyDrops : MonoBehaviour
{

    private EnemyStats enemyStats;

    public GameObject goldCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject bronzeCoinPrefab;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }
    public void DropCoins()
    {
        int wave = WaveManager.Instance.currentWave;
        int totalValue = Mathf.RoundToInt(enemyStats.goldValue * Mathf.Pow(enemyStats.goldGrowth, wave - 1));

        int goldCoins = totalValue / 10;
        int remainder = totalValue % 10;

        int silverCoins = remainder / 5;
        int bronzeCoins = remainder % 5;

        // Drop gold coins
        for (int i = 0; i < goldCoins; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            Instantiate(goldCoinPrefab, transform.position + offset, Quaternion.identity);
        }

        // Drop silver coins
        for (int i = 0; i < silverCoins; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            Instantiate(silverCoinPrefab, transform.position + offset, Quaternion.identity);
        }

        // Drop bronze coins
        for (int i = 0; i < bronzeCoins; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            Instantiate(bronzeCoinPrefab, transform.position + offset, Quaternion.identity);
        }

    }
}
