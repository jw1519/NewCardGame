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
        public GameObject CreateCard(BaseCard card, Vector3 position, Quaternion rotation)
        {
            GameObject instance = Instantiate(cardPrefab, position, rotation);
            instance.GetComponent<SetCardUI>().card = Instantiate(card);
            return instance;
        }
    }
}
