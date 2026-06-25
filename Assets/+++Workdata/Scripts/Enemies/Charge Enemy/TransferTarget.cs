using System;
using UnityEngine;

public class TransferTarget : MonoBehaviour
{
    public static Action onChargeStart;
    
    private GameObject target;
    
    public enum ForWhatEnemy{Charge, Tumbleweed}

    public ForWhatEnemy forWhatEnemy;

    private void OnEnable()
    {
        onChargeStart += GiveTarget;
    }

    private void OnDisable()
    {
        onChargeStart -= GiveTarget;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2D");
        
            target = other.gameObject;
        
            ChargeAbility.onTargetChange?.Invoke(true); // switch case for chasebahviour ????????????
        }
    }

    private void GiveTarget()
    {
        if (target == null) return;
        ChargeAbility.onChargeEnter(target.transform.position);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject == target)
            {
                target = null;
                Debug.Log("OnTriggerExit2D");
                ChargeAbility.onTargetChange?.Invoke(false);
            }
        }
    }
}
