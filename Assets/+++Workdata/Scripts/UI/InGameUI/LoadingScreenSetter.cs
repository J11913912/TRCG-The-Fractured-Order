using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Plane = Unity.Mathematics.Geometry.Plane;
using Random = UnityEngine.Random;

public class LoadingScreenSetter : MonoBehaviour
{
    public static Action<RoomTransition> onTransition;
    
    public bool start = false;
    public bool special = false;

    public Slider slider;
    
    private Animator animator;

    public int randomDuration;
    public float duration;
    public float realTimeDuration;
    public float time;
    
    public RoomTransition roomTransition;
    public PlayerInput playerInput;
    public PauseMenuManager pauseMenuManager;

    private void Awake()
    {
        animator = slider.GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        onTransition += StartTransition;
    }

    private void OnDisable()
    {
        onTransition -= StartTransition;
    }

    private void StartTransition(RoomTransition transition)
    {
        roomTransition = transition;
    }

    private void Update()
    {
        if (start)
        { 
            playerInput.DisableInput();
            pauseMenuManager.DisableInput();
            
            time += Time.deltaTime;
            
            animator.SetBool("isLoading", true);
            
            slider.value += Time.deltaTime * duration;

            if (slider.value >= 0.80f)
            {
                roomTransition.EndTransition();
                playerInput.EnableInput();
                pauseMenuManager.EnableInput();
            }
            
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
            int waitDuration = Random.Range(3, 7);
            
            animator.SetBool("isLoading", true);
            
            slider.value += Time.deltaTime * duration;
            
            if (slider.value >= 0.75f)
            { 
                realTimeDuration += waitDuration;
                special = false;
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
        time = 0;
        randomDuration = Random.Range(0, 5);

        switch (randomDuration)
        {
            case 0:
                duration = 0.3f;
                realTimeDuration = 3.335087f;
                break;
            case 1:
                duration = 0.4f;
                realTimeDuration = 2.500116f;
                break;
            case 2:
                duration = 0.6f;
                realTimeDuration = 1.66752f;
                break;
            case 3:
                duration = 0.8f;
                realTimeDuration = 1.253806f;
                break;
            case 4:
                duration = 0.2f;
                realTimeDuration = 3.750636f;
                special = true;
                return;
                
        }
        
        start = true;
    }
}
