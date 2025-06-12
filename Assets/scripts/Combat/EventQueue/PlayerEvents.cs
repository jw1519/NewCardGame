using Enemy;
namespace Character
{
    public class PlayerAttackEvent : GameEvent
    {
        public BaseEnemy Target;
        public int Damage;
        public SetEnemyUI EnemyUI;

        public PlayerAttackEvent(BaseEnemy target, int damage, SetEnemyUI enemyUI)
        {
            Target = target;
            Damage = damage;
            EnemyUI = enemyUI;
        }
    }
    public class PlayerDefenceEvent : GameEvent
    {
        public BaseCharacter Target;
        public int Defence;
        public SetCharacterUI CharacterUI;

        public PlayerDefenceEvent(BaseCharacter target, int defence, SetCharacterUI characterUI)
        {
            Target = target;
            Defence = defence;
            CharacterUI = characterUI;  
        }
    }
    public class PlayerHealEvent : GameEvent
    {
        public BaseCharacter Target;
        public int HealAmount;

        public PlayerHealEvent(BaseCharacter target, int healAmount)
        {
            Target = target;
            HealAmount = healAmount;
        }
    }
}

