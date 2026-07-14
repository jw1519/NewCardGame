using Character;
using UnityEngine;
namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Fire Enemy")]
    public class AddStatusEffectEnemy : BaseEnemy
    {
        public int Firedamage;
        public int Fireduration;
        public override void UseAbility(GameObject target)
        {
            SetCharacterUI characterUI = target.GetComponent<SetCharacterUI>();
            //EventQueue.EnqueueEvent(new EnemyAddStatusEffectEvent(abilityAmount, "Burn", characterUI));
        }
    }
}
