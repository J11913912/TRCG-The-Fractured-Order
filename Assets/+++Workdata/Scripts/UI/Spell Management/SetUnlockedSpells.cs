using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUnlockedSpells : MonoBehaviour
{
   public List<Button> allButtons = new List<Button>();
   
   public List<bool> unlockedStates = new List<bool>();

   private void Update()
   {
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
   }
}
