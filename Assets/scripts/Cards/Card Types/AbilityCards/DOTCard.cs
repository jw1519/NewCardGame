using Character;
using Enemy;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New DOT Card", menuName = "Cards/DOT Card")]
    public class DOTCard : BaseCard
    {
        public StatusEffectData effectData;

        public override void Awake()
        {
            base.Awake();
            if (effectData == null)
            {
                Debug.LogWarning("Effect data is not assigned for " + cardName);
            }
            description = $"Applies {effectData.effectName} for {effectData.duration} turns, dealing {effectData.DOTAmount} damage per turn.";
        }

        public override void Use(GameObject target)
        {
            IEffectable effectable;
            if (target.GetComponent<SetCharacterUI>() != null)
            {
                effectable = target.GetComponent<SetCharacterUI>().character;
            }
            else if (target.GetComponent<SetEnemyUI>() != null)
            {
                effectable = target.GetComponent<SetEnemyUI>().enemy;
            }
            else
            {
                Debug.LogWarning("Target does not have a valid SetCharacterUI or SetEnemyUI component.");
                return;
            }
            if (effectable != null)
            {
                base.Use(target);
                effectable.ApplyEffect(Instantiate(effectData));
            }
            else
            {
                Debug.LogWarning("cannot use this effect on " + target);
            }
        }
    }
}
