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
        public GameObject statsPanel;

        [Header("Health")]
        public TextMeshProUGUI healthText;
        public Slider healthSlider;

        [Header("Energy")]
        public TextMeshProUGUI EnergyText;
        public Slider energySlider;

        [Header("Defence")]
        public TextMeshProUGUI defenceText;
        public GameObject defenceIcon;



        private void Awake()
        {
            healthText.text = character.health.ToString() + "/" + character.maxHealth.ToString();
            EnergyText.text = character.energy.ToString() + "/" + character.maxEnergy.ToString();
            healthSlider.maxValue = character.maxHealth;
            energySlider.maxValue = character.maxEnergy;

            healthSlider.value = character.health;
            energySlider.value = character.energy;

            spriteObject.sprite = character.characterSprite;
        }

        public void UpdateHealthUI()
        {
            healthText.text = character.health.ToString() + "/" + character.maxHealth.ToString();
            healthSlider.value = character.health;
            if (healthSlider.maxValue != character.maxHealth )
            {
                healthSlider.maxValue = character.maxHealth;
            }
            if (character.health <=0)
            {
                BasePanel panel = UIManager.instance.panelList.Find(panels => panels.name == "GameOverPanel");
                panel.OpenPanel();
                statsPanel.SetActive(false);
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
            if (character.defence > 0)
            {
                defenceIcon.SetActive(true);
            }
            defenceText.text = character.defence.ToString();
        }

    }
}
