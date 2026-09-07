using UnityEngine;

public class Weapon : BaseDamage
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }
}