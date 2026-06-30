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
        public int maxRelicAmount;

        public Transform itemContainer;
        public TextMeshProUGUI itemAmountText;
        public void Start()
        {
            maxItemAmount = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>().maxItemAmount;
            itemAmountText.text = "0/" + maxItemAmount.ToString();
        }
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
                itemAmountText.text = (itemContainer.childCount + 1).ToString() + "/" + maxItemAmount.ToString();
                item.transform.SetParent(itemContainer);
            }
        }
        public void RemoveItem(GameObject item)
        {
            if (itemContainer.childCount > 0)
            {
                itemAmountText.text = (itemContainer.childCount - 1).ToString() + "/" + maxItemAmount.ToString();
                item.transform.SetParent(null);
            }
        }
        public void Pause()
        {
            AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PausePanel").OpenPanel();
        }
    }
}
