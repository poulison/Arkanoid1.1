using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;

    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 8;
    public float spacingX = 1.2f;
    public float spacingY = 0.6f;

    [Header("Start Position")]
    public Vector2 startPosition = new Vector2(-4f, 3f);

    void Start()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(
                    startPosition.x + col * spacingX,
                    startPosition.y - row * spacingY
                );

                Instantiate(brickPrefab, position, Quaternion.identity);
            }
        }
    }
}