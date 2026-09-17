using UnityEngine;
using Character;
using Enemy;
namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/AddStatusEffectCard")]
    public class StatusEffectCard : BaseCard
    {
        public StatusEffectData effectData;
        public override void Use(GameObject target)
        {
            if (target.GetComponent<SetCharacterUI>() != null && !usedOnEnemy)
            {
                base.Use(target);
                EventQueue.EnqueueEvent(new PlayerAddStatusEffectEvent(target, effectData));
            }
            else if (target.GetComponent<SetEnemyUI>() != null && usedOnEnemy)
            {
                base.Use(target);
                EventQueue.EnqueueEvent(new EnemyAddStatusEffectEvent(target, effectData));
            }
            else
            {
                Debug.Log(" cant use on " + target);
            }
        }
    }
}
