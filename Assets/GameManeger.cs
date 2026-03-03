using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Board board;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isGameOver = false;

        if (board != null)
        {
            board.enabled = true;
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        if (board != null)
        {
            board.enabled = false;
        }

        Debug.Log("Game Over");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}