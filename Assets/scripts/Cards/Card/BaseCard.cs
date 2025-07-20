using Character;
using Unity.VisualScripting;
using UnityEngine;

namespace Card
{
    public abstract class BaseCard : ScriptableObject
    {
        public Sprite cardSprite;
        public CardType cardType;
        public int cardEnergy;
        public string description;

        [HideInInspector] public BaseCharacter player;

        public enum CardType
        {
            Attack,
            Defence,
            Heal,
            Ability
        }
        public virtual void Use(GameObject target)
        {
            player.UseEnergy(cardEnergy);
            player.gameObject.GetComponent<SetCharacterUI>().UpdateEnergyUI();
            //CardManager.instance.DiscardCard()
        }
    }
}
