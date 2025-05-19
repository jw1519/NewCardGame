using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class BaseCard : ScriptableObject
    {
        public Sprite cardSprite;
        public CardType cardType;
        public int cardEnergy;

        public enum CardType
        {
            Magic,
            Ranged,
            Melee
        }
    }
}
