using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Enemy.BaseEnemy;

namespace Enemy
{
    public class SetEnemyUI : MonoBehaviour
    {
        BaseEnemy enemy;
        public Image spriteObject;

        [Header("Health")]
        public TextMeshProUGUI healthText;
        public Slider healthSlider;

        [Header("Defence")]
        public TextMeshProUGUI defenceText;
        public GameObject defenceIcon;

        [Header("Actions")]
        public Image actionSprite;
        public TextMeshProUGUI actionText;

        private void Awake()
        {
            enemy = GetComponent<BaseEnemy>();

            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.maxValue = enemy.maxHealth;
            healthSlider.value = enemy.health;

            spriteObject.sprite = enemy.enemySprite;
            SetEnemySprite(enemy.enemySprite);
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


        public void UpdateActionUI(EnemyAction action)
        {
            switch (enemy.action)
            {
                case EnemyAction.Attack:
                    actionSprite.sprite = enemy.attackSprite;
                    actionText.text = enemy.damage.ToString();
                    return;

                case EnemyAction.Defend:
                    actionSprite.sprite = enemy.defenceSprite;
                    actionText.text = enemy.defenceAmount.ToString();
                    return;

                case EnemyAction.Ability:
                    actionSprite.sprite = enemy.abilitySprite;
                    //actionText.text = enemy.healAmount.ToString();
                    return;
            }
        }
        public void SetEnemySprite(Sprite sprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
