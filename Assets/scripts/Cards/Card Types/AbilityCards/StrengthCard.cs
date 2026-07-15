using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Strength Card")]
    public class StrengthCard : BaseCard
    {
        public int strengthIncrease;
        public int duration;

        public override void Use(GameObject target)
        {
            base.Use(target);


        }
    }
}
