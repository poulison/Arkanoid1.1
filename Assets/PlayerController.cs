using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    public Collider2D leftWall;
    public Collider2D rightWall;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * move * speed * Time.deltaTime);

        float paddleHalfWidth = GetComponent<BoxCollider2D>().bounds.extents.x;

        float minX = leftWall.bounds.max.x + paddleHalfWidth;
        float maxX = rightWall.bounds.min.x - paddleHalfWidth;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}