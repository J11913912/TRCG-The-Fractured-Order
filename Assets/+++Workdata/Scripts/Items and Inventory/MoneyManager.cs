using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static Action<int> OnMoneyIncrease;
    public static Action<int> OnMoneyDecrease;

    public static Action<int> CurrentPrize;
    
    public int money;
    public int currentPrize;
    
    public DialogueController dialogueController;

    private void OnEnable()
    {
        OnMoneyIncrease += MoreMoney;
        OnMoneyDecrease += LessMoney;
        CurrentPrize += SetCurrentPrize;
    }

    private void OnDisable()
    {
        OnMoneyIncrease -= MoreMoney;
        OnMoneyDecrease -= LessMoney;
        CurrentPrize -= SetCurrentPrize;
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

    public int ReturnMoney()
    {
        return money;
    }
}
