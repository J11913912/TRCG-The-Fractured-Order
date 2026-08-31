using System;
using UnityEngine;

public class TurnOffTutorial : MonoBehaviour
{
    public float timeBeforeGone = 30f;
    private float timer;
    public bool isOn = false;

    public GameObject tutorial;
    
    public void SetOn()
    {
        isOn = true;
        tutorial.SetActive(true);
    }

    private void Update()
    {
        if (isOn)
        {
            timer += Time.deltaTime;

            if (timer >= timeBeforeGone)
            {
                timer = 0;
                isOn = false;
                tutorial.SetActive(false);
            }
        }
    }

    public void ResetTime()
    {
        timer = 0;
        isOn = false;
        tutorial.SetActive(false);
    }
}
