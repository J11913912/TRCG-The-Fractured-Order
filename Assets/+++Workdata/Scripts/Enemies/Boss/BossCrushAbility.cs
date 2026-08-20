using System;
using UnityEngine;

public class BossCrushAbility : MonoBehaviour
{
    private Transform _target;
    
    private GameObject _player;
    
    private Rigidbody2D _rb;

    public bool start = false;

    public float moveSpeed;

    public bool crush = false;
    
    public bool isSecondPhase = false;
    
    private BossPatrol _bossPatrol;
    
    public Animator animator;

    public GameObject dummyPrefab;
    private GameObject _dummy;

    
    private void Awake()
    {
        _bossPatrol = GetComponent<BossPatrol>();
        
        _rb = GetComponent<Rigidbody2D>(); 
        
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (start && !crush)
        {
            start = false;

            HoverOver();
        }

        if (crush)
        {
            Vector3 direction = _dummy.transform.position - transform.position;
        
            _rb.linearVelocity = direction * moveSpeed;
        }
    }

    public void HoverOver()
    {
        _target = _player.transform;

        _dummy = Instantiate(dummyPrefab);
        _dummy.transform.position = _target.position;
        
        crush = true;
        
        _bossPatrol.SetAnimationAction(20);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDummy"))
        {
            Debug.Log("dummy stop");
            StopHover();
        }
    }

    private void StopHover()
    {
        if (!crush) return;
        
        _bossPatrol.SetAnimationAction(30);
        
        Debug.Log("CrushOff");
        crush = false;
        _rb.linearVelocity = Vector2.zero;

        //Crush();
    }

    public void Crush() // via animation event in slam_ease
    {
        _bossPatrol.ResumePatrolAfterAttack();
        Destroy(_dummy);
    }

    public void Shockwave()
    {
        
    }

    public void SetSecondPhase()
    {
        isSecondPhase = true;
    }
}
