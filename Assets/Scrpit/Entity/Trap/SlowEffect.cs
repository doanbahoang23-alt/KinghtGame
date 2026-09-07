using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    [SerializeField] private float percentageSlow = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISlowable slowable))
        {
            slowable.SetZoneSlow(percentageSlow, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISlowable slowable))
        {
            slowable.SetZoneSlow(percentageSlow, false);
        }
    }
}