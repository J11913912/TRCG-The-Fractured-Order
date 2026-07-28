using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "LuckyFeli/Inventory/SpellIcon")]
public class SpellDefinition : ScriptableObject
{
    public string id;
    
    public Sprite sprite;
    
    public string displayName;
    
    [TextArea(3,10)]
    public string description;
}