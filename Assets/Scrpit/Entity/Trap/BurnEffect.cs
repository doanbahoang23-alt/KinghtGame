using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    [SerializeField] private float burnDamagePerSecond = 5f;
    [SerializeField] private float burnDuration = 3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IBurnable burnable))
        {
            burnable.ApplyBurn(burnDamagePerSecond, burnDuration);
        }
    }
}
