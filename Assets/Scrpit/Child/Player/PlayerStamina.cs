using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : BaseStamina
{
    [SerializeField] private Image staminaBarFill;
    [SerializeField] private float lostStamina = 30f;
    [SerializeField] private float regenStamina = 15f;

    public float Lost { get { return lostStamina; } }
    public float Regen { get { return regenStamina; } }

    protected override void Start()
    {
        base.Start();
        updateUI();
    }

    public override void UseStamina(float amount)
    {
        base.UseStamina(amount);
        updateUI();
    }

    public override void RegenStamina(float amount)
    {
        base.RegenStamina(amount);
        updateUI();
    }

    private void updateUI()
    {
        if (staminaBarFill != null)
        {
            staminaBarFill.fillAmount = currentStamina / maxStamina;
        }
    }

}