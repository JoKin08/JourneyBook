using UnityEngine;
using UnityEngine.UI;

public class CarriageBoarding : MonoBehaviour
{
    [Header("References")]
    public PlayerController player;    
    public Transform carriage;         // 马车Transform

    [Header("Boarding Settings")]
    public float boardRadius = 1.5f;          // 允许上车的距离
    public KeyCode boardKey = KeyCode.E;      // 上车按键
    public KeyCode dismountKey = KeyCode.Q;   // 下车按键
    public KeyCode talkKey = KeyCode.F;   // 对话按键
    public Vector3 onCarriageOffset = new Vector3(0f, 0.5f, 0f); // 上车后相对马车的本地偏移
    public Vector3 offCarriageOffset = new Vector3(0f, -1.5f, 0f);   // 下车后相对马车的本地偏移
    private bool isOnCarriage = false;        // 是否已经上车

    [Header("UI")]
    public Text promptText;  // 提示框1
    public Text promptText2;  // 提示框2

    void Start()
    {
        // 初始化时先隐藏提示
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
            promptText2.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || carriage == null) return;

        if (!isOnCarriage)
        {
            HidePrompt2();
            // 1. 当玩家未上车，检测距离
            float dist = Vector2.Distance(player.transform.position, carriage.position);

            if (dist <= boardRadius)
            {
                
                // 显示“按 E 上车”
                ShowPrompt($"按 [{boardKey}] 上车");
                // 若按下 E，执行上车
                if (Input.GetKeyDown(boardKey))
                {
                    BoardCarriage();
                }
            }
            else
            {
                // 不在范围就隐藏提示
                HidePrompt();
                HidePrompt2();
            }
        }
        else
        {
            // 2. 当已经在车上，提示“按 Q 下车”
            ShowPrompt($"按 [{dismountKey}] 下车");
            if (Input.GetKeyDown(dismountKey))
            {
                DismountCarriage();
            }

            // 3. 对话
            ShowPrompt2($"按 [{talkKey}] 对话");
            if (Input.GetKeyDown(talkKey))
            {
                // 对话
                Debug.Log("对话");
                FindObjectOfType<ChoiceUIManager>().StartChoice("start"); 
            }
        }
    }

    void BoardCarriage()
    {
        if (isOnCarriage) return;

        // 禁用玩家移动
        player.canMove = false;

        // 将玩家成为马车子对象
        player.transform.SetParent(carriage, worldPositionStays: false);

        player.transform.rotation = Quaternion.identity;
        
        // 设定玩家在马车本地偏移(相对位置)
        player.transform.localPosition = onCarriageOffset;

        isOnCarriage = true;
        Debug.Log("玩家已上车");
    }

    void DismountCarriage()
    {
        if (!isOnCarriage) return;

        // 解除父子关系
        player.transform.SetParent(null, true);

        Vector3 scale = player.transform.localScale;
        scale.x = Mathf.Abs(scale.x); // 让x为正
        player.transform.localScale = scale;

        player.transform.rotation = Quaternion.identity;
        
        // 设定玩家在马车本地偏移(相对位置)
        player.transform.position = carriage.position + offCarriageOffset;

        // 恢复玩家移动
        player.canMove = true;
        isOnCarriage = false;
        Debug.Log("玩家下车");
    }

    private void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = message;
        }
    }

    private void ShowPrompt2(string message)
    {
        if (promptText2 != null)
        {
            promptText2.gameObject.SetActive(true);
            promptText2.text = message;
        }
    }

    private void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void HidePrompt2()
    {
        if (promptText2 != null)
        {
            promptText2.gameObject.SetActive(false);
        }
    }
}