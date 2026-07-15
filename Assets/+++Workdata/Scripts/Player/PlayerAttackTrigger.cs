using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponentInParent<EnemyInformation>().SetEnemyDamage(1);
        }

        if (other.CompareTag("Guy"))
        {
            Debug.Log("found guy");
            //other.GetComponent<GuyInformation>().SetGuyDamage(1);
        }
    }
}
