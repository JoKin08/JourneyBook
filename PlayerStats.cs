using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int mind;
    public int strength;
    public int dexterity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadPlayerStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadPlayerStats()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "player.json");
        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonText);
            mind = data.player.skills.mind;
            strength = data.player.skills.strength;
            dexterity = data.player.skills.dexterity;
        }
        else
        {
            Debug.LogError("未找到 player.json 文件！");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public PlayerInfo player;
}

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public SkillSet skills;
}

[System.Serializable]
public class SkillSet
{
    public int mind;
    public int strength;
    public int dexterity;
}