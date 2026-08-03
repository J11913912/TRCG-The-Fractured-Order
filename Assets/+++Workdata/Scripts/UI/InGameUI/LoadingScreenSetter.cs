using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingScreenSetter : MonoBehaviour
{
    public bool start = false;
    public bool special = false;

    public Slider slider;
    
    private Animator animator;

    public int randomDuration;
    public float duration;
    
    public RoomTransition roomTransition;

    private void Awake()
    {
        animator = slider.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (start)
        { 
            animator.SetBool("isLoading", true);
            
            slider.value += Time.deltaTime * duration;
            
            if (slider.value >= 1)
            {
                start = false;
                animator.SetBool("isLoading", false);
                slider.value = 0;
                
               // roomTransition.
            }
        }

        if (special)
        {
            animator.SetBool("isLoading", true);
            
            slider.value += Time.deltaTime * duration;
            
            if (slider.value >= 0.75f)
            { 
                special = false;
                int waitDuration = Random.Range(3, 7);
                animator.SetBool("isPausing", true);
                StartCoroutine(WaitToContinue(waitDuration));
            }
        }
    }

    private IEnumerator WaitToContinue(int waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        animator.SetBool("isPausing", false);
        start = true;
    }

    public void StartThingy()
    {
        randomDuration = Random.Range(0, 5);

        switch (randomDuration)
        {
            case 0:
                duration = 0.2f;
                break;
            case 1:
                duration = 0.4f;
                break;
            case 2:
                duration = 0.6f;
                break;
            case 3:
                duration = 0.8f;
                break;
            case 4:
                duration = 0.2f;
                special = true;
                return;
                
        }
        
        start = true;
    }
}
