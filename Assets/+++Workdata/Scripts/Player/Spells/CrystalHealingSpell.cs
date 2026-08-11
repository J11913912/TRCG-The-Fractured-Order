using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public float timeToNextHeal;

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
                healAmount = Random.Range(3, 10);
                
                time = 0;
                _playerInformation.SetHealth(healAmount);
                crystalBall.GetComponent<CrystalBallBehaviour>().ChangeColor();
                Debug.Log("healed a bit");
            }
        }
    }
    
    // TODO destroy ball on cancel and everywhere          DONE
    // TODO sprite setter for on heal                      DONE BUT UGLY
    // TODO fix spawn timing projectile and aoe            DONE
    // TODO make bubble appear longer                      DONE
    // TODO random healing amout crystal healing           DONE
    // TODO add back hand shoot for crystal projectile     DONE But adjust spamming?
    // TODO destroy crystal walls                         DONE
    // TODO adjust crystal walls collision
    // TODO make non unlocked spells somehow disappear in spell menu (delete them completely, make them invisble but keep the naivagtion running)       DONE but needs logic to set unokicng spells while running
    // TODO start on health bar

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
