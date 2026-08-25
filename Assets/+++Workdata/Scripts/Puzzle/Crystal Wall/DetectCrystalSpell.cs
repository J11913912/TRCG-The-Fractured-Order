using System;
using UnityEngine;
using UnityEngine.Events;

public class DetectCrystalSpell : MonoBehaviour
{
    public UnityEvent OnPillar;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        
        if (other.GetComponent<CrystalPillarBehaviour>() != null)
        {
            Debug.Log("Crystal pillar entered");
            OnPillar?.Invoke();
        }
    }
    
    
    private  void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");

        if (other.GetComponent<CrystalPillarBehaviour>() != null)
        {
            Debug.Log("Crystal pillar exited");
            OnPillar?.Invoke();
        }
    }
}
