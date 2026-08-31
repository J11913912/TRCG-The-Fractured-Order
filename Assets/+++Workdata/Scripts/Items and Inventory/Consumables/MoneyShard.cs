using System;
using UnityEngine;

public class MoneyShard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.OnMoneyIncrease?.Invoke(1);
            Destroy(gameObject);
        }
    }
}
