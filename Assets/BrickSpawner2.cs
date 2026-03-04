using UnityEngine;

public class BrickSpawner2 : MonoBehaviour
{
    public GameObject brickPrefab;
    public Sprite[] brickSprites;

    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;

    [Header("Tamanho do Losango")]
    public int diamondHeight = 9;   // número total de linhas (ímpar fica melhor)

    [Header("Espaçamento")]
    public float spacingXMultiplier = 0.9f; // menor que 1 = menos espaço horizontal
    public float spacingY = 0.5f;           // menor valor = menos espaço vertical

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

        int maxColumns = diamondHeight; // largura baseada na altura
        float spacingX = (totalWidth / maxColumns) * spacingXMultiplier;

        int middle = diamondHeight / 2;

        for (int row = 0; row < diamondHeight; row++)
        {
            int bricksInRow;

            if (row <= middle)
                bricksInRow = 1 + row * 2;
            else
                bricksInRow = 1 + (diamondHeight - row - 1) * 2;

            float startX = leftLimit + (totalWidth - bricksInRow * spacingX) / 2f;

            for (int col = 0; col < bricksInRow; col++)
            {
                float posX = startX + col * spacingX;
                float posY = topLimit - 0.5f - row * spacingY;

                Vector2 position = new Vector2(posX, posY);

                GameObject newBrick = Instantiate(brickPrefab, position, Quaternion.identity);

                if (brickSprites.Length > 0)
                {
                    Sprite rowSprite = brickSprites[row % brickSprites.Length];
                    newBrick.GetComponent<SpriteRenderer>().sprite = rowSprite;
                }
            }
        }
    }
}
