using UnityEngine;
using Character;
using Enemy;


namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/Basic Card")]
    public class AttackCard : BaseCard, ICanUpgrade
    {
        public int damage;
        public int upgradedDamage;
        int originalDamage;
        public override void Awake()
        {
            base.Awake();
            originalDamage = damage;
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseEnemy enemy = target.GetComponent<SetEnemyUI>().enemy;
            EventQueue.EnqueueEvent(new PlayerAttackEvent(characterUI.character, enemy, damage, target.GetComponent<SetEnemyUI>()));
        }
        public void Upgrade()
        {
            damage = upgradedDamage;
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
            damage = originalDamage;
        }
}
}
