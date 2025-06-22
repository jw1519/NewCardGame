using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/health potion")]

    public class HealthPotion : BaseItem
    {
        public int healAmount;
        public void Heal()
        {
            character.Heal(healAmount);
        }
    }
}