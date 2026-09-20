using Unity.Mathematics;
using UnityEngine;

public class BowWeapon : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowSpeed = 20f;

    private BaseDamage bowBaseStarts;
    private PlayerController playerController;

    private float lastShootTime = 0f;

    void Awake()
    {
        bowBaseStarts = GetComponent<BaseDamage>();
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Shoot()
    {
        if (Time.time - lastShootTime < 0.25f) return;
        lastShootTime = Time.time;

        if (arrowPrefab == null || firePoint == null)
        {
            return;
        }

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        if (arrow.TryGetComponent(out ProjectileDamage projectileDamage))
        {
            projectileDamage.SetupProjectile(bowBaseStarts.AttackDamage(), 10f);
        }
        if (arrow.TryGetComponent(out Rigidbody2D rb))
        {
            Animator playerAnim = transform.root.GetComponent<Animator>();
            Vector2 shootDirection = Vector2.right;

            if (playerAnim != null)
            {
                float x = playerAnim.GetFloat("LastInputX");
                float y = playerAnim.GetFloat("LastInputY");
                shootDirection = new Vector2(x, y).normalized;

                if (shootDirection == Vector2.zero)
                {
                    shootDirection = new Vector2(Mathf.Sign(transform.root.localScale.x), 0);
                }

                float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

                rb.linearVelocity = shootDirection * arrowSpeed;
            }
        }
    }
}