using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damageMultiplier = 1f;

    private HeroData heroData;
    private Animator animator;
    private bool isAttacking = false;
    private bool hasDealtDamage = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        heroData = GetComponent<HeroData>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 0.5f && !hasDealtDamage)
            {
                Debug.Log("Performing attack hit!");
                PerformAttackHit();
                hasDealtDamage = true;
            }

            if (stateInfo.normalizedTime >= 0.95f)
            {
                isAttacking = false;
                hasDealtDamage = false;
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Attack method called!");
        isAttacking = true;
        hasDealtDamage = false;
        animator.CrossFade("Attack1", 0f);
    }

    private void PerformAttackHit()
    {
        Vector3 attackPosition = transform.position + transform.forward * attackRange;
        Collider[] hitEnemies = Physics.OverlapSphere(attackPosition, attackRadius, enemyLayer);

        Debug.Log($"Attack performed! Found {hitEnemies.Length} enemies in range");

        float damage = CalculateDamage();
        Debug.Log($"Dealing {damage} damage");

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log($"Hitting enemy: {enemy.gameObject.name}");
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage, transform.position);
            }
            else
            {
                enemy.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private float CalculateDamage()
    {
        if (heroData != null)
        {
            return heroData.power * damageMultiplier;
        }
        return 10f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 attackPosition = transform.position + transform.forward * attackRange;
        Gizmos.DrawWireSphere(attackPosition, attackRadius);
    }
}