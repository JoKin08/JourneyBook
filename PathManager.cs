using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathManager
{ 
    public HexCell currentAt;
    public playerMove myMover;
    public float speed = 0.1f;
    public float def_path_dis = 100f;
    Deque<PathUnit> pathUnits = new Deque<PathUnit>();
    public UIManager uiManager;
    

    public void addNewPoint(HexCell newCell){
        HexCell from = null;
        HexCell to = newCell;
        if(pathUnits.PeekLast() == null){
            from = currentAt;
        }
        else if(newCell != pathUnits.PeekLast().cellTo){
            from = pathUnits.PeekLast().cellTo;
        }
        if((from != null) && isNeighborhood(from, to)){
            pathUnits.AddLast(new PathUnit(from, to, def_path_dis));
        }
    }
    public void debugSeeQueue()
    {
        string output = "";
        foreach (PathUnit pathunit in pathUnits)
        {
            // 输出 cell 的 x, y, z，cell 之间用换行符隔开
            output += $"{pathunit}\n";
        }
        Debug.Log(output);
    }
    public void awake(HexCell initialAt){
        currentAt = initialAt;
    }
    public void updataState(){
        PathUnit currentPathUnit = pathUnits.PeekFirst();
        if(currentPathUnit != null){
            if(currentPathUnit.currentDis < speed){
                uiManager.WriteText(currentDescribe());
            }
            currentPathUnit.moveForward(speed);
            currentPathUnit.setMover(myMover);
            if(currentPathUnit.stage == "up"){
                currentAt = currentPathUnit.cellFrom;
            }
            if(currentPathUnit.stage == "down"){
                currentAt = currentPathUnit.cellTo;
            }
            if(currentPathUnit.currentDis > currentPathUnit.totalDis){
                currentAt = currentPathUnit.cellTo;
                currentPathUnit.DestroyCylinder();
                pathUnits.RemoveFirst();
            }
        }
    }
    string currentDescribe(){
        return $"正在从<b>{pathUnits.PeekFirst().cellFrom.cellName}</b>前往<b>{pathUnits.PeekFirst().cellTo.cellName}</b>的路上...";
    }
    bool isNeighborhood(HexCell A, HexCell B){
        Vector3 dis = A.coordinates.xyz - B.coordinates.xyz;
        return Math.Abs(dis.x) <= 1 && Math.Abs(dis.y) <= 1 && Math.Abs(dis.z) <= 1 && dis != new Vector3(0,0,0);
    }

}