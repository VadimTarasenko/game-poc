using UnityEngine;

public class EnemyExample : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject healthBarPrefab;

    private float currentHealth;
    private EnemyHealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        CreateHealthBar();
    }

    private void CreateHealthBar()
    {
        if (healthBarPrefab != null)
        {
            GameObject healthBarObj = Instantiate(healthBarPrefab);
            healthBar = healthBarObj.GetComponent<EnemyHealthBar>();
            if (healthBar != null)
            {
                healthBar.Initialize(transform, maxHealth);
            }
        }
    }

    public void TakeDamage(float damage, Vector3 attackerPosition)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! Health: {currentHealth}/{maxHealth}");

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");

        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        Destroy(gameObject);
    }
}
