using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/energy potion")]
    public class EnergyPotions : BaseItem
    {
        public int energyAmount;
        public override void Use()
        {
            if (isBought)
            {
                RestoreEnergy();
                Debug.Log("Used energy potion");
            }
        }
    }
}
