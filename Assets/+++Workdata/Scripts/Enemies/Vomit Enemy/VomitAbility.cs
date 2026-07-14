using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VomitAbility : MonoBehaviour
{
    public static VomitAbility Instance;

    private SlurpEnemyPatrol _slurpEnemyPatrol;
    
    public GameObject vomitPrefab;
    public bool canVomit;
    private Vector2 _spawnPosition;
    
    public float vomitTime; 
    private float _counter;

    public int vomitsSpawned;

    public List<GameObject> vomitSpawned;

    private void Awake()
    {
        Instance = this;

        _slurpEnemyPatrol = GetComponent<SlurpEnemyPatrol>();
    }

    public void ToggleVomit(bool value)
    {
        canVomit = value;
    }
    
    private void Update()
    {
        if (!canVomit) return;
        
        SpawnVomit();
        
        SetSpawnPosition();
    }

    public void SpawnVomit()
    {
        canVomit = false;
        
        _counter += Time.deltaTime;

        if (_counter > vomitTime)
        {
            _counter = 0;
            GameObject newVomit = Instantiate(vomitPrefab);
            newVomit.transform.position = _spawnPosition;
            
            vomitSpawned.Add(newVomit);

            vomitsSpawned++;
        }
        
        _slurpEnemyPatrol.ResumePatrol(); 
        _slurpEnemyPatrol.EnterAggroDistance();
    }

    public void SetSpawnPosition()
    {
        _spawnPosition.x = transform.position.x;
        _spawnPosition.y = transform.position.y - 1;
    }

    public void ResetVomits()
    {
        vomitsSpawned = 0;
    }
    
    
    
    // netwas schreiben was index 0 aus liste nimmt und die vomit dazu löscht
}
