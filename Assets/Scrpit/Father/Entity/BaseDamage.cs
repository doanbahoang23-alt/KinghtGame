using System;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    [SerializeField] protected float baseDamage = 5f;
    protected float buffDamage = 0f;

    public virtual float AttackDamage()
    {
        float currentDamage = baseDamage + buffDamage;
        return currentDamage;
    }
}