using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform enemyParent;

        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = Instantiate(enemy);
            GameObject instance = Instantiate(enemyPrefab);
            if (enemyParent != null)
            {
                instance.transform.SetParent(enemyParent);
            }
            return instance;
        }
    }
}
