using System;
using System.Collections;
using FMODUnity;
using UnityEngine;
// using FMODUnity;

public class PushCrate : MonoBehaviour
{
   public enum Direction
   {
      Left,
      Right,
      Up,
      Down
   }

   public Direction direction;

   public Vector2 moveDirection;
   
   public GameObject crate;

   public Rigidbody2D rb;

   public bool _canPush = true;

   public float pushLength;

   public CrateGrid _CrateGrid;

   public bool beamOffWhilePushing = false;
   public GameObject beam;

   public void Push()
   {
      if (!_canPush) return;
      
      switch (direction)
      {
         case Direction.Left:
            moveDirection = Vector2.right;
            if (_CrateGrid.GetGrid(_CrateGrid.PosX + 1, _CrateGrid.PosY) == 1)
            {
               _canPush = false;
               rb.linearVelocity = moveDirection * 2;
               Debug.Log("Moving");
               StartCoroutine(StopPush());

               _CrateGrid.SetPositionX(_CrateGrid.PosX + 1);

               PlayPush();

               if (beamOffWhilePushing)
               {
                  beam.SetActive(false);
               }
            }
            else
            {
               PlayStuck();
            }

            break;

         case Direction.Right:
            moveDirection = Vector2.left;
            if (_CrateGrid.GetGrid(_CrateGrid.PosX - 1, _CrateGrid.PosY) == 1)
            {
               _canPush = false;
               rb.linearVelocity = moveDirection * 2;
               Debug.Log("Moving");
               StartCoroutine(StopPush());

               _CrateGrid.SetPositionX(_CrateGrid.PosX - 1);
               
               PlayPush();
               
               if (beamOffWhilePushing)
               {
                  beam.SetActive(false);
               }
            }
            else
            {
               PlayStuck();
            }

            break;

         case Direction.Up:
            moveDirection = Vector2.down;
            if (_CrateGrid.GetGrid(_CrateGrid.PosX, _CrateGrid.PosY + 1) == 1)
            {
               _canPush = false;
               rb.linearVelocity = moveDirection * 2;
               Debug.Log("Moving");
               StartCoroutine(StopPush());

               _CrateGrid.SetPositionY(_CrateGrid.PosY + 1);
               
               PlayPush();
               
               if (beamOffWhilePushing)
               {
                  beam.SetActive(false);
               }
            }
            else
            {
               PlayStuck();
            }

            break;

         case Direction.Down:
            moveDirection = Vector2.up;
            if (_CrateGrid.GetGrid(_CrateGrid.PosX, _CrateGrid.PosY - 1) == 1)
            {
               _canPush = false;
               rb.linearVelocity = moveDirection * 2;
               Debug.Log("Moving");
               StartCoroutine(StopPush());

               _CrateGrid.SetPositionY(_CrateGrid.PosY - 1);
               
               PlayPush();
               
               if (beamOffWhilePushing)
               {
                  beam.SetActive(false);
               }
            }
            else
            {
               PlayStuck();
            }

            break;
      }

   }

   private IEnumerator StopPush()
   {
      yield return new WaitForSeconds(pushLength);
      rb.linearVelocity = Vector2.zero;
      _canPush = true;
      
      if (beamOffWhilePushing)
      {
         beam.SetActive(true);
         beam.GetComponent<BeamBehaviour>().SetNewPos(crate.transform.position);
      }
   }

   private void PlayPush()
   {
      RuntimeManager.PlayOneShot("event:/Puzzle/Block Push");
      //RuntimeManager.PlayOneShot("event:/SFX/CratePush", transform.position);
   }
   
   private void PlayStuck()
   {
      //RuntimeManager.PlayOneShot("event:/SFX/CrateStuck", transform.position);
   }
   
}
