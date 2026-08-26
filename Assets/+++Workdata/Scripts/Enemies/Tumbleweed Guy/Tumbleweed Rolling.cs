using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TumbleweedRolling : MonoBehaviour
{
    private int HashMovementValue = Animator.StringToHash("MovementValue");
    private int HashDirX = Animator.StringToHash("XDirection");
    private int HashDirY = Animator.StringToHash("YDirection");
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionId = Animator.StringToHash("ActionID");

    private Rigidbody2D _rb;
    private Animator animator;
    private float _moveSpeed = 5f;

    public bool isChasing = true;
    private bool _isBounce = false;
    public GameObject _target;

    private Vector2 _movingDirection;
    private Vector2 _lookDirection;

    public EnemyState enemyState;
    [SerializeField] private EnemyFacingDirection enemyFacingDirection;

    private Transform _startPosition;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        _startPosition = gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Debug.Log("Aaaaa");
        }

        if (isChasing)
        {
            _movingDirection = _target.transform.position - transform.position;
            _rb.linearVelocity = _movingDirection;
        }
    }

    private void LateUpdate()
    {
        UpdateFacing();
        UpdateAniamtor();
    }

    public void HitWall()
    {
        if (_isBounce) return;

        SetAnimationAction(10);

        _isBounce = true;

        Debug.Log("hit");
        isChasing = false;
        Vector2 pushBack = _rb.linearVelocity;

        switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:
                pushBack.y *= -1;
                break;

            case EnemyFacingDirection.Down:
                //pushBack = Vector2.up;
                pushBack.y *= -1;
                break;

            case EnemyFacingDirection.Left:
                //pushBack = Vector2.right;
                pushBack.x *= -1;
                break;

            case EnemyFacingDirection.Right:
                //pushBack = Vector2.left;
                pushBack.x *= -1;
                break;
        }


        _rb.linearVelocity = (pushBack * 1f);

        StartCoroutine(StunnedCountdown(1.5f));
    }

    private IEnumerator StunnedCountdown(float time)
    {
        yield return new WaitForSeconds(time);

        float xDir = Random.Range(1, 6);
        float yDir = Random.Range(1, 6);

        Vector2 newImpulse = new Vector2(xDir, yDir).normalized;

        _rb.linearVelocity = newImpulse * 3f;

        //  _rb.linearVelocityX = Random.Range(1, 3) * 3f;
        //_rb.linearVelocityY = Random.Range(1, 3) * 3f;

        // isChasing = true;
    }

    public void FreezeGuy()
    {
        _rb.linearVelocity = Vector2.zero;
        _isBounce = false;
    }

    private void UpdateFacing()
    {
        if (_isBounce) return;

        Vector2 velocity = _rb.linearVelocity;

        if (velocity.sqrMagnitude > 0.0001f)
        {
            _lookDirection = velocity.normalized;
        }
        else if (enemyState == EnemyState.Chasing)
        {
            Vector2 toPlayer = _target.transform.position - transform.position;
            _lookDirection = toPlayer.normalized;
        }

        if (enemyState == EnemyState.Attacking)
        {
            UpdateFacingDirection(_lookDirection * -1);
        }
        else
        {
            UpdateFacingDirection(_lookDirection);
        }
    }

    private void UpdateFacingDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            enemyFacingDirection = dir.x > 0 ? EnemyFacingDirection.Right : EnemyFacingDirection.Left;
        }
        else
        {
            enemyFacingDirection = dir.y > 0 ? EnemyFacingDirection.Up : EnemyFacingDirection.Down;
        }

        SetAnimationDirection(new Vector2(dir.x, dir.y));

        switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:
                SetAnimationDirection(new Vector2(0, 1));
                break;

            case EnemyFacingDirection.Down:
                SetAnimationDirection(new Vector2(0, -1));
                break;

            case EnemyFacingDirection.Left:
                SetAnimationDirection(new Vector2(-1, 0));
                break;

            case EnemyFacingDirection.Right:
                SetAnimationDirection(new Vector2(1, 0));
                break;
        }
    }

    private void UpdateAniamtor()
    {
        animator.SetFloat(HashMovementValue, _rb.linearVelocity.magnitude);
    }

    private void SetAnimationDirection(Vector2 direction)
    {
        animator.SetFloat(HashDirX, direction.x);
        animator.SetFloat(HashDirY, direction.y);
    }

    public void SetAnimationAction(int actionId)
    {
        animator.SetTrigger(HashActionTrigger);
        animator.SetInteger(HashActionId, actionId);
    }


    public void SetChase(bool value)
    {
        isChasing = value;

        if (!value)
        {
            _target = null;
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void ResetPosition()
    {
        Debug.Log("teleport");
        transform.position = _startPosition.position;
    }
}
