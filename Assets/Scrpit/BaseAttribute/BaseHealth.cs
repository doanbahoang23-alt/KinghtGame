
using UnityEngine;
using System;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] private float healthRegen = 1f;
    [SerializeField] private float timeOutCombat = 3f;

    protected float currentHealth;
    private float lastTimeCombat;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead { get; protected set; }
    public event Action<GameObject> OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        lastTimeCombat = -timeOutCombat;
    }

    protected virtual void Update()
    {
        HandleHealthRegen();
    }

    public virtual void TakeDamage(float damageAmount)
    {
        if (IsDead) return;
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        RestartCombatTime();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestartCombatTime()
    {
        lastTimeCombat = Time.time;
    }

    protected virtual void Die()
    {
        if (IsDead) return;
        IsDead = true;
        OnDeath?.Invoke(gameObject);
    }

    public virtual void RegentHealth(float amount)
    {
        // if (currentHealth < maxHealth)
        // {
        //     currentHealth += amount;
        //     if (currentHealth > maxHealth)
        //     {
        //         currentHealth = maxHealth;
        //     }
        // }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    private void HandleHealthRegen()
    {
        if (IsDead || currentHealth >= maxHealth) return;
        if (Time.time >= lastTimeCombat + timeOutCombat)
        {
            RegentHealth(healthRegen * Time.deltaTime);
        }
    }

}