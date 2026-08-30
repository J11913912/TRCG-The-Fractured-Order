using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

public class LogBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    public Transform target;
    public UnityEvent LogRollStart;
    public UnityEvent LogRollStop;

    private float _moveSpeed = 3f;

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
            LogRollStop?.Invoke();
            RuntimeManager.PlayOneShot("event:/Enviroment/Desert/Log Crash");
            _rb.linearVelocity = Vector2.zero;
            _animator.SetBool("RollSide", false);
            _animator.SetBool("Roll", false); 
            _boxCollider.enabled = false;
        }
    }

    public void RollDown()
    {
        LogRollStart?.Invoke();
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
        LogRollStart?.Invoke();
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
