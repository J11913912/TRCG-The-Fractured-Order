using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VomitAbility : MonoBehaviour
{
    public static VomitAbility Instance;
    
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
        _counter += Time.deltaTime;

        if (_counter > vomitTime)
        {
            _counter = 0;
            GameObject newVomit = Instantiate(vomitPrefab);
            newVomit.transform.position = _spawnPosition;
            
            vomitSpawned.Add(newVomit);

            vomitsSpawned++;
        }
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
