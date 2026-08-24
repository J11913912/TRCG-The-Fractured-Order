using System;
using UnityEditor.Events;
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
            // TODO player health reduzieren
            Debug.Log("found player");
            OnDamage?.Invoke();
        }
        
        if (other.CompareTag("GoUp"))
        {
            Debug.Log("found wall");
            OnCollision?.Invoke();
        }
    }
}
