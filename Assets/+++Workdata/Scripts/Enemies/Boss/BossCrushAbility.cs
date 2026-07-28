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
            Vector3 direction = _target.position - transform.position;
        
            _rb.linearVelocity = direction * moveSpeed;
        }
    }

    public void HoverOver()
    {
        _target = _player.transform;
        
        crush = true;
       
    }

    public void StopHover()
    {
        if (!crush) return;
        
        Debug.Log("CrushOff");
        crush = false;
        _rb.linearVelocity = Vector2.zero;

        Crush();
    }

    public void Crush()
    {
        // animation
        // if second phase also shockwave
        // damage player
        
        _bossPatrol.ResumePatrolAfterAttack();
    }

    public void Shockwave()
    {
        
    }

    public void SetSecondPhase()
    {
        isSecondPhase = true;
    }
}
