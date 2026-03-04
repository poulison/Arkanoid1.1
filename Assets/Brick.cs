using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 10;
    public int hitsToBreak = 1;

    private int currentHits;

    void Start()
    {
        currentHits = hitsToBreak;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        currentHits--;

        if (currentHits <= 0)
{
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(points);
            GameManager.instance.BrickDestroyed();
    }

        FindFirstObjectByType<BallControl>().IncreaseSpeed();
        Destroy(gameObject);
}
}
}