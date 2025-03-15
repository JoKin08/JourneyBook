using UnityEngine;

public class CarriageMovement : MonoBehaviour
{
    private float speed = 0f;  // 在generator中设置

    public void SetSpeed(float s)
    {
        speed = s;
    }

    private void Update()
    {
        // 每帧向右移动 speed * dt
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
