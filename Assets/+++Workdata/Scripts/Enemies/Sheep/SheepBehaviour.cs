using System;
using System.Collections;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    [SerializeField] private EnemyState enemyState;

    public bool _canAttack;

    private void Update()
    {
        if (enemyState == EnemyState.Attacking && _canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _canAttack = false;

        for (int i = 0; i < 4; i++)
        {
            Vector2 direction = Vector2.zero;
            
            if (i == 0)
            {
                direction = Vector2.right;
            }
            else if (i == 1)
            {
                direction = Vector2.left;
            }
            else if (i == 2)
            {
                direction = Vector2.up;
            }
            else if (i == 3)
            {
                direction = Vector2.down;
            }
            
            
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = gameObject.transform.position;
        
            newProjectile.GetComponent<ShootProjectile>().Shoot(direction);
            newProjectile.GetComponent<ShootProjectile>().StartDeathCountdown();
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(3f);
        _canAttack = true;
    }

    public void SetEnemyState(EnemyState state)
    {
        enemyState = state;
    }

    public void SetCanAttack(bool value)
    {
        _canAttack = value;
    }
}
