using NUnit.Framework;
using System.Collections.Generic;
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
        public Animator effectAnimator;
        public List<GameObject> effectIcons;
        private void Start()
        {
            enemy.health = enemy.maxHealth;
            healthText.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();
            healthSlider.maxValue = enemy.maxHealth;
            healthSlider.value = enemy.health;

            spriteObject.GetComponent<Animator>().runtimeAnimatorController = enemy.animatorController;
            enemy.animator = spriteObject.GetComponent<Animator>();

            if (enemy.abilityEffect != null)
            {
                enemy.abilityEffect = Instantiate(enemy.abilityEffect);
            }
        }
        private void OnEnable()
        {
            enemyHealthChange += UpdateHealthUI;
            enemydied += EnemyDied;
            enemyDefenceChange += UpdateDefenceUI;
            addEffectToEnemy += EnableStatusEffect;
            UpdateEffectToEnemy += UpdateStatusEffects;
            RemoveEffectToEnemy += RemoveStatusEffects;
        }
        private void OnDestroy()
        {
            enemyHealthChange -= UpdateHealthUI;
            enemydied -= DisableUI;
            enemydied -= EnemyDied;
            enemyDefenceChange -= UpdateDefenceUI;
            addEffectToEnemy -= EnableStatusEffect;
            UpdateEffectToEnemy -= UpdateStatusEffects;
            RemoveEffectToEnemy -= RemoveStatusEffects;
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
            defenceText.text = enemy.defence.ToString();
            if (enemy.defence == 0)
            {
                EffectAnimation("DefenceBreak");
            }
        }
        public void UpdateActionUI()
        {
            switch (enemy.action)
            {
                case EnemyAction.Attack:
                    actionSprite.sprite = enemy.attackSprite;
                    enemy.damage = Random.Range(enemy.minDamage, enemy.maxDamage);
                    if (enemy.GetEffect("Strength"))
                        enemy.damage = Mathf.RoundToInt(enemy.damage * 1.5f);
                    if (enemy.GetEffect("Weakness"))
                        enemy.damage = Mathf.RoundToInt(enemy.damage / 1.5f);
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
        public void EnableStatusEffect(StatusEffectData data)
        {
            if (enemy.activeEffects.Find(p => p.effectName == data.effectName) == null) return;
            GameObject icon = GetEffectIcon(data.effectName);
            if (icon != null)
            {
                icon.SetActive(true);
                icon.GetComponentInChildren<TextMeshProUGUI>().text = data.duration.ToString();
            }
            else
                Debug.LogWarning("Unknown status effect: " + data.effectName);
        }
        public void UpdateStatusEffects()
        {
            foreach(GameObject icon in effectIcons)
            {
                StatusEffectData effectData = enemy.GetEffect(icon.name);
                if (effectData != null)
                {
                    icon.GetComponentInChildren<TextMeshProUGUI>().text = effectData.duration.ToString();
                }
            }
        }
        public void RemoveStatusEffects(string effectname)
        {
            GameObject icon = GetEffectIcon(effectname);
            icon.SetActive(false);
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
        public GameObject GetEffectIcon(string name)
        {
            foreach (GameObject icon in effectIcons)
            {
                if (icon.name == name)
                    return icon;
            }
            return null;
        }
        public void EffectAnimation(string effectName)
        {
            effectAnimator.gameObject.SetActive(true);

            switch (effectName)
            {
                case "ApplyDefence":
                    effectAnimator.SetTrigger("ApplyDefence");
                    effectAnimator.SetBool("hasDefence", true);
                    return;
                case "DefenceBreak":
                    effectAnimator.SetBool("hasDefence", false);
                    return;
            }
        }
    }
}
