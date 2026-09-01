using System;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class PlayerInformation : MonoBehaviour
{
    public static Action<int> OnHealthDown;
    public static Action<int> OnHealthUp;
    public static Action OnReset;
    
    public static Action<string, bool> ShieldOn;
   
    [SerializeField] private PlayerStates playerState;
    [SerializeField] private PlayerAnimation playerAnimation;
   
    [SerializeField] private int maxHealth = 4;
    public int currentHealth;


    public bool canTakeDamage = true;
    public bool inIFrames = false;
    public bool shieldOn = false;
    
    public BasicBubbleSpell basicBubbleSpell;
    // all the other guard spells

    private MonoBehaviour _currentGuard;

    private int iFrames;
    public int howManyIFrames;
    
    public PauseMenuManager pauseMenuManager;
   
   

    private void Awake()
    {
        currentHealth = maxHealth;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        OnHealthDown += SetDamage;
        OnHealthUp += SetHealth;
        ShieldOn += ActivateShield;
        OnReset += SetHealthToMax;
    }

    private void OnDisable()
    {
        OnHealthDown -= SetDamage;
        OnHealthUp -= SetHealth;
        ShieldOn -= ActivateShield;
        OnReset -= SetHealthToMax;
    }

    private void Update()
    {
        if (iFrames > 0)
        {
            Debug.Log("taking iframes");
            iFrames -= 1;
            
            if (iFrames == 0)
            { 
                inIFrames = false;
            }
        }
    }

    public void SetIFrames(int frames)
    {
        iFrames += frames;
    }

    public void SetDamage(int damage)
    {
        if (!canTakeDamage)
        {
            shieldOn = false;
            canTakeDamage = true;
           // _currentGuard.Invoke("BurstBubble", 0);
            return;
        }

        if (inIFrames) return;
        
        playerAnimation.AnimationSetAction(90);

        inIFrames = true;
        canTakeDamage = false;
        currentHealth -= damage;
        RuntimeManager.PlayOneShot("event:/Player/Player Hit");

        SetIFrames(howManyIFrames);
        
        //RuntimeManager.PlayOneShot("event:/SFX/Hit/Player Hit");
        
        //StartCoroutine(TakeDamage());
      
        if (currentHealth < 1)
        {
            if (playerState.GetPlayerAction() == PlayerAction.Dead) return;
         
            playerAnimation.AnimationSetAction(100);
            PlayerStates.OnChangeAction?.Invoke(PlayerAction.Dead);
            pauseMenuManager.OpenGameOverMenu();
            RuntimeManager.PlayOneShot("event:/Misc/Game Over");
            
            HealthbarManager.OnHealthDecrease(damage);
        }
        else
        {
            HealthbarManager.OnHealthDecrease(damage);
        }
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
            HealthbarManager.OnHealthIncrease(maxHealth);
        }
        else
        {
            HealthbarManager.OnHealthIncrease(amount);
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
