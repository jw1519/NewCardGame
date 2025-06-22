using Character;
using TMPro;
using UnityEngine;

namespace Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        public Sprite itemSprite;
        public string itemName;
        [Header("Cost")]
        public int itemCost;
        public TextMeshProUGUI costText;

        public void Buy()
        {
            bool canBuy = ShopManager.instance.CanBuy(itemCost);
            if (canBuy)
            {
                costText.enabled = false;
                UIManager.instance.panelList.Find(panel => panel.name == "PlayerStatsPanel").gameObject.GetComponent<PlayerStatsPanel>().AddItem(this);
            }
        }
    }
}
