using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class SetEnemyUI : MonoBehaviour
    {
        BaseEnemy enemy;

        [Header("Health")]
        public TextMeshProUGUI healthText;
        public Slider healthSlider;

        [Header("Defence")]
        public TextMeshProUGUI defenceText;
        public GameObject defenceIcon;

        private void Awake()
        {
            enemy = GetComponent<BaseEnemy>();

            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.maxValue = enemy.maxHealth;
            healthSlider.value = enemy.health;
        }

        public void UpdateHealthUI()
        {
            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.value = enemy.health;
            if (healthSlider.maxValue != enemy.maxHealth)
            {
                healthSlider.maxValue = enemy.maxHealth;
            }
        }
        public void UpdateDefenceUI()
        {
            if (enemy.defence > 0)
            {
                defenceIcon.SetActive(true);
            }
            defenceText.text = enemy.defence.ToString();
        }
    }
}
