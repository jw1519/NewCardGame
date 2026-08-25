using Character;
using UnityEngine;

namespace Card
{
    public abstract class BaseCard : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;
        public CardType cardType;
        public int cardEnergy;
        public string description;
        public bool isInHand = false; //Check if card is in hand to prevent using it from discard pile or deck
        public bool isSingleUse = true; //Check if card is single use to allow is to ge in dead pile
        public bool usedOnEnemy;

        public CardRarety cardRarety;

        [HideInInspector] public SetCharacterUI characterUI;
        public virtual void Awake()
        {
            characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
        }
        public virtual void UpdateDescritpion()
        {

        }
        public enum CardType
        {
            Attack,
            Defence,
            Ability
        }
        public virtual void Use(GameObject target)
        {
            characterUI.character.UseEnergy(cardEnergy);
            characterUI.UpdateEnergyUI();
            isInHand = false;
        }
    }
    public enum CardRarety
    {
        common,
        uncommon,
        rare,
        unique,
    }
}
