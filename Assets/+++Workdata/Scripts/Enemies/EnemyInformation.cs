using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class EnemyInformation : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    
    private Animator _animator;
    public UnityEvent OnDeath;
    public GameObject moneyPrefab;
    private GameObject money;
    private GameObject money2;
    private GameObject money3;
    private GameObject money4;
    private GameObject money5;
    private GameObject money6;
    private GameObject money7;
    private GameObject money8;
    private GameObject money9;

    private float timer;
    private float time = 1f;
    
    private bool _dropped = false;
    
    public bool isBoss = false;
    public GameObject crystalPrefab;
    private GameObject crystal;
    
    public GameObject bossHealthBar;
    public SpriteColorChanger spriteColorChanger;

    private void Awake()
    {
        currentHealth = maxHealth;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_dropped)
        {
            timer += Time.deltaTime;

            if (timer >= time)
            {
                _dropped = false;
                
                money.GetComponent<Rigidbody2D>().linearVelocity = Vector2.negativeInfinity;
                money2.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money3.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money4.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money5.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money6.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money7.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money8.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                money9.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetAnimation(90);

        if (isBoss)
        {
            spriteColorChanger.ColorObject();
            bossHealthBar.GetComponent<HealthbarManager>().SetSliderDown(damage);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            if (isBoss) return;
            
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
        if (isBoss)
        {
            crystal = Instantiate(crystalPrefab);
            crystal.transform.position = transform.position;
            bossHealthBar.SetActive(false);
            return;
        }
        
        RuntimeManager.PlayOneShot("event:/Enemies/Crystal/Death Enemy Crystal");
        
        Destroy(this.gameObject);
        
        money = Instantiate(moneyPrefab);
        money.transform.position = transform.position;
        money.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * 4);
        
        money2 = Instantiate(moneyPrefab);
        money2.transform.position = transform.position;
        money2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3);
        
        money3 = Instantiate(moneyPrefab);
        money3.transform.position = transform.position;
        money3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * 2);
        
        money4 = Instantiate(moneyPrefab);
        money4.transform.position = transform.position;
        money4.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3);
        
        money5 = Instantiate(moneyPrefab);
        money5.transform.position = transform.position;
        money5.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 4);
        
        money6 = Instantiate(moneyPrefab);
        money6.transform.position = transform.position;
        money6.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3);
        
        money7 = Instantiate(moneyPrefab);
        money7.transform.position = transform.position;
        money7.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3);
        
        money8 = Instantiate(moneyPrefab);
        money8.transform.position = transform.position;
        money8.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 3);
        
        money9 = Instantiate(moneyPrefab);
        money9.transform.position = transform.position;
        money9.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 2);

        _dropped = true;
    }

    private IEnumerator StopMoney()
    {
        yield return new WaitForSeconds(1f);
        
    }
}
