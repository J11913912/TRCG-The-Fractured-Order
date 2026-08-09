using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public CrystalAoESpell crystalAoESpell;
    public BasicBubbleSpell baseBubbleSpell;
    public BasicHealingSpell baseHealingSpell;
    public BasicAoESpell baseAoESpell;

    public void AttackCrystalAoESpell()
    {
        crystalAoESpell.Attack2();
    }

    public void BasicShieldSpell()
    {
        baseBubbleSpell.SpawnBubble();
    }

    public void BasicHealingSpell()
    {
        baseHealingSpell.SpawnSparkles();
    }
    
}
