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
        if (gameEvent is PlayerAttackEvent playerAttack)
        {
            ApplyDamage(playerAttack.Target, playerAttack.Damage);
            playerAttack.EnemyUI.UpdateHealthUI();
            playerAttack.EnemyUI.UpdateDefenceUI();
            yield return new WaitForSeconds(1); //do animation here
        }
        else if (gameEvent is PlayerDefenceEvent playerDefence)
        {
            playerDefence.Target.defence = playerDefence.Target.defence + playerDefence.Defence;
            playerDefence.Target.gameObject.GetComponent<SetCharacterUI>().UpdateDefenceUI();
            yield return new WaitForSeconds(1); //do animation here
        }
        else if (gameEvent is PlayerHealEvent playerHeal)
        {
            ApplyHeal(playerHeal.Target, playerHeal.HealAmount);
            yield return new WaitForSeconds(1); //do animation here
        }
        else if (gameEvent is EnemyAttackEvent enemyAttack)
        {
            ApplyDamage(enemyAttack.Target, enemyAttack.Damage);
            yield return new WaitForSeconds(1); //do animation here
        }
        else if (gameEvent is EnemyDefenceEvent enemyDefence)
        {
            enemyDefence.Target.defence = enemyDefence.Target.defenceAmount;
            yield return new WaitForSeconds(1);
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
