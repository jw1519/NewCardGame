using Character;
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
        [HideInInspector] public PlayerStatsPanel statsPanel;
        public TextMeshProUGUI descriptionText;
        public Transform descriptionPanel;
        public GameObject discardButton;
        bool showButton = false;

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
            gameObject.SetActive(false);
            statsPanel.RemoveRelic(relic);
            relic = null;
        }
        public void displayDiscardButton()
        {
            if (showButton)
            {
                discardButton.SetActive(false);
                showButton = false;
            }
            else
            {
                discardButton.SetActive(true);
                showButton = true;
            }
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
