using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class BasicAoESpell : MonoBehaviour
{
    public static Action<bool> OtherSpellActive;
    
    public static Action BaseAoESpell;
    public static Action OnAttack;
    public static Action OnAttackEnd;
    public static Action OnAttackCancel;
    public static Action OnCooldownEnd;
    public GameObject projectilePrefab;
    
    private PlayerDirection _playerDirection;
    
    private PlayerStates _playerState;
    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    private BaseProjectileBehaviour _projectileBehaviour;
    
    private Vector2 _spawnPosition;
    private Vector2 _direction;

    private bool _canAttack = true;
    private bool _currentlyActive = false;

    public float ySpawnOffset;

    public SpellDefinition spell;

    private bool _isCooling = false;
    
    public UnityEvent ChargeStart;
    public UnityEvent ChargeStop;
    
    private bool _otherSpellActive = false;
    
    private void Awake()
    {
        _playerState =  GetComponent<PlayerStates>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        BaseAoESpell += Cast;

        OnAttack += Attack;
        
        OnAttackEnd += EndAttack;

        OnAttackCancel += CancelAttack;

        OnCooldownEnd += EndCooldown;

        OtherSpellActive += SetOtherSpellActive;
    }

    private void OnDisable()
    {
        BaseAoESpell -= Cast;

        OnAttack -= Attack;
        
        OnAttackEnd -= EndAttack;

        OnAttackCancel -= CancelAttack;
        
        OnCooldownEnd -= EndCooldown;
        
        OtherSpellActive -= SetOtherSpellActive;
    }

    private void SetOtherSpellActive(bool value)
    {
        _otherSpellActive = value;
    }

    private void Cast()                                                                                                 // on Input
    {
        if (_otherSpellActive) return;
        
        if (_isCooling) return;
        
        if (!_canAttack) return;
        
        BasicProjectileSpell.OtherSpellActive?.Invoke(true);
        BasicBubbleSpell.OtherSpellActive?.Invoke(true);
        CrystalGuardSpell.OtherSpellActive?.Invoke(true);
        CrystalAoESpell.OtherSpellActive?.Invoke(true);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(true);
        CrystalHealingSpell.OtherSpellActive?.Invoke(true);
        BasicHealingSpell.OtherSpellActive?.Invoke(true);
        
        _currentlyActive = true;

        _playerInput.ToggleMovement(false);
        
        _playerAnimation.AnimationSetAction(20);
        _playerAnimation.AnimationSetBool("isCharging", true);                                                          // hold key to start charge (via interaction hold in inputmap)
        ChargeStart?.Invoke();                                                                                       
        
        _canAttack = false;
    }

    private void Attack()                                                                                               // when held long enough
    {
        if (!_currentlyActive) return;
        
        _playerAnimation.AnimationSetBool("isCharging", false);
    }

    public void Attack2()                                                                                               // triggered via Animationevent in attack animation
    {
        if (!_currentlyActive) return;
        
        _isCooling = true;
        SpellCooldownManager.OnStartCooldown(spell);
        
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
        
        RuntimeManager.PlayOneShot("event:/Player/Standard/Orb Release");
        
        _projectileBehaviour = projectile.GetComponent<BaseProjectileBehaviour>();
        
        _projectileBehaviour.Shoot(_direction);
        
        _playerInput.ToggleMovement(true);
        
        ChargeStop?.Invoke();
    }

    public void EndAttack()                                                                                             // after attack aniamtion
    {
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
        
        UnFreezeSpells();
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }

    private void CancelAttack()                                                                                         // cancel attack before charged up
    {
        ChargeStop?.Invoke();
        
        Debug.Log("CANCEL");
        
        _playerAnimation.AnimationSetBool("secondPress", true);

        StartCoroutine(ResetCharge());
        
        _playerInput.ToggleMovement(true);
        
        if (!_currentlyActive) return;
        
        _currentlyActive = false;
        _canAttack = true;
        
        UnFreezeSpells();
        
    }

    private IEnumerator ResetCharge()
    {
        yield return new WaitForSeconds(0.5f);
        _playerAnimation.AnimationSetBool("isCharging", false);
        _playerAnimation.AnimationSetBool("secondPress", false);

    }

    private void UnFreezeSpells()
    {
        BasicProjectileSpell.OtherSpellActive?.Invoke(false);
        BasicBubbleSpell.OtherSpellActive?.Invoke(false);
        CrystalGuardSpell.OtherSpellActive?.Invoke(false);
        CrystalAoESpell.OtherSpellActive?.Invoke(false);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(false);
        CrystalHealingSpell.OtherSpellActive?.Invoke(false);
        BasicHealingSpell.OtherSpellActive?.Invoke(false);
    }
}
