using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemDisplay;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    
    private Button _inventorySlotButton;
    private ItemDefinition _itemDefenition;

    public bool _isHotbar;

    private void Awake()
    {
        _inventorySlotButton = GetComponent<Button>();
        
        _inventorySlotButton.interactable = false;
    }

    public void ResetInventorySlot()
    {
        itemDisplay.sprite = null;
        GetComponentInChildren<CanvasGroup>().alpha = 0; // von mir
        itemAmountText.SetText("");
        _inventorySlotButton.interactable = false;
    }
    
    public void ResetHotbarSlot() // von mir
    {
        itemDisplay.sprite = null;
        GetComponentInChildren<CanvasGroup>().alpha = 0; // von mir
        _inventorySlotButton.interactable = false;
    }

    public void FillInventorySlot(ItemDefinition itemDefenition, int amount)
    {
        _inventorySlotButton.interactable = true;
        
        itemDisplay.sprite = itemDefenition.sprite;

        if (!_isHotbar)
        {
            itemAmountText.SetText(amount.ToString());
        }
        
        _inventorySlotButton.interactable = true;
        
        _itemDefenition = itemDefenition;
    }

    public void SelectItemSlot()
    {
        if (!_itemDefenition) return;
        InventorySystem.OnItemSelected?.Invoke(_itemDefenition);
    }
    
    public void FindItemInSlot() // von mir
    {
        if (!_itemDefenition) return;
        InventoryManager.GetDef?.Invoke(_itemDefenition);
    }
    public void OnClickUse() // von mir
    {
        if (!_itemDefenition) return;
        
        
    }

    public ItemDefinition ReturnItemDefenition()
    {
        if (_itemDefenition == null)
        {
            
        }
        
        return _itemDefenition;
    }
}