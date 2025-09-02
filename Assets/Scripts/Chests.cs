using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    public bool isOpened = false;
    private bool isBusy = false;
    private bool playerInRange = false;

    public Sprite opendChest;

    public int chestPrice;
    private int baseChestCost = 10;

    public TMP_Text promptUI; 
    public GameObject powerUpSelecterPrefab;

    private PlayerWallet playerWallet;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        chestPrice = baseChestCost;
    }

    private void Update()
    {
        if (playerInRange && !isBusy && Input.GetKeyDown(KeyCode.E))
        {
            if (playerWallet != null && playerWallet.SpendMoney(chestPrice))
            {
                if (isOpened == false)
                {
                    isOpened = true;
                    ChestManager.ChestPriceUpdate(baseChestCost);
                    StartCoroutine(DispensePowerUp());
                }
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerWallet = collision.GetComponent<PlayerWallet>();
            if (promptUI != null) promptUI.gameObject.SetActive(true);
        }
    }

      private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerWallet = null;
            if (promptUI != null) promptUI.gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator DispensePowerUp()
    {
        isBusy = true;
        sr.sprite = opendChest;
        Debug.Log("Processing purchase...");

        yield return new WaitForSeconds(1f); // wait a second

        Vector3 offset = new Vector3(((Random.Range(-0.3f, 0.3f) + 1) * 1.5f), ((Random.Range(-0.3f, 0.3f) + 1) * 1.5f));

        Instantiate(powerUpSelecterPrefab, transform.position + offset, Quaternion.identity);

        isBusy = false;
    }
}
