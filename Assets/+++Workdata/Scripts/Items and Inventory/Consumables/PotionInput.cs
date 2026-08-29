using UnityEngine;
using UnityEngine.InputSystem;

public class PotionInput : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _healthAction;
    private InputAction _manaAction;

    public int heallingPower = 20;
    public int manaPower = 10;

    public GameObject player;
    
    private MoneyManager _moneyManager;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _healthAction = _inputActions.Player.HealthPotion;
        _manaAction = _inputActions.Player.ManaPotion;
        
        _moneyManager = GetComponent<MoneyManager>();
    }

    private void OnEnable()
    {
        EnableInput();
        _healthAction.performed += UseHealthPotion;
        _manaAction.performed += UseManaPotion;
    }

    private void OnDisable()
    {
        DisableInput();
        _healthAction.performed -= UseHealthPotion;
        _manaAction.performed -= UseManaPotion;
    }

    public void EnableInput()
    {
        _inputActions.Enable();
    }

    public void DisableInput()
    {
        _inputActions.Disable();
    }

    public void UseHealthPotion(InputAction.CallbackContext ctx)
    {
        if (_moneyManager.ReturnHealthPotions() <= 0) return;
        
        MoneyManager.OnHealthPotion?.Invoke(-1);
        PlayerInformation.OnHealthUp?.Invoke(heallingPower);
    }

    public void UseManaPotion(InputAction.CallbackContext ctx)
    {
        if (_moneyManager.ReturnManaPotions() <= 0) return;
        
        MoneyManager.OnManaPotion?.Invoke(-1);
        player.GetComponent<ManaManager>().IncreaseMana(manaPower);
    }
}
