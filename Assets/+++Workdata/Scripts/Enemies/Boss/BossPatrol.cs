using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class BossPatrol : MonoBehaviour
{
    private int HashMovementValue = Animator.StringToHash("MovementValue");
    private int HashDirX = Animator.StringToHash("XDirection");
    private int HashDirY = Animator.StringToHash("YDirection");
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionID = Animator.StringToHash("ActionID");
    
    
    #region Inspector

    public UnityEvent OnAttack;
    
    [Header("Enemy States")] 
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private EnemyFacingDirection enemyFacingDirection;

    [Header("Navigation")] 
    [SerializeField] private float navmeshPathTimer = .25f;
    
    [Header("NPC Reference")] 
    [SerializeField] private Animator animator;
    [SerializeField] private bool startDirectionIsRight = false;

    [Header("AttackSetting")] 
    [SerializeField] private float stopChasingTimer = 2f;

    [SerializeField] private float attackCooldown = 1f;
    
    public int attackRandom;
    public int random;
    public bool isSpinning = false;
    
    [Header("Waypoints")] 
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<Transform> spinningWaypoints;
    private List<Transform> oldWaypoints;
    [SerializeField] private bool waitAtWaypoint = true;
    [SerializeField] private bool randomOrder;
    [SerializeField] private bool canPatrol = true;
    [SerializeField] private Vector2 waitDuration = new Vector2(1, 5);
    private Vector2 oldWaitDuration;

    public bool isTumbleweed = false;
    
    #endregion
    
    #region Private Variables

    private NavMeshAgent _agent;
    private Transform _target;
    private Transform _player;
    
    private int _currentWaypointIndex = -1;
    
    private bool _isWaiting;
    public bool _isAggroed = false;
    public bool _canAttack = false;

    public float _attackCooldownTimer;
    private float _lastNavmeshTime;

    private Coroutine _attackCoroutine;
    private Coroutine _aggroCoroutine;
    private Coroutine _newWaitpoint;

    private Vector2 _lookDirection;

    private Vector3 _targetBeyond;
    
    private BossCrushAbility _bossCrushAbility;
    private CrownAbility _crownAbility;
    private BossSpinAbility _bossSpinAbility;

    #endregion
    
    #region Unity Event Functions

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindFirstObjectByType<PlayerController>().transform;
        _agent.autoBraking = waitAtWaypoint;
        
        _bossCrushAbility = GetComponent<BossCrushAbility>();
        _bossSpinAbility = GetComponent<BossSpinAbility>();
        _crownAbility = GetComponent<CrownAbility>();
        
        oldWaypoints = waypoints;
        oldWaitDuration = waitDuration;
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        SetNextWaypoint();
    }

    private void Update()
    {
        if (isSpinning)
        {
            SetNewTarget(_player);
        }
        
        if (_canAttack && enemyState != EnemyState.Attacking)
        {
            _attackCooldownTimer += Time.deltaTime;
            if (_attackCooldownTimer > attackCooldown)
            {
                enemyState = EnemyState.Attacking;
                OnAttack?.Invoke();
                //SetAnimationAction(1);
            }
        }

        if (!_agent.isStopped && enemyState != EnemyState.Chasing && enemyState != EnemyState.Attacking)
        {
            CheckIfWaypointIsReached();
        }
        else if (!_agent.isStopped && enemyState == EnemyState.Chasing)
        {
            float distance = Vector2.Distance(transform.position, _target.position);

            if (distance > _agent.stoppingDistance + 0.01f)
            {
                if (_lastNavmeshTime + navmeshPathTimer < Time.time)
                {
                    _agent.SetDestination(_target.position);
                    _lastNavmeshTime = Time.time; 
                }
            }
            else
            {
                _targetBeyond = _target.position + new Vector3(_lookDirection.x, _lookDirection.y, 0) * 5f;
                
                if (isTumbleweed)
                {
                    _agent.SetDestination(_targetBeyond);
                    return;
                }
                
                _agent.ResetPath();
            }
        }
    }


    private void LateUpdate()
    {
        if (!_isWaiting)
        { 
            UpdateFacing();
        }
        else
        {
            LookAtPlayer();
        }
        
        UpdateAniamtor();
    }
    
    #endregion
    
    #region Navigation

    private void UpdateFacing()
    {
        Vector2 velocity = _agent.velocity;

        if (velocity.sqrMagnitude > 0.0001f)
        {
            _lookDirection = velocity.normalized;
        }
        else if(enemyState == EnemyState.Chasing || enemyState == EnemyState.Attacking)
        {
            Vector2 toPlayer = _player.position - transform.position;
            _lookDirection = toPlayer.normalized;
        }

        UpdateFacingDirection(_lookDirection);
        RotateObj(_lookDirection);
    }

    private void UpdateFacingDirection(Vector2 dir)
    {
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
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

    private void LookAtPlayer()
    {
        Vector2 direction = _player.position - transform.position;
        
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            enemyFacingDirection = direction.x > 0 ? EnemyFacingDirection.Right : EnemyFacingDirection.Left;
        }
        else
        {
            enemyFacingDirection = direction.y > 0 ? EnemyFacingDirection.Up : EnemyFacingDirection.Down;
        }
        SetAnimationDirection(new Vector2(direction.x, direction.y));

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
    
    private void RotateObj(Vector2 direction)
    {
        /*
        
        if (direction.x < 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, startDirectionIsRight ? 180 : 0, 0);
        }
        else if (direction.x > 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, startDirectionIsRight ? 0 : 180, 0);
        }
        
        */
    }

    public void StopPatrolForDialogue()
    {
        
    }

    public void StopPatrol()
    {
        _agent.isStopped = true;
    }

    public void ResumePatrol()
    {
        _agent.isStopped = false;
    }

    public void TogglePatrol()
    {
        _agent.isStopped = !_agent.isStopped;
        canPatrol = !canPatrol;
    }

    public void SetNewTarget(Transform newTarget)
    {
        _target = newTarget;
        _agent.isStopped = false;
        canPatrol = false;
        _agent.SetDestination(_target.position);
    }

    public void SetNewWaypoints(List<Transform> newWaypoints)
    {
        waypoints = newWaypoints;
        canPatrol = true;
        ResumePatrol();
    }
    
    public void SetSpinning(List<Transform> newWaypoints)
    {
        waypoints = newWaypoints;
       // canPatrol = true;
        _isWaiting = false;
        waitAtWaypoint = false;
        waitDuration = new Vector2(0, 0);
        
        SetNewTarget(_player);
    }
    
    private void SetNextWaypoint()
    {
        if (randomOrder)
        {
            int newWaypointIndex;

            do
            {
                newWaypointIndex = Random.Range(0, waypoints.Count);
            } while (newWaypointIndex == _currentWaypointIndex);

            _currentWaypointIndex = newWaypointIndex;
        }
        else
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
        }

        _target = waypoints[_currentWaypointIndex];
        _agent.SetDestination(_target.position);
    }

    private void CheckIfWaypointIsReached()
    {
        if (_isWaiting) return;
        if (_agent.pathPending) return;

        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.01f)
        {
            if (waitAtWaypoint)
            {
                _newWaitpoint = StartCoroutine(WaitBeforeNextWaypoint(Random.Range(waitDuration.x, waitDuration.y)));
            }
            else
            {
                SetNextWaypoint();
            }
        }
    }
    
    private IEnumerator WaitBeforeNextWaypoint(float duration)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(duration);
        random = Random.Range(0, 2);

        if (isSpinning)
        {
            SetNextWaypoint();
        }
        else
        {
            if (random == 0)
            {
                _isWaiting = false;
                SetNextWaypoint();
            }
            else
            {
                _isWaiting = true;
                DecideAttack();
              //  StopPatrol();
            }
        }
        
       
    }
    
    #endregion

    #region Animation

    private void UpdateAniamtor()
    { 
        animator.SetFloat(HashMovementValue, _agent.velocity.magnitude);
    }

    private void SetAnimationDirection(Vector2 direction)
    { 
        animator.SetFloat(HashDirX, direction.x); 
        animator.SetFloat(HashDirY, direction.y);
    }

    public void SetAnimationAction(int actionId)
    { 
        animator.SetTrigger(HashActionTrigger); 
        animator.SetInteger(HashActionID, actionId);
    }

    #endregion
    
    #region Attack

    
    public void DecideAttack()
    {
        StopPatrol();
        
        attackRandom =  Random.Range(0, 2); // 0, 3 für mit projectile aber nicht im game

        switch (attackRandom)
        {
            case 0:
                StopPatrol();
                _bossCrushAbility.HoverOver();
                Debug.Log("Boss Crush Ability");
                break;
            
            case 1:
                _bossSpinAbility.StartSpin();
                Debug.Log("Boss Spin Ability");
                isSpinning  = true;
                //SetSpinning(spinningWaypoints);
                SetAnimationAction(10);
                break;
            
            case 2:
                StopPatrol();
               // _crownAbility.Attack();
                Debug.Log("Crown Ability");
                break;
        }
    }

    public void ResumePatrolAfterAttack()
    {
        //animator.SetBool("crushOn", false);
        
        waypoints = oldWaypoints;
        waitDuration = oldWaitDuration;
        waitAtWaypoint = true;
        isSpinning = false;
        ResumePatrol();
        _isWaiting = false;
    }

    public void EnterAttackDistance()
    {
        _canAttack = true;
        _agent.isStopped = true;
    }

    public void ExitAttackDistance()
    {
        _canAttack = false;
        enemyState = EnemyState.Chasing;
        _agent.isStopped = false;
    }

    public void EndAttack()
    {
        _attackCooldownTimer = 0;
        enemyState = EnemyState.Chasing;
        _agent.isStopped = false;
    }
    
    #endregion
}