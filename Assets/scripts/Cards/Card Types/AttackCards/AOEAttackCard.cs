using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/AOE Attack Card")]
    public class AOEAttackCard : AttackCard
    {
        public override void Awake()
        {
            description = "the card attacks all enemies for " + damage.ToString();
            player = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerAOEAttackEvent(damage));
        }
    }
}
