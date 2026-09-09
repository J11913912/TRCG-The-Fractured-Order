using UnityEngine;

public class DropManaPotion : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ManaManager>().IncreaseMana(40);
            Destroy(this.gameObject);
        }
    }
}
