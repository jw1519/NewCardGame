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
            item = Instantiate(item);
            if (item.itemSprite != null)
            {
                GetComponent<Image>().sprite = item.itemSprite;
            }
            costText = GetComponentInChildren<TextMeshProUGUI>();
            UpdateUI();
        }
        

        public void Buy()
        {
            if (item.isBought) return;
            bool canBuy = ShopManager.instance.CanBuy(item.itemCost);
            if (canBuy && !item.isBought)
            {
                costText.enabled = false;
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>().AddItem(gameObject);
                item.isBought = true;
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Button>().onClick.AddListener(Use);
            }
        }
        public void Use()
        {
            if (item.isBought)
            {
                item.Use();
                GetComponentInParent<PlayerStatsPanel>().RemoveItem(gameObject);
                Destroy(gameObject);
            }
        }

        public void UpdateUI()
        {
            costText.text = item.itemName + " " + item.itemCost.ToString() + "g";
        }

    }
}
