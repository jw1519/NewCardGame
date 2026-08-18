using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability Card/AddEnergyCard")]
    public class AddEnergyCard : AbilityCard
    {
        public override void Use(GameObject target)
        {
            base.Use(target);
            AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>().character.GainEnergy(abilityPower);
        }
    }
}
