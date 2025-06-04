using Card;
using UnityEngine;

namespace Enemy
{
    public class SelectEnemy : MonoBehaviour
    {
        SetEnemyUI enemy;

        private void Awake()
        {
            enemy = GetComponent<SetEnemyUI>();
        }
        public void OnClick()
        {
            SelectManager.instance.SelectEnemy(enemy);
        }
    }
}
