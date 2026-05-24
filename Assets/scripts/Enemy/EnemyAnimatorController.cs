using UnityEngine;

namespace Enemy
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        public Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            ChangeAnimation("Idle");
        }
        public void ChangeAnimation(string animationName, EnemyAction enemyAction = EnemyAction.None)
        {
            if (enemyAction != EnemyAction.None)
            {
                switch (enemyAction)
                {
                    case EnemyAction.Attack:
                        animator.SetTrigger("Attack");
                        break;
                    case EnemyAction.Defend:
                        animator.SetTrigger("Defend");
                        break;
                    case EnemyAction.Ability:
                        animator.SetTrigger("Ability");
                        break;
                }
            }
            else
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
                }
            }
        }
    }
}
