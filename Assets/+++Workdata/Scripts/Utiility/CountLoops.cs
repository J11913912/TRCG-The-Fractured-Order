using System;
using System.Collections;
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
        loopsWent = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loopsWent++;

            if (loopsWent >= loopsToGo)
            {
                loopsWent = 0;
                //StartCoroutine(WaitBeforeTurnOff());
                OnEnoughLoops?.Invoke();
            }
        }
    }

    private IEnumerator WaitBeforeTurnOff()
    {
        yield return new WaitForSeconds(2f);
        OnEnoughLoops?.Invoke();
    }
}
