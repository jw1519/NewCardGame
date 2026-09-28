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

        public GameObject itemFunctionPanel;
        public TextMeshProUGUI itemDescriptionText;

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
            bool canBuy = ShopManager.instance.ItemCanBeBought(item.itemCost);
            if (canBuy && !item.isBought)
            {
                costText.enabled = false;
                AssetManager.Instance.GetAsset("UIManager").GetComponent<UIManager>().GetPanel("PlayerStatsPanel").GetComponent<PlayerStatsPanel>().AddItem(gameObject);
                item.isBought = true;
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Button>().onClick.AddListener(OpenFunctionPanel);
                itemDescriptionText.text = item.description;
            }
        }
        public void OpenFunctionPanel()
        {
            if (item.isBought)
            {
                if (itemFunctionPanel.activeSelf == true)
                    itemFunctionPanel.SetActive(false);
                else
                    itemFunctionPanel.SetActive(true);
            }
        }
        public void Use()
        {
            if (item.isBought && CombatManager.inCombat == true)
            {
                item.Use();
                GetComponentInParent<PlayerStatsPanel>().RemoveItem(gameObject);
                Destroy(gameObject);
            }
        }
        public void Delete()
        {
            GetComponentInParent<PlayerStatsPanel>().RemoveItem(gameObject);
            Destroy(gameObject);
        }

        public void UpdateUI()
        {
            costText.text = item.itemName + " " + item.itemCost.ToString() + "g";
        }

    }
}
