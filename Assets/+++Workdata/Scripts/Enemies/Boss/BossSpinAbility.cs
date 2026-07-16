using System;
using System.Collections;
using UnityEngine;

public class BossSpinAbility : MonoBehaviour
{
    public bool start;

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
        // start animation
        StartCoroutine(SpinTimer());
    }

    private IEnumerator SpinTimer()
    {
        yield return new WaitForSeconds(spintime);
        start = false;
        Debug.Log("StopSpin");
        _bossPatrol.ResumePatrolAfterAttack();
        //stop animation
    }
    
}
