using UnityEngine;

public class BossAnimationEventManager : MonoBehaviour
{
    public BossCrushAbility bossCrushAbility;
    public BossSpinAbility bossSpinAbility;

    public void AfterCrush()
    {
        bossCrushAbility.Crush();
    }

    public void SpinColliders()
    {
        bossSpinAbility.ActivateSpinColliders();
    }
}
