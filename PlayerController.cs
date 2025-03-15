using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb; 
    public bool canMove = true;

    [Header("Carriage Reference")]
    public Transform carriage; // 马车
    public float maxDistanceBehindCarriage = 155f;
    public float moveSpeed = 5f;

    private float idleTimer = 0f; //无输入时间计时器

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // horizontal: -1: left, 0: no movement, 1: right
            float horizontal = Input.GetAxisRaw("Horizontal");
            // vertical: -1: down, 0: no movement, 1: up
            float vertical = Input.GetAxisRaw("Vertical");

            //计时
            if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f)
            {
                idleTimer += Time.deltaTime;
            }
            else
            {
                idleTimer = 0f;
                moveSpeed = 5f;
            }

            if (carriage != null && transform.position.x < carriage.position.x && idleTimer >= 5f)
            {
                horizontal = 1;
                vertical = 0;
            }

            if (carriage != null && transform.position.x >= (carriage.position.x - 1) && idleTimer >= 5f)
            {
                moveSpeed = 1.2f;
            }

            if (horizontal!=0)
            {
                ani.SetFloat("Horizontal", horizontal);
                ani.SetFloat("Vertical", 0);
            }
            if (vertical!=0)
            {
                ani.SetFloat("Vertical", vertical);
                ani.SetFloat("Horizontal", 0);
            }

            // move
            Vector2 move = new Vector2(horizontal, vertical);
            ani.SetFloat("Speed", move.magnitude);
            if (horizontal != 0 && vertical == 0)
            {
                transform.Translate(move * Time.deltaTime * moveSpeed);
            }

            if (carriage != null)
            {
                // 计算允许的最大 x
                float maxAllowedX = carriage.position.x + maxDistanceBehindCarriage;
                
                // 如果玩家的 x 超过这个值，则强制夹在这个值内
                Vector3 pos = transform.position;
                if (pos.x > maxAllowedX)
                {
                    pos.x = maxAllowedX;
                    transform.position = pos;
                }
            }
        }
    }
}
