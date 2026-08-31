using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu_UiManager : MonoBehaviour
{
    public GameObject mainMenuContainer;
    public GameObject loadMenuContainer;
    public GameObject optionsMenuContainer;
    public GameObject creditsMenuContainer;
    
    private GameObject _currentMenu;
    
    // Methoden werden ueber entsprechende Methoden von MainMenu_ButtpManager aufgerufen

    private void Awake()
    {
        _currentMenu = mainMenuContainer;
    }

    public void OpenOptionsMenu()
    {
        _currentMenu.SetActive(false);
        optionsMenuContainer.SetActive(true);
        
        _currentMenu = optionsMenuContainer;
    }
    
    public void OpenLoadMenu()
    {
        _currentMenu.SetActive(false);
        loadMenuContainer.SetActive(true);
        
        _currentMenu = loadMenuContainer;
        
        ResetGame();
    }
    
    public void OpenCreditsMenu()
    {
        _currentMenu.SetActive(false);
        creditsMenuContainer.SetActive(true);
        
        _currentMenu = creditsMenuContainer; 
    }

    public void OpenMainMenu()
    {
        _currentMenu.SetActive(false);
        mainMenuContainer.SetActive(true);
        
        _currentMenu = mainMenuContainer;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("UnlockedCrystalHealing", 0);
        PlayerPrefs.SetInt("UnlockedCrystalAoE", 0);
        PlayerPrefs.SetInt("UnlockedCrystalProjectile", 0);
        PlayerPrefs.SetInt("UnlockedCrystalGuard", 0);    
        
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("HealthPotions", 0);
        PlayerPrefs.SetInt("ManaPotions", 0);
        
        PlayerPrefs.SetInt("HatUnlocked", 0);
        PlayerPrefs.SetInt("HatOn", 0);
    }
    
}