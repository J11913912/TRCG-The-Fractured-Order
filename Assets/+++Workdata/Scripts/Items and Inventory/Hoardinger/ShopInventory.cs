using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopInventory : MonoBehaviour
{
    public UnityEvent OnHealthAttempt;
    public UnityEvent OnManaAttempt;
    public UnityEvent OnHatAttempt;
    public UnityEvent OnSpellAttempt;

    public UnityEvent OnBackToShop;
    public UnityEvent OnClose;
    public UnityEvent OnEmpty;

    public UnityEvent OnHighPrizes;
    public UnityEvent OnLowPrizes;
    
    public int healthPotionsAmount;
    public int manaPotionsAmount;

    private int _costumisableAmount = 1;
    private int _spellAmount = 1;

    public TextMeshProUGUI healthPotionsText;
    public TextMeshProUGUI manaPotionsText;
    public TextMeshProUGUI costumisableText;
    public TextMeshProUGUI spellText;
    
    public TextMeshProUGUI description;
    public TextMeshProUGUI header;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button backButton;

    public MoneyManager moneyManager;
    public PutOnHat _putOnHat;
    
    public int prizeHealthPotions;
    public int prizeManaPotions;
    public int prizeCostumisable;
    public int prizeSpell;

    private int _defaultPrizeHealth;
    private int _defaultPrizeMana;
    private int _defaultPrizeHat;
    private int _defaultPrizeSpell;

    public bool isActive = false;
    private bool _isEmpty = false;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _defaultPrizeHealth = prizeHealthPotions;
        _defaultPrizeMana = prizeManaPotions;
        _defaultPrizeHat = prizeCostumisable;
        _defaultPrizeSpell = prizeSpell;
    }

    public void Focus()
    {
        if (!isActive) return;
        button1.Select();
    }

    public void PopUp()
    {
        _canvasGroup.alpha = 1;
        isActive = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        isActive = false;
        OnClose?.Invoke();
    }

    public void RollForHigherPrizes()
    {
        prizeHealthPotions = _defaultPrizeHealth;
        prizeManaPotions = _defaultPrizeMana;
        prizeCostumisable = _defaultPrizeHat;
        prizeSpell = _defaultPrizeSpell;
        
        int random = Random.Range(0, 5);

        if (random == 1)
        {
            prizeHealthPotions = 10;
            prizeManaPotions = 14;
            prizeCostumisable = 30;
            prizeSpell = 50;
            
            OnHighPrizes?.Invoke();
        }
        else
        {
            OnLowPrizes?.Invoke();
        }
    }

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        
        if (isActive)
        {
             GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            
            if (currentSelectedGameObject != button2.gameObject && currentSelectedGameObject != button3.gameObject && currentSelectedGameObject != button4.gameObject &&  currentSelectedGameObject != backButton.gameObject)
            {
                button1.Select();
                currentSelectedGameObject = button1.gameObject;

               if (healthPotionsAmount <= 0)
                {
                    button2.Select();
                    currentSelectedGameObject = button2.gameObject;
                    
                    if (manaPotionsAmount <= 0)
                    {
                        button3.Select();
                        currentSelectedGameObject = button3.gameObject;
                        
                        if (_costumisableAmount <= 0)
                        {
                            button4.Select();
                            currentSelectedGameObject = button4.gameObject;
                            
                            if (_spellAmount <= 0)
                            {
                               Close();
                               _isEmpty = true;
                               OnEmpty?.Invoke();
                            }
                        }
                    } 
                } 
            }
        }
        
        //healthPotionsText.SetText(healthPotionsAmount.ToString());
        //manaPotionsText.SetText(manaPotionsAmount.ToString());
        costumisableText.SetText(_costumisableAmount.ToString());
        spellText.SetText(_spellAmount.ToString());
    }

    public void ToggleActive(bool value)
    {
        isActive = false;
    }

    public void ToggleActiveON()
    {
        isActive = true;
    }

    public void TryHealth()
    {
        MoneyManager.CurrentPrize?.Invoke(prizeHealthPotions);
        OnHealthAttempt?.Invoke();
        OnHealthAttempt?.Invoke();
    }

    public void TryMana()
    {
        MoneyManager.CurrentPrize?.Invoke(prizeManaPotions);
        OnManaAttempt?.Invoke();
        OnManaAttempt?.Invoke();
    }

    public void TryHat()
    {
        MoneyManager.CurrentPrize?.Invoke(prizeCostumisable);
        OnHatAttempt?.Invoke();
        OnHatAttempt?.Invoke();
    }

    public void TrySpell()
    {
        MoneyManager.CurrentPrize?.Invoke(prizeSpell);
        OnSpellAttempt?.Invoke();
        OnSpellAttempt?.Invoke();
    }

    public void CheckHealth()
    {
        if (moneyManager.ReturnMoney() >= prizeHealthPotions)
        {
            //ChangeHealthPotions(-1);
            MoneyManager.OnMoneyDecrease?.Invoke(prizeHealthPotions);
            MoneyManager.OnHealthPotion?.Invoke(1);
        }
    }

    public void CheckMana()
    {
        if (moneyManager.ReturnMoney() >= prizeManaPotions)
        {
           // ChangeManaPotions(-1);
            MoneyManager.OnMoneyDecrease?.Invoke(prizeManaPotions);
            MoneyManager.OnManaPotion?.Invoke(1);
        }
    }

    public void CheckHat()
    {
        if (moneyManager.ReturnMoney() >= prizeCostumisable)
        {
            BoughtCostumisable();
            MoneyManager.OnMoneyDecrease?.Invoke(prizeCostumisable);
        }
    }

    public void CheckSpell()
    {
        if (moneyManager.ReturnMoney() >= prizeSpell)
        {
            BoughtSpell();
            MoneyManager.OnMoneyDecrease?.Invoke(prizeSpell);
        }
    }
    
    public void ChangeHealthPotions(int amount)
    {
        healthPotionsAmount += amount;
        
        GoBackToShop();

        if (healthPotionsAmount <= 0)
        {
            button1.interactable = false;
        }
        
    }

    public void ChangeManaPotions(int amount)
    {
        manaPotionsAmount += amount;

        if (manaPotionsAmount <= 0)
        {
            button2.interactable = false;
        }
        
        GoBackToShop();
    }

    public void BoughtCostumisable()
    {
        _costumisableAmount = 0;

        button3.interactable = false;
        
        _putOnHat.HatUnlock();
        
        GoBackToShop();
    }

    public void BoughtSpell()
    {
        _spellAmount = 0;

        button4.interactable = false;
        
        GoBackToShop();
    }

    public void GoBackToShop()
    {
        isActive = true;
        OnBackToShop?.Invoke();
        
        Debug.Log("went back ");
    }

    public void SetDescription(string desc)
    {
        description.SetText(desc);
        description.SetText(desc);
    }

    public void SetHead(string heading)
    {
        header.SetText(heading);
    }
    
    
    
}
