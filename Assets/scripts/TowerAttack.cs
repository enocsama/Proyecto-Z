using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public float damage = 2f;

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Collider[] enemies = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                if (fireCooldown <= 0f)
                {
                    col.GetComponent<EnemyHealth>().TakeDamage(damage);
                    fireCooldown = 1f / fireRate;
                }

                break; // solo ataca al primero que encuentre
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}