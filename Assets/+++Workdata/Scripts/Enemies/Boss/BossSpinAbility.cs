using System;
using System.Collections;
using UnityEngine;

public class BossSpinAbility : MonoBehaviour
{
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionID = Animator.StringToHash("ActionID");
    
    public bool start;
    public bool isSecondPhase = false;
    public Animator animator;
    public GameObject spinCollider;

    public float spintime;
    
    private BossPatrol _bossPatrol;

    private void Awake()
    {
        _bossPatrol = GetComponent<BossPatrol>();
    }
    
    public void StartSpin()
    {
        Debug.Log("StartSpin");
        start = true;
        StartCoroutine(SpinTimer());
    }

    public void ActivateSpinColliders()
    {
        spinCollider.SetActive(true);
    }

    private IEnumerator SpinTimer()
    {
        yield return new WaitForSeconds(spintime);
        start = false;
        Debug.Log("StopSpin");
        spinCollider.SetActive(false);
        _bossPatrol.ResumePatrolAfterAttack();
        animator.SetInteger(HashActionID, 11); 
    }

    public void SetSecondPhase()
    {
        isSecondPhase = true;
    }
    
}
