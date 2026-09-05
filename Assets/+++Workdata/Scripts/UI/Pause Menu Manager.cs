using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject spellMenuContainer;
    public GameObject pauseMenuContainer;
    public GameObject inventoryMenuContainer;
    public GameObject optionsMenuContainer;
    public GameObject gameOverMenuContainer;
    public GameObject winMenuContainer; 
    public GameObject hudContainer;
    public GameObject questLogContainer;

    public Button pauseButton;
    public Button inventoryButton;
    public Button optionsButton;
    public Button gameOverButton;
    public Button spellMenuButton;

    public SpellTutorialManager spellTutorialManager;
       
    private InputSystem_Actions _inputActions;
    private InputAction _pauseAction;
    private InputAction _spellAction;
    private InputAction _inventoryAction;
    private InputAction _questLogAction;
       
    private GameObject _currentMenu;
   
    private bool _isPaused = false;
    private bool _isInventory = false;
    private bool _isQuestLog = false;
    private bool _menuAlreadyOpen = false;

    private bool unlockQuestLog = false;
       
    public PlayerInput playerInput;
    
    private bool _dialogueOn = false;
    
       private void SetInputActions()
       {
           _inputActions = new InputSystem_Actions();
           _pauseAction = _inputActions.UI.Pause;
           _spellAction = _inputActions.UI.Spells;
           _inventoryAction = _inputActions.UI.Inventory;
           //_questLogAction = _inputActions.UI.QuestLog;

       }
       private void Awake()
       {
           _currentMenu = pauseMenuContainer;
           SetInputActions();
       }
       
       private void OnEnable()
       {
           EnableInput();
           _pauseAction.performed += Pause;
           _spellAction.performed += SpellMenu;
           _inventoryAction.performed += Inventory;
           //_questLogAction.performed += QuestLog;
           
           //OnGameOver += OpenGameOverMenu;
       }
   
       private void OnDisable()
       {
           DisableInput();
           _pauseAction.performed -= Pause;
           _spellAction.performed -= SpellMenu;
            _inventoryAction.performed -= Inventory;
            //_questLogAction.performed -= QuestLog;
            
            //OnGameOver -= OpenGameOverMenu;
       }

       public void EnableInput()
       {
           _inputActions.Enable();
       }

       public void DisableInput()
       {
           _inputActions.Disable();
       }
   
       private void Pause(InputAction.CallbackContext context)
       {
           OpenPauseMenu();
       }
       
       private void SpellMenu(InputAction.CallbackContext context)
       {
           OpenSpellMenu();
       }
       
       private void Inventory(InputAction.CallbackContext context)
       {
           OpenInventoryMenu();
       }
       
       /*private void QuestLog(InputAction.CallbackContext context)
       {
           OpenQuestLogMenu();
       }*/


       public void SetDialogue(bool value)
       {
           _dialogueOn = value;
       }
       
       public void OpenSpellMenu()
       {
           if (_dialogueOn) return;
           
           if (!_isPaused && !_menuAlreadyOpen)
           {
               if (_menuAlreadyOpen) return;
               
               playerInput.ToggleSpells(false);
               playerInput.DisableInput();
               SpellButtonSetter.SpellMenuToggle(true);
               
               spellMenuContainer.SetActive(true);
               spellMenuContainer.GetComponent<CanvasGroup>().alpha = 1;
               
               spellTutorialManager.StartTutorial();
               
               //hudContainer.SetActive(false);
               Time.timeScale = 0;
               _isPaused = true;
               _menuAlreadyOpen = true;
           
               _currentMenu = spellMenuContainer;
              // spellMenuButton.Select();
               
               SpellEquipping.OnMenuActive?.Invoke(true);
               SwitchButtonCorners.OnOpenMenu?.Invoke(true);
           }
           
           else if (_isPaused)
           {
               playerInput.ToggleSpells(true);
               playerInput.EnableInput();
               SpellButtonSetter.SpellMenuToggle(false);
               
               spellMenuContainer.GetComponent<CanvasGroup>().alpha = 0;
               //hudContainer.SetActive(true);
               Time.timeScale = 1;
               _isPaused = false;
               _menuAlreadyOpen = false;
               
               SpellEquipping.OnMenuActive?.Invoke(false);
               SwitchButtonCorners.OnOpenMenu?.Invoke(false);
           }
       }
       
       public void OpenPauseMenu()
       {
           if (_dialogueOn) return;
           
           if (_isInventory)
           {
               OpenInventoryMenu();
               return;
           }

           if (_isQuestLog) return;
           
           if (!_isPaused && !_menuAlreadyOpen)
           {
               if (_menuAlreadyOpen) return;
               
               playerInput.ToggleSpells(false);
               playerInput.DisableInput();
               
               _currentMenu.SetActive(false);
               pauseMenuContainer.SetActive(true);
               //hudContainer.SetActive(false);
               Time.timeScale = 0;
               _isPaused = true;
               _menuAlreadyOpen = true;
               
               pauseButton.Select();
               
               if (_currentMenu == spellMenuContainer)
               {
                   spellMenuContainer.SetActive(true);
               }
           
               _currentMenu = pauseMenuContainer;
           }
           
           else if (_isPaused)
           {
               playerInput.ToggleSpells(true);
               playerInput.EnableInput();
               
               pauseMenuContainer.SetActive(false);
               //hudContainer.SetActive(true);
               Time.timeScale = 1;
               _isPaused = false;
               _menuAlreadyOpen = false;
           }
       }
       
       public void OpenOptionsMenu()
       {
           if (_dialogueOn) return;
           
           if (_isQuestLog) return;
           
               _currentMenu.SetActive(false);
               optionsMenuContainer.SetActive(true);
               
               optionsButton.Select();
               
               if (_currentMenu == spellMenuContainer)
               {
                   spellMenuContainer.SetActive(true);
               } 
           
               _currentMenu = optionsMenuContainer;
               Time.timeScale = 0;
       }

       public void Continue()
       {
           _currentMenu.SetActive(false);
           Time.timeScale = 1;
           playerInput.ToggleSpells(true);
           playerInput.EnableInput();
       }
   
       public void OpenGameOverMenu()
       {
           _currentMenu.SetActive(false);
           gameOverMenuContainer.SetActive(true);
           
           playerInput.DisableInput();
           playerInput.ToggleSpells(false);
           
           gameOverButton.Select();
           
           if (_currentMenu == spellMenuContainer)
           {
               spellMenuContainer.SetActive(true);
           }
           
           _currentMenu = gameOverMenuContainer;
           Time.timeScale = 0;
       }

       public void CloseGameOverMenu()
       {
           _currentMenu.SetActive(false);
           Time.timeScale = 1;
           playerInput.EnableInput();
       }
   
       public void OpenWinMenu()
       {
           _currentMenu.SetActive(false);
           winMenuContainer.SetActive(true);
           
           if (_currentMenu == spellMenuContainer)
           {
               spellMenuContainer.SetActive(true);
           }
           
           _currentMenu = winMenuContainer;
           Time.timeScale = 0;
       }

       public void OpenInventoryMenu()
       {
           if (_dialogueOn) return;
           
           if (_isPaused) return;
           if (_isQuestLog) return;
           
           if (!_isInventory && !_menuAlreadyOpen)
           {
               _currentMenu.SetActive(false);
               playerInput.DisableInput();
               inventoryMenuContainer.SetActive(true);
               Time.timeScale = 0;
               _isInventory = true;
               _menuAlreadyOpen = true;
               
               inventoryButton.Select();
               
               if (_currentMenu == spellMenuContainer)
               {
                   spellMenuContainer.SetActive(true);
               }

               _currentMenu = inventoryMenuContainer;
           }

           else if (_isInventory && _menuAlreadyOpen)
           {
               inventoryMenuContainer.SetActive(false);
               playerInput.EnableInput();
               Time.timeScale = 1;
               _isInventory = false;
               _menuAlreadyOpen = false;
           }

           //InventorySystem.OnChangeInventory?.Invoke();
       }

       public void OpenQuestLogMenu()
       {
           if (!unlockQuestLog) return;
           
           if (_isInventory)
           {
               OpenInventoryMenu();
               return;
           }

           if (_isPaused) return;

           if (!_isQuestLog && !_menuAlreadyOpen)
           {
               _currentMenu.SetActive(false);
               questLogContainer.SetActive(true);
               hudContainer.SetActive(false);
               Time.timeScale = 0;
               _isQuestLog = true;
               _menuAlreadyOpen = true;

               _currentMenu = pauseMenuContainer;
           }

           else if (_isQuestLog)
           {
               questLogContainer.SetActive(false);
               hudContainer.SetActive(true);
               Time.timeScale = 1;
               _isQuestLog = false;
               _menuAlreadyOpen = false;
           }
       }

       public void UnlockQuestLog()
       {
           unlockQuestLog = true;
       }

       public void QuitGame()
           {
               Application.Quit();
           }
}
