using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpened = false;
    private bool isBusy = false;
    private bool playerInRange = false;

    private int chestPrice = 10;

    public GameObject promptUI; // UI that says "Press E to buy Heal Pack"
    public GameObject powerUpSelecterPrefab;

    private PlayerWallet playerWallet;

    private void Update()
    {
        if (playerInRange && !isBusy && Input.GetKeyDown(KeyCode.E))
        {
            if (playerWallet != null && playerWallet.SpendMoney(chestPrice))
            {
                StartCoroutine(DispensePowerUp());
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
            if (promptUI != null) promptUI.SetActive(true);
        }
    }

      private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerWallet = null;
            if (promptUI != null) promptUI.SetActive(false);
        }
    }

    private System.Collections.IEnumerator DispensePowerUp()
    {
        isBusy = true;
        Debug.Log("Processing purchase...");

        yield return new WaitForSeconds(1f); // wait a second

        Vector3 offset = new Vector3(((Random.Range(-0.3f, 0.3f) + 1) * 1.5f), ((Random.Range(-0.3f, 0.3f) + 1) * 1.5f));

        Instantiate(powerUpSelecterPrefab, transform.position + offset, Quaternion.identity);

        isBusy = false;
    }
}
