using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ChoiceUIManager>().StartChoice("start"); 
        }
    }
}
