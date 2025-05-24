using System;
using UnityEngine;
using static Enemy.BaseEnemy;
using TMPro;
using Character;

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
        public void StartTurn(GameObject Target)
        {
            enemy.defence = 0;
            enemyUI.UpdateDefenceUI();
            switch (enemy.action)
            {
                case EnemyAction.Attack:
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
