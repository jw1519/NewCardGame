using Card;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relics/Health")]
    public class HealthRelic : Relic
    {
        public BaseCard healthCard;
        public int healAmount;
        public Abilty abilty;
        public override void Equip()
        {
            switch (abilty)
            {
                case Abilty.AddCard:
                    GameObject instance = CardFactory.instance.CreateCard(healthCard);
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(instance);
                    AssetManager.Instance.GetAsset("CardManager").GetComponent<CardManager>().AddCard(instance);
                    break;
            }
        }
        public enum Abilty
        {
            AddCard,

        }
    }
}
