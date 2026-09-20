using UnityEngine;

public class Weapon : BaseDamage
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    public override void SetWeaponStats(float weaponDamage, float knockback)
    {
        base.SetWeaponStats(weaponDamage, knockback);
    }
}