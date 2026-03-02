using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;
    public Sprite[] brickSprites;

    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;

    public int rows = 5;
    public int columns = 10;

    public float spacingY = 0.6f;

    void Start()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        Collider2D leftCol = leftWall.GetComponent<Collider2D>();
        Collider2D rightCol = rightWall.GetComponent<Collider2D>();
        Collider2D topCol = topWall.GetComponent<Collider2D>();

        float leftLimit = leftCol.bounds.max.x;
        float rightLimit = rightCol.bounds.min.x;
        float topLimit = topCol.bounds.min.y;

        float totalWidth = rightLimit - leftLimit;
        float spacingX = totalWidth / columns;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float posX = leftLimit + spacingX * col + spacingX / 2f;
                float posY = topLimit - 0.5f - row * spacingY;

                Vector2 position = new Vector2(posX, posY);

                GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity);

                if (brickSprites.Length > 0)
                {
                    Sprite randomSprite = brickSprites[Random.Range(0, brickSprites.Length)];
                    newBrick.GetComponent<SpriteRenderer>().sprite = randomSprite;
                }
            }
        }
    }
}