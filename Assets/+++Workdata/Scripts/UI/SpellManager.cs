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

    public void WhichSpell(ArrowPressed key, string id)
    {
        ArrowPressed _key = key;
        
        foreach (SpellDefinition spell in allSpells)
        {
            if (spell.id == id)
            {
                SetSpell(spell, key);
            }
        }
    }

    private void SetSpell(SpellDefinition spell, ArrowPressed key)
    {
        switch (key)
        {
            case ArrowPressed.left:
                leftSpell = spell;
                break;
            case ArrowPressed.right:
                rightSpell = spell;
                break;
            case ArrowPressed.up:
                upSpell = spell;
                break;
            case ArrowPressed.down:
                downSpell = spell;
                break;
        }
        SetImages();
    }
    
}
