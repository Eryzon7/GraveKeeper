using UnityEngine;

public class BossActions : EnemyActions
{
    private BossAttacks _attacks;

    public override void Start()
    {
        _attacks = GetComponent<BossAttacks>();
        base.Start();
    }

    public override void TryAttack()
    {
        _attacks.attackSet(1/*Random.Range(1, 4)*/);
    }  
}
