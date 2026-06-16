using UnityEngine;
using Enemy;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/Fire Attack")]

    public class FireAttackCard : AttackCard
    {
        public int burnDuration;
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseEnemy enemy = target.GetComponent<SetEnemyUI>().enemy;
            enemy.ApplyBurn(damage, burnDuration);
        }
    }
}
