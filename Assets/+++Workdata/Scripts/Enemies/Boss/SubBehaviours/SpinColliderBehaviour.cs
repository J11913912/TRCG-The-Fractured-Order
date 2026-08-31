using Unity.VisualScripting;
using UnityEngine;

public class SpinColliderBehaviour : MonoBehaviour
{
   private Rigidbody2D _rb;
   private Vector2 _pushBack;
   private PlayerController _playerController;
   private PlayerInformation _playerInformation;
   private bool _inYeetingRange = false;
   public int damage;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         _playerInformation =  other.GetComponent<PlayerInformation>();
         _playerController = other.GetComponent<PlayerController>();
         
         _inYeetingRange = true;
      }
   }

   public void WhatDirection(int directionClockwise)
   {
      switch (directionClockwise)
      {
         case 0: // up
            _pushBack = Vector2.up;
            break;
         case 1: // up right
            _pushBack = new Vector2(1, 1);
            break;
         case 2: // right
            _pushBack = Vector2.right;
            break;
         case 3: // down right
            _pushBack = new Vector2(1, -1);
            break;
         case 4: // down
            _pushBack = Vector2.down;
            break;
         case 5: // down left
            _pushBack = new Vector2(-1, -1);
            break;
         case 6: // left
            _pushBack = Vector2.left;
            break;
         case 7: // up left
            _pushBack = new Vector2(-1, 1);
            break;
      }

      if (_inYeetingRange)
      {
         _playerController.ApplyForce(_pushBack, true);
         _playerInformation.SetDamage(damage);
      }
   }
}
