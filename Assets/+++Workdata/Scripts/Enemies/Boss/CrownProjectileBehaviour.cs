using System;
using UnityEngine;

public class CrownProjectileBehaviour : MonoBehaviour
{
    public Action<bool> onActiveChange;
    
    private Transform _target;
    
    private GameObject _player;
    
    private Vector2  _direction;
    
    private Rigidbody2D _rb;
    
    public bool projectileActive = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        
        _player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        onActiveChange += IsActive;
    }

    private void OnDisable()
    {
        onActiveChange -= IsActive;
    }

    public void IsActive(bool value)
    {
        projectileActive = value;
        gameObject.SetActive(value);
    }
    
    public void Shoot()
    {
        _target = _player.transform;
            
        _direction = _target.position - transform.position;
        
        _rb.linearVelocity = _direction;
    }
}
