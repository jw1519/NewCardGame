using UnityEngine;
using Character;
using Enemy;


namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack Card")]
    public class AttackCard : BaseCard, IAttack
    {
        public int damage;
        private void Awake()
        {
            description = "the card attacks for " +  damage.ToString();
        }
        public override void Use(GameObject target)
        {
            BaseEnemy enemy = target.GetComponent<BaseEnemy>();
            EventQueue.EnqueueEvent(new PlayerAttackEvent(enemy, damage, target.GetComponent<SetEnemyUI>()));
        }
    }
}
