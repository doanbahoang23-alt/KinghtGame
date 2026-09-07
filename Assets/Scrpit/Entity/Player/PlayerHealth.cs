using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    protected override void Start()
    {
        base.Start();
        UpdateHealthUI();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damageAmount)
    {
        float oldHealth = currentHealth;
        base.TakeDamage(damageAmount);
        if (currentHealth < oldHealth)
        {
            UpdateHealthUI();
        }
    }

    public override void RegentHealth(float amount)
    {
        float oldHealth = currentHealth;
        base.RegentHealth(amount);
        if (currentHealth > oldHealth)
        {
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{Mathf.RoundToInt(currentHealth)} / {maxHealth}";
        }
    }

}