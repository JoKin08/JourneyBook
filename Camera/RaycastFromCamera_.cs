using UnityEngine;

public class RaycastFromCamera_ : MonoBehaviour
{
    // 用于存储射线碰撞到的父对象
    private GameObject hitParentObject;
    private HexCell pointedObjectOld = null;
    private HexCell pointedObject = null;
    public Camera mapCamera;

    void Update()
    {
        // 获取鼠标位置
        Vector3 mousePosition = Input.mousePosition;

        // 从摄像机通过鼠标指向的方向发射射线
        Ray ray = mapCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // 如果射线碰撞到物体
        if (Physics.Raycast(ray, out hit))
        {
            // 获取碰撞物体的父对象
            hitParentObject = hit.collider.transform.parent?.gameObject;

            // 如果碰撞到的物体有 PointableObject 组件
            pointedObject = hit.collider.GetComponent<HexCell>();
            if (pointedObject != pointedObjectOld)
            {
                // 更新被指向的状态
                if(pointedObject != null){
                    pointedObject.isPointedAt = true;
                }
                if(pointedObjectOld != null){
                    pointedObjectOld.isPointedAt = false;
                }
                // 可选：在控制台输出父对象的名称
                // Debug.Log("Hit Parent Object: " + hitParentObject?.name);
            }
        }
        else
        {
            // 如果没有碰撞到物体
            hitParentObject = null;
            pointedObject = null;

            // 检查是否有其他对象的 isPointedAt 需要更新
            HexCell[] allPointableObjects = FindObjectsOfType<HexCell>();
            foreach (HexCell obj in allPointableObjects)
            {
                // 如果物体之前被指向，恢复其状态
                obj.isPointedAt = false;
            }

            // Debug.Log("No object hit by ray.");
        }
        pointedObjectOld = pointedObject;
    }

    // 获取射线碰撞到的父对象
    // public GameObject GetHitParentObject()
    // {
    //     return hitParentObject;
    // }
}
