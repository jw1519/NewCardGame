using Character;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/Health")]
    public class HealthRelic : Relic
    {
        public int healAmount;
        public Abilty abilty;

        public override void Equip()
        {
            switch (abilty)
            {
                case Abilty.ExtraHealth:
                    characterUI.character.maxHealth += abilityValue;
                    characterUI.character.health += abilityValue;
                    characterUI.UpdateHealthUI();
                    break;
            }
        }
        public override void UnEquip()
        {
           switch (abilty)
            {
                case Abilty.ExtraHealth:
                    characterUI.character.maxHealth -= abilityValue;
                    characterUI.character.health -= abilityValue;
                    characterUI.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
                    break;
            }
        }
        public enum Abilty
        {
            ExtraHealth,
        }
    }
}
