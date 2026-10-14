using NUnit.Framework;
using TMPro;
using UnityEngine;
using Item;
using System.Collections.Generic;

namespace Character
{
    public class PlayerStatsPanel : BasePanel, ISave
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
            UpdateRoomUI(0);

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
            GameManager.updateRoomsCleared += UpdateRoomUI;
        }
        public void SetUp(BaseCharacter baseCharacter)
        {
            maxItemAmount = baseCharacter.maxItemAmount;
            itemAmountText.text = "0/" + maxItemAmount.ToString();
        }

        public void UpdatePlayerHealthUI(int health, int maxHealth)
        {
            healthText.text = health + "/" + maxHealth.ToString();
        }
        public void UpdateGoldUI(int goldAmount)
        {
            goldText.text = goldAmount.ToString() + "g";
        }
        public void UpdateRoomUI(int rooms)
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
                    Debug.Log("here");
                    transform.GetComponent<RelicUI>().SetRelic(Instantiate(item));
                    transform.gameObject.SetActive(true);
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

        public void SaveData(ref GameData data)
        {
            data.items.Clear();
            data.relics.Clear();
            //save relics and items
            if (itemContainer.childCount !=0)
            {
                foreach (Transform transform in itemContainer.transform)
                {
                    data.items.Add(transform.GetComponent<SetItemUI>().item.itemName);
                }
            }
            if (relicContainer.childCount != 0)
            {
                foreach (Transform transform in relicContainer.transform)
                {
                    if (transform.GetComponent<RelicUI>().relic != null)
                        data.relics.Add(transform.GetComponent<RelicUI>().relic.itemName);
                }
            }

        }
        public void LoadData(GameData data)
        {
            if (data.items.Count != 0)
            {
                foreach (string itemName in data.items)
                {
                    BaseItem item = Resources.Load<BaseItem>("ItemSO/" + itemName.Replace(" ", ""));
                    item.isBought = true;
                    GameObject gameObject = ItemFactory.instance.CreateItem(item);
                    gameObject.GetComponent<SetItemUI>().HasBeenBought();
                    AddItem(gameObject);
                }
            }
            if (data.relics.Count != 0)
            {
                foreach (string relicName in data.relics)
                {
                    Relic relic = Resources.Load<Relic>("Relics/" + relicName.Replace(" ", ""));
                    AddRelic(relic);
                }
            }
        }
    }
}
