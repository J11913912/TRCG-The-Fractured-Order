using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnButtonSelect : MonoBehaviour
{
    public Button thisButton;
    public ShopInventory shopInventory;

    public string description;

    public bool isInventorySlot = false;
    
    private void Update()
    {
        GameObject currentSelect = EventSystem.current.currentSelectedGameObject;

        if (currentSelect == this.gameObject)
        {
            Debug.Log("slot sleceted");
            OnSelect();
        }
    }

    private void OnSelect()
    {
        if (isInventorySlot)
        {
            
        }
        
        shopInventory.SetDescription(description);
    }
}
