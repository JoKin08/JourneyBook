using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public static HintManager instance;
    public GameObject hintBox;
    public Text hintText;

    [TextArea(1, 3)]
    public string[] hintLines;
    [SerializeField] public int currentLine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        hintText.text = hintLines[currentLine];
    }

    private void Update()
    {
        if (hintBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // currentLine++;
                // if (currentLine < dialogueLines.Length)
                // {
                //     dialogueText.text = dialogueLines[currentLine];
                // }
                // else
                // {
                //     dialogueBox.SetActive(false);
                // }
                hintBox.SetActive(false);
            }
        }
    
    }

    public void ShowHint(string[] hint, int current)
    {
        hintLines = hint;
        currentLine = current;
        hintText.text = hintLines[currentLine];
        // Debug.Log("Show hint");
        hintBox.SetActive(true);
    }
    public void HideHint()
    {
        hintBox.SetActive(false);
    }
 }
