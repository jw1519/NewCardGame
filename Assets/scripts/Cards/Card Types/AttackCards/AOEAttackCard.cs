using UnityEngine;
using Character;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Attack/AOE Attack Card")]
    public class AOEAttackCard : AttackCard, ICanUpgrade
    {
        public override void UpdateDescritpion()
        {
            description = "Attack all enemies for " + damage.ToString() + "damage";
        }
        public override void Use(GameObject target)
        {
            if (characterUI == null)
                characterUI = AssetManager.Instance.GetAsset("Player").GetComponent<SetCharacterUI>();
            characterUI.character.UseEnergy(cardEnergy);
            characterUI.UpdateEnergyUI();
            isInHand = false;
            EventQueue.EnqueueEvent(new PlayerAOEAttackEvent(characterUI.character, damage));
        }
    }
}
