using UnityEngine;

public class NoSpellEquipped : MonoBehaviour
{
    public ArrowPressed equippedButton;
    
    public SpellDefinition[] baseSpells = new SpellDefinition[4];
    
    public SpellDefinition currentSpell;
    public SpellDefinition newSpell;
    
    public SpellManager spellManager;
    
    private SpellEquipping _spellEquipping;


    public void UnequippSpell()
    {
       currentSpell = spellManager.ReturnSpell(equippedButton);

       if (currentSpell == null)
       {
           return;
       }
       
       if (currentSpell.Id.Contains("dart"))
       {
           newSpell = baseSpells[0];
       }
       else if (currentSpell.Id.Contains("guard"))
       {
           newSpell = baseSpells[1];
       }
       else if (currentSpell.Id.Contains("orb"))
       {
           newSpell = baseSpells[2];
       }
       else if (currentSpell.Id.Contains("healing"))
       {
           newSpell = baseSpells[3];
       }
       
       spellManager.WhichSpell(equippedButton, newSpell.Id);
    }
 }
