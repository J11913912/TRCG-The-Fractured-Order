using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMeshPatrol : MonoBehaviour
{
    private int HashMovementValue = Animator.StringToHash("MovementValue");
    private int HashDirX = Animator.StringToHash("dirX");
    private int HashDirY = Animator.StringToHash("dirY");
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionId = Animator.StringToHash("ActionId");
    
    
    #region Inspector
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
    
    [Header("Waypoints")] 
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private bool waitAtWaypoint = true;
    [SerializeField] private bool randomOrder;
    [SerializeField] private bool canPatrol = true;
    [SerializeField] private Vector2 waitDuration = new Vector2(1, 5);

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

    #endregion
    
    #region Unity Event Functions

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindFirstObjectByType<PlayerController>().transform;
        _agent.autoBraking = waitAtWaypoint;
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        SetNextWaypoint();
    }

    private void Update()
    {
        if (_canAttack && enemyState != EnemyState.Attacking)
        {
            _attackCooldownTimer += Time.deltaTime;
            if (_attackCooldownTimer > attackCooldown)
            {
                enemyState = EnemyState.Attacking;
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
                _agent.ResetPath();
            }
        }
    }


    private void LateUpdate()
    {
        UpdateFacing();
        //UpdateAniamtor();
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
        //SetAnimationDirection(new Vector2(dir.x, dir.y));
        /*
        switch (enemyFacingDirection)
        {
            case EnemyFacingDirection.Up:

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
        }*/
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
        _isWaiting = false;
        SetNextWaypoint();
    }
    
    #endregion

    #region Animation

    private void UpdateAniamtor()
    {
     //   animator.SetFloat(HashMovementValue, _agent.velocity.magnitude);
    }

    private void SetAnimationDirection(Vector2 direction)
    {
     //   animator.SetFloat(HashDirX, direction.x);
      //  animator.SetFloat(HashDirY, direction.y);
    }

    private void SetAnimationAction(int actionId)
    {
       // animator.SetTrigger(HashActionTrigger);
       // animator.SetInteger(HashActionId, actionId);
    }

    #endregion

    #region Aggro

    public void EnterAggroDistance()
    {
        enemyState = EnemyState.Chasing;
        _target = _player;
        if(_aggroCoroutine != null)
            StopCoroutine(_aggroCoroutine);
        
        if(_newWaitpoint != null)
        {
            _isWaiting = false;
            StopCoroutine(_newWaitpoint);
        }
    }

    public void ExitAggroDistance()
    {
        enemyState = EnemyState.Chasing;
        _aggroCoroutine = StartCoroutine(InitiateAggroTimer());
    }

    IEnumerator InitiateAggroTimer()
    {
        yield return new WaitForSeconds(stopChasingTimer);
        if (!_isAggroed)
        {
            enemyState = EnemyState.Idle;
            SetNextWaypoint();
            //TODO: Heal to max ?!
        }
    }

    #endregion
    
    
    #region Attack

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