using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public int abilityValue;
    }
    public enum RelicType
    {
        None,
        HealthBoost,
        DamageBoost,
        SpeedBoost,
        DefenseBoost,
        LifeSteal,
        Shield,
        ExtraLife,
        GoldBoost,
        ExperienceBoost
    }
}
