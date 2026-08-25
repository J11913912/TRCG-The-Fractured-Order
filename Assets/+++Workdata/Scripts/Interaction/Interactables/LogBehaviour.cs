using System;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    public Transform target;

    private float _moveSpeed = 5f;

    private void Awake()
    {
        _rb =  GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopLog"))
        {
            _rb.linearVelocity = Vector2.zero;
            _animator.SetBool("RollSide", false);
            _animator.SetBool("Roll", false); 
            _boxCollider.enabled = false;
        }
    }

    public void RollDown()
    {
        _rb.linearVelocity = Vector2.down * _moveSpeed;
        _animator.SetBool("Roll", true);
    }
    
    public void RollRight()
    {
        _rb.linearVelocity = Vector2.right * _moveSpeed;
        _animator.SetBool("RollSide", true);
    }
    
    public void RollLeft()
    {
        Vector2 direction = target.position - transform.position;
        _rb.linearVelocity = direction * _moveSpeed;
        _animator.SetBool("RollSide", true);
    }
    
    public void RollRealLeft()
    {
        _rb.linearVelocity = Vector2.left * _moveSpeed;
    }

    public void RollRealRight()
    {
        _rb.linearVelocity = Vector2.right * _moveSpeed;
    }
}
