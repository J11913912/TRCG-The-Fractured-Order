using System;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyInformation>().TakeDamage(damage);
        }
    }
}
