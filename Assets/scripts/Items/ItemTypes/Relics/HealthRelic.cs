using Card;
using Character;
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
                    if (healthCards.Count == 0)
                    {
                        healthCards.Clear();
                    }
                    else if (healthCards[0]  == null)
                    {
                        healthCards.Clear();
                    }
                    if (healthCards.Count < 2)
                    {
                        GameObject instance = CardFactory.instance.CreateCard(Instantiate(healthCard));
                        healthCards.Add(instance);
                        healthCards.Add(Instantiate(instance));
                    }
                        AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(healthCards[0]);
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(healthCards[1]);
                    break;

                case Abilty.ExtraHealth:
                    GameObject player = AssetManager.Instance.GetAsset("Player");
                    player.GetComponent<BaseCharacter>().maxHealth += abilityValue;
                    player.GetComponent<BaseCharacter>().health += abilityValue;
                    player.GetComponent<SetCharacterUI>().UpdateHealthUI();
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
            ExtraHealth,

        }
    }
}
