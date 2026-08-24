using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/AOE Attack Card")]
    public class AOEAttackCard : AttackCard, ICanUpgrade
    {
        public override void Awake()
        {
            base.Awake();
            originalDamage = damage;
            UpdateDescritpion();
        }
        public override void UpdateDescritpion()
        {
            description = "Attack all enemies for " + damage.ToString() + "damage";
        }
        public override void Use(GameObject target)
        {
            characterUI.character.UseEnergy(cardEnergy);
            characterUI.UpdateEnergyUI();
            isInHand = false;
            EventQueue.EnqueueEvent(new PlayerAOEAttackEvent(characterUI.character, damage));
        }
    }
}
