using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public float damage = 2f;

    private float fireCooldown = 0f;

    private Transform GetClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);

        float shortestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        return closestEnemy;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}