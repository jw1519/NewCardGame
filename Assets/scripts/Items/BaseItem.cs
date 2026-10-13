using Character;
using UnityEngine;

namespace Item
{
    public abstract class BaseItem : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemName;
        public string description;
        public int itemCost;
        public bool isBought;
        public SetCharacterUI characterUI;
        public virtual void Use()
        {
            if (isBought)
            {
                Debug.Log("Use");
            }
        }
    }
}
