using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider2D leftWall;
    public Collider2D rightWall;

    void Update()
    {
        float paddleHalfWidth = GetComponent<BoxCollider2D>().bounds.extents.x;

        float minX = leftWall.bounds.max.x + paddleHalfWidth;
        float maxX = rightWall.bounds.min.x - paddleHalfWidth;

        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; 

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(worldPos.x, minX, maxX);

        transform.position = pos;
    }
}
