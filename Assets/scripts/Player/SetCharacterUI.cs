using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class SetCharacterUI : MonoBehaviour
    {
        [Header("Character")]
        public BaseCharacter character;
        BaseCharacter baseCharacter;
        public Image spriteObject;

        [Header("Health")]
        public TextMeshProUGUI healthText;
        public Slider healthSlider;

        [Header("Energy")]
        public TextMeshProUGUI EnergyText;
        public Slider energySlider;

        [Header("Defence")]
        public TextMeshProUGUI defenceText;
        public GameObject defenceIcon;

        [Header("Effects")]
        public List<GameObject> effectIcons;
        public Animator effectAnimator;

        PlayerStatsPanel playerStatsPanel;

        private void Start()
        {
            character = Instantiate(character);
            baseCharacter = character;

            character.animator = spriteObject.GetComponent<Animator>();
            character.animator.runtimeAnimatorController = character.animatorController;

            playerStatsPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>();
            NewRun();
        }
        private void OnEnable()
        {
            BaseCharacter.playerHealthChanged += UpdateHealthUI;
            BaseCharacter.playerDefenceChanged += UpdateDefenceUI;
            BaseCharacter.playerEnergyChanged += UpdateEnergyUI;
            BaseCharacter.AddEffectToPlayer += EnableStatusEffect;
            BaseCharacter.RemoveEffectToPlayer += RemoveStatusEffects;
        }
        public void OnDisable()
        {
            BaseCharacter.playerHealthChanged -= UpdateHealthUI;
            BaseCharacter.playerDefenceChanged -= UpdateDefenceUI;
            BaseCharacter.playerEnergyChanged -= UpdateEnergyUI;
            BaseCharacter.AddEffectToPlayer -= EnableStatusEffect;
            BaseCharacter.RemoveEffectToPlayer -= RemoveStatusEffects;
        }
        public void NewRun()
        {
            //Reset Stats
            character = baseCharacter;

            healthSlider.maxValue = character.maxHealth;
            energySlider.maxValue = character.maxEnergy;
            character.health = character.maxHealth;
            character.energy = character.maxEnergy;
            character.gold = 0;
            character.totalGoldCollected = 0;
            character.animator.SetBool("isAlive", true);

            UpdateEnergyUI();
            UpdateHealthUI();
            UpdateGoldUI();
            AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>().AddToCombat(gameObject);
        }
        public void UpdateHealthUI()
        {
            healthText.text = character.health.ToString() + "/" + character.maxHealth.ToString();
            GameObject panelStats = UIManager.instance.panelList.Find(panels => panels.name == "PlayerStatsPanel").gameObject;
            panelStats.GetComponent<PlayerStatsPanel>().UpdatePlayerHealthUI(character.health, character.maxHealth);

            healthSlider.maxValue = character.maxHealth;
            healthSlider.value = character.health;
            if (character.health <= 0)
            {
                BasePanel panel = UIManager.instance.panelList.Find(panels => panels.name == "GameOverPanel");
                //panel.gameObject.GetComponent<GameOverPanel>().PlayerStatsDisplay(character);
                panel.OpenPanel();
            }
        }
        public void UpdateEnergyUI()
        {
            EnergyText.text = character.energy.ToString() + "/" + character.maxEnergy.ToString();
            energySlider.value = character.energy;
            if (energySlider.maxValue != character.maxEnergy)
            {
                energySlider.maxValue = character.maxEnergy;
            }
        }
        public void UpdateDefenceUI()
        {
            defenceText.text = character.defence.ToString();
            if (character.defence == 0)
            {
                if (effectAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "ApplyShield")
                    EffectAnimation("defenceBreak");
            }
        }
        public void UpdateGoldUI()
        {
            playerStatsPanel.UpdateGoldUI(character.gold);
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
        public void EnableStatusEffect(StatusEffectData data)
        {
            GameObject icon = GetEffectIcon(data.effectName);
            if (icon != null)
            {
                icon.SetActive(true);
                icon.GetComponentInChildren<TextMeshProUGUI>().text = data.duration.ToString();
            }
            else
                Debug.LogWarning("Unknown status effect: " + data.effectName);
        }
        public void UpdateStatusEffectUI()
        {
            if (character.GetEffect("burn") != null)
            {
                StatusEffectData burnEffect = character.GetEffect("burn");
                GetEffectIcon("burn").GetComponentInChildren<TextMeshProUGUI>().text = burnEffect.duration.ToString();
                if (!character.GetEffect("burn")) RemoveStatusEffects("burn");
            }
        }
        public void RemoveStatusEffects(string effectName)
        {
            GameObject icon = GetEffectIcon(effectName);
            if (icon != null)
            {
                icon.SetActive(false);
            }
        }
        public void EffectAnimation(string effectName)
        {
            effectAnimator.gameObject.SetActive(true);
            effectAnimator.SetTrigger(effectName);
        }
    }
}
