using UnityEngine;

public class DropBossCrystal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
}
