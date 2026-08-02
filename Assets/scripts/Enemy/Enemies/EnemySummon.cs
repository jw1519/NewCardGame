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
        public void OnEnemyDied()
        {
            // Remove the dead enemy from the list
            enemyList.RemoveAll(enemy => !enemy.isAlive);
        }
    }
}
