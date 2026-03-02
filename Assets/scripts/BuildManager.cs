using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerPrefab;
    public InputReader inputReader;

    private void OnEnable()
    {
        inputReader.OnBuildPressed += BuildTower;
    }

    private void OnDisable()
    {
        inputReader.OnBuildPressed -= BuildTower;
    }

    private void BuildTower(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Vector3 buildPosition = hit.point;
                buildPosition.y = 0.5f;

                Instantiate(towerPrefab, buildPosition, Quaternion.identity);
            }
        }
    }
}