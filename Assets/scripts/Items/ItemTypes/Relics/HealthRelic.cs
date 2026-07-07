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
                    character.maxHealth += abilityValue;
                    character.health += abilityValue;
                    character.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
                    break;
            }
        }
        public override void UnEquip()
        {
           switch (abilty)
            {
                case Abilty.ExtraHealth:
                    character.maxHealth -= abilityValue;
                    character.health -= abilityValue;
                    character.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
                    break;
            }
        }
        public enum Abilty
        {
            ExtraHealth,
        }
    }
}
