using Character;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public int abilityValue;
        public bool isEquiped;

        public virtual void Equip()
        {
            if (characterUI == null) characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
            isEquiped = true;
        }
        public virtual void UnEquip()
        {
            isEquiped = false;
        }
    }
    public enum RelicType
    {
        Health,
        Damage,
        Defense,
        Gold,
    }
}
