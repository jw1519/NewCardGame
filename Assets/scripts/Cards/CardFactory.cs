using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardFactory : MonoBehaviour
    {
        public static CardFactory instance;
        public List<BaseCard> cardSO;
        public GameObject cardPrefab;
        public GameObject CreateCard(BaseCard card)
        {
            GameObject instance = Instantiate(cardPrefab);
            instance.GetComponent<SetCardUI>().card = Instantiate(card);
            instance.GetComponent<SetCardUI>().UpdateDescriptionText();
            return instance;
        }
        public GameObject CreateCard(string cardName)
        {
            BaseCard card = cardSO.Find(c => c.cardName == cardName);
            if (card == null)
            {
                Debug.LogError("Card not found: " + cardName);
                return null;
            }
            return CreateCard(card);
        }
    }
}
