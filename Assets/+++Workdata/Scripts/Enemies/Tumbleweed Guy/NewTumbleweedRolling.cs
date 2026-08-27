using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody2D))]
public class NewTumbleweedRolling : MonoBehaviour
{
    private enum RollState
    {
        Rolling,
        Stopped
    }

    private static readonly int HashMovementValue = Animator.StringToHash("MovementValue");
    private static readonly int HashDirX = Animator.StringToHash("XDirection");
    private static readonly int HashDirY = Animator.StringToHash("YDirection");
    private static readonly int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private static readonly int HashActionId = Animator.StringToHash("ActionID");

    private const float MinSeparationDot = 0.15f;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    private float _defaultMoveSpeed;
    [SerializeField] private Vector2 _startDirection = Vector2.right;
    [SerializeField] private float _stopDuration = 1.5f;
    [SerializeField, Range(0f, 80f)] private float _maxBounceAngle = 35f;

    [Header("Roam Area (offset relative to the spawn position)")]
    [SerializeField] private Vector2 _areaCenterOffset = Vector2.zero;
    [SerializeField] private Vector2 _areaSize = new Vector2(12f, 8f);

    [Header("Obstacle Detection")]
    [SerializeField] private LayerMask _obstacleMask = ~0;
    [Tooltip("Distance of the predictive cast. Set to 0 to rely on physics collisions only.")]
    [SerializeField] private float _lookAheadDistance = 0.3f;
    [SerializeField] private float _lookAheadRadius = 0.25f;

    [Header("Visuals")]
    [SerializeField] private Transform _visualRoot;
    [SerializeField] private bool _spinVisual;
    [SerializeField] private float _rollDegreesPerSecond = 360f;

    [Header("Animation")]
    [SerializeField] private int _bounceActionId = 10;
    [SerializeField] private EnemyFacingDirection _facingDirection = EnemyFacingDirection.Right;

    [Header("Chasing")] 
    public bool canChase = true;
    public bool isChasing = false;
    [FormerlySerializedAs("rolledOver")] public bool isNextToPlayer = false;

    public GameObject player;

    private Rigidbody2D _rb;
    private Animator _animator;

    private Vector2 _direction;
    private Vector2 _pendingDirection;
    private Vector2 _lookDirection;
    private Vector2 _startPosition;
    private Vector2 _areaCenter;

    private RollState _state = RollState.Rolling;
    private float _stopTimer;
    public bool _isPaused;

    public Vector2 CurrentDirection => _direction;

    public bool IsStopped => _state == RollState.Stopped;

    public EnemyFacingDirection FacingDirection => _facingDirection;
    
    private void Awake()
    {
        _defaultMoveSpeed = _moveSpeed;
        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        // top down setup: no gravity, no physical rotation of the root object
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;

        _startPosition = _rb.position;
        _areaCenter = _startPosition + _areaCenterOffset;

        _direction = _startDirection.sqrMagnitude > 0.0001f
            ? _startDirection.normalized
            : RotateVector(Vector2.right, Random.Range(0f, 360f));

        _lookDirection = _direction;
    }

    private void FixedUpdate()
    {
        if (_isPaused)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        if (_state == RollState.Stopped)
        {
            TickStop();
            return;
        }

        Vector2 nextPosition = _rb.position + _direction * (_moveSpeed * Time.fixedDeltaTime);

        if (TryGetAreaNormal(nextPosition, out Vector2 areaNormal))
        {
            Bounce(areaNormal);
            return;
        }

        if (_lookAheadDistance > 0f)
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                _rb.position, _lookAheadRadius, _direction, _lookAheadDistance, _obstacleMask);

