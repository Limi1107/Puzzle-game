
using UnityEngine;

public class BlockPair : MonoBehaviour
{
    public float fallInterval = 0.5f;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fallInterval)
        {
            timer = 0;
            MoveDown();
        }
    }

    void MoveDown()
    {
        if (CanMove(Vector2.down))
        {
            transform.position += Vector3.down;
        }
        else
        {
            FixToBoard();
            GameManager.Instance.SpawnPair();
            Destroy(this.gameObject);
        }
    }

    bool CanMove(Vector2 dir)
    {
        foreach (Transform child in transform)
        {
            Vector2 newPos = (Vector2)child.position + dir;
            int x = Mathf.RoundToInt(newPos.x);
            int y = Mathf.RoundToInt(newPos.y);
            if (!Board.Instance.IsInside(x, y) || Board.Instance.IsOccupied(x, y))
                return false;
        }
        return true;
    }

    void FixToBoard()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            Board.Instance.AddToGrid(x, y, child);
            child.SetParent(null);
        }
    }
}