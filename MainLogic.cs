using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainLogic : MonoBehaviour
{
    public playerMove playerMover;
    public FlyCamera cameraMover;
    public HexGrid gridObject;
    public UIManager uiManager;
    private PathManager pathManager = new PathManager();
    static private CameraSwitcher cameraSwitcher;
    private CanvasGroup mapCanvasGroup;
    public BlockInfoUIManager blockInfoManager;

    void Start()
    {
		playerMover.transform.SetParent(transform, false);
        playerMover.transform.localPosition = new Vector3(0, 3.7f, 0);
        cameraMover.transform.position = playerMover.transform.position + new Vector3(0, 10f, -15f);
        cameraMover.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
        pathManager.myMover = playerMover;
        pathManager.awake(gridObject.cells[0]);
        pathManager.uiManager = uiManager;
        cameraSwitcher = GetComponent<CameraSwitcher>();
        mapCanvasGroup = uiManager.GetComponent<CanvasGroup>();
        blockInfoManager.setCanvasVisibility(false);
    }

    // Update is called once per frame
    public void Update()
    {
        setMapCanvasVisibility();
        // Debug.Log(GetMousePointedObject());
        Mouse mouse = Mouse.current;
        playerMover.followMouse();
        if(mouse.leftButton.wasPressedThisFrame){
            GameObject clickObject = GetMousePointedObject();
            if(clickObject != null){
                HexCell cellObject = clickObject.GetComponent<HexCell>();
                if(cellObject != null){
                    pathManager.addNewPoint(cellObject);
                }
            }
        }
        
        if(mouse.rightButton.wasPressedThisFrame){
            if(IsMouseOverRect(blockInfoManager.GetComponent<RectTransform>())){
                blockInfoManager.setCanvasVisibility(true);
            }
            GameObject clickObject = GetMousePointedObject();
            if(clickObject != null){
                HexCell cellObject = clickObject.GetComponent<HexCell>();
                if(cellObject != null){
                    blockInfoManager.setData(cellObject);
                }
            }
        }
        pathManager.updataState();
    }
    public GameObject getPointedObject(){
        GameObject hitParentObject = null;
        Vector3 mousePosition = Input.mousePosition;

        // 从摄像机通过鼠标指向的方向发射射线
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            hitParentObject = hit.collider.transform.gameObject;
        }
        return hitParentObject;
    }
    public GameObject GetMousePointedObject()
    {
        // 检查是否指向UI对象
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 获取鼠标下的UI对象
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            // 存储射线检测结果
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            // 过滤掉Layer为Ignore Raycast的对象
            foreach (var result in results)
            {
                if (result.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                {
                    return result.gameObject; // 返回第一个非Ignore Raycast的UI对象名称
                }
            }
        }
        // 检查是否指向3D世界中的对象
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 返回射线检测到的3D对象的名称
            return hit.collider.gameObject;
        }

        // 如果没有检测到任何对象，返回空字符串
        return null;
    }
    public void toSceneWageon(){
        SceneManager.LoadScene("SceneWagon");
    }
    public void setMapCanvasVisibility(){
        cameraMover.mapEnabled = cameraSwitcher.cameraMap.enabled;
        if(cameraSwitcher.cameraMap.enabled){
            mapCanvasGroup.alpha = 1;
            mapCanvasGroup.interactable = true;
            mapCanvasGroup.blocksRaycasts = true;
        }
        else{
            mapCanvasGroup.alpha = 0;
            mapCanvasGroup.interactable = false;
            mapCanvasGroup.blocksRaycasts = false;
            blockInfoManager.setCanvasVisibility(cameraMover.mapEnabled);
        }
    }

    public static bool IsMouseOverRect(RectTransform regionRect)
    {
        // if (objectWithRectTransform == null)
        // {
        //     Debug.LogError("GameObject 参数不能为空！");
        //     return false;
        // }

        // // 获取 GameObject 的 RectTransform
        // RectTransform regionRect = objectWithRectTransform.GetComponent<RectTransform>();
        // if (regionRect == null)
        // {
        //     Debug.LogError("objectWithRectTransform 没有 RectTransform 组件！");
        //     return false;
        // }

        // 将鼠标屏幕坐标转换为 Canvas 的局部坐标
        Vector2 localMousePosition;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            regionRect,
            Input.mousePosition,
            cameraSwitcher.cameraMap,
            out localMousePosition
        );

        // // 检查坐标是否在 regionRect 的矩形范围内
        // Debug.Log(isInside && regionRect.rect.Contains(localMousePosition));
        return regionRect.rect.Contains(localMousePosition);
    }

}
