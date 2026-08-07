using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public PlayerInput playerInput;
    
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

    private bool _allowImageSetting = false;

    private void Awake()
    {
        StartCoroutine(SetDefault());
    }

    private IEnumerator SetDefault()
    {
        yield return new WaitForEndOfFrame();
        WhichSpell(ArrowPressed.up, "spell_dart");
        WhichSpell(ArrowPressed.down, "spell_orb");
        WhichSpell(ArrowPressed.left, "spell_guard");
        WhichSpell(ArrowPressed.right, "spell_healing");
        
        yield return new WaitForEndOfFrame();
        _allowImageSetting = true;
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
        SpellDefinition oldSpell = null;
        
        string path = "";
        
        switch (key)
        {
            case ArrowPressed.left:
                if (_allowImageSetting)
                {
                    oldSpell = leftSpell;
                }
                leftSpell = spell;
                path = "<Keyboard>/leftArrow";
                break;
            case ArrowPressed.right:
                if (_allowImageSetting)
                {
                    oldSpell = rightSpell;
                }
                rightSpell = spell;
                path = "<Keyboard>/rightArrow";
                break;
            case ArrowPressed.up:
                if (_allowImageSetting)
                {
                    oldSpell = upSpell;
                }
                upSpell = spell;
                path = "<Keyboard>/upArrow";
                break;
            case ArrowPressed.down:
                if (_allowImageSetting)
                {
                    oldSpell = downSpell;
                }
                downSpell = spell;
                path = "<Keyboard>/downArrow";
                break;
        }
        
        if (_allowImageSetting)
        {
            SetImages();
        }
        
        
        playerInput.ChangeBinding(spell, path, oldSpell);
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
