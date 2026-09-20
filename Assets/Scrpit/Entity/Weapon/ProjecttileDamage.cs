using UnityEngine;

public class ProjectileDamage : BaseDamage
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private string obstacleTag = "Environment";

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            base.HandleCollision(collision);
            Destroy(gameObject);
        }
        else if (collision.CompareTag(obstacleTag))
        {
            Destroy(gameObject);
        }
    }

    public void SetupProjectile(float damage, float knockback)
    {
        currentBaseDamage = damage;
        currentKnockbackForce = knockback;
    }
}