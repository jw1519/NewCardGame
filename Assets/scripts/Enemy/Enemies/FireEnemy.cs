using Character;
using UnityEngine;
namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Fire Enemy")]
    public class FireEnemy : BaseEnemy
    {
        public int burnDamage;
        public int burnDuration;
        public override void UseAbility(GameObject target)
        {
            target.GetComponent<SetCharacterUI>().UpdateStatusEffectUI("Burn", burnDuration);
        }
    }
}
