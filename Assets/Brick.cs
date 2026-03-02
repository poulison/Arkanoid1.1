using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (GameManager.instance != null)
                GameManager.instance.AddScore(points);

            Destroy(gameObject);
        }
    }
}