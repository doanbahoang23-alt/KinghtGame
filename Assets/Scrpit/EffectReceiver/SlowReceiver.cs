using UnityEngine;

public class SlowReceiver : MonoBehaviour, ISlowable
{
    private BaseSpeed baseSpeed;

    void Awake()
    {
        baseSpeed = GetComponentInParent<BaseSpeed>();
    }

    public void ApplySlow(float PercentageSlow, float duration)
    {

    } // (skill/wepon)
    public void SetZoneSlow(float PercentageSlow, bool isInsideZone)
    {
        if (isInsideZone)
        {
            float speedLeft = 1f - PercentageSlow;
            baseSpeed.SetSlowMultiplier(speedLeft);
        }
        else
        {
            baseSpeed.SetSlowMultiplier(1f);
        }
    } //(zone)
}