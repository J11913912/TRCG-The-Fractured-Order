using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    
    public string targetTag;
    public bool everyCollision;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (everyCollision)
        {
            onTriggerEnter?.Invoke();
        }
        else
        {
            if (other.CompareTag(targetTag))
            {
                onTriggerEnter?.Invoke();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (everyCollision)
        {
            onTriggerExit?.Invoke();
        }
        else
        {
            if (other.CompareTag(targetTag))
            {
                onTriggerExit?.Invoke();
            }
        }
    }
}
