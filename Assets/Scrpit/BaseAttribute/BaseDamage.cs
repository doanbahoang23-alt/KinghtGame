using System;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    [SerializeField] protected float baseDamage = 5f;
    protected float buffDamage = 0f;

    [SerializeField] protected float knockbackForce = 15f;
    [SerializeField] protected string targetTag = "Enemy";

    public string TargetTag => targetTag;
    public float KnockbackForce => knockbackForce;

    public virtual float AttackDamage()
    {
        float currentDamage = baseDamage + buffDamage;
        return currentDamage;
    }

    protected virtual void HandleCollision(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(AttackDamage());
        }

        if (collision.TryGetComponent(out IKnockbackable knockbackable))
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            knockbackable.ApplyKnockback(knockbackDir, knockbackForce);
        }
    }
}