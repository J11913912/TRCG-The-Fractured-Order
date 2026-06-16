using System;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO player health reduzieren
        }
    }
}
