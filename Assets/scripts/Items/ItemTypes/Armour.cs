using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Armour", menuName = "Items/Armour")]
    public class Armour : BaseItem
    {
        public ArmourType armourType;
        public int defenceValue;
        public int healthBonus;
        public override void Use()
        {
            // Implement the logic to equip the armour
            Debug.Log($"Equipped {name} with {defenceValue} defence and {healthBonus} health.");
        }
    }
    public enum ArmourType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
}
