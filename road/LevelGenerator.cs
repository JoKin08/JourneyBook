using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("JSON 配置")]
    public TextAsset configFile;       // 配置文件的 JSON 文本

    [Header("场景引用")]
    public CarriageMovement carriage;  // 马车对象的脚本
    public NPCMovement npc;            // NPC对象的脚本
    public PlayerController player;    // 玩家对象的脚本
    public GameObject[] farBgPrefabs;  // 三组远景的 prefab 
    public GameObject tilemapPrefab;   // 用于 grids 的 prefab
    public GameObject[] nearPrefabs;   // 近景随机生成用
    public Transform nearParent;       // 近景物体的父层级

    private LevelConfigData configData;
    private float currentBgEndX = 0f;
    private float currentTilemapEndX = 0f;
    private float nextNearSpawnX = 0f;
    private bool generationFinished = false;

    void Start()
    {
        if (configFile == null)
        {
            Debug.LogError("No configFile assigned in inspector!");
            return;
        }
        // 1. 解析 JSON
        configData = JsonUtility.FromJson<LevelConfigData>(configFile.text);
        if (configData == null)
        {
            Debug.LogError("Failed to parse JSON config!");
            return;
        }

        // 2. 设置马车速度
        if (carriage != null)
        {
            carriage.SetSpeed(configData.carriageSpeed);
        }
        // 设置NPC速度
        if (npc != null)
        {
            npc.SetSpeed(configData.NPCSpeed);
        }

        // 3. 先生成一些初始的场景块，避免一开局就空
        GenerateInitialBackground();
        GenerateInitialTilemap();
    }

    void Update()
    {
        if (generationFinished) return;
        if (carriage == null) return;

        float carriageX = carriage.transform.position.x;
        float playerX = player.transform.position.x;

        float considerX = Mathf.Max(carriageX, playerX);

        // A. 背景生成
        if (considerX + configData.bgSpawnOffset >= currentBgEndX)
        {
            GenerateBackgroundChunk();
        }

        // B. Tilemap 生成
        if (considerX + configData.tilemapSpawnOffset >= currentTilemapEndX)
        {
            GenerateTilemapChunk();
        }

        // C. 近景随机生成
        if (considerX + configData.nearSpawnLookAhead >= nextNearSpawnX)
        {
            SpawnNearObject();
            nextNearSpawnX += configData.nearSpawnInterval;
        }

        // D. 判断到达终点
        if (considerX >= configData.roadLength)
        {
            generationFinished = true;
            Debug.Log("Carriage reached the end of the road!");
            carriage.SetSpeed(0);
        }
    }

    private void GenerateInitialBackground()
    {
        // // 在游戏开始时，先铺一点点，防止马车在 x=0 附近就没有场景
        // float initRange = 30f; 
        // while (currentBgEndX < carriage.transform.position.x + initRange && currentBgEndX < configData.roadLength)
        // {
        //     GenerateBackgroundChunk();
        // }
        while (currentBgEndX < configData.bgSpawnRange)
        {
            Debug.Log("Carriage at " + carriage.transform.position.x + ", currentBgEndX at " + currentBgEndX);
            GenerateBackgroundChunk();
        }
        
    }

    private void GenerateBackgroundChunk()
    {
        if (farBgPrefabs == null || farBgPrefabs.Length == 0) return;

        GameObject bgPrefab = farBgPrefabs[0];

        Vector3 spawnPos = new Vector3(currentBgEndX-30, 0f, 0f);
        Instantiate(bgPrefab, spawnPos, Quaternion.identity);

        // 更新最右端
        currentBgEndX += configData.bgChunkWidth;
    }

    // ---------------------- Tilemap生成 ----------------------
    private void GenerateInitialTilemap()
    {
        while (currentTilemapEndX < 50f)
        {
            GenerateTilemapChunk();
        }
    }

    private void GenerateTilemapChunk()
    {
        if (tilemapPrefab == null) return;

        Vector3 tilemapPos = new Vector3(currentTilemapEndX, -9.0f, 0f);
        Instantiate(tilemapPrefab, tilemapPos, Quaternion.identity);

        currentTilemapEndX += configData.tilemapWidth;
    }

    // ---------------------- 近景生成 ----------------------
    private void SpawnNearObject()
    {
        if (nearPrefabs == null || nearPrefabs.Length == 0) return;

        int idx = Random.Range(0, nearPrefabs.Length);
        GameObject nearPrefab = nearPrefabs[idx];

        float spawnX = nextNearSpawnX;
        Vector3 spawnPos = new Vector3(spawnX, -6.95f, 0f);

        Instantiate(nearPrefab, spawnPos, Quaternion.identity, nearParent);
    }
}
