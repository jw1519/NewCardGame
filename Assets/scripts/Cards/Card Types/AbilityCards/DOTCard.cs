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
            var effectable = target.GetComponent<IEffectable>();
            if (effectable != null)
            {
                base.Use(target);
                effectable.ApplyEffect(effectData);
            }
            else
            {
                Debug.LogWarning("cannot use this effect on " + target);
            }
        }
    }
}
