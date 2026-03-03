using UnityEngine;

public class Board : MonoBehaviour
{
    // ■ ボードのサイズ
    public int width = 4;
    public int height = 12;

    // ■ 盤面を右寄せなどで移動させるためのオフセット
    public Vector2 boardOffset;

    // ■ ぷよペアのプレハブ
    public GameObject puyoPairPrefab;

    // ■ 内部管理用
    private Transform[,] grid;
    private GameObject currentPair;

    private float fallTimer = 0f;
    public float fallSpeed = 1f;

    // ■ スコア管理
    public int score = 0;

    // ======================================
    private void Start()
    {
        grid = new Transform[width, height];
        SpawnNewPair();
    }

    private void Update()
    {
        // ゲームオーバーなら何もしない
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

        if (currentPair == null) return;

        fallTimer += Time.deltaTime;

        if (fallTimer >= fallSpeed)
        {
            MoveDown();
            fallTimer = 0f;
        }
    }

    // ■ 新しいぷよペアを生成
    private void SpawnNewPair()
    {
        currentPair = Instantiate(
            puyoPairPrefab,
            new Vector3(
                boardOffset.x + width / 2,
                boardOffset.y + height - 1,
                0),
            Quaternion.identity
        );

        if (!IsValidPosition(currentPair))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
        }
    }

    // ■ 下に移動
    private void MoveDown()
    {
        currentPair.transform.position += Vector3.down;

        if (!IsValidPosition(currentPair))
        {
            currentPair.transform.position += Vector3.up;
            AddToGrid(currentPair);

            // スコア追加
            AddScore(10);

            SpawnNewPair();
        }
    }

    // ■ 現在位置がボード内か判定
    private bool IsValidPosition(GameObject pair)
    {
        foreach (Transform child in pair.transform)
        {
            int x = Mathf.RoundToInt(child.position.x - boardOffset.x);
            int y = Mathf.RoundToInt(child.position.y - boardOffset.y);

            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            if (grid[x, y] != null)
                return false;
        }

        return true;
    }

    // ■ 盤面に固定
    private void AddToGrid(GameObject pair)
    {
        foreach (Transform child in pair.transform)
        {
            int x = Mathf.RoundToInt(child.position.x - boardOffset.x);
            int y = Mathf.RoundToInt(child.position.y - boardOffset.y);

            grid[x, y] = child;
        }
    }

    // ■ スコア追加用
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
}