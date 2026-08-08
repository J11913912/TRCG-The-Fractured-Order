using UnityEngine;

public class EndOfActionManager : MonoBehaviour
{
    public void SetEnd(string method)
    {
        switch (method)
        {
            case "BasicProjectile":
                BasicProjectileSpell.OnAttackEnd?.Invoke();
                break;
            
            case "BasicHealing":
                BasicHealingSpell.OnHealEnd?.Invoke();
                break;
            
            case "CrysProjectile":
                CrystalProjectileSpell.OnAttackEnd?.Invoke();
                break;
        }
    }
}
