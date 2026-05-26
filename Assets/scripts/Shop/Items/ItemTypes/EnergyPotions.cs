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
                Debug.Log(character);
                character.GainEnergy(energyAmount);
                Debug.Log("Used energy potion");
            }
        }
    }
}
