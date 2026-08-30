using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static Action<int> OnMoneyIncrease;
    public static Action<int> OnMoneyDecrease;

    public static Action<int> CurrentPrize;
    
    public static Action<int> OnHealthPotion;
    public static Action<int> OnManaPotion;
    
    public int money;
    public int currentPrize;

    public int healthPotions;
    public int manaPotions;
    
    public DialogueController dialogueController;
    
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;

    private void OnEnable()
    {
        OnMoneyIncrease += MoreMoney;
        OnMoneyDecrease += LessMoney;
        CurrentPrize += SetCurrentPrize;

        OnHealthPotion += ChangeHealthPotion;
        OnManaPotion += ChangeManaPotion;
    }

    private void OnDisable()
    {
        OnMoneyIncrease -= MoreMoney;
        OnMoneyDecrease -= LessMoney;
        CurrentPrize -= SetCurrentPrize;

        OnHealthPotion -= ChangeHealthPotion;
        OnManaPotion -= ChangeManaPotion;
    }

    private void Update()
    {
        moneyText.SetText(money.ToString());
        healthText.SetText(healthPotions.ToString());
        manaText.SetText(manaPotions.ToString());
    }

    private void MoreMoney(int amount)
    {
        money += amount;
    }

    private void LessMoney(int amount)
    {
        money -= amount;

        if (money < 0)
        {
            money = 0;
        }
    }

    private void SetCurrentPrize(int prize)
    {
        currentPrize = prize;
        dialogueController.SetCurrentPrize(currentPrize);
    }

    public void ChangeHealthPotion(int amount)
    {
        healthPotions += amount;

        if (healthPotions < 0)
        {
            healthPotions = 0;
        }
    }

    public void ChangeManaPotion(int amount)
    {
        manaPotions += amount;
        
        if (manaPotions < 0)
        {
            manaPotions = 0;
        }
    }

    public int ReturnMoney()
    {
        return money;
    }

    public int ReturnHealthPotions()
    {
        return healthPotions;
    }

    public int ReturnManaPotions()
    {
        return manaPotions;
    }
}
