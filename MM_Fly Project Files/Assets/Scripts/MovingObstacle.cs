using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float deadZone = -50f;

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
