using UnityEngine;

public class BossAnimationEventManager : MonoBehaviour
{
    public BossCrushAbility bossCrushAbility;

    public void AfterCrush()
    {
        bossCrushAbility.Crush();
    }
}
