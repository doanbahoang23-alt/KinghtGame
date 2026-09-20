using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private WeaponData currentWeapon;
    [SerializeField] private Transform weaponSocket;

    [SerializeField] private WeaponData spearDataTest;
    [SerializeField] private WeaponData axeDataTest;
    [SerializeField] private WeaponData bowDataTest;

    [SerializeField] private GameObject projectilePrefabs;
    [SerializeField] private float projectileSpeed;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (currentWeapon != null)
        {
            EquipWeapon(currentWeapon);
        }
    }

    void Update()
    {
        changeEquip();
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        BaseDamage activeWeaponDamage = null;

        foreach (Transform child in weaponSocket)
        {
            if (child.name.ToLower() == currentWeapon.WeaponName.ToLower())
            {
                child.gameObject.SetActive(true);
                activeWeaponDamage = child.GetComponent<BaseDamage>();
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        if (activeWeaponDamage != null)
        {
            activeWeaponDamage.SetWeaponStats(currentWeapon.BaseDamage, currentWeapon.KnockbackForce);
        }

        if (currentWeapon.overrideController != null)
        {
            AnimatorOverrideController currentOverride = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (currentOverride == null)
            {
                currentOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
                animator.runtimeAnimatorController = currentOverride;
            }

            var overrides = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<AnimationClip, AnimationClip>>();
            currentWeapon.overrideController.GetOverrides(overrides);
            currentOverride.ApplyOverrides(overrides);
        }

    }

    public void changeEquip()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current.digit1Key.wasPressedThisFrame && spearDataTest != null)
        {
            EquipWeapon(spearDataTest);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame && axeDataTest != null)
        {
            EquipWeapon(axeDataTest);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame && bowDataTest != null)
        {
            EquipWeapon(bowDataTest);
        }
    }

    public void AnimEvent_Shoot()
    {
        BowWeapon activeBow = weaponSocket.GetComponentInChildren<BowWeapon>();

        if (activeBow != null && activeBow.gameObject.activeInHierarchy)
        {
            activeBow.Shoot();
        }
    }
}