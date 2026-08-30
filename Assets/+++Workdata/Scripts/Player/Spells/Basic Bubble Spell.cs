using System;
using UnityEngine;
using FMODUnity;

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

    private bool _bubbleOn = false;
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

    private void Cast()                                                                                                 // on input
    { 
        if (_bubbleOn) return;
        
        if (_isCooling) return;
        
        _isActive = true;
        
        _playerAnimation.AnimationSetAction(30);
    }

    public void SpawnBubble()                                                                                           // triggered via animation event in attack animation
    {
        if (!_isActive) return;
        
        _bubbleOn = true;
        
        _spawnPosition  = transform.position;
        _spawnPosition.y = transform.position.y + 0.7f;
        
        bubble =  Instantiate(bubblePrefab);
        bubble.transform.position = _spawnPosition;
        
        RuntimeManager.PlayOneShot("event:/Player/Standard/Shield Activation");
        
        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
    }

    private void ActivateShield() // doenst get called yet
    {
        _shieldOn = true;
        PlayerInformation.ShieldOn?.Invoke("Basic", true);
        
    }

    public void BurstBubble()                                                                                           // triggered when bubble is touched
    {
        _bubbleOn = false;
        _shieldOn = false;
        
        _isActive = false;
        
        PlayerInformation.ShieldOn?.Invoke("Basic", false);
        RuntimeManager.PlayOneShot("event:/Player/Standard/Shield Pop");
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }
}
