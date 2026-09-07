using System.Collections;
using UnityEngine;

public class BurnReceiver : MonoBehaviour, IBurnable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color originalColor = Color.white;
    private IDamageable damageable;
    private bool isBurning = false;
    void Awake()
    {
        damageable = GetComponentInParent<IDamageable>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null) originalColor = spriteRenderer.color;
    }

    public void ApplyBurn(float damePerSecond, float duration)
    {
        if (!isBurning)
        {
            StartCoroutine(BurnRoutine(damePerSecond, duration));
        }

    }

    private IEnumerator BurnRoutine(float damePerSecond, float duration)
    {
        isBurning = true;
        float timer = 0f;
        while (timer < duration)
        {
            if (damageable != null)
            {
                damageable.TakeDamage(damePerSecond);
            }

            if (spriteRenderer != null)
            {
                StartCoroutine(FlickerBurnColor());
            }
            timer += 1f;
            yield return new WaitForSeconds(1f);
        }
        if (spriteRenderer != null) spriteRenderer.color = originalColor;
        isBurning = false;
    }

    private IEnumerator FlickerBurnColor()
    {
        spriteRenderer.color = new Color(1f, 0.4f, 0f);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = originalColor;
    }
}