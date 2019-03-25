using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFixed : MonoBehaviour
{
    //初始宽度 
    public float originalWidth = 2340f;
    //初始高度  
    public float originalHeight = 1080f;

    //当前X位置
    private float curPosX;
    //当前Y位置
    private float curPosY;

    void Start()
    {
        gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
    }
}
