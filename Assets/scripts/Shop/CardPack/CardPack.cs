using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardPack : MonoBehaviour
    {
        public int packCost;
        public int amountCardscontained;

        public void Buy()
        {
            bool canBuy = ShopManager.instance.CanBuy(packCost);
            if (canBuy)
            {
                ShopManager.instance.OpenCardPack();
                Destroy(gameObject);
            }
        }
    }
}
