using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueBox;
    public Text dialogueText;

    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

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
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
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
                dialogueBox.SetActive(false);
                HintManager.instance.ShowHint(HintManager.instance.hintLines, HintManager.instance.currentLine);
            }
        }
    
    }

    public void ShowDialogue(string[] dialogue, int current)
    {
        dialogueLines = dialogue;
        currentLine = current;
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
    }
 }
