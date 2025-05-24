using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SelectEnemy : MonoBehaviour
    {
        BaseEnemy enemy;

        private void Awake()
        {
            enemy = GetComponent<BaseEnemy>();
        }
        public void OnClick()
        {
            SelectManager.instance.SelectEnemy(enemy);
        }
    }
}
