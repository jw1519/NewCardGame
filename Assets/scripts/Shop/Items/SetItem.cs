using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class SetItemUI : MonoBehaviour
    {
        public BaseItem item;
        public TextMeshProUGUI costText;


        private void OnEnable()
        {
            if (item.itemSprite != null)
            {
                GetComponent<Image>().sprite = item.itemSprite;
            }
            costText = GetComponentInChildren<TextMeshProUGUI>();
            UpdateUI();
        }
        

        public void Buy()
        {
            bool canBuy = ShopManager.instance.CanBuy(item.itemCost);
            if (canBuy && !item.isBought)
            {
                costText.enabled = false;
                UIManager.instance.panelList.Find(panel => panel.name == "PlayerStatsPanel").gameObject.GetComponent<PlayerStatsPanel>().AddItem(gameObject);
                item.isBought = true;
            }
        }

        public void UpdateUI()
        {
            costText.text = item.itemName + " " + item.itemCost.ToString() + "g";
        }

    }
}
