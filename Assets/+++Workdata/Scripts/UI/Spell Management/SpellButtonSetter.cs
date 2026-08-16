using System;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButtonSetter : MonoBehaviour
{
    private Image _image;
    private string _spellName;
    private string _spellDescription;
    private string _Id;

    public SpellDefinition spell;
    
    public Corner corner;
    
    private SpellEquipping _spellEquipping;
    public DisplaySpellInfos _displaySpellInfos;
    

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        _spellName = spell.displayName;
        
        _spellDescription = spell.description;
        
        _Id = spell.Id;
        
        _spellEquipping = GetComponent<SpellEquipping>();
    }

    public void GetId() // on click
    {
      _spellEquipping.EnterAssginMode(_Id);
    }

    private void Update()
    {
        GameObject currentSelect = EventSystem.current.currentSelectedGameObject;

        if (currentSelect == this.gameObject)
        {
            OnSelect();
        }
    }

    public void OnSelect()
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            _displaySpellInfos.SetDescription(corner, _spellName, _spellDescription);
        }
        else
        {
            _displaySpellInfos.SetEmpty(corner);
        }
        
    }

    public void UnlockedSpell()
    {
        _image.sprite = spell.sprite;
        gameObject.GetComponent<Button>().interactable = true;
    }
    
}
