using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public PowerUp[] allPowerUps;
    private PlayerStats playerStats;
    public PowerUp[] offered;

    public PowerUpUI powerUpUi; 

    public PowerUp[] GetRandomChoices(int count)
    {
        List<PowerUp> pool = new List<PowerUp>(allPowerUps);
        List<PowerUp> chosen = new List<PowerUp>();

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int randIndex = Random.Range(0, pool.Count);
            chosen.Add(pool[randIndex]);
            pool.RemoveAt(randIndex); // ensures no duplicates
        }

        return chosen.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.CompareTag("Player"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            //give 3 options
            offered = GetRandomChoices(3);
            
            //update UI
            int counter = 0;
            foreach (PowerUp powerup in offered)
            {
                powerup.GetRandomValue();
                powerUpUi.Setup(powerup, counter);
                counter++;
            }
            Debug.Log("UI");
            powerUpUi.ToggleUI();
        }
    }

    public void SelectedPowerUp(int selected)
    {
        Debug.Log("power up selected");
        playerStats.ApplyPowerUp(offered[selected]);
        powerUpUi.ToggleUI();
        Destroy(gameObject);
    }
}