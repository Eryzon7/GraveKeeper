using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    public int healCost = 20;
    public GameObject healPackPrefab;
    public Transform dropPoint; // where the pack appears
    public GameObject promptUI; // UI that says "Press E to buy Heal Pack"

    private bool isBusy = false;
    private bool playerInRange = false;
    private PlayerWallet playerWallet;

    private void Update()
    {
        if (playerInRange && !isBusy && Input.GetKeyDown(KeyCode.E))
        {
            if (playerWallet != null && playerWallet.SpendMoney(healCost))
            {
                StartCoroutine(DispenseHealPack());
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

    private System.Collections.IEnumerator DispenseHealPack()
    {
        isBusy = true;
        Debug.Log("Processing purchase...");

        yield return new WaitForSeconds(1f); // wait a second

        Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);

        Instantiate(healPackPrefab, dropPoint.position + offset, Quaternion.identity);

        Debug.Log("Heal pack dispensed!");
        isBusy = false;
    }
}