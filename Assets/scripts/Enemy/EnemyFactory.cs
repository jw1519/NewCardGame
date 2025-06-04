using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public static EnemyFactory instance;
        public GameObject enemyPrefab;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            GameObject instance = Instantiate(enemyPrefab);
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = enemy;
            return instance;
        }
    }
}
