using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/EnemySplit")]
    // enemy seperates into multiple enemies at low health
    public class MultipleEnemy : BaseEnemy
    {
        public BaseEnemy enemy;

        public override void UseAbility(GameObject target)
        {
            base.UseAbility(target);
        }
    }
}
