using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : MonoBehaviour, ITakeDamage, IHeal, IUseEnergy, IEffectable
    {
        public static event Action playerHealthChanged;
        public static event Action playerDefenceChanged;
        public static event Action playerEnergyChanged;
        public static event Action<StatusEffectData> AddEffectToPlayer;
        public static event Action<string> RemoveEffectToPlayer;

        [Header("Character Sprite")]
        public Sprite characterSprite;

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
        public bool isBurning;

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
            }
            else
            {
                health = 0;
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
            activeEffects.Add(data);
            isBurning = data.effectName == "burn";
            AddEffectToPlayer?.Invoke(data);
        }
        public void UpdateEffect()
        {
            if (GetEffect("burn") != null)
            {
                StatusEffectData burnEffect = GetEffect("burn");
                TakeDamage(burnEffect.DOTAmount);
                burnEffect.duration--;
                Debug.Log("Burn effect applied to enemy: Damage = " + burnEffect.DOTAmount + ", Remaining Duration = " + burnEffect.duration);
                if (burnEffect.duration <= 0)
                {
                    RemoveEffect("burn");
                }
            }
        }
        public void RemoveEffect(string name)
        {
            StatusEffectData effectToRemove = GetEffect(name);
            activeEffects.Remove(effectToRemove);
            RemoveEffectToPlayer?.Invoke(name);
        }
        public StatusEffectData GetEffect(string name)
        {
            return activeEffects.Find(j => j.effectName == name);
        }
    }
}
