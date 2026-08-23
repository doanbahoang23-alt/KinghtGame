using UnityEngine;

public class BaseHealth : MonoBehaviour, IDameable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] private float healthRegen = 1f;
    [SerializeField] private float timeOutCombat = 3f;

    protected float currentHealth;
    private float lastTimeCombat;

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
        Destroy(gameObject);
    }

    public virtual void RegentHealth(float amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    private void HandleHealthRegen()
    {
        if (currentHealth < maxHealth && Time.time >= lastTimeCombat + timeOutCombat)
        {
            RegentHealth(healthRegen * Time.deltaTime);
        }
    }
}