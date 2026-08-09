using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Slime")]
    public class Slime : BaseEnemy
    {
        public override void UseAbility(GameObject target)
        {
            EventQueue.EnqueueEvent(new EnemyHealEvent(this, abilityAmount));
            Debug.Log("Slime healed for " + abilityAmount);
        }
    }
}
