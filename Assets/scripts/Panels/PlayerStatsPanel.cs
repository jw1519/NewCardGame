using NUnit.Framework;
using TMPro;
using UnityEngine;
using Item;
using System.Collections.Generic;

namespace Character
{
    public class PlayerStatsPanel : BasePanel
    {
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI roomsClearedText;

        [Header("Items")]
        public int maxItemAmount;
        public Transform itemContainer;
        public TextMeshProUGUI itemAmountText;

        [Header("Relics")]
        public int maxRelicAmount;
        public Transform relicContainer;
        public GameObject relicPrefab;

        public void Start()
        {
            maxItemAmount = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>().maxItemAmount;
            itemAmountText.text = "0/" + maxItemAmount.ToString();
            UpdateRoundUI(0);

            for (int i = 0; i < maxRelicAmount; i++)
            {
                GameObject instance = Instantiate(relicPrefab);
                instance.GetComponent<RelicUI>().statsPanel = this;
                instance.transform.SetParent(relicContainer, false);
                instance.SetActive(false);
            }
        }
        private void OnEnable()
        {
            GameManager.updateRoomsCleared += UpdateRoundUI;
        }

        public void UpdatePlayerHealthUI(int health, int maxHealth)
        {
            healthText.text = health + "/" + maxHealth.ToString();
        }
        public void UpdateGoldUI(int goldAmount)
        {
            goldText.text = goldAmount.ToString() + "g";
        }
        public void UpdateRoundUI(int rooms)
        {
            roomsClearedText.text = "Rooms Cleared " + rooms.ToString();
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
        public void AddRelic(Relic item)
        {
            foreach (Transform transform in relicContainer.transform)
            {
                if (!transform.gameObject.activeSelf)
                {
                    transform.GetComponent<RelicUI>().SetRelic(item);
                    return;
                }
            }
        }
        public void RemoveRelic(Relic item)
        {
            //remove relic abilities
        }
        public void Pause()
        {
            AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PausePanel").OpenPanel();
        }
        public bool CanAddRelic()
        {
            foreach (Transform transform in relicContainer.transform)
            {
                if (!transform.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
