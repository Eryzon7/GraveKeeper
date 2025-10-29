using UnityEngine;
using System.Collections;

public class BossAttacks : MonoBehaviour
{

    private ShotgunAttack shotgun;
    private SpiralAttack spiral;
    private MeteorAttack meteor;
    private ExplodingZombieAttack spawnZombie;

    private void Start()
    {
        shotgun = GetComponent<ShotgunAttack>();
        spiral = GetComponent<SpiralAttack>();
        meteor = GetComponent<MeteorAttack>();
        spawnZombie = GetComponent<ExplodingZombieAttack>();
    }
    

    public void attackSet(int move)
    {
        switch (move)
        {
            case 1:
                int random = Random.Range(3, 7);
                for (int i = 0; i < random; i++)
                {
                    StartCoroutine(AttackPause(1, shotgun.Shotgun())); 
                }
                break;
            case 2:
                StartCoroutine(MovementPause(3));
                StartCoroutine(spiral.Spiral(3f, 0.05f));
                break;
            case 3:
                StartCoroutine(meteor.MeteorShower());
                break;
            case 4:
                Debug.Log("attackSet");
                StartCoroutine(MovementPause(1));
                spawnZombie.SpawnZombie();
                break;
            case 5:
                //circle nuke
                break;
        }
    }

    private IEnumerator MovementPause(int pauseTime)
    {
        EnemyStats movement = GetComponent<EnemyStats>();
        float originalMoveSpeed = movement.moveSpeed;
        movement.moveSpeed = 0;

        // spawn the zombies

        // wait before moving again
        yield return new WaitForSeconds(pauseTime);

        movement.moveSpeed = originalMoveSpeed;
    }

    private IEnumerator AttackPause(int pauseTime, IEnumerator attack)
    {
        StartCoroutine(attack);
        StopCoroutine(attack);
       yield return new WaitForSeconds(pauseTime);
    }
}
