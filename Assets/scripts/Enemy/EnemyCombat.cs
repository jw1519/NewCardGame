using System;
using UnityEngine;
using Character;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        BaseEnemy enemy;
        SetEnemyUI enemyUI;
        CombatManager combatManager;

        private void Awake()
        {
            enemyUI = GetComponent<SetEnemyUI>();
            enemy = enemyUI.enemy;
            combatManager = AssetManager.Instance.GetAsset("CombatManager").GetComponent<CombatManager>();
            combatManager.AddToCombat(gameObject);
            SelectNextAction();
        }
        public void StartTurn()
        {
            if (enemy != null && enemy.isBurning)
            {
                enemy.TakeDamage(enemy.burnDamage);
                enemyUI.UpdateStatusEffects();
                if (enemy.isAlive == false) return;

                Debug.Log("Burn");
            }
            enemy.defence = 0;
            enemyUI.UpdateDefenceUI();
            switch (enemy.action)
            {
                case EnemyAction.Attack:
                    GameObject Target = FindTarget();
                    EventQueue.EnqueueEvent(new EnemyAttackEvent(Target.GetComponent<SetCharacterUI>().character, enemy, enemy.damage, enemyUI));
                    Debug.Log("Attack");
                    break;

                case EnemyAction.Defend:
                    EventQueue.EnqueueEvent(new EnemyDefenceEvent(enemy, enemy.defenceAmount, enemyUI));
                    enemyUI.UpdateDefenceUI();
                    Debug.Log("Defend");
                    break;

                case EnemyAction.Ability:
                    EventQueue.EnqueueEvent(new EnemyAbilityEvent(enemy, enemy.abilityAmount, enemyUI));
                    Debug.Log("Ability used");
                    break;
            }
            StartCoroutine(combatManager.StartCombat());
            SelectNextAction();
        }
        public GameObject FindTarget()
        {
            List<GameObject> targets = combatManager.combatOrder.FindAll(u => u.GetComponent<SetCharacterUI>() != null && u.GetComponent<SetCharacterUI>().character.isAlive); // find all potential characters to attack

            if (targets.Count == 0)
            {
                Debug.Log("target is null");
                return null;
            }

            return targets[UnityEngine.Random.Range(0, targets.Count)];

        }
        public void SelectNextAction()
        {
            EnemyAction action = GetRandomEnumValue<EnemyAction>();
            enemy.action = action;
            enemyUI.UpdateActionUI();

        }
        public EnemyAction GetRandomEnumValue<Action>()
        {
            Array enumvalues = Enum.GetValues(typeof(EnemyAction)); // gets all posable values for the enum
            return (EnemyAction)enumvalues.GetValue(UnityEngine.Random.Range(0, enumvalues.Length)); //randomly selcts on of the actions from the enum and returns it
        }
    }
}
