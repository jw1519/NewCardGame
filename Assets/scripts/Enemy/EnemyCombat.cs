using System;
using UnityEngine;
using static Enemy.BaseEnemy;
using Character;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        BaseEnemy enemy;
        SetEnemyUI enemyUI;

        private void Awake()
        {
            enemyUI = GetComponent<SetEnemyUI>();
            enemy = enemyUI.enemy;
            CombatManager.instance.AddToCombat(gameObject);
            SelectNextAction();
        }
        public void StartTurn()
        {
            enemy.defence = 0;
            enemyUI.UpdateDefenceUI();
            switch (enemy.action)
            {
                case EnemyAction.Attack:
                    GameObject Target = FindTarget();
                    EventQueue.EnqueueEvent(new EnemyAttackEvent(Target.GetComponent<SetCharacterUI>().character, enemy.damage, enemyUI));
                    break;

                case EnemyAction.Defend:
                    EventQueue.EnqueueEvent(new EnemyDefenceEvent(enemy, enemy.defenceAmount, enemyUI));
                    enemyUI.UpdateDefenceUI();
                    break;

                case EnemyAction.Ability:
                    Debug.Log("Ability used");
                    break;
            }
            StartCoroutine(CombatManager.instance.StartCombat());
            SelectNextAction();
        }
        public GameObject FindTarget()
        {
            List<GameObject> targets = CombatManager.instance.combatOrder.FindAll(u => u.GetComponent<SetCharacterUI>() != null && u.GetComponent<SetCharacterUI>().character.isAlive); // find all potential characters to attack

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
            Debug.Log(action);
            enemyUI.UpdateActionUI();

        }
        public EnemyAction GetRandomEnumValue<Action>()
        {
            Array enumvalues = Enum.GetValues(typeof(EnemyAction)); // gets all posable values for the enum
            return (EnemyAction)enumvalues.GetValue(UnityEngine.Random.Range(0, enumvalues.Length)); //randomly selcts on of the actions from the enum and returns it
        }
    }
}
