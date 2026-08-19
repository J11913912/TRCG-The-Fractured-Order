using System;
using TMPro;
using UnityEngine;
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


    private void Awake()
    {
        button.Select();
        
        // TODO make it when talking to hoardinger only
        // TODO make sure the spell casting via arrow keys only happens when not in menus
    }

    private void Update()
    {
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

    public void SetHealthDesc()
    {
        description.SetText("Healf to go");
    }
    
    public void SetManaDesc()
    {
        description.SetText("Magic to go");
    }
    
    public void SetCostumisableDesc()
    {
        description.SetText("a new style");
    }
    
    public void SetSpellDesc()
    {
        description.SetText("crystal shield spell");
    }
    
    
}
