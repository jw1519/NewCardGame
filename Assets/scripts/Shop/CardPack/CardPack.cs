using UnityEngine;

namespace Card
{
    public class CardPack : MonoBehaviour
    {
        public int packCost;
        public int amountCardsContained;
        public CardPackType cardPackType;

        public enum CardPackType
        {
            Attack,
            Defence,
            Ability
        }

        public void Buy()
        {
            bool canBuy = ShopManager.instance.CanBuy(packCost);
            if (canBuy)
            {
                ShopManager.instance.OpenCardPack(amountCardsContained);
                Destroy(gameObject);
            }
        }
    }
}
