using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Defence Card")]
    public class DefenceCard : BaseCard, IDefence
    {
        public int defenceAmount;
        private void Awake()
        {
            description = "the card attacks for " + defenceAmount.ToString();
        }
        public override void Use(GameObject target)
        {
            BaseCharacter character = target.GetComponent<SetCharacterUI>().character;
            EventQueue.EnqueueEvent(new PlayerDefenceEvent(character, defenceAmount));
        }
    }
}
