using System;
using UnityEngine;

public class CrownProjectileBehaviour : MonoBehaviour
{
    public static Action<bool> onActiveChange;
    
    private Transform _target;
    
    private GameObject _player;
    
    private Vector2  _direction;

    public float moveSpeed;
    
    private Rigidbody2D _rb;

    public Vector3 _position;
    
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
        Debug.Log(_player);
        
        _target = _player.transform;
            
        _direction = _target.position - transform.position;
        
        _rb.linearVelocity = _direction * moveSpeed;
    }

    public void ResetPosition()
    {
        transform.position = gameObject.transform.parent.transform.position;
    }
    
    // TODO set anioamtion for direction
}
