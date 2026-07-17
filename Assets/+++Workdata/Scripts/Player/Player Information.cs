using System;
using System.Collections;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static Action<int> OnHealthChange;
   
    [SerializeField] private PlayerStates playerState;
    [SerializeField] private PlayerAnimation playerAnimation;
   
    [SerializeField] private int maxHealth = 4;
    public int currentHealth;


    public bool canTakeDamage = true;
   
   

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        OnHealthChange += SetDamage;
    }

    private void OnDisable()
    {
        OnHealthChange -= SetDamage;
    }

    public void SetDamage(int damage)
    {
        if (!canTakeDamage)  return;
      
        canTakeDamage = false;
        currentHealth -= damage;
        //RuntimeManager.PlayOneShot("event:/SFX/Hit/Player Hit");
      
        if (currentHealth < 1)
        {
            if (playerState.GetPlayerAction() == PlayerAction.Dead) return;
         
            playerAnimation.AnimationSetAction(100);
            PlayerStates.OnChangeAction?.Invoke(PlayerAction.Dead);
         
        }
        
        StartCoroutine(TakeDamage());
    }

    public IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    public void SetHealthToMax()
    {
        currentHealth = maxHealth;
    }
}
