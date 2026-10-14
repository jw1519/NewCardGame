using Character;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public int abilityValue;

        public virtual void Equip()
        {
            if (characterUI == null) characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
        }
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