            if (hit.collider != null && !hit.collider.isTrigger)
            {
                Bounce(hit.normal);
                return;
            }
        }

        if (isChasing)
        {
            _moveSpeed = _defaultMoveSpeed + 0.1f;
            
            //GetComponentInChildren<SpriteRenderer>().color = Color.red;
           // float distance = Vector3.Distance(player.transform.position, transform.position);
            
            if (isNextToPlayer)
            {
                Debug.Log("rolled over player");
                isNextToPlayer = true;
                isChasing = false;
                _moveSpeed = _defaultMoveSpeed + 0.5f;
                // GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                _moveSpeed = _defaultMoveSpeed + 0.1f;
                _direction = (player.transform.position - transform.position).normalized;
            }

            _rb.linearVelocity = _direction * (_moveSpeed);
        }
        else
        {
            _rb.linearVelocity = _direction * _moveSpeed;
        }
    }

   
    private void Update()
    {
        //UpdateVisualSpin();
    }

    private void LateUpdate()
    {
        UpdateFacing();
        UpdateAnimator();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_state != RollState.Rolling || _isPaused) return;
        if ((_obstacleMask.value & (1 << collision.gameObject.layer)) == 0) return;

        Bounce(GetAveragedNormal(collision));
    }

    public void IsNextToPlayer(bool value)
    {
        isNextToPlayer = value;
    }
    
    public void HitWall()
    {
        if (_state != RollState.Rolling) return;

        Bounce(-_direction);
    }

    public void SetMovingDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.0001f) return;

        _direction = direction.normalized;
        _pendingDirection = _direction;
        StartRolling();
    }

    public void SetPaused(bool value)
    {
        _isPaused = value;

        if (value)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public void ResetPosition()
    {
        _rb.position = _startPosition;
        _rb.linearVelocity = Vector2.zero;
        StartRolling();
    }

    public void SetAnimationAction(int actionId)
    {
        if (_animator == null) return;

        _animator.SetInteger(HashActionId, actionId);
        _animator.SetTrigger(HashActionTrigger);
    }

    protected virtual void OnHitObstacle(Vector2 normal, Vector2 newDirection)
    {
        
    }

    protected virtual void OnStopBegin()
    {
        canChase = false;
    }

    protected virtual void OnRollBegin()
    {
        _moveSpeed = _defaultMoveSpeed;
        canChase = true;
        isChasing = false;
    }

    private void Bounce(Vector2 normal)
    {
        _pendingDirection = CalculateBounceDirection(_direction, normal);

        _state = RollState.Stopped;
        _stopTimer = _stopDuration;
        _rb.linearVelocity = Vector2.zero;

        SetAnimationAction(_bounceActionId);
        OnHitObstacle(normal, _pendingDirection);
        OnStopBegin();
    }

    private void TickStop()
    {
        _rb.linearVelocity = Vector2.zero;
        _stopTimer -= Time.fixedDeltaTime;

        if (_stopTimer > 0f) return;

        _direction = _pendingDirection;
        StartRolling();
    }

    private void StartRolling()
    {
        _state = RollState.Rolling;
        _stopTimer = 0f;
        OnRollBegin();
    }

    private Vector2 CalculateBounceDirection(Vector2 direction, Vector2 normal)
    {
        Vector2 reflected = Vector2.Reflect(direction, normal).normalized;

        float angle = Random.Range(-_maxBounceAngle, _maxBounceAngle);
        Vector2 result = RotateVector(reflected, angle);

        if (Vector2.Dot(result, normal) < MinSeparationDot) result = RotateVector(reflected, -angle);
        if (Vector2.Dot(result, normal) < MinSeparationDot) result = reflected;
        if (Vector2.Dot(result, normal) < MinSeparationDot) result = normal;

        return result.normalized;
    }

    private bool TryGetAreaNormal(Vector2 nextPosition, out Vector2 normal)
    {
        normal = Vector2.zero;

        Vector2 half = _areaSize * 0.5f;
        Vector2 min = _areaCenter - half;
        Vector2 max = _areaCenter + half;

        if (nextPosition.x < min.x && _direction.x < 0f) normal += Vector2.right;
        else if (nextPosition.x > max.x && _direction.x > 0f) normal += Vector2.left;

        if (nextPosition.y < min.y && _direction.y < 0f) normal += Vector2.up;
        else if (nextPosition.y > max.y && _direction.y > 0f) normal += Vector2.down;

        if (normal.sqrMagnitude < 0.0001f) return false;

        normal = normal.normalized;
        return true;
    }

    private static Vector2 GetAveragedNormal(Collision2D collision)
    {
        Vector2 sum = Vector2.zero;

        for (int i = 0; i < collision.contactCount; i++)
        {
            sum += collision.GetContact(i).normal;
        }

        return sum.sqrMagnitude < 0.0001f ? Vector2.up : sum.normalized;
    }

    private static Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
    }

    public void SetChase(bool value)
    {
        if (!canChase) return;
        
        isChasing = value;
    }
    
    #region Animation
    private void UpdateFacing()
    {
        if (_state != RollState.Rolling || _isPaused) return;

        Vector2 velocity = _rb.linearVelocity;

        if (velocity.sqrMagnitude > 0.0001f)
        {
            _lookDirection = velocity.normalized;
        }

        if (Mathf.Abs(_lookDirection.x) > Mathf.Abs(_lookDirection.y))
        {
            _facingDirection = _lookDirection.x > 0f ? EnemyFacingDirection.Right : EnemyFacingDirection.Left;
        }
        else
        {
            _facingDirection = _lookDirection.y > 0f ? EnemyFacingDirection.Up : EnemyFacingDirection.Down;
        }

        switch (_facingDirection)
        {
            case EnemyFacingDirection.Up:
                SetAnimationDirection(Vector2.up);
                break;

            case EnemyFacingDirection.Down:
                SetAnimationDirection(Vector2.down);
                break;

            case EnemyFacingDirection.Left:
                SetAnimationDirection(Vector2.left);
                break;

            case EnemyFacingDirection.Right:
                SetAnimationDirection(Vector2.right);
                break;
        }
    }

    private void SetAnimationDirection(Vector2 direction)
    {
        if (_animator == null) return;

        _animator.SetFloat(HashDirX, direction.x);
        _animator.SetFloat(HashDirY, direction.y);
    }

    private void UpdateAnimator()
    {
        if (_animator == null) return;

        _animator.SetFloat(HashMovementValue, _rb.linearVelocity.magnitude);
    }

    private void UpdateVisualSpin()
    {
        if (!_spinVisual || _visualRoot == null) return;

        float normalizedSpeed = _moveSpeed > 0f ? _rb.linearVelocity.magnitude / _moveSpeed : 0f;
        float sign = _direction.x >= 0f ? -1f : 1f;

        _visualRoot.Rotate(0f, 0f, sign * _rollDegreesPerSecond * normalizedSpeed * Time.deltaTime);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Vector2 center = Application.isPlaying
            ? _areaCenter
            : (Vector2)transform.position + _areaCenterOffset;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, _areaSize);

        Gizmos.color = Color.green;
        Vector2 dir = Application.isPlaying ? _direction : _startDirection.normalized;
        Gizmos.DrawRay(transform.position, dir * 2f);

        if (_lookAheadDistance > 0f)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere((Vector2)transform.position + dir * _lookAheadDistance, _lookAheadRadius);
        }
    }
}