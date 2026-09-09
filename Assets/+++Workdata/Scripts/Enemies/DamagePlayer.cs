using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInformation>().SetDamage(damage);
        }
    }
}
