using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability/Cleanse Card")]
    public class CleanseCard : BaseCard
    {
        public override void Awake()
        {
            base.Awake();
            UpdateDescritpion();
        }
        public override void UpdateDescritpion()
        {
            description = "Cleanse character of all current status effects";
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerRemoveAllStatusEffectsEvent(characterUI.character));
        }
    }
}
