using Character;
namespace Enemy
{
    public class EnemyAttackEvent : GameEvent
    {
        public BaseCharacter Target;
        public int Damage;
        public SetEnemyUI EnemyUI;

        public EnemyAttackEvent(BaseCharacter target, int damage, SetEnemyUI enemyUI)
        {
            Target = target;
            Damage = damage;
            EnemyUI = enemyUI;
        }
    }
    public class EnemyDefenceEvent : GameEvent
    {
        public BaseEnemy Target;
        public int Defence;
        public SetEnemyUI EnemyUI;

        public EnemyDefenceEvent(BaseEnemy target, int defence, SetEnemyUI enemyUI)
        {
            Target = target;
            Defence = defence;
            EnemyUI = enemyUI;
        }
    }
}