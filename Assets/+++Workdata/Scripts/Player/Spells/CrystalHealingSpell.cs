using System;
using System.Collections;
using UnityEngine;

public class CrystalHealingSpell : MonoBehaviour
{
    public static Action CrysHealingSpell;
    public static Action OnHealEnd;
    public static Action OnCooldownEnd;
    
    private bool _canHeal = true;
    
    private PlayerAnimation _playerAnimation;
    private PlayerInformation _playerInformation;
    private PlayerInput _playerInput;
    private ManaManager _manaManager;
    
    public GameObject crystalBallPrefab;
    private GameObject crystalBall;
    
    private Vector2 _spawnPosition;

    public int healAmount;

    public bool _isCooling = false;
    private bool _isActive = false;
    private bool _isHealing = false;

    public float maxHealTime;
    public int manaCost;

    public SpellDefinition spell;

    private float time;
    private float timeToNextHeal = 1f;

    private void Awake()
    {
        _playerAnimation =  GetComponent<PlayerAnimation>();
        _playerInformation = GetComponent<PlayerInformation>();
        _playerInput = GetComponent<PlayerInput>();
        _manaManager = GetComponent<ManaManager>();
    }

    private void OnEnable()
    {
        CrysHealingSpell += Cast;
        OnHealEnd += EndHeal;
        OnCooldownEnd += EndCooldown;
    }

    private void OnDisable()
    {
        CrysHealingSpell -= Cast;
        OnHealEnd -= EndHeal; 
        OnCooldownEnd -= EndCooldown;
    }

    private void Update()
    {
        if (_isHealing)
        {
            time += Time.deltaTime;
            if (time >= timeToNextHeal)
            {
                time = 0;
                _playerInformation.SetHealth(healAmount);
                Debug.Log("healed a bit");
            }
        }
    }

    private void Cast()
    {
        if (_isCooling) return;
        
        if (!_canHeal) return;
        
        if (!_manaManager.CheckIfSpellIsAllowed(manaCost)) return;
        
        _canHeal = false;
        
        _isActive = true;
        
        _playerAnimation.AnimationSetAction(50);
        _playerAnimation.AnimationSetBool("isCharging", true);
        
        HealOverTime();
        
        _playerInput.ToggleMovement(false);
        
        // TODO fix the target camera follow
    }

    private void HealOverTime()
    {
        _isHealing = true;
        StartCoroutine(StopHealing());
            
    }

    private IEnumerator StopHealing()
    {
        yield return new WaitForSeconds(maxHealTime);
        _isHealing = false;
        EndHeal();
    }
   

    public void SpawnSparkles()
    {
        if (!_isActive) return;
        
        _spawnPosition = transform.position;
        _spawnPosition.y = transform.position.y + 0.8f;
        
        crystalBall = Instantiate(crystalBallPrefab);
        crystalBall.transform.position = _spawnPosition;
    }

    private void EndHeal()
    {
        if (crystalBall == null) return;
        
        if (_canHeal) return;
        
        crystalBall.GetComponent<Animator>().SetTrigger("ActionTrigger");
        crystalBall.GetComponent<Animator>().SetInteger("ActionID", 100);
        
        _canHeal = true;
        
        _playerInput.ToggleMovement(true);
        
        _playerAnimation.AnimationSetBool("isCharging", false);

        _isCooling = true;
        SpellCooldownManager.OnStartCooldown?.Invoke(spell);
        
        _isActive = false;
    }

    private void EndCooldown()
    {
        _isCooling = false;
    }
}
