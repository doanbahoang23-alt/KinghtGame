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
        if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        {
            TakeDamage(20f); // Gọi hàm trừ 20 máu
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        RestartCombatTime();
        UpdateHealthUI();
    }

    public override void RegentHealth(float amount)
    {
        base.RegentHealth(amount);
        UpdateHealthUI();
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