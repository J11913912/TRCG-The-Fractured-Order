using System;
using UnityEngine;

public class BasicProjectileSpell : MonoBehaviour
{
    public static Action BaseProjectileSpell;
    public static Action OnAttackEnd;
    public GameObject projectilePrefab;
    
    private PlayerDirection _playerDirection;
    
    private PlayerStates _playerState;
    private PlayerAnimation _playerAnimation;
    private BaseProjectileBehaviour _projectileBehaviour;
    
    private Vector2 _spawnPosition;
    private Vector2 _direction;

    private bool _canAttack = true;
    private bool _currentlyActive = false;

    public float ySpawnOffset;

    public SpellDefinition spell;

    // TODO charging
    // TODO cooldown
    
    private void Awake()
    {
        _playerState =  GetComponent<PlayerStates>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        BaseProjectileSpell += Cast;
        
        OnAttackEnd += EndAttack;
    }

    private void OnDisable()
    {
        BaseProjectileSpell -= Cast;
        
        OnAttackEnd -= EndAttack;
    }

    private void Cast()
    {
        _currentlyActive = true;
        
        if (!_canAttack) return;

        SpellCooldownManager.OnStartCooldown(spell);
        
        _canAttack = false;
        
        _playerAnimation.AnimationSetAction(10);
    }

    public void Attack()
    {
        if (!_currentlyActive) return;
        
        _playerDirection = _playerState.GetPlayerDirection();
        
        if (_playerDirection == PlayerDirection.Left)
        {
            _spawnPosition = new Vector2(transform.position.x - 1, transform.position.y + ySpawnOffset);
            _direction = Vector2.left;
        }
        else if (_playerDirection == PlayerDirection.Right)
        {
            _spawnPosition = new Vector2(transform.position.x + 1, transform.position.y + ySpawnOffset);
            _direction = Vector2.right;
        }
        else if (_playerDirection == PlayerDirection.Up)
        {
            _spawnPosition = new Vector2(transform.position.x, transform.position.y + 1 + ySpawnOffset);
            _direction = Vector2.up;
        }
        else if (_playerDirection == PlayerDirection.Down)
        {
            _spawnPosition = new Vector2(transform.position.x, transform.position.y - 1 + ySpawnOffset);
            _direction = Vector2.down;
        }
        
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = _spawnPosition;
        
        _projectileBehaviour = projectile.GetComponent<BaseProjectileBehaviour>();
        
        _projectileBehaviour.Shoot(_direction);
    }

    public void EndAttack()
    {
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
    }
}
