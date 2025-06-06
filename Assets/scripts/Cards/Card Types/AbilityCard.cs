using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace Card
{
    public class AbilityCard : BaseCard
    {
        public int abilityPower;

        private void Awake()
        {
            player = FindFirstObjectByType<BaseCharacter>();
        }
    }
}
