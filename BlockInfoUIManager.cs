using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInfoUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup BlockInfoCanvasGroup;
    public Canvas BlockInfoCanvas;
    public bool activated;
    
    public Text blockNameText;
    public Text blockPriceText;

    public List<InfoObject>goodInfoObjectList;
    public InfoObject goodInfoPrefab;
    public GameObject GoodInfoParent;
    public float goodInfo_offset_x = 100f;
    public float goodInfo_offset_y = 40f;
    void Start()
    {
        BlockInfoCanvasGroup = GetComponent<CanvasGroup>();
        BlockInfoCanvas = GetComponent<Canvas>();
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCanvasVisibility(bool new_stat){
        activated = new_stat;
        if(activated){
            BlockInfoCanvasGroup.alpha = 1;
            BlockInfoCanvasGroup.interactable = true;
            BlockInfoCanvasGroup.blocksRaycasts = true;
        }
        else{
            BlockInfoCanvasGroup.alpha = 0;
            BlockInfoCanvasGroup.interactable = false;
            BlockInfoCanvasGroup.blocksRaycasts = false;
        }
    }

    public void setData(HexCell cell){
        resetGoodInfo();
        setBlockName(cell.cellName);
        setPriceInfo(cell.goodInfos);
    }

    public void setBlockName(string name){
        blockNameText.text = name;
    }
    public void setPriceInfo(List<GoodInfo>goodInfoList){
        foreach (GoodInfo goodInfo in goodInfoList){
            Vector3 panelPosition = getPositionByIndex(goodInfoObjectList.Count);
            addGoodInfo(goodInfo, panelPosition);
        }
    }

    public void addGoodInfo(GoodInfo goodInfo, Vector3 panelPosition){
        InfoObject goodInfoObject_i = Instantiate(goodInfoPrefab);
        goodInfoObject_i.transform.SetParent(GoodInfoParent.transform);
        goodInfoObject_i.setPriceValue(goodInfo.price);
        goodInfoObject_i.setName(goodInfo.disp);
        goodInfoObjectList.Add(goodInfoObject_i);
        goodInfoObject_i.setPosition(panelPosition);
    }
    Vector3 getPositionByIndex(int goodIndex){
        return new Vector3(goodInfo_offset_x, goodIndex * goodInfo_offset_y * -1, 0);
    }
    public void resetGoodInfo(){
        foreach (var infoObject in goodInfoObjectList)
        {
            infoObject.destroySelf();
        }
        goodInfoObjectList.Clear();
    }
}
