using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : MonoBehaviour, ITakeDamage, IHeal
    {
        [Header("Character Sprite")]
        public Sprite characterSprite;

        [Header("Stats")]
        public string characterName;
        public int health;
        public int maxHealth;
        public int defence;
        public int energy;
        public int maxEnergy;
        public CharcterType type;
        public bool isAlive => health > 0;

        public enum CharcterType
        {
            Melee,
            Ranged,
            Magic
        }
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
            }
            if (health - damageTaken > 0)
            {
                health -= damageTaken;
            }
            else
            {
                health = 0;
            }
        }
    }
}
