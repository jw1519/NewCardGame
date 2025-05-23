using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, ITakeDamage, IHeal
    {
        public Sprite enemySprite;

        [Header("Stats")]
        public string enemyName;
        public int health;
        public int damage;
        public int maxHealth;
        public int defence;

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
                //character dies
                //check if all enemies are dead
            }
        }
    }
}
