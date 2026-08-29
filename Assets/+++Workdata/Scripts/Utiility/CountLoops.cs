using System;
using UnityEngine;
using UnityEngine.Events;

public class CountLoops : MonoBehaviour
{
    public int loopsWent;
    public int loopsToGo;

    public UnityEvent OnEnoughLoops;

    public void SetLoopsToGo(int loops)
    {
        loopsToGo = loops + 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loopsWent++;

            if (loopsWent >= loopsToGo)
            {
                loopsWent = 0;
                OnEnoughLoops?.Invoke();
            }
        }
    }
}
