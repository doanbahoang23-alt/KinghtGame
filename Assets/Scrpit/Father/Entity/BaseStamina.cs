using UnityEngine;

public class BaseStamina : MonoBehaviour, IExhaustible
{
    [SerializeField] protected float maxStamina = 100f;
    protected float currentStamina;

    protected virtual void Start()
    {
        currentStamina = maxStamina;
    }

    public virtual void UseStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    public virtual bool HasEnoughStamina(float amount)
    {
        return currentStamina >= amount;
    }

    public virtual void RegenStamina(float amount)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += amount;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}