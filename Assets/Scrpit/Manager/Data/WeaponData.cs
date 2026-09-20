
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "KnightGame/Weapon Data")]
public class WeaponData : ScriptableObject
{

    [SerializeField] private string weaponName;
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float knockbackForce = 15f;
    public AnimatorOverrideController overrideController;

    public string WeaponName => weaponName;
    public float BaseDamage => baseDamage;
    public float KnockbackForce => knockbackForce;
    public AnimatorOverrideController OverrideController => overrideController;
}