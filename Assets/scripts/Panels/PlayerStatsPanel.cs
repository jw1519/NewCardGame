using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Character
{
    public class PlayerStatsPanel : BasePanel
    {
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI roundText;

        public int maxItemAmount;

        public Transform itemContainer;
        private void OnEnable()
        {
            GameManager.updateRounds += UpdateRoundUI;
        }

        public void UpdatePlayerHealthUI(int health, int maxHealth)
        {
            healthText.text = health + "/" + maxHealth.ToString();
        }
        public void UpdateGoldUI(int goldAmount)
        {
            goldText.text = goldAmount.ToString() + "g";
        }
        public void UpdateRoundUI(int round)
        {
            roundText.text = "Round " + round.ToString();
        }

        public void AddItem(GameObject item)
        {
            if (itemContainer.childCount < maxItemAmount)
            {
                item.transform.SetParent(itemContainer);
            }
            
        }
    }
}
