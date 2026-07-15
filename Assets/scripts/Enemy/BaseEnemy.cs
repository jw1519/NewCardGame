using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/BasicEnemy")]
    public class BaseEnemy : ScriptableObject, ITakeDamage, IHeal, IChangeAnimation, IEffectable
    {
        public static event Action enemyHealthChange;
        public static event Action enemyDefenceChange;
        public static event Action<string> addEffectToEnemy;
        public static event Action<string> RemoveEffectToEnemy;
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
        public EnemyType enemyType;
        public AbilityTargetType abilityTargetType;
        [HideInInspector] public int health;
        public int maxHealth;
        [HideInInspector] public int damage;
        public int maxDamage;
        public int minDamage;
        [HideInInspector] public int defence;
        public int defenceAmount;
        public string abilityName;
        public int abilityAmount;
        public int abilityDuration;
        public StatusEffectData abilityEffect;

        public int goldOnDefeat;

        public bool isAlive => health > 0;

        [Header("Status Effects")]
        public bool isBurning => burnDuration > 0;
        public int burnDamage;
        public int burnDuration;
        public List<StatusEffectData> activeEffects = new List<StatusEffectData>();

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
                ChangeAnimation("TakeDamage");
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
        }
        public void ChangeAnimation(string animationName)
        {
            switch (animationName)
            {
                case "Idle":
                    animator.CrossFade("Idle", 0.1f);
                    break;
                case "TakeDamage":
                    animator.SetTrigger("takeDamage");
                    break;
                case "Die":
                    animator.SetBool("isAlive", false);
                    break;
                case "Attack":
                    animator.SetTrigger("attack");
                    break;
                case "Defend":
                    animator.SetTrigger("defend");
                    break;
                case "Ability":
                    animator.SetTrigger("ability");
                    break;
            }
        }

        public void ApplyEffect(StatusEffectData data)
        { 
            switch (data.effectName)
            {
                case "burn":
                    burnDamage = data.DOTAmount;
                    burnDuration = data.duration;
                    addEffectToEnemy?.Invoke("burn");
                    break;
                default:
                    Debug.LogWarning("Status effect not recognized: " + data.effectName);
                    break;
            }
        }
        public void UpdateEffect()
        {
            if (isBurning)
            {
                TakeDamage(burnDamage);
                burnDuration--;
                Debug.Log("Burn effect applied to enemy: Damage = " + burnDamage + ", Remaining Duration = " + burnDuration);
                if (burnDuration <= 0)
                {
                    RemoveEffect("burn");
                }
            }
        }
        public void RemoveEffect(string name)
        {
            if (name == null) return;
            switch (name)
            {
                case "burn":
                    burnDamage = 0;
                    burnDuration = 0;
                    Debug.Log("Removing burn effect from enemy.");
                    RemoveEffectToEnemy?.Invoke("burn");
                    break;
                default:
                    Debug.LogWarning("Status effect not recognized: " + name);
                    break;
            }
        }
    }
    public enum EnemyAction
    {
        Attack,
        Defend,
        Ability,
    }
    public enum EnemyType
    {
        Basic,
        Boss,
    }
    public enum AbilityTargetType
    {
        Self,
        Player,
    }
}
