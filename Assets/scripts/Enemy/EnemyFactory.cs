using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public static EnemyFactory instance;
        public GameObject enemyPrefab;

        public BaseEnemy enemy;
        public Transform enemyParent;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = Instantiate(enemy);
            GameObject instance = Instantiate(enemyPrefab);
            return instance;
        }
    }
}
