using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public List<BaseEnemy> enemyList;
        public List<BaseEnemy> eliteList;

        public BaseEnemy GetEnemy(EnemyType type)
        {
            if (type == EnemyType.Basic)
            {
                int random = Random.Range(0, enemyList.Count);
                return enemyList[random];
            }
            else
            {
                int random = Random.Range(0, eliteList.Count);
                return eliteList[random];
            }

        }
        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = Instantiate(enemy);
            GameObject instance = Instantiate(enemyPrefab);
            return instance;
        }
    }
}
