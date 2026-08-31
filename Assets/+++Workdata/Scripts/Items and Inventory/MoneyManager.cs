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

    public void SetInventory()
    {
        Debug.Log("SetInventory");
        money = PlayerPrefs.GetInt("Money");
        healthPotions = PlayerPrefs.GetInt("HealthPotions");
        manaPotions = PlayerPrefs.GetInt("ManaPotions");
    }

    public void MoreMoney(int amount)
    {
        money += amount;
        
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }

    private void LessMoney(int amount)
    {
        money -= amount;

        if (money < 0)
        {
            money = 0;
        }
        
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
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
        
        PlayerPrefs.SetInt("HealthPotions", healthPotions);
        PlayerPrefs.Save();
    }

    public void ChangeManaPotion(int amount)
    {
        manaPotions += amount;
        
        if (manaPotions < 0)
        {
            manaPotions = 0;
        }
        
        PlayerPrefs.SetInt("ManaPotions", manaPotions);
        PlayerPrefs.Save();
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
