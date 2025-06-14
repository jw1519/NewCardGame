using UnityEngine;

namespace Card
{
    public class CardFactory : MonoBehaviour
    {
        public static CardFactory instance;
        public GameObject cardPrefab;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public GameObject CreateCard(BaseCard card)
        {
            GameObject instance = Instantiate(cardPrefab);
            instance.GetComponent<SetCardUI>().card = Instantiate(card);
            return instance;
        }
    }
}
