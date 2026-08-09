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
    
    public Image leftImageHUD;
    public Image rightImageHUD;
    public Image upImageHUD;
    public Image downImageHUD;
    
    public ArrowPressed arrowPressed = ArrowPressed.none;
    
    public List<SpellDefinition> allSpells = new List<SpellDefinition>();

    private bool _allowImageSetting = false;

    private void Awake()
    {
        StartCoroutine(SetDefault());

        foreach (SpellDefinition spell in allSpells)
        {
            spell.cooldownState = SpellDefinition.CooldownState.cooled;
        }
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

    private void Update()
    {
        if (_allowImageSetting)
        {
            SetCooldownGrey();
        }
    }

    private void SetCooldownGrey()
    {
        if (leftSpell.cooldownState == SpellDefinition.CooldownState.cooling)
        {
            Color alpha = leftImage.color;
            alpha.a = 0.5f;
            leftImage.color = alpha;
            
            Color alphaHUD = leftImageHUD.color;
            alphaHUD.a = 0.5f;
            leftImageHUD.color = alpha;
        }
        else
        {
            Color alpha = leftImage.color;
            alpha.a = 1f;
            leftImage.color = alpha;
            
            Color alphaHUD = leftImageHUD.color;
            alphaHUD.a = 1f;
            leftImageHUD.color = alpha;
        }
        
        if (rightSpell.cooldownState == SpellDefinition.CooldownState.cooling)
        {
            Color alpha = rightImage.color;
            alpha.a = 0.5f;
            rightImage.color = alpha;
            
            Color alphaHUD = rightImageHUD.color;
            alphaHUD.a = 0.5f;
            rightImageHUD.color = alpha;
        }
        else
        {
            Color alpha = rightImage.color;
            alpha.a = 1f;
            rightImage.color = alpha;
            
            Color alphaHUD = rightImageHUD.color;
            alphaHUD.a = 1f;
            rightImageHUD.color = alpha;
        }
        
        if (upSpell.cooldownState == SpellDefinition.CooldownState.cooling)
        {
            Color alpha = upImage.color;
            alpha.a = 0.5f;
            upImage.color = alpha;
            
            Color alphaHUD = upImageHUD.color;
            alphaHUD.a = 0.5f;
            upImageHUD.color = alpha;
        }
        else
        {
            Color alpha = upImage.color;
            alpha.a = 1f;
            upImage.color = alpha;
            
            Color alphaHUD = upImageHUD.color;
            alphaHUD.a = 1f;
            upImageHUD.color = alpha;
        }
        
        if (downSpell.cooldownState == SpellDefinition.CooldownState.cooling)
        {
            Color alpha = downImage.color;
            alpha.a = 0.5f;
            downImage.color = alpha;
            
            Color alphaHUD = downImageHUD.color;
            alphaHUD.a = 0.5f;
            downImageHUD.color = alpha;
        }
        else
        {
            Color alpha = downImage.color;
            alpha.a = 1f;
            downImage.color = alpha;
            
            Color alphaHUD = downImageHUD.color;
            alphaHUD.a = 1f;
            downImageHUD.color = alpha;
        }
    }
    
    private void SetImages()
    {
        leftImage.sprite = leftSpell.sprite;
        rightImage.sprite = rightSpell.sprite;
        upImage.sprite = upSpell.sprite;
        downImage.sprite = downSpell.sprite;
        
        leftImageHUD.sprite = leftSpell.sprite;
        rightImageHUD.sprite = rightSpell.sprite;
        upImageHUD.sprite = upSpell.sprite;
        downImageHUD.sprite = downSpell.sprite;
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
        if (spell == leftSpell || spell == rightSpell || spell == upSpell || spell == downSpell)
        {
            Debug.Log("already got that spell you oaf!");
            return;
        }
        
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
