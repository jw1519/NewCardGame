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
        private void Start()
        {
            GameObject gameObject = CreateEnemy(enemy);
            gameObject.transform.SetParent(enemyParent);

        }
        public GameObject CreateEnemy(BaseEnemy enemy)
        {
            GameObject instance = Instantiate(enemyPrefab);
            enemyPrefab.GetComponent<SetEnemyUI>().enemy = Instantiate(enemy);
            return instance;
        }
    }
}
