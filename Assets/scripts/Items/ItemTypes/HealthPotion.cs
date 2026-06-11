using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/health potion")]

    public class HealthPotion : BaseItem
    {
        public int healAmount;

        public override void Use()
        {
            if (isBought)
            {
                Heal();
                Debug.Log("Used health potion");
            }
        }
        public void Heal()
        {
            character.Heal(healAmount);
        }
    }
}