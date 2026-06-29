using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability Card/Heal")]
    public class HealCard : AbilityCard
    {
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerHealEvent(player, abilityPower));
        }
    }
}
