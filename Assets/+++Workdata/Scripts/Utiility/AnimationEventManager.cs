using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public BasicAoESpell baseAoESpell;

    public void AttackBasicAoESpell()
    {
        baseAoESpell.Attack2();
    }
}
