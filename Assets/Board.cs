using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public int width = 4;
    public int height = 12;
    public Transform[,] grid;

    void Awake()
    {
        Instance = this;
        grid = new Transform[width, height];
    }

    public bool IsInside(int x, int y) => x >= 0 && x < width && y >= 0;
    public bool IsOccupied(int x, int y) => y < height && grid[x, y] != null;

    public void AddToGrid(int x, int y, Transform block)
    {
        if (y < height) grid[x, y] = block;
    }
}