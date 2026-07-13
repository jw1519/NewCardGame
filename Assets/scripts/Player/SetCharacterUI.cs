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
        public GameObject burnIcon;

        PlayerStatsPanel playerStatsPanel;

        private void Start()
        {
            baseCharacter = character;

            playerStatsPanel = AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>();
            spriteObject.sprite = character.characterSprite;
            NewRun();
        }
        private void OnEnable()
        {
            BaseCharacter.playerHealthChanged += UpdateHealthUI;
            BaseCharacter.playerDefenceChanged += UpdateDefenceUI;
            BaseCharacter.playerEnergyChanged += UpdateEnergyUI;
        }
        public void NewRun()
        {
            character = baseCharacter;

            healthSlider.maxValue = character.maxHealth;
            energySlider.maxValue = character.maxEnergy;
            character.health = character.maxHealth;
            character.energy = character.maxEnergy;

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
                panel.gameObject.GetComponent<GameOverPanel>().PlayerStatsDisplay(character);
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
        }
        public void UpdateGoldUI()
        {
            playerStatsPanel.UpdateGoldUI(character.gold);
        }
        public void UpdateStatusEffectUI(string effectName, int duration)
        {
            switch (effectName)
            {
                case "burn":
                    
                    break;
                default:
                    Debug.LogWarning("Unknown status effect: " + effectName);
                    break;
            }
        }
    }
}
