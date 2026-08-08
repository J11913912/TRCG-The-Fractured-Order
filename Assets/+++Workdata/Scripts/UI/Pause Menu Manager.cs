using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
     public GameObject pauseMenuContainer;
    public GameObject inventoryMenuContainer;
    public GameObject optionsMenuContainer;
       public GameObject gameOverMenuContainer;
       public GameObject winMenuContainer;
       public GameObject hudContainer;
       public GameObject questLogContainer;
       
       private InputSystem_Actions _inputActions;
       private InputAction _pauseAction;
       private InputAction _inventoryAction;
       private InputAction _questLogAction;
       
       private GameObject _currentMenu;
   
       private bool _isPaused = false;
       private bool _isInventory = false;
       private bool _isQuestLog = false;
       private bool _menuAlreadyOpen = false;

       private bool unlockQuestLog = false;
       
       public PlayerInput playerInput;
       
       
       
       
       private void SetInputActions()
       {
           _inputActions = new InputSystem_Actions();
           _pauseAction = _inputActions.UI.Pause;
           //_inventoryAction = _inputActions.UI.Inventory;
           //_questLogAction = _inputActions.UI.QuestLog;

       }
       private void Awake()
       {
           _currentMenu = pauseMenuContainer;
           SetInputActions();
       }
       
       private void OnEnable()
       {
           _inputActions.Enable();
           _pauseAction.performed += Pause;
           //_inventoryAction.performed += Inventory;
           //_questLogAction.performed += QuestLog;
           
           //OnGameOver += OpenGameOverMenu;
       }
   
       private void OnDisable()
       {
           _inputActions.Disable();
           _pauseAction.performed -= Pause;
            //_inventoryAction.performed -= Inventory;
            //_questLogAction.performed -= QuestLog;
            
            //OnGameOver -= OpenGameOverMenu;
       }
   
       private void Pause(InputAction.CallbackContext context)
       {
           OpenPauseMenu();
       }
       
       /*private void Inventory(InputAction.CallbackContext context)
       {
           OpenInventoryMenu();
       }
       
       private void QuestLog(InputAction.CallbackContext context)
       {
           OpenQuestLogMenu();
       }*/
       
       public void OpenPauseMenu()
       {
           if (_isInventory)
           {
               OpenInventoryMenu();
               return;
           }

           if (_isQuestLog) return;
           
           if (!_isPaused && !_menuAlreadyOpen)
           {
               playerInput.ToggleSpells(false);
               
               _currentMenu.SetActive(false);
               pauseMenuContainer.SetActive(true);
               //hudContainer.SetActive(false);
               Time.timeScale = 0;
               _isPaused = true;
               _menuAlreadyOpen = true;
           
               _currentMenu = pauseMenuContainer;
           }
           
           else if (_isPaused)
           {
               playerInput.ToggleSpells(true);
               
               pauseMenuContainer.SetActive(false);
               //hudContainer.SetActive(true);
               Time.timeScale = 1;
               _isPaused = false;
               _menuAlreadyOpen = false;
           }
       }
       
       public void OpenOptionsMenu()
       {
           if (_isQuestLog) return;
           
               _currentMenu.SetActive(false);
               optionsMenuContainer.SetActive(true);
           
               _currentMenu = optionsMenuContainer;
               Time.timeScale = 0;
       }
   
       public void OpenGameOverMenu()
       {
           _currentMenu.SetActive(false);
           gameOverMenuContainer.SetActive(true);
           
           _currentMenu = gameOverMenuContainer;
           Time.timeScale = 0;
       }
   
       public void OpenWinMenu()
       {
           _currentMenu.SetActive(false);
           winMenuContainer.SetActive(true);
           
           _currentMenu = winMenuContainer;
           Time.timeScale = 0;
       }

       public void OpenInventoryMenu()
       {
           if (_isPaused) return;
           if (_isQuestLog) return;
           
           if (!_isInventory && !_menuAlreadyOpen)
           {
               _currentMenu.SetActive(false);
               inventoryMenuContainer.SetActive(true);
               hudContainer.SetActive(false);
               Time.timeScale = 0;
               _isInventory = true;
               _menuAlreadyOpen = true;

               _currentMenu = inventoryMenuContainer;
           }

           else if (_isInventory && _menuAlreadyOpen)
           {
               inventoryMenuContainer.SetActive(false);
               hudContainer.SetActive(true);
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
