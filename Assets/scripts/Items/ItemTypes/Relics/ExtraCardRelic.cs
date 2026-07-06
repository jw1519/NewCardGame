using UnityEngine;
using Card;
using System.Collections.Generic;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/ExtraCard")]
    public class ExtraCardRelic : Relic
    {
        public BaseCard extraCard;
        public int cardAmount;
        List<GameObject> cards = new List<GameObject>();

        public override void Equip()
        {
            if (cards.Count == 0 || cards[0] == null)
            {
                cards.Clear();
                for (int i = 0; i < cardAmount; i++)
                {
                    GameObject instance = CardFactory.instance.CreateCard(Instantiate(extraCard));
                    cards.Add(instance);
                }
            }
            for (int i = 0; i < cardAmount; i++)
            {
                AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(cards[i]);
            }
        }
        public override void UnEquip()
        {
            for (int i = 0; i < cardAmount; i++)
            {
                AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().RemoveCard(cards[i]);
            }
        }
    }
}
