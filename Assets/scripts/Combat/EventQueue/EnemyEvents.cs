using Character;
using UnityEngine;
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
    public class EnemyAddStatusEffectEvent : GameEvent
    {
        public GameObject Target;
        public StatusEffectData statusEffect;

        public EnemyAddStatusEffectEvent(GameObject target, StatusEffectData effectData)
        {
            Target = target;
            statusEffect = effectData;
        }
    }
    public class EnemySummonEvent : GameEvent
    {
        public EnemySummon Summoner;
        public BaseEnemy EnemyToSummon;
        public EnemySummonEvent(EnemySummon summoner, BaseEnemy enemyToSummon)
        {
            Summoner = summoner;
            EnemyToSummon = enemyToSummon;
        }
    }
    public class EnemyHealEvent : GameEvent
    {
        public BaseEnemy Target;
        public int HealAmount;
        public EnemyHealEvent(BaseEnemy target, int healAmount)
        {
            Target = target;
            HealAmount = healAmount;
        }
    }
    public class EnemySplitEvent : GameEvent
    {
        public BaseEnemy EnemyToSpawn;
        public BaseEnemy Enemy;
        public int Amount;

        public EnemySplitEvent(BaseEnemy enemyToSpawn, BaseEnemy enemy, int amount)
        {
            EnemyToSpawn = enemyToSpawn;
            Enemy = enemy;
            Amount = amount;
        }
    }
}