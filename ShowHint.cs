using UnityEngine;

public class ShowHint : MonoBehaviour
{
    [SerializeField] private bool isEntered;
    [TextArea(1,3)]
    public string[] hintLines;
    [SerializeField] private int current = 0;
    private bool hintDisplayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("I am triggered!");
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player is near me!");
            isEntered = true;
            if (!hintDisplayed)
            {
                HintManager.instance.ShowHint(hintLines, current);
                hintDisplayed = true;  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player is far from me!");
            isEntered = false;
            HintManager.instance.HideHint();
            hintDisplayed = false;
        }
    }

    private void Update()
    {
        if (isEntered && !hintDisplayed)
        {
            HintManager.instance.ShowHint(hintLines, current);
            hintDisplayed = true;  
        }

        if (isEntered && Input.GetKeyDown(KeyCode.Space))
        {
            if (current < hintLines.Length - 1)
            {
                current++;
            }

            HintManager.instance.ShowHint(hintLines, current);
        }
    }
}
