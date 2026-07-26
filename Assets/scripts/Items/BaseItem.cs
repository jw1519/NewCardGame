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
        public SetCharacterUI characterUI;

        public virtual void Awake()
        {
            characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
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
