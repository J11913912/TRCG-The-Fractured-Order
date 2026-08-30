using System;
using System.Linq;
using FMODUnity;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFootstepSound : MonoBehaviour
{ 
    [Header("Footstep Timer")] [SerializeField]
    private float foostepTime;
    private float _footstepTimer;
    
    private StudioEventEmitter _footstepEmitter;

    private void Awake()
    {
        _footstepEmitter = GetComponent<StudioEventEmitter>();
    }


    private void Update()
    {
        CalculateFootstepTimer();
    }

    private void CalculateFootstepTimer()
    {
        if (PlayerStates.Instance.PlayerMovement == PlayerMovement.Idle) return;

        _footstepTimer += Time.deltaTime;

        if (_footstepTimer > foostepTime)
        {
            _footstepTimer = 0;
            PlayTileSound();
        }
    }

    private void PlayTileSound()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.02f);
        int highestPriority = -1;
        EnvironmentTrigger selectedArea = null;  
        
        
        foreach (var hit in hits)
        {
            EnvironmentTrigger area = hit.GetComponent<EnvironmentTrigger>();  

            if (area != null && area.priority > highestPriority)
            {
                highestPriority = area.priority;
                selectedArea = area;
            }
        }

       _footstepEmitter.Play();
        
        if (selectedArea == null)
        {
            //fallback sound?
            FMOD.RESULT defaulResult = _footstepEmitter.EventInstance.setParameterByNameWithLabel("surface", "Default");
            print(defaulResult);
            return;
        }
        
       // print(selectedArea.footstepSoundArea.fmodFootstepEvent);
        _footstepEmitter.EventInstance.setParameterByNameWithLabel("surface", selectedArea.footstepSoundArea.fmodFootstepEvent);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.06f); 
    }
}
