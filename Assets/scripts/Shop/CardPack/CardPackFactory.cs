using TMPro;
using UnityEngine;

namespace Card
{
    public class CardPackFactory : MonoBehaviour
    {
        public static CardPackFactory instance;
        public GameObject cardPackGO;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public GameObject CreateCardPack(CardPack.CardPackType type)
        {
            GameObject instance = Instantiate(cardPackGO);
            CardPack cardPack = instance.GetComponent<CardPack>();
            cardPack.cardPackType = type;
            instance.GetComponentInChildren<TextMeshProUGUI>().text = cardPack.packCost.ToString() + "g";
            return instance;
        }
    }
}
