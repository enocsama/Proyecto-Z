using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public int reward = 10;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            FindObjectOfType<EconomyManager>().Add(reward);
            Destroy(gameObject);
        }
    }
}