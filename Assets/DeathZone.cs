using UnityEngine;

public class DeathZone : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ball"))
    {
        BallControl ball = other.GetComponent<BallControl>();

        if (ball != null)
        {
            if (GameManager.instance != null)
                GameManager.instance.LoseLife();

            ball.RestartFromCenter();
        }
    }
}
}