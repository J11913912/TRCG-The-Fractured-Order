using System;
using UnityEngine;

public class BasicHealingSpell : MonoBehaviour
{
    public static Action BaseHealingSpell;
    public static Action OnHealEnd;
    public static Action OnCooldownEnd;
    
    private bool _canHeal = true;
    
    private PlayerAnimation _playerAnimation;
    private PlayerInformation _playerInformation;
    private PlayerInput _playerInput;
    
    public GameObject sparklesPrefab;
    private GameObject sparkles;
    
    private Vector2 _spawnPosition;

    public int healAmount;

    private bool _isCooling = false;
    private bool _isActive = false;

    public SpellDefinition spell;

    private void Awake()
    {
        _playerAnimation =  GetComponent<PlayerAnimation>();
        _playerInformation = GetComponent<PlayerInformation>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        BaseHealingSpell += Cast;
        OnHealEnd += EndHeal;
        OnCooldownEnd += EndCooldown;
    }

    private void OnDisable()
    {
        BaseHealingSpell -= Cast;
        OnHealEnd -= EndHeal; 
        OnCooldownEnd -= EndCooldown;
    }
    
    private void FixedUpdate()
    {
        if (!_canHeal)
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = transform.position.y + 0.7f;
            
            if (sparkles == null) return;
            
            sparkles.transform.position = _spawnPosition;
        }
    }

    private void Cast()
    {
        if (_isCooling) return;
        
        if (!_canHeal) return;
        
        _isActive = true;
        
        _canHeal = false;
        
        _playerAnimation.AnimationSetAction(40);
        _playerInput.ToggleMovement(false);
    }

    public void SpawnSparkles()
    {
        if (!_isActive) return;
        
        _spawnPosition = transform.position;
        _spawnPosition.y = transform.position.y + 0.56f;
        
        sparkles = Instantiate(sparklesPrefab);
        sparkles.transform.position = _spawnPosition;
    }

    private void EndHeal()
    {
        _canHeal = true;
        
        _playerInformation.SetHealth(healAmount);
        
        Destroy(sparkles);
        
        _playerInput.ToggleMovement(true);
        
        _isActive = false;

        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }
}
