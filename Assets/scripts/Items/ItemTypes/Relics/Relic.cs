using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public int abilityValue;

        public string description;

        public virtual void Equip() { }
        public virtual void UnEquip() { }
    }
    public enum RelicType
    {
        Health,
        Damage,
        Defense,
        Gold,
    }
}
