using System;
using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : MonoBehaviour, ITakeDamage, IHeal, IUseEnergy
    {
        public static event Action playerHealthChanged;
        public static event Action playerDefenceChanged;
        public static event Action playerEnergyChanged;
        public static event Action<string> AddEffectToPlayer;

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
        public bool isBurning => burnDuration > 0;
        public int burnDamage;
        public int burnDuration;


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
        public void ApplyBurn(int burnDamage, int burnDuration)
        {
            this.burnDamage = burnDamage;
            this.burnDuration = burnDuration;
            AddEffectToPlayer?.Invoke("burn");
        }
    }
}
