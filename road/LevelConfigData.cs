using UnityEngine;

[System.Serializable]
public class LevelConfigData
{
    // 原本就有
    public float roadLength;         // 整条路的长度
    public float carriageSpeed;      // 马车速度

    public float NPCSpeed;           // NPC速度

    // 背景生成相关
    public float bgChunkWidth;
    public float bgSpawnRange;
    public float bgSpawnOffset;

    // Tilemap生成相关
    public float tilemapWidth;
    public float tilemapSpawnOffset;

    // 近景生成相关
    public float nearSpawnInterval;
    public float nearSpawnLookAhead;
}
