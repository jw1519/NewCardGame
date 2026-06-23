using UnityEngine;

namespace Item
{
    public class Relic : BaseItem
    {
        public RelicType relicType;
        public RelicAbility relicAbility;
        public int abilityValue;

        public void Equip()
        {
            switch (relicAbility)
            {
                case RelicAbility.None:
                    break;
                case RelicAbility.HealthBoost:
                    break;
                case RelicAbility.DamageBoost:
                    break;
                case RelicAbility.DefenseBoost:
                    break;
                case RelicAbility.Heal:
                    break;
            }
        }
    }
    public enum RelicType
    {
        None,
        Health,
        Damage,
        Defense,
        Gold,
    }
    public enum RelicAbility
    {
        None,
        HealthBoost,
        DamageBoost,
        SpeedBoost,
        DefenseBoost,
        Shield,
        ExtraLife,
        Heal,
        GoldBoost,
    }
}
