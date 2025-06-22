using Character;
using UnityEngine;

namespace Item
{
    public abstract class BaseItem : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemName;
        public int itemCost;
        public BaseCharacter character;

        private void Awake()
        {
            character = FindAnyObjectByType<BaseCharacter>();
        }
    }
}
