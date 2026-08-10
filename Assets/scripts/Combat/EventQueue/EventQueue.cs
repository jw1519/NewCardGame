using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Enemy;

public class EventQueue : MonoBehaviour
{
    private static Queue<GameEvent> eventQueue = new Queue<GameEvent>();
    private static bool isProcessing = false;
    public static EventQueue instance;

    private void Awake()
    {
        instance = this;
        isProcessing = false;
    }
    public void ResetProcessing()
    {
        isProcessing = false;
        eventQueue.Clear();
    }
    public static void EnqueueEvent(GameEvent gameEvent)
    {
        eventQueue.Enqueue(gameEvent);
        if (!isProcessing)
        {
            isProcessing = true;
            instance.StartCoroutine(ProcessEvents());
        }
    }
    private static IEnumerator ProcessEvents()
    {
        while (eventQueue.Count > 0)
        {
            GameEvent gameEvent = eventQueue.Dequeue();
            yield return HandleEvent(gameEvent);
        }
        isProcessing = false;
        eventQueue.Clear();
    }
    private static IEnumerator HandleEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            // Player Events
            case PlayerAttackEvent playerAttack:
                ApplyDamage(playerAttack.Target, playerAttack.Damage);
                playerAttack.Character.ChangeAnimation("Attack");
                yield return new WaitForSeconds(1); //do animation here
                break;
            case PlayerDefenceEvent playerDefence:
                playerDefence.Target.defence += playerDefence.Defence;
                playerDefence.CharacterUI.UpdateDefenceUI();
                playerDefence.CharacterUI.EffectAnimation("ApplyShield");
                yield return new WaitForSeconds(1); //do animation here
                break;
            case PlayerHealEvent playerHeal:
                ApplyHeal(playerHeal.Target, playerHeal.HealAmount);
                yield return new WaitForSeconds(1); //do animation here
                break;
            case PlayerAOEAttackEvent playerAOEAttack:
                foreach (BaseEnemy enemy in playerAOEAttack.Targets)
                {
                    ApplyDamage(enemy, playerAOEAttack.Damage);
                }
                playerAOEAttack.Character.ChangeAnimation("Attack");
                yield return new WaitForSeconds(1); //do animation here
                break;
            case PlayerRemoveAllStatusEffectsEvent playerRemoveAllStatusEffects:
                playerRemoveAllStatusEffects.Target.RemoveAllEffects();
                yield return new WaitForSeconds(1); //do animation here
                break;

            // Enemy Events
            case EnemyAttackEvent enemyAttack:
                ApplyDamage(enemyAttack.Target, enemyAttack.Damage);
                enemyAttack.Enemy.ChangeAnimation("Attack");
                enemyAttack.Target.ChangeAnimation("TakeDamage");
                yield return new WaitForSeconds(1); //do animation here
                break;
            case EnemyDefenceEvent enemyDefence:
                enemyDefence.Target.defence = enemyDefence.Target.defenceAmount;
                enemyDefence.EnemyUI.UpdateDefenceUI();
                enemyDefence.EnemyUI.EffectAnimation("ApplyShield");
                yield return new WaitForSeconds(1);
                break;
            case EnemyHealEvent enemyHeal:
                enemyHeal.Target.Heal(enemyHeal.HealAmount);
                enemyHeal.Target.ChangeAnimation("Heal");
                yield return new WaitForSeconds(1); //do animation here
                break;
            case EnemyAddStatusEffectEvent enemyStatusEffect:
                enemyStatusEffect.Target.ApplyEffect(enemyStatusEffect.statusEffect);
                yield return new WaitForSeconds(1); //do animation here
                break;
            case EnemySummonEvent enemySummon:
                GameObject newEnemy = AssetManager.Instance.GetAsset("EnemyFactory").GetComponent<EnemyFactory>().CreateEnemy(enemySummon.EnemyToSummon);
                newEnemy.GetComponent<SetEnemyUI>().enemy.isSummon = true;
                enemySummon.Summoner.SummonEnemy(enemySummon.EnemyToSummon);
                yield return new WaitForSeconds(1); //do animation here
                break;
        }
    }
    public static void ApplyDamage(ITakeDamage target, int damage)
    {
        target.TakeDamage(damage);
    }
    public static void ApplyHeal(IHeal target, int healAmount)
    {
        target.Heal(healAmount);
    }
}
