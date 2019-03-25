using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFixed : MonoBehaviour
{
    //初始宽度 
    public float originalWidth = 2340f;
    //初始高度  
    public float originalHeight = 1080f;

    // Use this for initialization
    void Start()
    {
        transform.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / originalWidth, Screen.height / originalHeight, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
