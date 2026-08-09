using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemySummon", menuName = "Enemy/EnemySummon")]
    public class EnemySummon : BaseEnemy
    {
        public BaseEnemy enemyToSpawn;
        public int maxEnemySummons;
        public List<BaseEnemy> enemyList; //list of alive enemy summons
        public AbilityTargetType ability2TargetType;
        private void OnEnable()
        {
            enemydied += OnEnemyDied;
        }
        private void OnDestroy()
        {
            enemydied -= OnEnemyDied;
        }
        public void SummonEnemy(BaseEnemy enemyToSummon)
        {
            enemyList.Add(enemyToSummon);
        }
        public override void UseAbility(GameObject target)
        {
            if (enemyToSpawn != null && enemyList.Count < maxEnemySummons)
            {
                EventQueue.EnqueueEvent(new EnemySummonEvent(this, enemyToSpawn));
                Debug.Log("Summon");
            }
            else
            {
                Debug.Log("do another ability");
            }

        }
        public void OnEnemyDied()
        {
            // Remove the dead enemy from the list
            enemyList.RemoveAll(enemy => !enemy.isAlive);
        }
    }
}
