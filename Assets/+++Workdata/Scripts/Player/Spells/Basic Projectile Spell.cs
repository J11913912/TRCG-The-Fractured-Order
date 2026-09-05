using System;
using UnityEngine;
using FMODUnity;

public class BasicProjectileSpell : MonoBehaviour
{ 
    public static Action<bool> OtherSpellActive;
    public static Action BaseProjectileSpell;
    public static Action OnAttackEnd;
    public GameObject projectilePrefab;
    
    private PlayerDirection _playerDirection;
    
    private PlayerStates _playerState;
    private PlayerAnimation _playerAnimation;
    private BaseProjectileBehaviour _projectileBehaviour;
    
    private Vector2 _spawnPosition;
    private Vector2 _direction;

    public bool _canAttack = true;
    public bool _currentlyActive = false;

    public float ySpawnOffset;

    public SpellDefinition spell;
    
    public bool _otherSpellActive = false;

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
        
        OtherSpellActive += SetOtherSpellActive;
    }

    private void OnDisable()
    {
        BaseProjectileSpell -= Cast;
        
        OnAttackEnd -= EndAttack;
        
        OtherSpellActive -= SetOtherSpellActive;
    }

    private void SetOtherSpellActive(bool value)
    {
        _otherSpellActive = value;
    }

    private void Cast()                                                                                                 // on Input
    {
        if (_otherSpellActive) return;
        
        if (!_canAttack) return;
        
        BasicAoESpell.OtherSpellActive?.Invoke(true);
        BasicBubbleSpell.OtherSpellActive?.Invoke(true);
        CrystalGuardSpell.OtherSpellActive?.Invoke(true);
        CrystalAoESpell.OtherSpellActive?.Invoke(true);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(true);
        CrystalHealingSpell.OtherSpellActive?.Invoke(true);
        BasicHealingSpell.OtherSpellActive?.Invoke(true);
        
        _currentlyActive = true;
        
      // SpellCooldownManager.OnStartCooldown(spell);
        
        _canAttack = false;
        
        _playerAnimation.AnimationSetAction(10);
    }

    public void Attack()                                                                                                // triggered via Animationevent in attack animation
    {
        if (!_currentlyActive) return;
        
        _playerDirection = _playerState.GetPlayerDirection();
        
        if (_playerDirection == PlayerDirection.Left)                                                                   // spawn in position and direction according to playerDirection
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
        
        RuntimeManager.PlayOneShot("event:/Player/Standard/Dart Release");
        
        _projectileBehaviour = projectile.GetComponent<BaseProjectileBehaviour>();
        
        _projectileBehaviour.Shoot(_direction);
    }

    public void EndAttack()                                                                                             // triggered after attack animation ends
    { 
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
        
        BasicAoESpell.OtherSpellActive?.Invoke(false);
        BasicBubbleSpell.OtherSpellActive?.Invoke(false);
        CrystalGuardSpell.OtherSpellActive?.Invoke(false);
        CrystalAoESpell.OtherSpellActive?.Invoke(false);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(false);
        CrystalHealingSpell.OtherSpellActive?.Invoke(false);
        BasicHealingSpell.OtherSpellActive?.Invoke(false);
    }
}
