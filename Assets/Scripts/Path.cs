using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    private void Awake()
    {
        waypoints = GetComponentsInChildren<Transform>();
    }
}