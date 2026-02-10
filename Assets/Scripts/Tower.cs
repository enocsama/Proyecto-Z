using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / fireRate)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject);
                break;
            }
        }
    }
}