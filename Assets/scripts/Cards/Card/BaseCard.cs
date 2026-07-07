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

        [HideInInspector] public BaseCharacter player;
        public virtual void Awake()
        {
            player = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public enum CardType
        {
            Attack,
            Defence,
            Ability
        }
        public virtual void Use(GameObject target)
        {
            player.UseEnergy(cardEnergy);
            player.gameObject.GetComponent<SetCharacterUI>().UpdateEnergyUI();
            isInHand = false;
        }
    }
}
