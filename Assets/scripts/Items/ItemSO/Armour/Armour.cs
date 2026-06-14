using UnityEngine;

namespace Item
{     
    [CreateAssetMenu(fileName = "New Armour", menuName = "Item/Armour")]
    public class Armour : BaseItem
    {
        public int defence;
        public int durability;
        public ArmourType armourType;
        public SetType setType;
    }
    public enum ArmourType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }
    public enum SetType
    {
        Light,
        Medium,
        Heavy
    }
}
