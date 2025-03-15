using UnityEngine;
using System;
using System.Collections.Generic;

public class HexCell : MonoBehaviour
{
    public Material baseMaterial;
    public Material pointedMaterial;

    public HexCoordinates coordinates;
    public bool isPointedAt = false;
    private bool isPointAt_last = false;
    private Renderer objectRenderer;
    public string cellName;
    public string cellType;
    public List<GoodInfo> goodInfos;
    // cellType: town, plain, mountain, water


    // Start is called before the first frame update
    void Start()
    {
        Transform modelTransform = transform.Find("model");
        objectRenderer = modelTransform.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isPointAt_last != isPointedAt){
            if (isPointedAt){
                objectRenderer.material = pointedMaterial;
            }
            else{
                objectRenderer.material = baseMaterial;
            }
        }
        
        isPointAt_last = isPointedAt;
    }
    public override string ToString()
    {
        return coordinates.ToString();  // 调用 HexCoordinates 的 ToString 方法
    }
}

[Serializable]
public class GoodInfo
{
    public string name;    // 英文名称
    public string disp;    // 中文名称
    public float price;    // 基础价格
    public float weight;   // 每单位重量

    // 深拷贝方法
    public GoodInfo DeepClone()
    {
        return new GoodInfo
        {
            name = this.name,
            disp = this.disp,
            price = this.price,
            weight = this.weight
        };
    }
}

[Serializable]
public class GoodList
{
    public List<GoodInfo> goods; // 商品列表
}
