using System;
using UnityEngine;

public class BasicBubbleSpell : MonoBehaviour
{
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public static Action BaseBubbleSpell;
    public static Action OnActivateShield;
    public static Action KillBubble;
    public static Action OnCooledDown;
    
    public GameObject bubblePrefab;
    private GameObject bubble;

    public bool _bubbleOn = false;
    private bool _shieldOn = false;
    private bool _isCooling = false;
    private bool _isActive = false;
    
    private Animator _animator;
    
    private PlayerAnimation _playerAnimation;
    
    private Vector2 _spawnPosition;

    public SpellDefinition spell;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        BaseBubbleSpell += Cast;
        OnActivateShield += ActivateShield;
        KillBubble += BurstBubble;
        OnCooledDown += EndCooldown;
    }

    private void OnDisable()
    {
        BaseBubbleSpell -= Cast;
        OnActivateShield -= ActivateShield;
        KillBubble -= BurstBubble;
        OnCooledDown -= EndCooldown;
    }

    private void FixedUpdate()
    {
        if (_bubbleOn)
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = transform.position.y + 0.7f;
            
            bubble.transform.position = _spawnPosition;
        }
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
        
        _bubbleOn = true;
        
        _spawnPosition  = transform.position;
        _spawnPosition.y = transform.position.y + 0.7f;
        
        bubble =  Instantiate(bubblePrefab);
        bubble.transform.position = _spawnPosition;
        
        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
    }

    private void ActivateShield() // doenst get called yet
    {
        _shieldOn = true;
        PlayerInformation.ShieldOn?.Invoke("Basic", true);
        
    }

    public void BurstBubble()
    {
        _bubbleOn = false;
        _shieldOn = false;
        
        _isActive = false;
        
        PlayerInformation.ShieldOn?.Invoke("Basic", false);
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }
}
