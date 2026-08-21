using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{
    public int healthPotionsAmount;
    public int manaPotionsAmount;

    private int _costumisableAmount = 1;
    private int _spellAmount = 1;

    public TextMeshProUGUI healthPotionsText;
    public TextMeshProUGUI manaPotionsText;
    public TextMeshProUGUI costumisableText;
    public TextMeshProUGUI spellText;
    
    public TextMeshProUGUI description;

    public Button button;

    public int prizeHealthPotions;
    public int prizeManaPotions;
    public int prizeCostumisable;
    public int prizeSpell;


    private void OnEnable()
    {
        button.Select();
        
        // TODO make it when talking to hoardinger only
        // TODO make sure the spell casting via arrow keys only happens when not in menus
    }

    public void Focus()
    {
        Debug.Log("selcted buttox9rafwrdfgijuhrde");
        button.Select();
    }

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        
        healthPotionsText.SetText(healthPotionsAmount.ToString());
        manaPotionsText.SetText(manaPotionsAmount.ToString());
        costumisableText.SetText(_costumisableAmount.ToString());
        spellText.SetText(_spellAmount.ToString());
    }

    public void ChangeHealthPotions(int amount)
    {
        healthPotionsAmount += amount;

        if (healthPotionsAmount >= 0)
        {
            // disable
        }
    }

    public void ChangeManaPotions(int amount)
    {
        manaPotionsAmount += amount;
        
        if (manaPotionsAmount >= 0)
        {
            // disable
        }
    }

    public void BoughtCostumisable()
    {
        _costumisableAmount = 0;
        
        // disable
    }

    public void BoughtSpell()
    {
        _spellAmount = 0;
        
        // disable
    }

    public void SetDescription(string desc)
    {
        description.SetText(desc);
        description.SetText(desc);
    }
    
    
}
