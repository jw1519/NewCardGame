using System;
using UnityEngine;
using static Enemy.BaseEnemy;
using Character;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyTurn : MonoBehaviour
    {
        BaseEnemy enemy;
        SetEnemyUI enemyUI;


        private void Awake()
        {
            enemyUI = GetComponent<SetEnemyUI>();
            enemy = GetComponent<BaseEnemy>();
        }
        private void Start()
        {
            
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
                    EventQueue.EnqueueEvent(new EnemyAttackEvent(Target.GetComponent<SetCharacterUI>().character, enemy.damage));
                    //EndTurn();
                    return;

                case EnemyAction.Defend:
                    EventQueue.EnqueueEvent(new EnemyDefenceEvent(enemy, enemy.defenceAmount));
                    enemyUI.UpdateDefenceUI();
                    //EndTurn();
                    return;
            }
        }
        public GameObject FindTarget()
        {
            List<GameObject> targets = CombatManager.instance.combatOrder.FindAll(u => u.GetComponent<SetCharacterUI>() != null); // find all potential characters to attack

            if (targets.Count == 0) return null;

            return targets[UnityEngine.Random.Range(0, targets.Count)];

        }
        public void EndTurn()
        {
            SelectNextAction();
            //GameManager.instance.BeginTurn();
        }
        public void SelectNextAction()
        {
            EnemyAction action = GetRandomEnumValue<EnemyAction>();
            enemy.action = action;
            enemyUI.UpdateActionUI(action);

        }
        public EnemyAction GetRandomEnumValue<Action>()
        {
            Array enumvalues = Enum.GetValues(typeof(EnemyAction)); // gets all posable values for the enum
            return (EnemyAction)enumvalues.GetValue(UnityEngine.Random.Range(0, enumvalues.Length)); //randomly selcts on of the actions from the enum and returns it
        }
    }
}
