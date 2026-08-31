using System;
using UnityEngine;
using FMODUnity;

public class MoneyShard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.OnMoneyIncrease?.Invoke(1);
            RuntimeManager.PlayOneShot("event:/Misc/Shard Collect");
            Destroy(gameObject);
        }
    }
}
