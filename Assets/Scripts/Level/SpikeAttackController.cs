using UnityEngine;

public class SpikeAttackController : EnemyHitBoxBase, EnemyCanDoDamage
{
    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HealthLost()
    {
        return damage;
    }
}
