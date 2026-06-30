using Card;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/Health")]
    public class HealthRelic : Relic
    {
        public BaseCard healthCard;
        public int healAmount;
        public Abilty abilty;
        public List<GameObject> healthCards;

        public override void Equip()
        {
            switch (abilty)
            {
                case Abilty.AddCard:
                    if (healthCards[0] == null)
                    {
                        healthCards.Clear();
                    }
                    if (healthCards.Count < 2)
                    {
                        GameObject instance = CardFactory.instance.CreateCard(healthCard);
                        healthCards.Add(instance);
                        healthCards.Add(Instantiate(instance));
                    }
                        AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(healthCards[0]);
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(healthCards[1]);
                    break;
            }
        }
        public override void UnEquip()
        {
           switch (abilty)
            {
                case Abilty.AddCard:
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().RemoveCard(healthCards[0]);
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().RemoveCard(healthCards[1]);
                    break;
            }
        }
        public enum Abilty
        {
            AddCard,

        }
    }
}
