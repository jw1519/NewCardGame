using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class SetCardUI : MonoBehaviour
    {
        public BaseCard card;

        public TextMeshProUGUI cardTypeText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI energyText;
        public Image cardImage;
        private void Start()
        {
            cardTypeText.text = card.cardType.ToString();
            UpdateEnergyText();
            UpdateDescriptionText();

            name = card.name + "UI";

            if (card.cardSprite != null)
            {
                cardImage.sprite = card.cardSprite;
            }
        }
        public void UpdateDescriptionText()
        {
            descriptionText.text = card.description;
        }
        public void UpdateEnergyText()
        {
            energyText.text = card.cardEnergy.ToString();
        }
    }
}
