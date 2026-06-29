using Character;
namespace Enemy
{
    public class EnemyAttackEvent : GameEvent
    {
        public BaseCharacter Target;
        public BaseEnemy Enemy;
        public int Damage;
        public SetEnemyUI EnemyUI;

        public EnemyAttackEvent(BaseCharacter target, BaseEnemy enemy, int damage, SetEnemyUI enemyUI)
        {
            Target = target;
            Enemy = enemy;
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
    public class EnemyAbilityEvent : GameEvent
    {
        public BaseEnemy Target;
        public int AbilityAmount;
        public SetEnemyUI EnemyUI;
        public EnemyAbilityEvent(BaseEnemy target, int abilityAmount, SetEnemyUI enemyUI)
        {
            Target = target;
            AbilityAmount = abilityAmount;
            EnemyUI = enemyUI;
        }
    }
}