using System;
using System.Collections;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public static int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static int Hash_XDirection = Animator.StringToHash("XDirection");
    public static int Hash_YDirection = Animator.StringToHash("YDirection");
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public float speed;

    public Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _direction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        UpdateAnimator();
    }

    public void Shoot(Vector2 direction)
    {
        _direction = direction;
        _rb.linearVelocity = _direction * speed;
    }

    public void StartDeathCountdown()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void SetAnimation(int id)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionID, id);
    }
    private void UpdateAnimator()
    {
        if (_direction == Vector2.down)                                                                                 // set flying direction
        {
            _animator.SetFloat(Hash_YDirection, -1);
            _animator.SetFloat(Hash_XDirection, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -180);
        }

        if (_direction == Vector2.right)
        {
            _animator.SetFloat(Hash_YDirection, 0);
            _animator.SetFloat(Hash_XDirection, -1);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (_direction == Vector2.left)
        {
            _animator.SetFloat(Hash_YDirection, 0);
            _animator.SetFloat(Hash_XDirection, 1);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (_direction == Vector2.up)
        {
            _animator.SetFloat(Hash_YDirection, 1);
            _animator.SetFloat(Hash_XDirection, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
