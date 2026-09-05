using System;
using System.Diagnostics;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
using UnityEngine.Events;

public class CrystalAoESpell : MonoBehaviour
{
    public static Action<bool> OtherSpellActive;

    public static Action CrystalAoE;
    public static Action OnCooldownEnd;

    public GameObject targetPrefab;
    public GameObject attackPillarPrefab;
    
    private GameObject _target;
    private GameObject _pillar;

    private bool _castStarted = false;
    private bool _casting = false;
    private bool _canCast = true;
    
    private PlayerStates _playerState;
    private PlayerInput _playerInput;
    private PlayerAnimation _playerAnimation;
    private ManaManager _manaManager;
    
    private PlayerDirection _playerDirection;
    private Vector2 _direction;
    private Vector2 _spawnPosition;

    private float _time;
    public float deathTime;
    
    public int manaCost;

    private bool _isCooling = false;
    private bool _isActive = false;

    public SpellDefinition spell;
    
    public CinemachineCamera playerCamera;

    public float howFarFromPlayer;
    
    public UnityEvent ChargeStart;
    public UnityEvent ChargeStop;
    
    private bool _otherSpellActive = false;

    // TODO player has to really charge like with basic aoe??????
    
    // TODO  find better solution to keep target in camera????
    
    private void Awake()
    {
        _playerState = GetComponent<PlayerStates>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _manaManager =  GetComponent<ManaManager>();
    }
    
    private void OnEnable()
    {
        CrystalAoE += Cast;
        OnCooldownEnd += EndCooldown;
        OtherSpellActive += SetOtherSpellActive;
    }

    private void OnDisable()
    {
        CrystalAoE -= Cast;
        OnCooldownEnd -= EndCooldown;
        OtherSpellActive -= SetOtherSpellActive;
    }

    private void SetOtherSpellActive(bool value)
    {
        _otherSpellActive = value;
    }

    private void Update()
    {
        if (_target == null) return;
        
        if (_casting)
        {
            TargetBehaviour targetBehaviour= _target.GetComponent<TargetBehaviour>();

            howFarFromPlayer = Vector2.Distance(gameObject.transform.position, _target.transform.position);

            if (howFarFromPlayer >= 12f)                                                                                // target movement gets painfully slow when too far away
            {
               targetBehaviour.ToggleMoveSpeed(false);
            }

            if (howFarFromPlayer < 12f)
            {
                targetBehaviour.ToggleMoveSpeed(true);
            }
            
            _time += Time.deltaTime;

            if (_time >= deathTime)
            {
                Attack();
                _time = 0;
            }
        }
    }

    private void Cast()                                                                                                 // on input
    {
        if (_otherSpellActive) return;
        
        if (_isCooling) return;
        
        if (!_manaManager.CheckIfSpellIsAllowed(manaCost)) return;                                                      // only if enough mana
        
        _isActive = true;
        
        BasicAoESpell.OtherSpellActive?.Invoke(true);
        BasicBubbleSpell.OtherSpellActive?.Invoke(true);
        CrystalGuardSpell.OtherSpellActive?.Invoke(true);
        BasicProjectileSpell.OtherSpellActive?.Invoke(true);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(true);
        CrystalHealingSpell.OtherSpellActive?.Invoke(true);
        BasicHealingSpell.OtherSpellActive?.Invoke(true);
        
        if (!_castStarted && !_casting && _canCast)
        {
            if (_pillar != null)
            {
                _pillar.GetComponent<CrystalPillarBehaviour>().SetAction(100);
            }
            
            _playerAnimation.AnimationSetAction(20);
            _playerAnimation.AnimationSetBool("isCharging", true); 
            ChargeStart?.Invoke();                                                                                  
            
            _casting = true;
            _castStarted = true;
            _canCast = false;
            
            _playerInput.ToggleMovement(false);
            
            _playerDirection = _playerState.GetPlayerDirection();
        
            if (_playerDirection == PlayerDirection.Left)                                                               // target spawn position
            {
                _spawnPosition = new Vector2(transform.position.x - 1, transform.position.y);
                _direction = Vector2.left;
            }
            else if (_playerDirection == PlayerDirection.Right)
            {
                _spawnPosition = new Vector2(transform.position.x + 1, transform.position.y);
                _direction = Vector2.right;
            }
            else if (_playerDirection == PlayerDirection.Up)
            {
                _spawnPosition = new Vector2(transform.position.x, transform.position.y + 1);
                _direction = Vector2.up;
            }
            else if (_playerDirection == PlayerDirection.Down)
            {
                _spawnPosition = new Vector2(transform.position.x, transform.position.y - 1);
                _direction = Vector2.down;
            }

            _target = Instantiate(targetPrefab);
            _target.transform.position = _spawnPosition;
            
            playerCamera.Follow = _target.transform;                                                                    // camera follows target
        }
        else
        {
            Attack();                                                                                                   // on second inpuz
        }
    }

    private void Attack()
    {
        _castStarted = false;
        _casting = false;
        
        _playerInput.ToggleMovement(false);
        
        _playerAnimation.AnimationSetBool("isCharging", false);
    }

    public void Attack2()                                                                                               // triggered via animations event
    {
        if (!_isActive) return;
        
        ChargeStop?.Invoke();                                                                                       
        
        _pillar = Instantiate(attackPillarPrefab);                                                                      // pillar damage
        _pillar.transform.position = _target.transform.position;
        
        playerCamera.Follow = gameObject.transform;                                                                     // camera back to player
        
        Destroy(_target);
        
        _canCast = true;
            
        _playerInput.ToggleMovement(true);
        
        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
        
        _isActive = false;
        _playerInput.ToggleMovement(true);
        
        BasicAoESpell.OtherSpellActive?.Invoke(false);
        BasicBubbleSpell.OtherSpellActive?.Invoke(false);
        CrystalGuardSpell.OtherSpellActive?.Invoke(false);
        BasicProjectileSpell.OtherSpellActive?.Invoke(false);
        CrystalProjectileSpell.OtherSpellActive?.Invoke(false);
        CrystalHealingSpell.OtherSpellActive?.Invoke(false);
        BasicHealingSpell.OtherSpellActive?.Invoke(false);
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }
}
