using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
public class ChoiceManager : MonoBehaviour
{
    private Dictionary<string, Choice> choiceDict = new Dictionary<string, Choice>();

    private void Start()
    {
        LoadChoices();
    }

    void LoadChoices()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "choices.json");
        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            ChoiceData data = JsonConvert.DeserializeObject<ChoiceData>(jsonText);
            foreach (var choice in data.choices)
            {
                choiceDict[choice.id] = choice;
            }
        }
        else
        {
            Debug.LogError("未找到对话文件: " + filePath);
        }
    }

    public Choice GetChoice(string choiceID)
    {
        return choiceDict.ContainsKey(choiceID) ? choiceDict[choiceID] : null;
    }
}

public class ChoiceData
{
    public List<Choice> choices;
}
