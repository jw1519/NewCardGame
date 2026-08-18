using Character;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Defence Card")]
    public class DefenceCard : BaseCard, ICanUpgrade
    {
        public int defenceAmount;
        int originalDefenceAmount;
        public int upgradedDefenceAmount;

        public override void Awake()
        {
            base.Awake();
            originalDefenceAmount = defenceAmount;
        }
        public override void Use(GameObject target)
        {
            base.Use(target);
            BaseCharacter character = target.GetComponent<SetCharacterUI>().character;
            EventQueue.EnqueueEvent(new PlayerDefenceEvent(character, defenceAmount, target.GetComponent<SetCharacterUI>()));
        }
        public void Upgrade()
        {
            defenceAmount = upgradedDefenceAmount;
        }
    }
}
