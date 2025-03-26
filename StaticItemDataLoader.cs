using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StaticItemDataLoader : MonoBehaviour {
    public string jsonFileName = "StaticItemData.json";
    public List<StaticItemData> itemDataList = new List<StaticItemData>();

    void Awake() {
        LoadStaticItemData();
    }

    void LoadStaticItemData() {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        if (File.Exists(filePath)) {
            string jsonContent = File.ReadAllText(filePath);
            StaticItemDataContainer container = JsonUtility.FromJson<StaticItemDataContainer>(jsonContent);
            if (container != null && container.items != null) {
                itemDataList = container.items;
                // 遍历每个物品，动态加载图标（假设图标放在 Resources 文件夹下）
                foreach (var item in itemDataList) {
                    Sprite sprite = Resources.Load<Sprite>(item.iconPath);
                    if (sprite == null) {
                        Debug.LogWarning("无法加载图标: " + item.iconPath);
                    } else {
                        // 你可以将加载的 Sprite 存储起来以便后续在 UI 中使用
                    }
                }
            } else {
                Debug.LogError("解析 JSON 数据失败");
            }
        } else {
            Debug.LogError("找不到文件: " + filePath);
        }
    }
}
