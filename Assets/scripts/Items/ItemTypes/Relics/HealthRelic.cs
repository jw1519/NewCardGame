using Card;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/Health")]
    public class HealthRelic : Relic
    {
        public BaseCard healthCard;
        public int healAmount;
        public override void Equip()
        {

        }
        public enum Abilty
        {
            AddCard,

        }
    }
}
