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
        public static event Action UpdateEffectToEnemy;
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
            if (activeEffects.Find(p => p.effectName == data.effectName) != null)
            {
                StatusEffectData effect = activeEffects.Find(p => p.effectName == data.effectName);
                effect.duration = data.duration;
                UpdateEffectToEnemy?.Invoke();
            }
            else
            {
                activeEffects.Add(Instantiate(data));
                addEffectToEnemy?.Invoke(data.effectName);
            }
        }
        public void UpdateEffect()
        {
            if (GetEffect("burn"))
            {
                StatusEffectData burn = GetEffect("burn");
                TakeDamage(burn.DOTAmount);
                burn.duration--;
                Debug.Log("Burn effect applied to enemy: Damage = " + burn.DOTAmount + ", Remaining Duration = " + burn.duration);
                if (burn.duration <= 0)
                {
                    RemoveEffect("burn");
                }
            }
        }
        public void RemoveEffect(string name)
        {
            if (name == null) return;
            activeEffects.Remove(GetEffect(name));
            RemoveEffectToEnemy?.Invoke(name);
        }
        public StatusEffectData GetEffect(string name)
        {
            return activeEffects.Find(j => j.effectName == name);
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
