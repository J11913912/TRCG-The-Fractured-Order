using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUnlockedSpells : MonoBehaviour
{
   public List<Button> allButtons = new List<Button>();
   
   public List<bool> unlockedStates = new List<bool>();

   private void Update()
   {
      if (PlayerPrefs.GetInt("UnlockedCrystalHealing") == 1)
      {
         unlockedStates[9] = true;
      }

      if (PlayerPrefs.GetInt("UnlockedCrystalAoE") == 1)
      {
         unlockedStates[13] = true;
      }

      if (PlayerPrefs.GetInt("UnlockedCrystalProjectile") == 1)
      {
         unlockedStates[1] = true;
      }

      if (PlayerPrefs.GetInt("UnlockedCrystalGuard") == 1)
      {
         unlockedStates[5] = true;
      }
      
      int index = 0;
      
      foreach (var var in unlockedStates)
      {
         if (var == true)
         {
            allButtons[index].GetComponent<SpellButtonSetter>().UnlockedSpell();
         }
         
         index++;
      }
   }

   public void UnlockSpell(int index)
   {
      unlockedStates[index] = true;

      if (index == 9)
      {
         PlayerPrefs.SetInt("UnlockedCrystalHealing", 1);
      }

      if (index == 13)
      {
         PlayerPrefs.SetInt("UnlockedCrystalAoE", 1);
      }

      if (index == 1)
      {
         PlayerPrefs.SetInt("UnlockedCrystalProjectile", 1);
      }

      if (index == 5)
      {
         PlayerPrefs.SetInt("UnlockedCrystalGuard", 1);
      }
   }
}
