using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Enemy.BaseEnemy;

namespace Enemy
{
    public class SetEnemyUI : MonoBehaviour
    {
        public BaseEnemy enemy;
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

        [Header("Effects")]
        public GameObject burnSprite;
        private void Start()
        {
            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.maxValue = enemy.maxHealth;
            healthSlider.value = enemy.health;

            spriteObject.GetComponent<Animator>().runtimeAnimatorController = enemy.animatorController;
            enemy.animator = spriteObject.GetComponent<Animator>();
        }
        private void OnEnable()
        {
            enemyHealthChange += UpdateHealthUI;
            enemydied += EnemyDied;
            enemyDefenceChange += UpdateDefenceUI;
            addEffectToEnemy += EnableStatusEffect;
        }
        private void OnDestroy()
        {
            enemyHealthChange -= UpdateHealthUI;
            enemydied -= DisableUI;
            enemydied -= EnemyDied;
            enemyDefenceChange -= UpdateDefenceUI;
            addEffectToEnemy -= EnableStatusEffect;
        }
        public void UpdateHealthUI()
        {
            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.value = enemy.health;
            if (healthSlider.maxValue != enemy.maxHealth)
            {
                healthSlider.maxValue = enemy.maxHealth;
            }
            if (!enemy.isAlive)
            {
                EnemyDied();
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
        public void UpdateActionUI()
        {
            switch (enemy.action)
            {
                case EnemyAction.Attack:
                    actionSprite.sprite = enemy.attackSprite;
                    enemy.damage = Random.Range(enemy.minDamage, enemy.maxDamage);
                    actionText.text = enemy.damage.ToString();
                    return;

                case EnemyAction.Defend:
                    actionSprite.sprite = enemy.defenceSprite;
                    actionText.text = enemy.defenceAmount.ToString();
                    return;

                case EnemyAction.Ability:
                    actionSprite.sprite = enemy.abilitySprite;
                    actionText.text = "";
                    return;
            }
        }
        public void EnableStatusEffect(string effectname)
        {
            switch (effectname)
            {
                case "burn":
                    if (enemy.isBurning != true) return;
                    burnSprite.SetActive(true);
                    burnSprite.GetComponentInChildren<TextMeshProUGUI>().text = enemy.burnDuration.ToString();
                    return;
            }
        }
        public void UpdateStatusEffects()
        {
            if (enemy.isBurning && enemy.burnDuration-- != 0)
            {
                //update burn UI here
                burnSprite.SetActive(true);
                burnSprite.GetComponentInChildren<TextMeshProUGUI>().text = enemy.burnDuration.ToString();
                if (!enemy.isBurning)
                {
                    enemy.burnDuration = 0;
                    enemy.burnDamage = 0;
                    burnSprite.SetActive(false);
                }
            }
        }
        public void DisableUI()
        {
            foreach (Transform child in spriteObject.transform)
            {
                child.gameObject.SetActive(false);
            }
            enemy.ChangeAnimation("Die");
            GetComponent<Button>().enabled = false;
        }
        public void EnemyDied()
        {
            if (enemy.isAlive == false)
            {
                DisableUI();
                AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>().RemoveFromCombat(gameObject);
                GameObject gameWonPanel = UIManager.instance.panelList.Find(panel => panel.name == "GameWonPanel").gameObject;
                gameWonPanel.GetComponent<GameWonPanel>().UpdateGold(enemy.goldOnDefeat);
                gameWonPanel.GetComponent<GameWonPanel>().UpdateStats();
            }
        }
    }
}
