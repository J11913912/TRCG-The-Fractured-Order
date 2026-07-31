using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public SpellDefinition leftSpell;
    public SpellDefinition rightSpell;
    public SpellDefinition upSpell;
    public SpellDefinition downSpell;
    
    public Image leftImage;
    public Image rightImage;
    public Image upImage;
    public Image downImage;
    
    public ArrowPressed arrowPressed = ArrowPressed.none;

    
    public List<SpellDefinition> allSpells = new List<SpellDefinition>();

    private void Awake()
    {
        SetImages();
    }
    
    private void SetImages()
    {
        leftImage.sprite = leftSpell.sprite;
        rightImage.sprite = rightSpell.sprite;
        upImage.sprite = upSpell.sprite;
        downImage.sprite = downSpell.sprite;
    }

    public void WhichSpell(ArrowPressed key, string Id)
    {
        ArrowPressed _key = key;
        
        foreach (SpellDefinition spell in allSpells)
        {
            if (spell.Id == Id)
            {
                SetSpell(spell, key);
            }
        }
    }

    private void SetSpell(SpellDefinition spell, ArrowPressed key)
    {
        string path = "";
        
        switch (key)
        {
            case ArrowPressed.left:
                leftSpell = spell;
                path = "<Keyboard>/leftArrow";
                break;
            case ArrowPressed.right:
                rightSpell = spell;
                path = "<Keyboard>/rightArrow";
                break;
            case ArrowPressed.up:
                upSpell = spell;
                path = "<Keyboard>/upArrow";
                break;
            case ArrowPressed.down:
                downSpell = spell;
                path = "<Keyboard>/downArrow";
                break;
        }
        SetImages();
        PlayerInput.OnChangeBinding?.Invoke(spell, path);
    }

    public SpellDefinition ReturnSpell(ArrowPressed key)
    {
        SpellDefinition spell = leftSpell;
        
        switch (key)
        {
            case ArrowPressed.left: 
                spell = leftSpell;
                break;
            case ArrowPressed.right:
                spell = rightSpell;
                break;
            case ArrowPressed.up:
                spell = upSpell;
                break;
            case ArrowPressed.down: 
                spell = downSpell;
                break;
        }

        return spell;
    }
    
}
