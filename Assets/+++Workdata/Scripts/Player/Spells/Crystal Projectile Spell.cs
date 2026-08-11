using System;
using UnityEngine;

public class CrystalProjectileSpell : MonoBehaviour
{
    public static Action CrysProjectileSpell;
    public static Action OnAttackEnd;
    public GameObject projectilePrefab;
    
    private PlayerDirection _playerDirection;
    
    private PlayerStates _playerState;
    private PlayerAnimation _playerAnimation;
    private BaseProjectileBehaviour _projectileBehaviour;
    private ManaManager _manaManager;
    
    private Vector2 _spawnPosition;
    private Vector2 _direction;
    
    public int manaCost;
    
    public float ySpawnOffset;
    public bool _canAttack = true;

    private bool _secondAttack = false;
    
    private bool _currentlyActive = false;

    // TODO charging
    
    private void Awake()
    {
        _playerState =  GetComponent<PlayerStates>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _manaManager = GetComponent<ManaManager>();
    }

    private void OnEnable()
    {
        CrysProjectileSpell += Cast;
        OnAttackEnd += EndAttack;
    }

    private void OnDisable()
    {
        CrysProjectileSpell -= Cast;
        OnAttackEnd += EndAttack;
    }

    private void Cast()
    {
        _currentlyActive = true;
        
        if (!_canAttack) return;
        
        if (!_manaManager.CheckIfSpellIsAllowed(manaCost)) return;
        
        _canAttack  = false;

        if (_secondAttack)
        {
            _secondAttack = false;
            _playerAnimation.AnimationSetAction(10);
            _playerAnimation.AnimationSetBool("secondPress", true);
        }
        else
        {
            _secondAttack = true;
            _playerAnimation.AnimationSetAction(10);
            _playerAnimation.AnimationSetBool("secondPress", false);
        }
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

    private void EndAttack()
    {
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
    }
}
