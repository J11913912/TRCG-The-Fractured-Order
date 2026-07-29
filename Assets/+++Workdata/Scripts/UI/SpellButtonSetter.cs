using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonSetter : MonoBehaviour
{
    private Image _image;
    private string _spellName;
    private string _Id;

    public SpellDefinition spell;
    
    private SpellEquipping _spellEquipping;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image.sprite = spell.sprite;
        
        _spellName = spell.displayName;
        
        _Id = spell.Id;
        
        Debug.Log(_Id);
        
        _spellEquipping = GetComponent<SpellEquipping>();
    }

    public void GetId() // on click
    {
      _spellEquipping.EnterAssginMode(_Id);
    }
    
}
