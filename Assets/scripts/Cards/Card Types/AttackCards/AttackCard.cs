using UnityEngine;
using Character;
using Enemy;


namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/Basic Card")]
    public class AttackCard : BaseCard
    {
        public int damage;
        public virtual void Awake()
        {
            description = "the card attacks for " +  damage.ToString();
            player = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseEnemy enemy = target.GetComponent<SetEnemyUI>().enemy;
            EventQueue.EnqueueEvent(new PlayerAttackEvent(enemy, damage, target.GetComponent<SetEnemyUI>()));
        }
    }
}
