using Character;
using Enemy;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "Attack Card", menuName = "Cards/Attack/AttackAndAddEffect")]
    public class AttackCardWithEffect : AttackCard
    {
        public StatusEffectData effectData;
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new EnemyAddStatusEffectEvent(target, effectData));
        }
        public override void UpdateDescritpion()
        {
            description = $"Attack for {damage} damage and apply {effectData.effectName} for {effectData.duration}";
        }
    }
}
