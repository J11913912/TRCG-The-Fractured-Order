using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static Action<int> OnMoneyIncrease;
    public static Action<int> OnMoneyDecrease;
    
    public int money;

    private void OnEnable()
    {
        OnMoneyIncrease += MoreMoney;
        OnMoneyDecrease += LessMoney;
    }

    private void OnDisable()
    {
        OnMoneyIncrease -= MoreMoney;
        OnMoneyDecrease -= LessMoney;
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

    public int ReturnMoney()
    {
        return money;
    }
}
