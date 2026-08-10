using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public CrystalAoESpell crystalAoESpell;
    public BasicBubbleSpell baseBubbleSpell;
    public BasicHealingSpell baseHealingSpell;
    public CrystalGuardSpell crystalGuardSpell;
    public CrystalHealingSpell crystalHealingSpell;
    public BasicAoESpell baseAoESpell;
    public BasicProjectileSpell baseProjectileSpell;
    public CrystalProjectileSpell crystalProjectileSpell;
    
    public void AttackCrystalAoESpell()
    {
        crystalAoESpell.Attack2();
        baseAoESpell.Attack2();
    }

    public void BasicShieldSpell()
    {
        baseBubbleSpell.SpawnBubble();
        crystalGuardSpell.SpawnBubble();
    }

    public void BasicHealingSpell()
    {
        baseHealingSpell.SpawnSparkles();
        crystalHealingSpell.SpawnSparkles();
    }

    public void ProjectileSpell()
    {
        baseProjectileSpell.Attack();
        crystalProjectileSpell.Attack();
    }
    
}
