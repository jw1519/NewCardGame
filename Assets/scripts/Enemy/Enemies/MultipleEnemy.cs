using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Enemy Split")]
    // enemy seperates into multiple enemies at low health
    public class MultipleEnemy : BaseEnemy
    {
        public BaseEnemy enemy;
        public int enemyToSpawnAmount;

        public override void UseAbility(GameObject target)
        {
            base.UseAbility(target);
        }
        public override void TakeDamage(int damageTaken)
        {
            base.TakeDamage(damageTaken);
            if (health <= maxHealth/2)
            {
                EventQueue.EnqueueEvent(new EnemySplitEvent(enemy, this, enemyToSpawnAmount));
            }
        }
    }
}
