using System.Collections.Generic;
using NUnit.Framework;
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
    }
}
