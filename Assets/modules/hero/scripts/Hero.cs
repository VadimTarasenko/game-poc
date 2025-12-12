using UnityEngine;

public class Hero : MonoBehaviour
{
    private float agility = 12f;
    private float strength = 8f;
    private float intelligence = 6f;
    
    private float attackSpeed = 1f;
    private float health = 120f;
    private float damage = 10f;
    private float armor = 0f;

    private Animator animator;
    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            // Check if the attack animation is finished
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Attack"))
            {
                isAttacking = false;
            } 
        }
    }

    public void Attack() {
        isAttacking = true; 
        animator.CrossFade("Attack", 0f);
    }
}