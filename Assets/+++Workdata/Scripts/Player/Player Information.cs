using System;
using System.Collections;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static Action<int> OnHealthChange;
    
    public static Action<string, bool> ShieldOn;
   
    [SerializeField] private PlayerStates playerState;
    [SerializeField] private PlayerAnimation playerAnimation;
   
    [SerializeField] private int maxHealth = 4;
    public int currentHealth;


    public bool canTakeDamage = true;
    public bool shieldOn = false;
    
    public BasicBubbleSpell basicBubbleSpell;
    // all the other guard spells

    private MonoBehaviour _currentGuard;
   
   

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        OnHealthChange += SetDamage;
        ShieldOn += ActivateShield;
    }

    private void OnDisable()
    {
        OnHealthChange -= SetDamage;
        ShieldOn -= ActivateShield;
    }

    public void SetDamage(int damage)
    {
        if (!canTakeDamage)
        {
            shieldOn = false;
            canTakeDamage = true;
            _currentGuard.Invoke("BurstBubble", 0);
            return;
        }
        
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

    public void SetHealth(int amount)
    {
        currentHealth += amount;
        
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        } 
    }

    public void SetHealthToMax()
    {
        currentHealth = maxHealth;
    }

    private void ActivateShield(string guardSpell, bool value)
    {
        if (!value)
        {
            canTakeDamage = true;
            return;
        }
        
        switch (guardSpell)
        {
            case "Basic": 
                _currentGuard = basicBubbleSpell;
                break;
            
            case "Crystal":
                _currentGuard = basicBubbleSpell;
                break;
  
        }
        
        canTakeDamage = false;
        shieldOn = true;
    }
}
