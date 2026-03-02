using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public TowerData[] availableTowers;
    public GameObject ghostPrefab;
    public InputReader inputReader;
    public float gridSize = 1f;
    private GameObject currentGhost;
    private bool canBuild = false;
    private TowerData selectedTower;
    public EconomyManager economyManager;
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
                Vector3 pos = SnapToGrid(hit.point);

                currentGhost.transform.position = pos;

                bool validPosition = IsBuildPositionValid(pos);
                bool hasMoney = selectedTower != null && 
                                economyManager.CanAfford(selectedTower.cost);

                canBuild = validPosition && hasMoney;
                UpdateGhostColor();
            }
        }
    }

    private bool IsBuildPositionValid(Vector3 position)
    {
        float checkRadius = 0.4f;

        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Tower") || col.CompareTag("Path"))
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
        if (!canBuild || selectedTower == null)
            return;

        economyManager.Spend(selectedTower.cost);

        Instantiate(selectedTower.prefab, 
                    currentGhost.transform.position, 
                    Quaternion.identity);
    }
    private Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x, 0.5f, z);
    }
    public void SelectTower(int index)
{
    if (index < 0 || index >= availableTowers.Length)
        return;

    selectedTower = availableTowers[index];
}
}

    