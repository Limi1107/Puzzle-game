using UnityEngine;

public class blockPair : MonoBehaviour
{
    // ¡‚Í‰½‚à‚³‚¹‚È‚¢
    // Board‚ª‚·‚×‚Ä§Œä‚·‚é

    public void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void Rotate()
    {
        transform.Rotate(0, 0, 90);
    }
}