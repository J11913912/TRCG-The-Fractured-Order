using System;
using UnityEngine;

public class BasicHealingSpell : MonoBehaviour
{
    public static Action BaseHealingSpell;
    public static Action OnHealEnd;
    
    private bool _canHeal = true;
    
    private PlayerAnimation _playerAnimation;
    private PlayerInformation _playerInformation;
    
    public GameObject sparklesPrefab;
    private GameObject sparkles;
    
    private Vector2 _spawnPosition;

    public int healAmount;

    private void Awake()
    {
        _playerAnimation =  GetComponent<PlayerAnimation>();
        _playerInformation = GetComponent<PlayerInformation>();
    }

    private void OnEnable()
    {
        BaseHealingSpell += Cast;
        OnHealEnd += EndHeal;
    }

    private void OnDisable()
    {
        BaseHealingSpell -= Cast;
        OnHealEnd -= EndHeal; 
    }

    private void Cast()
    {
        if (!_canHeal) return;
        
        _canHeal = false;
        
        _playerAnimation.AnimationSetAction(40);
    }

    public void SpawnSparkles()
    {
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
    }
}
