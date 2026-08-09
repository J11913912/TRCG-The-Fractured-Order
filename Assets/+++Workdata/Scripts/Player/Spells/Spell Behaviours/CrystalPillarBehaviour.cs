using System;
using UnityEngine;

public class CrystalPillarBehaviour : MonoBehaviour
{
    private Animator _animator;

    private float time;
    public float timeToSelfDestruct;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToSelfDestruct)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetAction(int ID)
    {
        _animator.SetTrigger("ActionTrigger");
        _animator.SetInteger("ActionID", ID);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
