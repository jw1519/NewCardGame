using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Slime")]
    public class Slime : BaseEnemy
    {
        public override void UseAbility(GameObject target)
        {
            Heal(abilityAmount);
        }
    }
}
