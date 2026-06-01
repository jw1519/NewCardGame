using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/AOE Attack Card")]
    public class AOEAttackCard : BaseCard
    {
        public int damage;
        public override void Awake()
        {
            base.Awake();
            description = "the card attacks all enemies for " + damage.ToString();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerAOEAttackEvent(damage));
        }
    }
}
