using System;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    public static Action<Vector3> onChargeEnter;
    
    public bool chasing = false;

    public Rigidbody2D rb;

    private Vector3 target;

    private Vector3 _direction;
    
    public void StartChase(Vector2 _target)
    { 
        chasing = true;

        target = _target;

    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chasing = false;
        }
    }

    private void FixedUpdate()
    {
        if (!chasing) return;
        
    /////    _direction = target - 

        rb.linearVelocity = target;
    }
}
