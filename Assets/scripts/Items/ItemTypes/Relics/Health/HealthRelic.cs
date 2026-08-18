using Character;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/Health")]
    public class HealthRelic : Relic
    {
        public int healAmount;

        public override void Equip()
        {
            characterUI.character.maxHealth += abilityValue;
            characterUI.character.health += abilityValue;
            characterUI.UpdateHealthUI();
        }
        public override void UnEquip()
        {
            characterUI.character.maxHealth -= abilityValue;
            characterUI.character.health -= abilityValue;
            characterUI.gameObject.GetComponent<SetCharacterUI>().UpdateHealthUI();
        }
    }
}
