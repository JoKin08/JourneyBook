using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;
    [TextArea(1,3)]
    public string[] dialogueLines;
    [SerializeField] private int current = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("I am triggered!");
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player is near me!");
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player is far from me!");
            isEntered = false;
        }
    }

    private void Update()
    {
        if (isEntered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HintManager.instance.HideHint();
                DialogueManager.instance.ShowDialogue(dialogueLines, current);
                if (current < dialogueLines.Length - 1)
                {
                    current++;
                }
                
            }
        }
    }
}
