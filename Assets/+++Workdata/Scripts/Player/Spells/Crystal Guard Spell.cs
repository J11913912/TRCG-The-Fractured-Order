using System;
using UnityEngine;

public class CrystalGuardSpell : MonoBehaviour
{
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public static Action CrysGuardSpell;
    public static Action OnActivateShield;
    public static Action KillShield;
    public static Action OnCooledDown;
    
    public GameObject shieldPrefab;
    private GameObject _shield;

    public bool _bubbleOn = false;
    public bool _shieldOn = false;
    public bool _isCooling = false;
    public bool _isActive = false;
    
    private Animator _animator;
    
    private PlayerAnimation _playerAnimation;
    private PlayerInput _playerInput;
    
    private PlayerDirection _playerDirection;
    private PlayerStates _playerState;
    private Vector2 _direction;
    
    public float ySpawnOffset;
    
    private Vector2 _spawnPosition;

    public SpellDefinition spell;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerInput = GetComponent<PlayerInput>();
        _playerState =  GetComponent<PlayerStates>();
    }

    private void OnEnable()
    {
        CrysGuardSpell += Cast;
        OnActivateShield += ActivateShield;
        KillShield += BurstBubble;
        OnCooledDown += EndCooldown;
    }

    private void OnDisable()
    {
        CrysGuardSpell -= Cast;
        OnActivateShield -= ActivateShield;
        KillShield -= BurstBubble;
        OnCooledDown -= EndCooldown;
    }

    private void Cast()
    { 
        if (_bubbleOn) return;
        
        if (_isCooling) return;
        
        _isActive = true;
        
        Debug.Log("Bubble spell casting");
        
        _playerAnimation.AnimationSetAction(30);
    }

    public void SpawnBubble()
    {
        if (!_isActive) return;
        
        Debug.Log("Bubble spawning");
        
        _bubbleOn = true;
        
        _spawnPosition  = transform.position;
        _spawnPosition.y = transform.position.y + 0.7f;
        
        _playerDirection = _playerState.GetPlayerDirection();
        
        if (_playerDirection == PlayerDirection.Left)
        {
            _spawnPosition = new Vector2(transform.position.x - 1.5f, transform.position.y);
            _direction = Vector2.left;
        }
        else if (_playerDirection == PlayerDirection.Right)
        {
            _spawnPosition = new Vector2(transform.position.x + 1.5f, transform.position.y);
            _direction = Vector2.right;
        }
        else if (_playerDirection == PlayerDirection.Up)
        {
            _spawnPosition = new Vector2(transform.position.x, transform.position.y + 1.5f);
            _direction = Vector2.up;
        }
        else if (_playerDirection == PlayerDirection.Down)
        {
            _spawnPosition = new Vector2(transform.position.x, transform.position.y - 1.5f);
            _direction = Vector2.down;
        }
        
        _shield =  Instantiate(shieldPrefab);
        _shield.transform.position = _spawnPosition;
        
        _shield.GetComponent<CrystalGuardBehaviour>().Shoot(_direction);
        
        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
    }

    private void ActivateShield() // doenst get called yet
    {
        _shieldOn = true;
        PlayerInformation.ShieldOn?.Invoke("Crystal", true);
        
    }

    public void BurstBubble()
    {
        _bubbleOn = false;
        _shieldOn = false;
        
        PlayerInformation.ShieldOn?.Invoke("Crystal", false);
        
        _isActive = false;
    }

    private void EndCooldown()
    {
        _isCooling = false;
        
        _bubbleOn = false;
        _shieldOn = false;
        
        _isActive = false;
    }
}
