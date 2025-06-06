using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Defence Card")]
    public class DefenceCard : BaseCard
    {
        public int defenceAmount;
        private void Awake()
        {
            description = "the card defends for " + defenceAmount.ToString();
            player = FindFirstObjectByType<BaseCharacter>();
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseCharacter character = target.GetComponent<SetCharacterUI>().character;
            EventQueue.EnqueueEvent(new PlayerDefenceEvent(character, defenceAmount));
        }
    }
}
