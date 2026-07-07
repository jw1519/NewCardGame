using UnityEngine;
using Character;
using Enemy;


namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/Basic Card")]
    public class AttackCard : BaseCard
    {
        public int damage;
        int baseDamage;
        public override void Awake()
        {
            base.Awake();
            baseDamage = damage;
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseEnemy enemy = target.GetComponent<SetEnemyUI>().enemy;
            EventQueue.EnqueueEvent(new PlayerAttackEvent(enemy, damage, target.GetComponent<SetEnemyUI>()));
        }
        public void IncreaseDamage(float multiplier)
        {
            damage = Mathf.RoundToInt(damage * multiplier);
        }
        public void DecreaseDamage(float multiplier)
        {
            damage = Mathf.RoundToInt(damage / multiplier);
        }
        public void ResetDamage()
        {
            damage = baseDamage;
        }
}
}
