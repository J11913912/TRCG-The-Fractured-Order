using System;
using UnityEngine;
using FMODUnity;

public class CrystalProjectileSpell : MonoBehaviour
{
    public static Action<bool> OtherSpellActive;
    
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
    private bool _canAttack = true;

    private bool _secondAttack = false;
    
    private bool _currentlyActive = false;
    
    private bool _otherSpellActive = false;

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
        OtherSpellActive += SetOtherSpellActive;
    }

    private void OnDisable()
    {
        CrysProjectileSpell -= Cast;
        OnAttackEnd += EndAttack;
        OtherSpellActive -= SetOtherSpellActive;
    }

    private void SetOtherSpellActive(bool value)
    {
        _otherSpellActive = value;
    }

    private void Cast()                                                                                                 // on input
    {
        if (_otherSpellActive) return;
        
        if (!_canAttack) return;
        
        if (!_manaManager.CheckIfSpellIsAllowed(manaCost)) return;
        
        _currentlyActive = true;
        
        BasicAoESpell.OtherSpellActive?.Invoke(true);
        BasicBubbleSpell.OtherSpellActive?.Invoke(true);
        CrystalGuardSpell.OtherSpellActive?.Invoke(true);
        CrystalAoESpell.OtherSpellActive?.Invoke(true);
        BasicProjectileSpell.OtherSpellActive?.Invoke(true);
        CrystalHealingSpell.OtherSpellActive?.Invoke(true);
        BasicHealingSpell.OtherSpellActive?.Invoke(true);
        
        _canAttack  = false;

        if (_secondAttack)                                                                                              // switch between two animations
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

    public void Attack()                                                                                                // triggered via animation event in attack animation
    {
        if (!_currentlyActive) return;
        
        _playerDirection = _playerState.GetPlayerDirection();
        
        if (_playerDirection == PlayerDirection.Left)                                                                   // spawn position and direction
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
        
        RuntimeManager.PlayOneShot("event:/Player/Crystal/Crystal Dart Cast");
        
        _projectileBehaviour = projectile.GetComponent<BaseProjectileBehaviour>();
        
        _projectileBehaviour.Shoot(_direction);
    }

    private void EndAttack()                                                                                            // after end of attack animation
    {
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
        
        BasicAoESpell.OtherSpellActive?.Invoke(false);
        BasicBubbleSpell.OtherSpellActive?.Invoke(false);
        CrystalGuardSpell.OtherSpellActive?.Invoke(false);
        CrystalAoESpell.OtherSpellActive?.Invoke(false);
        BasicProjectileSpell.OtherSpellActive?.Invoke(false);
        CrystalHealingSpell.OtherSpellActive?.Invoke(false);
        BasicHealingSpell.OtherSpellActive?.Invoke(false);
    }
}
