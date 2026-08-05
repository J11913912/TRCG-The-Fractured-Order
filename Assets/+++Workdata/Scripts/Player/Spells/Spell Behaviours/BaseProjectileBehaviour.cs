using System;
using UnityEngine;

public class BaseProjectileBehaviour : MonoBehaviour
{
    public float timeToSelfDestruct;
    public float moveSpeed;
    private float time;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToSelfDestruct)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 direction)
    {
        _rb.linearVelocity = direction * moveSpeed;
    }
}
