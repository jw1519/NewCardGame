using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Item
{
    public class RelicUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Relic relic;
        public Image itemImage;
        public TextMeshProUGUI descriptionText;
        public Transform descriptionPanel;

        public void SetRelic(Relic item)
        {
            relic = item;
            itemImage.sprite = item.itemSprite;
            descriptionText.text = item.description;
            item.Equip();
            gameObject.SetActive(true);
        }
        public void RemoveRelic()
        {
            relic.UnEquip();
            gameObject.SetActive(true);
            relic = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (relic != null)
                descriptionPanel.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (relic != null)
                descriptionPanel.gameObject.SetActive(false);
        }
    }
}
