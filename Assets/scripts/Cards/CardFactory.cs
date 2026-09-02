using Character;
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
            card.characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
            instance.GetComponent<SetCardUI>().card = Instantiate(card);
            return instance;
        }
    }
}
