using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class DashAbility : MonoBehaviour
{
    public static Action<Vector2> OnDashInput;
    public int actionId = 10;
    public float rollForce = 5f;
    private PlayerStates _playerState;
    private PlayerDirection _playerDirection;
    private PlayerController _playerController;
    private PlayerAnimation _playerAnimation;

    public bool inTeleportZone = false;
    public Vector2 whereDoWeWantToGo;
    private Vector2 _direction;

    public bool _isTeleporting = false;
    public bool unlockedTeleport = false;
    
    private void Awake()
    {
        _playerState = GetComponent<PlayerStates>();
        _playerController = GetComponent<PlayerController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        OnDashInput += Dash;
    }

    private void OnDisable()
    {
        OnDashInput -= Dash;
    }

    public void UnlockTeleport()
    {
        unlockedTeleport = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoUp"))
        {
            whereDoWeWantToGo = Vector2.up;
            inTeleportZone = true; 
        }
        else if (other.CompareTag("GoDown"))
        {
            whereDoWeWantToGo = Vector2.down;
            inTeleportZone = true; 
        }
        else if (other.CompareTag("GoLeft"))
        {
            whereDoWeWantToGo = Vector2.left;
            inTeleportZone = true; 
        }
        else if (other.CompareTag("GoRight"))
        {
            whereDoWeWantToGo = Vector2.right;
            inTeleportZone = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inTeleportZone = false;
    }

    void Dash(Vector2 input)
    {
        if (!unlockedTeleport) return;
        
        //if (_playerState.GetPlayerAction() != PlayerAction.Default) return;
    
      //  RuntimeManager.PlayOneShot("event:/SFX/Charakter/Rolling");
      
      RuntimeManager.PlayOneShot("event:/Player/Player Teleport");
        
        PlayerStates.OnChangeAction?.Invoke(PlayerAction.Roll);
       // PlayerAnimation.OnAnimationAction?.Invoke(actionId);
       
       _playerDirection = _playerState.GetPlayerDirection();

       _direction = input.normalized;

       if (inTeleportZone)
       {
           if (_isTeleporting) return;
           _isTeleporting = true;
           StartCoroutine(Teleport());
       }
       else
       {
           _playerController.ApplyDash(_direction);
       }
       
       _playerAnimation.AnimationSetAction(70);
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);
            
        Debug.Log("Teleport");
        
        if (_direction == whereDoWeWantToGo)
        {
            Vector2 pos = gameObject.transform.position;
               
            if (whereDoWeWantToGo == Vector2.left)
            {
                pos.x -= 3;
                gameObject.transform.position = pos;
            }
            else if (whereDoWeWantToGo == Vector2.right)
            {
                pos.x += 3;
                gameObject.transform.position = pos;
            }
            else if (whereDoWeWantToGo == Vector2.up)
            {
                pos.y += 3;
                gameObject.transform.position = pos;
            }
            else if (whereDoWeWantToGo == Vector2.down)
            {
                pos.y -= 3;
                gameObject.transform.position = pos;
            }

            _isTeleporting = false;
        }
        else
        {
            _playerController.ApplyDash(_direction);
            _isTeleporting = false;
        }
    }
}
