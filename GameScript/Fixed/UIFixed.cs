using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFixed : MonoBehaviour
{
    //初始宽度 
    private float originalWidth = 2340f;
    //初始高度  
    private float originalHeight = 1080f;

    //当前X位置
    private float curPosX;
    //当前Y位置
    private float curPosY;

    // Use this for initialization
    void Start()
    {
        curPosX = transform.localPosition.x;
        curPosY = transform.localPosition.y;
        transform.localPosition = new Vector3(curPosX / originalWidth * Screen.width, curPosY / originalHeight * Screen.width, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
