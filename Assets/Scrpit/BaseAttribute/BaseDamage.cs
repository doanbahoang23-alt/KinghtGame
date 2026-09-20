using System;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    [SerializeField] protected float currentBaseDamage = 5f;
    protected float buffDamage = 0f;

    [SerializeField] protected float currentKnockbackForce = 15f;
    [SerializeField] protected string targetTag = "Enemy";

    public string TargetTag => targetTag;
    public float KnockbackForce => currentKnockbackForce;

    public virtual void SetWeaponStats(float weaponDamage, float knockback)
    {
        currentBaseDamage = weaponDamage;
        currentKnockbackForce = knockback;
    }
    public virtual float AttackDamage()
    {
        float currentDamage = currentBaseDamage + buffDamage;
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
            knockbackable.ApplyKnockback(knockbackDir, currentKnockbackForce);
        }
    }
}