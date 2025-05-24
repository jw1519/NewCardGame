using Character;
namespace Enemy
{
    public class EnemyAttackEvent : GameEvent
    {
        public BaseCharacter Target;
        public int Damage;

        public EnemyAttackEvent(BaseCharacter target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
    public class EnemyDefenceEvent : GameEvent
    {
        public BaseEnemy Target;
        public int Defence;

        public EnemyDefenceEvent(BaseEnemy target, int defence)
        {
            Target = target;
            Defence = defence;
        }
    }
}