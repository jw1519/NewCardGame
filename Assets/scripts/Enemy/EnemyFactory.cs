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

            GameObject gameObject = CreateEnemy(enemy);
            gameObject.transform.SetParent(enemyParent);
            gameObject.transform.position = enemyParent.position;
        }

        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = Instantiate(enemy);
            GameObject instance = Instantiate(enemyPrefab);
            return instance;
        }
    }
}
