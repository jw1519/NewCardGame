using UnityEngine;
using Character;
using Enemy;


namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack Card")]
    public class AttackCard : BaseCard
    {
        public int damage;
        private void Awake()
        {
            description = "the card attacks for " +  damage.ToString();
            player = FindFirstObjectByType<BaseCharacter>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseEnemy enemy = target.GetComponent<SetEnemyUI>().enemy;
            EventQueue.EnqueueEvent(new PlayerAttackEvent(enemy, damage, target.GetComponent<SetEnemyUI>()));
        }
    }
}
