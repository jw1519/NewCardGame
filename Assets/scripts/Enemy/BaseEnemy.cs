using System;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/BasicEnemy")]
    public class BaseEnemy : ScriptableObject, ITakeDamage, IHeal, IChangeAnimation
    {
        public static event Action enemyHealthChange;
        public static event Action enemyDefenceChange;
        public static event Action enemydied;
        public static event Action<int> enemydiedGold;

        public Sprite enemySprite;
        public RuntimeAnimatorController animatorController;
        public Animator animator;

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
                    defence -= damageTaken;
                    damageTaken = 0;
                }
                else
                {
                    damageTaken -= defence;
                    defence = 0;
                }
                enemyDefenceChange?.Invoke();
            }
            if (health - damageTaken > 0)
            {
                health -= damageTaken;
                enemyHealthChange?.Invoke();
            }
            else
            {
                health = 0;
                enemydiedGold?.Invoke(goldOnDefeat);
                enemydied?.Invoke();
                
            }
            
        }
        public virtual void UseAbility(GameObject target)
        {
            Debug.Log("Use Ability here");
            Heal(abilityAmount);
        }
        public void ChangeAnimation(string animationName)
        {

            switch (animationName)
            {
                case "Idle":
                    animator.SetTrigger("Idle");
                    break;
                case "TakeDamage":
                    animator.SetTrigger("TakeDamage");
                    break;
                case "Die":
                    animator.SetTrigger("Die");
                    break;
                case "Attack":
                    animator.SetTrigger("Attack");
                    break;
                case "Defend":
                    animator.SetTrigger("Defend");
                    break;
                case "Ability":
                    animator.SetTrigger("Ability");
                    break;
            }
        }
    }
    public enum EnemyAction
    {
        Attack,
        Defend,
        Heal,
        Ability,
        None
    }
}
