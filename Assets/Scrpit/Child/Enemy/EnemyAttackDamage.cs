using UnityEngine;

public class EnemyAttackDamage : BaseDamage
{
    [SerializeField] private float knockbackForce = 15f;
    public override float AttackDamage()
    {
        return base.AttackDamage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(AttackDamage());
            }

            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerController.TakeKnockback(knockbackDirection * knockbackForce);

                EnemyController enemyController = GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.PauseMovement(0.5f);
                }
            }
        }
    }


}