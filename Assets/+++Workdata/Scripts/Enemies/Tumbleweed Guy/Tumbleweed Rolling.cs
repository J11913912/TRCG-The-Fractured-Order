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

    public bool isChasing = false;
    private bool _isRolling = false;
    private bool _isBounce = false;
    public GameObject _target;

    private Vector2 _movingDirection;
    private Vector2 _lookDirection;

    public EnemyState enemyState;
    [SerializeField] private EnemyFacingDirection enemyFacingDirection;

    private Transform _startPosition;

    public int rayCount;
    public Vector2 rayDampingPos;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        _startPosition = gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            Debug.Log("is chasing");
            _movingDirection = _target.transform.position - transform.position;
            _rb.linearVelocity = _movingDirection;
        }
        
        if (_isRolling && !isChasing)
        {
            Debug.Log("is idle");
            _rb.linearVelocity = _movingDirection * _moveSpeed;
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
        _isRolling = false;

        Debug.Log("hit");
        isChasing = false;
        Vector2 pushBack = _rb.linearVelocity;

        switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up: 
                pushBack = Vector2.down;
                break;

            case EnemyFacingDirection.Down:
                pushBack = Vector2.down;
                break;

            case EnemyFacingDirection.Left:
                pushBack = Vector2.right;
                break;

            case EnemyFacingDirection.Right: 
                pushBack = Vector2.left;
                break;
        }
        
        _rb.linearVelocity = (pushBack * 2f);

        StartCoroutine(StunnedCountdown(1.5f));
    }
    
    private IEnumerator StunnedCountdown(float time)
    {
        yield return new WaitForSeconds(time);

        _isRolling = true;
        
        //_movingDirection

        _rb.linearVelocity *= -1;

    }

    public void SetMovingDirection(Vector2 direction)
    {
        Debug.Log("set direction");

        direction.x = direction.x + Random.Range(-2, 5);
        direction.y = direction.y + Random.Range(-2, 5);
        
        _movingDirection = direction;
    }
    

   /* public void HitWall()
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
    
    */

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

    public Vector3 GetDirection()
    {
        switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:
                return Vector3.up;
                break;

            case EnemyFacingDirection.Down:
                return Vector3.down;
                break;

            case EnemyFacingDirection.Left:
                return Vector3.left;
                break;

            case EnemyFacingDirection.Right:
                return Vector3.right;
                break;
            default:
                return Vector3.zero;
                break;
        }
        
    }
    
    private void OnDrawGizmos()
    {
        float startAngle = -50;
        for (int i = 0; i < 5; i++)
        {
            Vector3 rightDir = Quaternion.AngleAxis(startAngle, transform.forward) * GetDirection();
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + (Vector3)rayDampingPos, rightDir * 3);
            startAngle += 25;
        }
    }
}
