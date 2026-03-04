using UnityEngine;

public class DeathZone2 : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ball"))
    {
        BallControl ball = other.GetComponent<BallControl>();

        if (ball != null)
        {
            if (GameManager2.instance != null)
                GameManager2.instance.LoseLife();

            ball.RestartFromCenter();
        }
    }
}
}