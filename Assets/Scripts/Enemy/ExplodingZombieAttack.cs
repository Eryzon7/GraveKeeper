using UnityEngine;

public class ExplodingZombieAttack : MonoBehaviour
{
    public GameObject explodingZombie;
    public Transform[] spawnpoint;
    public void SpawnZombie()
    {
        for (int i = 0; i < spawnpoint.Length; i++)
        {
            Instantiate(explodingZombie, spawnpoint[i].position, Quaternion.identity);
        }
    }
}
