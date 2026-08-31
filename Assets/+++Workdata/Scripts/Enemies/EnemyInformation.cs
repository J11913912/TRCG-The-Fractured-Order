using UnityEngine;
using UnityEngine.Events;

public class EnemyInformation : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    
    private Animator _animator;
    public UnityEvent OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetAnimation(90);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SetAnimation(100);
        }
    }

    public void SetAnimation(int id)
    {
        _animator.SetTrigger("ActionTrigger");
        _animator.SetInteger("ActionID", id);
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
