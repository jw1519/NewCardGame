using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/AOE Attack Card")]
    public class AOEAttackCard : BaseCard, ICanUpgrade
    {
        public int damage;
        public int upgradedDamage;
        public int originalDamage;
        public override void Awake()
        {
            base.Awake();
            originalDamage = damage;
            description = "the card attacks all enemies for " + damage.ToString();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerAOEAttackEvent(characterUI.character, damage));
        }
        public void Upgrade()
        {
            damage = upgradedDamage;
        }
    }
}
