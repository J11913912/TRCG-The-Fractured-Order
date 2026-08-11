using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static Action<ItemDefinition> OnItemSelected;
    public static Action<ItemDefinition, int> OnAddItemDefinition;
    public static Action<string, int> OnAddItemId;
    public static Action OnChangeInventory;
    
    [Header("ItemDescription")]
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemHeaderText;
    [SerializeField] private GameObject options;
    
    
    [SerializeField] private List<Item> items;
    
    [SerializeField] private CanvasGroup inventoryCanvasGroup;

    private bool _dialogueActive;
    private bool _pauseActive;

    public GameObject player;


    public int _index; // extra
    
    private void Awake()
    {
        inventoryCanvasGroup.alpha = 0;
        inventoryCanvasGroup.blocksRaycasts = false;
        inventoryCanvasGroup.interactable = false;
    }

    private void OnEnable()
    {
        OnAddItemDefinition += Add;
        OnAddItemId += Add;

        OnChangeInventory += ChangeInventoryState;

        OnItemSelected += SetItemDescription;
    }

    private void OnDisable()
    {
        OnAddItemDefinition -= Add;
        OnAddItemId -= Add;
        
        OnChangeInventory -= ChangeInventoryState;
        
        OnItemSelected -= SetItemDescription;
    }
    
    public void SetDialogueBool(bool _bool)
    {
        _dialogueActive = _bool;
    }
    
    public void SetPauseBool(bool _bool)
    {
        _pauseActive = _bool;
    }
    
    public void SetItemDescription(ItemDefinition itemDefenition) //extra
    {
        itemDescriptionText.SetText("");
        itemHeaderText.SetText("");
        options.SetActive(false);
        itemDescriptionText.SetText(itemDefenition.description);
        itemHeaderText.SetText(itemDefenition.displayName);

        if (itemDefenition.itemType == ItemDefinition.ItemType.Consumable)
        { 
            options.SetActive(true);
        }
    }
    
    private void ChangeInventoryState()
    { 
        if (_dialogueActive) return;
        
        if (_pauseActive) return;
        
        itemDescriptionText.SetText("");
        itemHeaderText.SetText("");
        options.SetActive(false);
        
        float alpha = inventoryCanvasGroup.alpha;
        float time = Time.timeScale;

        inventoryCanvasGroup.blocksRaycasts = alpha == 0;
        inventoryCanvasGroup.interactable = alpha == 0;
        inventoryCanvasGroup.alpha = alpha == 0 ? 1 : 0;

        if (inventoryCanvasGroup.alpha == 0)
        {
            player.GetComponent<PlayerInput>().EnableInput();
        }
        else
        {
            player.GetComponent<PlayerInput>().DisableInput();

            InventoryManager.Instance.SetInventoryItems(items);
        }
    }

    public void RefreshInventory()
    {
        InventoryManager.Instance.SetInventoryItems(items);
    }
    
    public Item GetItem(string id)
    {
        foreach (var item in items)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
    
    public void Add(ItemDefinition itemDefenition, int amount)
    {
        Add(itemDefenition.id, amount);
    }

    public void Add(string id, int amount)
    {
        if(!ValidateItem(id)) return;

        Item item = GetItem(id);

        if (item == null)
        {
            items.Add(new Item(id, amount));
        }
        else
        {
            item.amount += amount;
            //TODO Check for error
        }
        
        
        InventoryManager.Instance.SetInventoryItems(items);
    }
    
    public void RemoveWhat(string id) //extra
    {
        int currentIndex = 0;
        
        foreach (var _item in items)
        {
            if (_item.id == id)
            {
                _index = currentIndex;

                return;
            }
            
            currentIndex++;
        }
    }
    
    public void RemoveHowMuch(int removeAmount) //extra
    {
        items[_index].amount -= removeAmount;

        if (items[_index].amount <= 0)
        {
            items.RemoveAt(_index);
            //GetComponent<TurnOffDescription>().TurnOff();
        }
            
        InventoryManager.Instance.SetInventoryItems(items);
    }

    private bool ValidateItem(string itemId)
    {
        if (string.IsNullOrWhiteSpace(itemId) || string.IsNullOrEmpty(itemId))
        {
            Debug.LogError("Item id is null or empty.");
            return false;
        }
        
        //TODO: Check if item exists

        return true;

    }

    public void RemoveLanternFromList(string id)
    {
        int currentIndex = 0;
        
        foreach (var _item in items)
        {
            Debug.Log(_item);
            if (_item.id == id)
            {
                Debug.Log("found lanter A");
                items.RemoveAt(currentIndex);
                
                InventoryManager.Instance.SetInventoryItems(items);

                
                return;
            }
            
            currentIndex++;
        }
    }
}