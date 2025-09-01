using UnityEngine;
using TMPro; // if you’re using TextMeshPro

public class PlayerWallet : MonoBehaviour
{
    private int money = 0;
    public TMP_Text moneyText; // assign in inspector

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
            
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + money;
    }
}
