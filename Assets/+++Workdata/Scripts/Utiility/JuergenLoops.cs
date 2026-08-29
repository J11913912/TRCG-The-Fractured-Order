using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class JuergenLoops : MonoBehaviour
{
    public int randomLoops;
    public CountLoops countLoops;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            randomLoops = Random.Range(2, 6);
            
            int specialRandom = Random.Range(0, 11);

            if (specialRandom == 1)
            {
                randomLoops = 15;
            }
            
            countLoops.SetLoopsToGo(randomLoops);
        }
    }
}
