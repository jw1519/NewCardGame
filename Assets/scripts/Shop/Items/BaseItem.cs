using Character;
using UnityEngine;

namespace Item
{
    public abstract class BaseItem : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemName;
        public int itemCost;
        public bool isBought;
        public BaseCharacter character;

        public void Awake()
        {
            character = AssetManager.Instance.GetAsset("Player").GetComponent<BaseCharacter>();
        }
        public virtual void Use()
        {
            if (isBought)
            {
                Debug.Log("Use");
            }
        }
    }
}
