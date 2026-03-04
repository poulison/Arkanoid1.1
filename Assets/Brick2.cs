using UnityEngine;

public class Brick2 : MonoBehaviour
{
    public int points = 10;
    public int hitsToBreak = 1;

    public Sprite damagedSprite; // sprite quando sofre 1 hit

    private int currentHits;
    private SpriteRenderer sr;
    private Sprite originalSprite;

    void Start()
    {
        currentHits = hitsToBreak;
        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
    }

    
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        currentHits--;

        if (currentHits <= 0)
{
        if (GameManager2.instance != null)
        {
            GameManager2.instance.AddScore(points);
            GameManager2.instance.BrickDestroyed();
    }

        FindFirstObjectByType<BallControl>().IncreaseSpeed();
        Destroy(gameObject);
}
        else
        {
            // Só troca a sprite se ainda tiver vida
            if (damagedSprite != null)
                sr.sprite = damagedSprite;
        }
    }
}