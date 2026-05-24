using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        ChangeAnimation("Idle");
    }
    public void ChangeAnimation(string animationName)
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
            case "Attack":
                animator.SetTrigger("Attack");
            case "Defend":
                animator.SetTrigger("Defend");
                break;
            case "Ability":
                animator.SetTrigger("Ability");
                break;
        }
    }
}