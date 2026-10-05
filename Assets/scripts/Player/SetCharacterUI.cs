using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Character
{
    public class SetCharacterUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISave
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
        public EffectDataPanel effectPanel;
        public List<GameObject> effectIcons;
        public Animator effectAnimator;

        PlayerStatsPanel playerStatsPanel;
        public void SetUp()
        {
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
            BaseCharacter.RemoveEffectToPlayer += RemoveStatusEffects;
        }
        public void OnDisable()
        {
            BaseCharacter.playerHealthChanged -= UpdateHealthUI;
            BaseCharacter.playerDefenceChanged -= UpdateDefenceUI;
            BaseCharacter.playerEnergyChanged -= UpdateEnergyUI;
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
                EffectAnimation("DefenceBreak");
                character.animator.SetBool("hasShield", false);
            }
            else
                character.animator.SetBool("hasShield", true);
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
            effectPanel.AddEffectUI(data);
        }
        public void UpdateStatusEffectUI()
        {
            if (character.activeEffects.Count == 0)
                RemoveAllStatusEffectsUI();
            foreach (GameObject icon in effectIcons)
            {
                StatusEffectData effectData = character.GetEffect(icon.name);
                if (effectData != null)
                {
                    icon.GetComponentInChildren<TextMeshProUGUI>().text = effectData.duration.ToString();
                }
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
        public void RemoveAllStatusEffectsUI()
        {
            foreach (GameObject icon in effectIcons)
            {
                icon.SetActive(false);
            }
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (character.activeEffects.Count == 0) return;
            effectPanel.OpenPanel();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            effectPanel.ClosePanel();
        }

        public void SaveData(ref GameData data)
        {
            data.characterData.maxHealth = character.maxHealth;
            data.characterData.maxEnergy = character.maxEnergy;
            data.characterData.health = character.health;
            data.characterData.energy = character.energy;
            data.characterData.defence = character.defence;
            data.characterData.gold = character.gold;
            data.characterData.totalGoldCollected = character.totalGoldCollected;

            if (data.characterData.activeEffects.Count > 0)
            {
                foreach (StatusEffectData effect in character.activeEffects)
                {
                    GameData.EffectData effectData = new GameData.EffectData
                    {
                        effectName = effect.effectName,
                        DOTAmount = effect.DOTAmount,
                        duration = effect.duration,
                        doesDamage = effect.doesDamage,
                        description = effect.description
                    };
                    data.characterData.activeEffects.Add(effectData);
                }
            }
        }

        public void LoadData(GameData data)
        {
            var saved = data.characterData;
            baseCharacter = ScriptableObject.CreateInstance<BaseCharacter>();
            character = baseCharacter;

            if (saved == null || string.IsNullOrEmpty(saved.characterName)) return;
            character.maxHealth = data.characterData.maxHealth;
            character.maxEnergy = data.characterData.maxEnergy;
            character.health = data.characterData.health;
            character.energy = data.characterData.energy;
            character.defence = data.characterData.defence;
            character.gold = data.characterData.gold;
            character.totalGoldCollected = data.characterData.totalGoldCollected;

            foreach (GameData.EffectData effectData in data.characterData.activeEffects)
            {
                StatusEffectData effect = ScriptableObject.CreateInstance<StatusEffectData>();
                effect.effectName = effectData.effectName;
                effect.DOTAmount = effectData.DOTAmount;
                effect.duration = effectData.duration;
                effect.doesDamage = effectData.doesDamage;
                effect.description = effectData.description;
                character.activeEffects.Add(effect);
            }
        }
    }
}
