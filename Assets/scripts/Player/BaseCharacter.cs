using System;
using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : MonoBehaviour, ITakeDamage, IHeal, IUseEnergy
    {
        public static event Action playerHealthChanged;
        public static event Action playerDefenceChanged;

        [Header("Character Sprite")]
        public Sprite characterSprite;

        [Header("Stats")]
        public string characterName;
        public int health;
        public int maxHealth;
        public int defence;

        [Header("Energy")]
        public int energy;
        public int maxEnergy;

        [Header("Gold")]
        public int gold;
        public int totalGoldCollected;
        public bool isAlive => health > 0;

        public virtual void Start()
        {
            CombatManager.instance.AddToCombat(gameObject);
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
    }
}
