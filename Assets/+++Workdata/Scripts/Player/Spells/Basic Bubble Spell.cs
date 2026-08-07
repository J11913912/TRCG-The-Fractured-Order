using System;
using UnityEngine;

public class BasicBubbleSpell : MonoBehaviour
{
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public static Action BaseBubbleSpell;
    public static Action OnActivateShield;
    public static Action KillBubble;
    
    public GameObject bubblePrefab;
    private GameObject bubble;

    public bool _bubbleOn = false;
    private bool _shieldOn = false;
    
    private Animator _animator;
    
    private PlayerAnimation _playerAnimation;
    
    private Vector2 _spawnPosition;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        BaseBubbleSpell += Cast;
        OnActivateShield += ActivateShield;
        KillBubble += BurstBubble;
    }

    private void OnDisable()
    {
        BaseBubbleSpell -= Cast;
        OnActivateShield -= ActivateShield;
        KillBubble -= BurstBubble;
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
        
        Debug.Log("Bubble spell casting");
        
        _playerAnimation.AnimationSetAction(30);
    }

    public void SpawnBubble()
    {
        _bubbleOn = true;
        
        _spawnPosition  = transform.position;
        _spawnPosition.y = transform.position.y + 0.7f;
        
        bubble =  Instantiate(bubblePrefab);
        bubble.transform.position = _spawnPosition;
    }

    private void ActivateShield()
    {
        _shieldOn = true;
        PlayerInformation.ShieldOn?.Invoke("Basic", true);
    }

    public void BurstBubble()
    {
        _bubbleOn = false;
        _shieldOn = false;
        
        PlayerInformation.ShieldOn?.Invoke("Basic", false);
    }
}
