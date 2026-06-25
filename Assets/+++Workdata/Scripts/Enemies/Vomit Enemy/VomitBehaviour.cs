using System;
using System.Collections;
using UnityEngine;

public class VomitBehaviour : MonoBehaviour
{
    public int damage;

    public int maxVomits;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO player health reduzieren
            Debug.Log("player damaged");
            StartCoroutine(DestroyItself());
        }
    }
    
    private void Start()
    {
        StartCoroutine(CountdownDestruction());
    }

    private void FixedUpdate()
    {
        if (VomitAbility.Instance.vomitsSpawned > maxVomits)
        {
            Destroy(gameObject);
            VomitAbility.Instance.ResetVomits();
            
            VomitAbility.Instance.vomitSpawned.RemoveAt(0);
        }
    }

    IEnumerator CountdownDestruction()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
