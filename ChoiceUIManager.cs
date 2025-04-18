using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceUIManager : MonoBehaviour
{
    public GameObject choicePanel;
    public GameObject content;
    public GameObject ButtonPanel;
    public GameObject optionPrefab;
    public GameObject DicePanel;
    public GameObject DiceText;
    public GameObject DiceResult;
    public GameObject ImagePanel;

    private ChoiceManager choiceManager;
    private Choice currentChoice;

    public static ChoiceUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        choiceManager = FindObjectOfType<ChoiceManager>();
        choicePanel.SetActive(false);
        ButtonPanel.SetActive(false);
        DicePanel.SetActive(false);
        ImagePanel.SetActive(false);
    }

    public void StartChoice(string choiceID)
    {
        choicePanel.SetActive(true);
        ButtonPanel.SetActive(true);
        ImagePanel.SetActive(true);
        DisplayChoice(choiceID);
    }

    void DisplayChoice(string choiceID)
    {
        currentChoice = choiceManager.GetChoice(choiceID);
        if (currentChoice == null) return;

        // 清空选项
        foreach (Transform child in ButtonPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // 显示对话
        // content.GetComponent<Text>().text = $"{currentChoice.speaker}: {currentChoice.text}";
        content.GetComponent<Text>().text += "\n\n" + $"{currentChoice.speaker}: {currentChoice.text}";

        // 显示选项
        foreach (var option in currentChoice.options)
        {
            GameObject optionObj = Instantiate(optionPrefab, ButtonPanel.transform);
            // optionObj.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;
            // optionObj.GetComponent<Button>().onClick.AddListener(() => ChooseOption(option.nextChoiceID));
            optionObj.GetComponentInChildren<Text>().text = option.optionText;

            bool isEndOption = (string.IsNullOrEmpty(option.nextChoiceID) || option.nextChoiceID == "end");

            if (isEndOption)  // 结束对话
            {
                optionObj.GetComponent<Button>().onClick.AddListener(() => EndConversation());
            }
            else if (!string.IsNullOrEmpty(option.skillCheck)) // 需要判定
            {
                optionObj.GetComponent<Button>().onClick.AddListener(() => {
                    string selectedText = optionObj.GetComponentInChildren<Text>().text;
                    content.GetComponent<Text>().text += "\n\nPlayer: " + selectedText;
                    RollDice(option);
                    });
            }
            else // 普通选项
            {
                optionObj.GetComponent<Button>().onClick.AddListener(() => {
                    string selectedText = optionObj.GetComponentInChildren<Text>().text;
                    content.GetComponent<Text>().text += "\n\nPlayer: " + selectedText;
                    ChooseOption(option.nextChoiceID);
                    });
            }
        }
    }

    void ChooseOption(string nextChoiceID)
    {
        if (string.IsNullOrEmpty(nextChoiceID))
        {
            choicePanel.SetActive(false);
            ButtonPanel.SetActive(false);
            DicePanel.SetActive(false);
            return;
        }
        DisplayChoice(nextChoiceID);
    }

    void RollDice(ChoiceOption option)
    {
        DicePanel.SetActive(true);
        int diceRoll = Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7); // 3D6
        int skillValue = 0;

        if (option.skillCheck == "mind") skillValue = PlayerStats.Instance.mind;
        if (option.skillCheck == "strength") skillValue = PlayerStats.Instance.strength;
        if (option.skillCheck == "dexterity") skillValue = PlayerStats.Instance.dexterity;

        int totalRoll = diceRoll + skillValue;
        Debug.Log($"骰子值: {diceRoll} + 技能({option.skillCheck}) {skillValue} = {totalRoll}");
        DiceText.GetComponent<Text>().text = $"骰子值: {diceRoll} + 技能({option.skillCheck}) {skillValue} = {totalRoll}";

        if (totalRoll >= option.minRollRequirement)
        {
            Debug.Log("判定成功！");
            DiceResult.GetComponent<Text>().text = "判定成功！";
            ChooseOption(option.nextChoiceID); // 成功
        }
        else
        {
            Debug.Log("判定失败！");
            DiceResult.GetComponent<Text>().text = "判定失败！";
            ChooseOption(option.failChoiceID); // 失败
        }
    }

    void EndConversation()
    {
        choicePanel.SetActive(false);
        currentChoice = null;
        ButtonPanel.SetActive(false);
        DicePanel.SetActive(false);
        ImagePanel.SetActive(false);
    }

}
