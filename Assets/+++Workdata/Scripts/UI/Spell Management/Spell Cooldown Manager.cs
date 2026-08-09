using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellCooldownManager : MonoBehaviour
{
    public static Action<SpellDefinition> OnStartCooldown;

    public GameObject player;
    private List<Component> playerComponents = new List<Component>();
    private void OnEnable()
    {
        OnStartCooldown += SetCooldown;
    }

    private void OnDisable()
    {
        OnStartCooldown -= SetCooldown;
    }

    private void SetCooldown(SpellDefinition spell) // dart orb guard healing
    {
        spell.cooldownState = SpellDefinition.CooldownState.cooling;
       
        StartCoroutine(CoolDown(spell, spell.cooldownTime));
    }

    private IEnumerator CoolDown(SpellDefinition spell, float time)
    {
        yield return new WaitForSeconds(time);
        spell.cooldownState = SpellDefinition.CooldownState.cooled;
        
        switch (spell.index)
        {
            case 0:
                // BasicProjectileSpell.OnAttackFree?.Invoke();
                break;
            case 1:
                BasicAoESpell.OnCooldownEnd?.Invoke();
                break;
            case 2:
                BasicBubbleSpell.OnCooledDown?.Invoke();
                break;
            case 3:
                BasicHealingSpell.OnCooldownEnd?.Invoke();
                break;
            case 4:
                //crystal projectile
                break;
            case 5:
                CrystalAoESpell.OnCooldownEnd?.Invoke();
                break;
            
        }
    }
}
