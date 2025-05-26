using TMPro;
using UnityEngine;

namespace Card
{
    public class SetCardUI : MonoBehaviour
    {
        public BaseCard card;

        public TextMeshProUGUI cardTypeText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI energyText;
        private void Start()
        {
            cardTypeText.text = card.cardType.ToString();
            energyText.text = card.cardEnergy.ToString();
            descriptionText.text = card.description;
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
