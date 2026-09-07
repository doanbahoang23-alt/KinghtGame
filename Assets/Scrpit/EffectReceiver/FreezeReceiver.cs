using System.Collections;
using UnityEngine;

public class FreezeReceiver : MonoBehaviour, IFreezable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BaseSpeed baseSpeed;
    private IDamageable damageable;

    private Color originalColor = Color.white;
    private bool isFrozen = false;

    void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        baseSpeed = GetComponentInParent<BaseSpeed>();
        damageable = GetComponentInParent<IDamageable>();
        if (spriteRenderer != null) originalColor = spriteRenderer.color;
    }

    public void ApplyFreeze(float stunDuration, float frostDuration, float damagePerSec)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeRoutine(stunDuration, frostDuration, damagePerSec));
        }
    }

    private IEnumerator FreezeRoutine(float stunDuration, float frostDuration, float damagePerSec)
    {
        isFrozen = true;
        float timer = 0f;

        if (baseSpeed != null) baseSpeed.SetSlowMultiplier(0f);
        if (spriteRenderer != null) spriteRenderer.color = new Color(0.2f, 0.6f, 1f);

        while (timer < stunDuration)
        {
            if (damageable != null) damageable.TakeDamage(damagePerSec);
            timer += 1f;
            yield return new WaitForSeconds(1f);
        }

        if (baseSpeed != null) baseSpeed.SetSlowMultiplier(0.5f);

        while (timer < frostDuration)
        {
            if (damageable != null) damageable.TakeDamage(damagePerSec);
            if (spriteRenderer != null) StartCoroutine(FlickerIceColor());

            timer += 1f;
            yield return new WaitForSeconds(1f);
        }

        if (baseSpeed != null) baseSpeed.SetSlowMultiplier(1f);
        if (spriteRenderer != null) spriteRenderer.color = originalColor;

        isFrozen = false;
    }

    private IEnumerator FlickerIceColor()
    {
        spriteRenderer.color = new Color(0.2f, 0.6f, 1f);
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = originalColor;
    }
}