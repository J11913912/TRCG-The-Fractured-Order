using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellTutorialManager : MonoBehaviour
{
   public GameObject wantTutorial;
   public GameObject tutorial1;
   public GameObject tutorial2;
   public GameObject tutorial3;

   public GameObject spellMenu;

   public Button yesButton;
   public Button page1Button;
   public Button page2Button;
   public Button page3Button;

   private GameObject _currentTutorial;
   public bool _firstOpened = false;
   
   public void StartTutorial()
   {
      if (_firstOpened) return;
      
      spellMenu.SetActive(false);
      
      _firstOpened = true;
      _currentTutorial = wantTutorial;
      wantTutorial.SetActive(true);
      
      yesButton.Select();
   }

   public void CloseTutorial()
   {
      wantTutorial.SetActive(false);
      tutorial1.SetActive(false);
      tutorial2.SetActive(false);
      tutorial3.SetActive(false);

      spellMenu.SetActive(true);
   }
   
   public void OpenTutorial1()
   {
      _currentTutorial.SetActive(false);
      _currentTutorial = tutorial1;
      tutorial1.SetActive(true);
      
      page1Button.Select(); 
   }
   
   public void OpenTutorial2()
   {
      _currentTutorial.SetActive(false);
      _currentTutorial = tutorial2;
      tutorial2.SetActive(true);
      
      page2Button.Select();
   }
   
   public void OpenTutorial3()
   {
      _currentTutorial.SetActive(false);
      _currentTutorial = tutorial3;
      tutorial3.SetActive(true);
      
      page3Button.Select();
   }
   
}
