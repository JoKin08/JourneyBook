using System;
using System.Collections.Generic;
using UnityEngine;

// 定义物品类别枚举
public enum ItemCategory {
    Cargo,    // 货物
    Supplies, // 补给
    Props     // 道具
}

[Serializable]
public class StaticItemData {
    public string itemID;
    public ItemCategory category;   // 使用枚举类型
    public string itemName;
    public string description;
    public string iconPath;         // 用于动态加载 Sprite 的路径
    public float unitWeight;        // 每单位物品重量
    public float unitCapacity;      // 每单位物品占用的马车容量
}

[Serializable]
public class StaticItemDataContainer {
    public List<StaticItemData> items;
}
