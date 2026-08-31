using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PotionInput : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _healthAction;
    private InputAction _manaAction;

    public int heallingPower = 40;
    public int manaPower = 200;

    public GameObject player;
    
    private MoneyManager _moneyManager;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        //_healthAction = _inputActions.Player.HealthPotion;
        //_manaAction = _inputActions.Player.ManaPotion;
        
        _moneyManager = FindAnyObjectByType<MoneyManager>();
    }

    private void OnEnable()
    {
        EnableInput();
        //_healthAction.performed += UseHealthPotion;
        //_manaAction.performed += UseManaPotion;
    }

    private void OnDisable()
    {
        DisableInput();
       // _healthAction.performed -= UseHealthPotion;
        //_manaAction.performed -= UseManaPotion;
    }

    public void EnableInput()
    {
        _inputActions.Enable();
    }

    public void DisableInput()
    {
        _inputActions.Disable();
    }

    public void UseHealthPotion()
    {
        if (_moneyManager.ReturnHealthPotions() <= 0) return;
        
        MoneyManager.OnHealthPotion?.Invoke(-1);
        PlayerInformation.OnHealthUp?.Invoke(heallingPower);
        RuntimeManager.PlayOneShot("event:/Misc/Health Potion");
    }

    public void UseManaPotion()
    {
        if (_moneyManager.ReturnManaPotions() <= 0) return;
        
        MoneyManager.OnManaPotion?.Invoke(-1);
        player.GetComponent<ManaManager>().IncreaseMana(manaPower);
        RuntimeManager.PlayOneShot("event:/Misc/Mana Potion");
    }
}
