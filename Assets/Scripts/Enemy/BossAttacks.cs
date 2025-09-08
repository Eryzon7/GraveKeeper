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
                //basic attack random ammount
                shotgun.Shotgun();
                break;
            case 2:
                StartCoroutine(spiral.Spiral(3f, 0.1f));
                break;
            case 3:
                StartCoroutine(meteor.MeteorShower());
                break;
            case 4:
                spawnZombie.SpawnZombie();
                break;
            case 5:
                //circle nuke
                break;
        }
    }
}
