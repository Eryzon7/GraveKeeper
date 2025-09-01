using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int value; // how much money this gives

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWallet wallet = other.GetComponent<PlayerWallet>();
            if (wallet != null)
            {
                wallet.AddMoney(value);
                Destroy(gameObject);
            }
        }
    }
}
