using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public HexCoordinates currentCoordinate = new HexCoordinates(0, 0);
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void followMouse(){
        // player model face to pointed direction
    }
    public void moveToCell(HexCoordinates cellCoordinate){
        Debug.Log((cellCoordinate.xyz_world.z, cellCoordinate.xyz_world.y, cellCoordinate.xyz_world.z));
        transform.localPosition = new Vector3(cellCoordinate.xyz_world.x, 3.7f, cellCoordinate.xyz_world.z);
    }
}
