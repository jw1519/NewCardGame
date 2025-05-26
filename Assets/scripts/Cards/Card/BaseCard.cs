using UnityEngine;

namespace Card
{
    public abstract class BaseCard : ScriptableObject
    {
        public Sprite cardSprite;
        public CardType cardType;
        public int cardEnergy;
        public string description;

        public enum CardType
        {
            Magic,
            Ranged,
            Melee
        }
        public virtual void Use(GameObject target)
        {
            Debug.Log("use card");
        }
    }
}
