using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Ability Card/Cleanse Card")]
    public class CleanseCard : BaseCard
    {
        public override void Use(GameObject target)
        {
            base.Use(target);
            EventQueue.EnqueueEvent(new PlayerRemoveAllStatusEffectsEvent(characterUI.character));
        }
    }
}
