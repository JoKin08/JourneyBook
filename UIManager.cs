using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    [SerializeField]
    private Text textView;
    [SerializeField]
    private ScrollRect scrollControl;
    public void Start()
    {
        textView.text = "你的旅程开始了。";
    }
    public void WriteText(string text)
        {
            textView.text += "\n"+ text;
            scrollControl.verticalNormalizedPosition = 0f;
        }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();        
        scrollControl.verticalNormalizedPosition = 0f;
    }
}
