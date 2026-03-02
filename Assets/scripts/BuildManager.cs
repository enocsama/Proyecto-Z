using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject ghostPrefab;
    public InputReader inputReader;

    private GameObject currentGhost;
    private bool canBuild = false;

    private void Start()
    {
        currentGhost = Instantiate(ghostPrefab);
    }

    private void OnEnable()
    {
        inputReader.OnBuildPressed += TryBuildTower;
    }

    private void OnDisable()
    {
        inputReader.OnBuildPressed -= TryBuildTower;
    }

    private void Update()
    {
        UpdateGhostPosition();
    }

    private void UpdateGhostPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            UnityEngine.InputSystem.Mouse.current.position.ReadValue()
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Vector3 pos = hit.point;
                pos.y = 0.5f;

                currentGhost.transform.position = pos;

                canBuild = IsBuildPositionValid(pos);
                UpdateGhostColor();
            }
        }
    }

    private bool IsBuildPositionValid(Vector3 position)
    {
        float checkRadius = 0.7f;

        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Tower"))
                return false;
        }

        return true;
    }

    private void UpdateGhostColor()
    {
        Renderer renderer = currentGhost.GetComponent<Renderer>();

        if (canBuild)
            renderer.material.color = new Color(0, 1, 0, 0.4f);
        else
            renderer.material.color = new Color(1, 0, 0, 0.4f);
    }

    private void TryBuildTower(Vector2 mousePosition)
    {
        if (!canBuild)
            return;

        Instantiate(towerPrefab, currentGhost.transform.position, Quaternion.identity);
    }
}