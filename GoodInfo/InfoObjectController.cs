using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InfoObject : MonoBehaviour
{
    public UnityEngine.UI.Image logoImage;
    public Text priceText;
    public Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setImage(){

    }
    public void setPriceValue(float value){
        priceText.text = value.ToString();
    }
    public void setName(string name){
        nameText.text = name;
    }
    public void setPosition(Vector3 position){
        transform.localPosition = position;
        Debug.Log(transform.localPosition);
    }
    public void destroySelf(){
        Destroy(gameObject);
    }
}
