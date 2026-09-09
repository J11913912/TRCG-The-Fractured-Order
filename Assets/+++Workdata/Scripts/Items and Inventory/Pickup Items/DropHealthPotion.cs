using System;
using UnityEngine;

public class DropHealthPotion : MonoBehaviour
{ 
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         PlayerInformation.OnHealthUp(20);
         Destroy(this.gameObject);
      }
   }
}
