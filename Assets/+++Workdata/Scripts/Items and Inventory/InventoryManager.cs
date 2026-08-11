using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
//using FMODUnity;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public static Action<ItemDefinition> GetDef;
    
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private List<InventorySlot> hotbarSlots;
    [SerializeField] private List<ItemDefinition> allItemsInGame;
    
    private ItemDefinition _itemDefenition;
    
    [SerializeField] private TextMeshProUGUI itemHeaderText;
    [SerializeField] private GameObject shouldntScreen;
    
    private InventorySystem _inventorySystem;

    public ItemDefinition _lantern;

    public ItemDefinition itemFromHotbar;
    

    private void Awake()
    {
        Instance = this;
        
        _inventorySystem = GetComponent<InventorySystem>();
    }

    private void OnEnable()
    {
        GetDef += GetItemDefinition;
    }

    private void OnDisable()
    {
        GetDef -= GetItemDefinition;
    }


    public void SetInventoryItems(List<Item> items)
    {
        foreach (var currentSlot in inventorySlots)
        {
            currentSlot.ResetInventorySlot();
        }

        int currentItemIndex = 0;
        int hotbarIndex = 0;
        foreach (var currentItemInInventory in items)
        {
            foreach (var currentItemInGame in allItemsInGame)
            {
                if (currentItemInGame.id == currentItemInInventory.id)
                {
                    if (currentItemInGame.itemType == ItemDefinition.ItemType.Consumable || currentItemInGame.itemType == ItemDefinition.ItemType.Quest)
                    {
                        inventorySlots[currentItemIndex].FillInventorySlot(currentItemInGame, currentItemInInventory.amount);
                        inventorySlots[currentItemIndex].GetComponentInChildren<CanvasGroup>().alpha = 1;
                        Debug.Log("found a cosubmable quest");
                        currentItemIndex++;
                        break;
                    }
                    
                    if (currentItemInGame.itemType == ItemDefinition.ItemType.Tool)
                    {
                        Debug.Log("found a tool");
                        hotbarSlots[hotbarIndex].FillInventorySlot(currentItemInGame, currentItemInInventory.amount);
                        hotbarSlots[hotbarIndex].GetComponentInChildren<CanvasGroup>().alpha = 1;
                        hotbarIndex++;
                        break;
                    }
                }
            }
        }
        
    }
    
    public void OnClickeUseButton() // extra
    {
        string name = itemHeaderText.text;
        
        Debug.Log(name);

        int currentItemIndex = 0;
        foreach (var item in allItemsInGame)
        {
            if (item.displayName == name)
            {
                ItemDefinition selectedItem = allItemsInGame[currentItemIndex];
                
                if (selectedItem.itemType == ItemDefinition.ItemType.Consumable)
                {
                    if (selectedItem.id == "item_healing")
                    {
                        if (FindAnyObjectByType<PlayerInformation>().currentHealth < 30)
                        {
                            FindAnyObjectByType<PlayerInformation>().SetHealth(10);
                           // RuntimeManager.PlayOneShot("event:/SFX/Charakter/Healing");
                        }
                        else
                        {
                            shouldntScreen.SetActive(true);
                            return;
                        }
                
                    }
                }
                _inventorySystem.RemoveWhat(selectedItem.id);
                _inventorySystem.RemoveHowMuch(1);

                return;
            }
            
            currentItemIndex++;
        }
    }

    private void GetItemDefinition(ItemDefinition _itemDef)
    {
        itemFromHotbar = _itemDef;
    }
    
    public void RemoveLantern()
    {
        _inventorySystem.RefreshInventory();
        
        foreach (var slot in hotbarSlots)
        {
            slot.FindItemInSlot();
            
            if (itemFromHotbar.id == _lantern.id)
            {
                _inventorySystem.RemoveWhat(_lantern.id);
                _inventorySystem.RemoveHowMuch(1);
                
            }
        }
    }
}