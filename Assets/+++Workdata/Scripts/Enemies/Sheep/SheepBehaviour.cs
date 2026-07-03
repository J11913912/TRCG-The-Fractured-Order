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
        
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.position = gameObject.transform.position;
        
        newProjectile.GetComponent<ShootProjectile>().Shoot(Vector2.right);
        newProjectile.GetComponent<ShootProjectile>().StartDeathCountdown();
        
        
        GameObject newProjectile1 = Instantiate(projectilePrefab);
        newProjectile1.transform.position = gameObject.transform.position;
        
        newProjectile1.GetComponent<ShootProjectile>().Shoot(Vector2.left);
        newProjectile1.GetComponent<ShootProjectile>().StartDeathCountdown();
        
        
        GameObject newProjectile2 = Instantiate(projectilePrefab);
        newProjectile2.transform.position = gameObject.transform.position;
        
        newProjectile2.GetComponent<ShootProjectile>().Shoot(Vector2.up);
        newProjectile2.GetComponent<ShootProjectile>().StartDeathCountdown();
        
        
        GameObject newProjectile3 = Instantiate(projectilePrefab);
        newProjectile3.transform.position = gameObject.transform.position;
        
        newProjectile3.GetComponent<ShootProjectile>().Shoot(Vector2.down);
        newProjectile3.GetComponent<ShootProjectile>().StartDeathCountdown();

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(3f);
        _canAttack = true;
    }
}
