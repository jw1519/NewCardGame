using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class SetCharacterUI : MonoBehaviour
    {
        [Header("Character")]
        public BaseCharacter character;
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

        PlayerStatsPanel playerStatsPanel;

        private void Start()
        {
            healthSlider.maxValue = character.maxHealth;
            energySlider.maxValue = character.maxEnergy;

            healthSlider.value = character.health;
            energySlider.value = character.energy;

            UpdateEnergyUI();
            UpdateHealthUI();

            playerStatsPanel = UIManager.instance.panelList.Find(panel => panel.name == "PlayerStatsPanel").gameObject.GetComponent<PlayerStatsPanel>();
            UpdateGoldUI();

            spriteObject.sprite = character.characterSprite;
        }
        private void OnEnable()
        {
            BaseCharacter.playerHealthChanged += UpdateHealthUI;
            BaseCharacter.playerDefenceChanged += UpdateDefenceUI;
        }

        public void UpdateHealthUI()
        {
            healthText.text = character.health.ToString() + "/" + character.maxHealth.ToString();
            healthSlider.value = character.health;
            GameObject panelStats = UIManager.instance.panelList.Find(panels => panels.name == "PlayerStatsPanel").gameObject;
            panelStats.GetComponent<PlayerStatsPanel>().UpdatePlayerHealthUI(character.health, character.maxHealth);
            if (healthSlider.maxValue != character.maxHealth )
            {
                healthSlider.maxValue = character.maxHealth;
            }
            if (character.health <=0)
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
            if (energySlider.maxValue != character.maxEnergy )
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
    }
}
