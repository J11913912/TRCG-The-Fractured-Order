using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage;
    
    public UnityEvent OnDamage;
    public UnityEvent OnCollision;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
            OnDamage?.Invoke();
        }

        if (other.CompareTag("Wall"))
        {
            OnCollision?.Invoke();
        }
    }
}
