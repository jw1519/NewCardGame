using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Strength Card")]
    public class StrengthCard : BaseCard
    {
        public StatusEffectData effectData;
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerAddStatusEffectEvent(target, effectData));
        }
    }
}
