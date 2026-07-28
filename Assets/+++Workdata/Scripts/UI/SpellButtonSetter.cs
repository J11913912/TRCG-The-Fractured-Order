using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonSetter : MonoBehaviour
{
    private Image _image;
    private string _spellName;
    private string _id;

    public SpellDefinition spell;
    
    private SpellEquipping _spellEquipping;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image.sprite = spell.sprite;
        
        _spellName = spell.displayName;
        
        _id = spell.id;
        
        _spellEquipping = GetComponent<SpellEquipping>();
    }

    public void GetId() // on click
    {
      _spellEquipping.EnterAssginMode(_id);
    }
    
}
