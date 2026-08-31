using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : ScriptableObject, ITakeDamage, IHeal, IUseEnergy, IEffectable, IChangeAnimation
    {
        public static event Action playerHealthChanged;
        public static event Action playerDefenceChanged;
        public static event Action playerEnergyChanged;
        public static event Action<StatusEffectData> AddEffectToPlayer;
        public static event Action<string> RemoveEffectToPlayer;

        [Header("Animation")]
        public Animator animator;
        public RuntimeAnimatorController animatorController;

        [Header("Stats")]
        public string characterName;
        public int health;
        public int maxHealth;
        public int defence;
        public bool isAlive => health > 0;

        [Header("Energy")]
        public int energy;
        public int maxEnergy;

        [Header("Gold")]
        public int gold;
        public int totalGoldCollected;

        [Header("Items")]
        public int maxItemAmount;

        [Header("Status Effects")]
        public List<StatusEffectData> activeEffects = new List<StatusEffectData>();

        public virtual void Start()
        {
            health = maxHealth;
            energy = maxEnergy;
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
            playerHealthChanged?.Invoke();
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
                playerDefenceChanged?.Invoke();
            }
            if (health - damageTaken > 0)
            {
                health -= damageTaken;
                ChangeAnimation("TakeDamage");
            }
            else
            {
                health = 0;
                ChangeAnimation("die");
            }
            playerHealthChanged?.Invoke();
        }
        public void UseEnergy(int amount)
        {
            if (energy - amount >= 0)
            {
                energy -= amount;
            }
        }
        public void GainEnergy(int amount)
        {
            if (energy + amount <= maxEnergy)
            {
                energy += amount;
            }
            else
            {
                energy = maxEnergy;
            }
            playerEnergyChanged?.Invoke();
        }
        public void ApplyEffect(StatusEffectData data)
        {
            if (data == null) return;
            if (activeEffects.Find(p => p.effectName == data.effectName) != null)
            {
                StatusEffectData effect = activeEffects.Find(p => p.effectName == data.effectName);
                effect.duration = data.duration;
            }
            else
            {
                activeEffects.Add(Instantiate(data));
                AddEffectToPlayer?.Invoke(data);
            }
        }
        public void UpdateEffect()
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                StatusEffectData effect = activeEffects[i];
                effect.duration--;
                if (effect.doesDamage)
                    TakeDamage(Mathf.RoundToInt(effect.DOTAmount));
                if (effect.duration <= 0)
                {
                    RemoveEffect(effect.effectName);
                }
            }
        }
        public void RemoveEffect(string name)
        {
            StatusEffectData effect = GetEffect(name);
            effect.RemoveEffect();
            activeEffects.Remove(effect);
            effect.RemoveEffect();
            RemoveEffectToPlayer?.Invoke(name);
        }
        public void RemoveAllEffects()
        {
            if (activeEffects.Count == 0) return;
            for (int i = activeEffects.Count + 1; i > 0; i--)
            {
                Debug.Log(activeEffects[i]);
                if (activeEffects[i] != null)
                {
                    RemoveEffect(activeEffects[i].effectName);
                }  
            }
        }
        public StatusEffectData GetEffect(string name)
        {
            return activeEffects.Find(j => j.effectName == name);
        }

        public void ChangeAnimation(string animationName)
        {
            switch (animationName)
            {
                case "die":
                    animator.SetBool("isAlive", false);
                    break;
                default:
                    animator.SetTrigger(animationName);
                    break;
            }
        }
    }
}
