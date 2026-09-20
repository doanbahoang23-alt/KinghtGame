using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;

    private Animator animator;
    public bool isDeath { get; private set; } = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateHealthUI();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        if (isDeath) return;
        base.Die();
        if (animator != null)
        {
            animator.SetBool("isDeath", true);
        }

        StartCoroutine(RespawnRouteTine());
    }

    private IEnumerator RespawnRouteTine()
    {
        yield return new WaitForSeconds(2.5f);

        GameObject respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.transform.position;
        }
        Revise();
    }

    private void Revise()
    {
        isDeath = false;
        currentHealth = maxHealth;
        if (animator != null)
        {
            animator.SetBool("isDeath", false);
        }
        UpdateHealthUI();
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