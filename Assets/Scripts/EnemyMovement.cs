using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Transform[] waypoints;
    private int index = 1;

    public void InitPath(Transform[] path)
    {
        waypoints = path;
        transform.position = waypoints[index].position;
    }

    void Update()
    {
        if (waypoints == null) return;

        Transform target = waypoints[index];
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            index++;
            if (index >= waypoints.Length)
                Destroy(gameObject);
        }
    }
}