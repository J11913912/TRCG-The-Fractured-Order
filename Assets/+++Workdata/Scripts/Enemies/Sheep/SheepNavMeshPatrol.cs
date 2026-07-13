using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshAgent))]
public class SheepNavMeshPatrol : EnemyNavMeshPatrolVererbung
{
    public SheepBehaviour sheepBehaviour;
    
   

    private IEnumerator MiniChase()
    {
        yield return new WaitForSeconds(2f);
    }

   

    public override void EnterAttackDistance()
    {
        _canAttack  = true;
        sheepBehaviour.SetCanAttack(true);
    }

    public override void ExitAttackDistance()
    {
        _canAttack = true;
        sheepBehaviour.SetCanAttack(false);
    }

    private void Update()
    {
        sheepBehaviour.SetEnemyState(enemyState);
    }
}