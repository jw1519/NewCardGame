using UnityEngine;

namespace Card
{
    public class CardFactory : MonoBehaviour
    {
        public static CardFactory instance;
        public GameObject cardPrefab;
        public GameObject CreateCard(BaseCard card)
        {
            GameObject instance = Instantiate(cardPrefab);
            instance.GetComponent<SetCardUI>().card = Instantiate(card);
            return instance;
        }
    }
}
