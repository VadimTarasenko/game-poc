using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);

    private Transform enemyTransform;
    private IDamageable damageable;

    public void Initialize(Transform enemy, float maxHealth)
    {
        enemyTransform = enemy;
        UpdateHealthBar(maxHealth, maxHealth);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        }
    }

    private void LateUpdate()
    {
        if (enemyTransform != null)
        {
            transform.position = enemyTransform.position + offset;
        }
    }
}
