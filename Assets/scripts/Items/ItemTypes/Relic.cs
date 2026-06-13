using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Relic", menuName = "Items/Relic")]
    public class Relic : BaseItem
    {

    }
    enum RelicType
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
