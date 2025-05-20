using System.Collections;
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
        }

        public void UpdateHealthUI()
        {
            healthText.text = character.health.ToString() + "/" + character.maxHealth.ToString();
            healthSlider.value = character.health;
            if (healthSlider.maxValue != character.maxHealth )
            {
                healthSlider.maxValue = character.maxHealth;
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
            if (character.defence > 0)
            {
                defenceIcon.SetActive(true);
            }
        }

    }
}
