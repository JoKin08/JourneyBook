using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;


public class HexGrid : MonoBehaviour {
    public float sq3 = 1.73205f;
    public float cell_size = 1.0f;
    public float cell_gap = 1.0f;
	public int width = 6;
	public int height = 6;
    public Text cellLabelPrefab;
    Canvas gridCanvas;

	public HexCell cellPrefab;
    public HexCell[] cells;

	[System.Serializable]
	public class CellNameResource
	{
		public List<string> Prefix;
		public List<string> Type;
	}	
	Dictionary<string, List<string>> cellNameResource = new Dictionary<string, List<string>>();
	public List<GoodInfo> goodsListBase;

	void Awake () {
		goodsListBase = readBaseGoodInfo();

		// JSON 文件路径
        string cellNamefilePath = Path.Combine(Application.dataPath, "StreamingAssets/names/cellName.json");

        // 读取 JSON 文件并解析为对象
        if (File.Exists(cellNamefilePath))
        {
            CellNameResource data = JsonUtility.FromJson<CellNameResource>(File.ReadAllText(cellNamefilePath));

            // 将对象转换为 Dictionary
            cellNameResource = new Dictionary<string, List<string>>
            {
                { "Prefix", data.Prefix },
                { "Type", data.Type }
            };
        }
        else
        {
            Debug.LogError("JSON 文件未找到: " + cellNamefilePath);
        }


		cells = new HexCell[height * width];
        gridCanvas = GetComponentInChildren<Canvas>();

		for (int x = 0, i = 0; x < height; x++) {
			for (int z = 0; z < width; z++) {
				CreateCell(x, z, i++);
			}
		}
		
	}
	
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * cell_gap;
		position.y = 0f;
		position.z = z * sq3 * cell_gap / 2;

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.localScale = new Vector3(cell_size, cell_size, cell_size);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z, cell_gap, sq3);
		cell.cellName = createCellName();
		cell.goodInfos = getRandomGoodInfoList();
        Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
		label.text = cell.cellName;
	}

	string createCellName(){
		string name = "";
		name += GetRandomString(cellNameResource["Prefix"], 1, 3);
		name += GetRandomString(cellNameResource["Type"], 1, 1);
		return name;
	}

	// 从列表中随机抽取指定数量的元素并拼接成字符串
    static string GetRandomString(List<string> list, int minCount, int maxCount)
    {
        if (list == null || list.Count < minCount)
        {
            throw new ArgumentException("列表不能为空，且元素数量必须大于等于最小抽取数量。");
        }

        System.Random random = new System.Random();
        int count = random.Next(minCount, maxCount + 1); // 随机确定抽取数量
        count = Math.Min(count, list.Count); // 确保不超过列表长度

        // 使用 LINQ 的 OrderBy 和 Take 方法随机抽取元素
        List<string> randomElements = list.OrderBy(x => random.Next()).Take(count).ToList();

        // 将抽取的元素拼接成一个字符串
        string result = string.Join("", randomElements);

        return result;
    }


	string ReadTextFile(string filePath)
	{
		// Check if the file exists
		if (!File.Exists(filePath))
		{
			Debug.LogError("File not found: " + filePath);
			return null;
		}

		try
		{
			// Read all text from the file
			string fileContent = File.ReadAllText(filePath);
			return fileContent;
		}
		catch (System.Exception e)
		{
			Debug.LogError("Error reading file: " + e.Message);
			return null;
		}
	}

	public List<GoodInfo> getRandomGoodInfoList()
    {
        List<GoodInfo> goodInfoListVariant = new List<GoodInfo>();
        System.Random random = new System.Random();

        // 随机漏掉 1-2 个商品
        int skipCount = random.Next(1, 3); // 随机生成 1 或 2

        // 遍历基础商品列表
        foreach (var good in goodsListBase)
        {
            // 随机决定是否跳过当前商品
            if (skipCount > 0 && random.Next(2) == 0) // 50% 的概率跳过
            {
                skipCount--;
                continue;
            }

            // 深拷贝商品
            GoodInfo clonedGood = good.DeepClone();

            // 添加价格波动（±10%）
            float priceVariation = (float)(random.NextDouble() * 0.2 - 0.1); // -10% 到 +10%
            clonedGood.price *= 1 + priceVariation;

            // 将商品添加到变体列表
            goodInfoListVariant.Add(clonedGood);
        }

        return goodInfoListVariant;
    }

	List<GoodInfo> readBaseGoodInfo(){
		
        string GoodInfoBasefilePath = Path.Combine(Application.dataPath, "StreamingAssets/names/goodInfo.json");
		// Read good info list
		GoodList goodsListBaseReadin = JsonUtility.FromJson<GoodList>(File.ReadAllText(GoodInfoBasefilePath));
		List<GoodInfo> goodInfos = goodsListBaseReadin.goods;
		return goodInfos;
	}

}