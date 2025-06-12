using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : ScriptableObject, ITakeDamage, IHeal
    {
        public static event Action enemyHealthChange;
        public static event Action enemydied;
        public static event Action<int> enemydiedGold;

        public Sprite enemySprite;
        public AnimatorController controller;

        [Header("actions")]
        public EnemyAction action;
        public Sprite attackSprite;
        public Sprite defenceSprite;
        public Sprite abilitySprite;

        [Header("Stats")]
        public string enemyName;
        public int health;
        public int maxHealth;
        public int damage;
        public int defence;
        public int defenceAmount;
        public int abilityAmount;

        public int goldOnDefeat;

         public bool isAlive => health > 0;

        public enum EnemyAction
        {
            Attack,
            Defend,
            Heal,
            Ability,
        }
        public void Heal(int healAmount)
        {
            if (health + healAmount <= maxHealth)
            {
                health += healAmount;
            }
            else
            {
                health = maxHealth;
            }
            enemyHealthChange?.Invoke();
        }

        public void TakeDamage(int damageTaken)
        {
            //check for defences
            if (defence > 0)
            {
                if (defence >= damageTaken)
                {
                    defence = defence - damageTaken;
                    damageTaken = 0;
                }
                else
                {
                    damageTaken = damageTaken - defence;
                    defence = 0;
                }
            }
            if (health - damageTaken > 0)
            {
                health = health - damageTaken;
            }
            else
            {
                health = 0;
                enemydied?.Invoke();
                enemydiedGold?.Invoke(goldOnDefeat);
            }
            enemyHealthChange?.Invoke();
        }
        public virtual void UseAbility(GameObject target)
        {
            Debug.Log("Use Ability here");
        }
    }
}
