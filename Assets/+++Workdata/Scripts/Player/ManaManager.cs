using System;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
   public static Action OnReset;
   
   public int maxMana;
   public int currrentMana;

   private void Awake()
   {
      currrentMana =  maxMana;
   }

   private void OnEnable()
   {
      OnReset += SetManaToMax;
   }

   private void OnDisable()
   {
      OnReset -= SetManaToMax;
   }

   public void DecreaseMana(int mana)
   {
      if (currrentMana - mana < 0)
      {
         currrentMana = 0;
         ManabarManager.OnManaDecrease(mana);
      }
      else
      {
         currrentMana -= mana;
         ManabarManager.OnManaDecrease(mana);
      }
   }

   public void IncreaseMana(int mana)
   {
      if (currrentMana + mana > maxMana)
      {
         currrentMana = maxMana;
         ManabarManager.OnManaIncrease(maxMana);
      }
      else
      {
         currrentMana += mana;
         ManabarManager.OnManaIncrease(mana);
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

   public void SetManaToMax()
   {
      currrentMana = maxMana;
   }
}
