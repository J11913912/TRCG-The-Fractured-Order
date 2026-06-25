using System;
using System.Collections;
using UnityEngine;

public class CrushAbility : MonoBehaviour
{
    public static int Hash_ActionID = Animator.StringToHash("ActionID");
    public static int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    
    public float shockWaveTime;

    public bool startShock = false;
    private bool shockWaveActive = false;

    private bool _inSafe;
    private bool _inDamage;

    private Animator _animator;

    public GameObject player;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (startShock && !shockWaveActive)
        {
            ShockWave();
        }

        if (shockWaveActive && _inDamage && !_inSafe)
        {
            Debug.Log("damage");
            // player.setdamage;
        }
         else if (shockWaveActive && _inDamage == true && _inSafe == true)
         {
             Debug.Log("safe");
         }
    }

    private void ShockWave()
    {
        Debug.Log("Shock Wave");
        
        startShock = false;
        
        shockWaveActive = true;
        
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionID, 10);
        
        StartCoroutine(ResetShockWave());
        
    }

    IEnumerator ResetShockWave()
    {
        yield return new WaitForSeconds(shockWaveTime);
        shockWaveActive = false;
    }

    public void InSafe(bool value)
    {
        _inSafe = value;
    }
    
    public void InDamage(bool value)
    {
        _inDamage = value;
    }
}
