using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "LuckyFeli/Inventory/Item")]
public class ItemDefinition : ScriptableObject
{
    public string id;

    public enum ItemType {Tool, Consumable, Quest};
    
    public ItemType itemType;
    
    [Min(1)]
    public int stackingCap = 1;
    
    public Sprite sprite;

    public string displayName;
    
    [TextArea(3,10)]
    public string description;
}