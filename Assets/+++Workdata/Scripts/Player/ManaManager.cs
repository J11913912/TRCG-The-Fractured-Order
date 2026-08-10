using System;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
   public int maxMana;
   public int currrentMana;

   private void Awake()
   {
      currrentMana =  maxMana;
   }
   private void Update()
   {
      // TODO update mana bar
   }

   public void DecreaseMana(int mana)
   {
      if (currrentMana - mana < 0)
      {
         currrentMana = 0;
      }
      else
      {
         currrentMana -= mana;
      }
   }

   public void IncreaseMana(int mana)
   {
      if (currrentMana + mana > maxMana)
      {
         currrentMana = maxMana;
      }
      else
      {
         currrentMana += mana;
      }
   }

   public bool CheckIfSpellIsAllowed(int neededMana)
   {
      if (currrentMana >= neededMana)
      {
         DecreaseMana(neededMana);
         return true;
      }
      else
      {
         return false;
      }
   }
}
