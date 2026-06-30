using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public RelicAbility relicAbility;
        public int abilityValue;

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
    public enum RelicAbility
    {
        IncreaseMaxHealth,
        AddHealCards,



    }
}
