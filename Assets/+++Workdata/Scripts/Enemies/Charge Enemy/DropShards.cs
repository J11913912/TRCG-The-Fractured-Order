using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShards : MonoBehaviour
{
    public GameObject vomitPrefab;

    public List<Transform> Drops;

    private bool _hasDropped = false;

    public void Drop()
    {
        if (_hasDropped) return;
        
        foreach (var drop in Drops)
        { 
            GameObject newVomit = Instantiate(vomitPrefab);
            newVomit.transform.position = drop.position;
        }
        
        _hasDropped = true;

        StartCoroutine(ResetDrop());
    }

    IEnumerator ResetDrop()
    {
        yield return new WaitForSeconds(2f);
        _hasDropped = false;
    }
}
