using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Item
{
    public class SetItemUI : MonoBehaviour
    {
        public BaseItem item;
        

        private void OnEnable()
        {
            if (item.itemSprite != null)
            {
                GetComponent<Image>().sprite = item.itemSprite;
            }
            item.costText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateCostUI()
        {
            item.costText.text = item.itemName + " " + item.itemCost.ToString() + "g";
        }

    }
}
