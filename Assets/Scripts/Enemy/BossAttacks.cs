using UnityEngine;
using System.Collections;

public class BossAttacks : MonoBehaviour
{

    private ShotgunAttack shotgun;
    private SpiralAttack spiral;
    private MeteorAttack meteor;

    private void Start()
    {
        shotgun = GetComponent<ShotgunAttack>();
        spiral = GetComponent<SpiralAttack>();
        meteor = GetComponent<MeteorAttack>();
    }


    void attackSet(int move)
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
                //cloud chase
                break;
            case 5:
                //circle nuke
                break;
        }
    }
}
