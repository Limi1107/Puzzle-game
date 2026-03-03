using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board Size")]
    public int width = 6;
    public int height = 12;

    [Header("Block Settings")]
    public GameObject blockPairPrefab;
    public float fallSpeed = 1f;

    private Transform[,] grid;
    private GameObject currentPair;

    private float fallTimer = 0f;

    private void Start()
    {
        grid = new Transform[width, height];
        SpawnNewPair();
    }

    private void Update()
    {
        if (currentPair == null) return;

        fallTimer += Time.deltaTime;

        if (fallTimer >= fallSpeed)
        {
            MoveDown();
            fallTimer = 0f;
        }

        HandleInput();
    }

    // ==============================
    // ê∂ê¨
    // ==============================
    private void SpawnNewPair()
    {
        currentPair = Instantiate(
            blockPairPrefab,
            new Vector3(width / 2, height - 1, 0),
            Quaternion.identity
        );
    }

    // ==============================
    // ì¸óÕèàóù
    // ==============================
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector3.left);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Rotate();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveDown();
    }

    // ==============================
    // â°à⁄ìÆ
    // ==============================
    private void Move(Vector3 direction)
    {
        currentPair.transform.position += direction;

        if (!IsValidPosition(currentPair))
            currentPair.transform.position -= direction;
    }

    // ==============================
    // âÒì]
    // ==============================
    private void Rotate()
    {
        currentPair.transform.Rotate(0, 0, 90);

        if (!IsValidPosition(currentPair))
            currentPair.transform.Rotate(0, 0, -90);
    }

    // ==============================
    // â∫à⁄ìÆ
    // ==============================
    private void MoveDown()
    {
        currentPair.transform.position += Vector3.down;

        if (!IsValidPosition(currentPair))
        {
            currentPair.transform.position += Vector3.up;
            AddToGrid(currentPair);
            SpawnNewPair();
        }
    }

    // ==============================
    // óLå¯à íuîªíË
    // ==============================
    private bool IsValidPosition(GameObject pair)
    {
        foreach (Transform child in pair.transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            if (grid[x, y] != null)
                return false;
        }

        return true;
    }

    // ==============================
    // å≈íË
    // ==============================
    private void AddToGrid(GameObject pair)
    {
        foreach (Transform child in pair.transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (y >= height)
            {
                Debug.Log("Game Over");
                enabled = false;
                return;
            }

            grid[x, y] = child;
        }
    }
}