using UnityEngine;

public class EnemyHealth : BaseHealth
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

}