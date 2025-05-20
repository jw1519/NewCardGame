using UnityEngine;

namespace Character
{
    public abstract class BaseCharacter : MonoBehaviour, ITakeDamage, IHeal
    {
        [Header("Stats")]
        public int health;
        public int maxHealth;
        public int defence;
        public int energy;
        public int maxEnergy;

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
                //character dies
                //check if all characters are dead
            }
        }
    }
}
