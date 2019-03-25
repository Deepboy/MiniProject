using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFixed : MonoBehaviour
{
    //初始宽度 
    public float originalWidth = 2340f;
    //初始高度  
    public float originalHeight = 1080f;

    //当前X位置
    private float curPosX;
    //当前Y位置
    private float curPosY;
    
    
    private List<GameObject> depthSearch;

    /// <summary>
    /// 深度搜索
    /// </summary>
    private void DeepSearch()
    {
        while (depthSearch.Count > 0)
        {
            if (depthSearch[0].GetComponent<RectTransform>() != null)
            {
                curPosX = depthSearch[0].transform.localPosition.x;
                curPosY = depthSearch[0].transform.localPosition.y;
                depthSearch[0].transform.localPosition = new Vector3(curPosX / originalWidth * Screen.width, curPosY / originalHeight * Screen.height, 0);
            }
            for (int i = 0; i < depthSearch[0].transform.childCount; ++i)
            {
                if (depthSearch[0].transform.GetChild(i).GetComponent<RectTransform>() != null)
                {
                    curPosX = depthSearch[0].transform.GetChild(i).localPosition.x;
                    curPosY = depthSearch[0].transform.GetChild(i).localPosition.y;
                    depthSearch[0].transform.GetChild(i).localPosition = new Vector3(curPosX / originalWidth * Screen.width, curPosY / originalHeight * Screen.height, 0);
                }
                depthSearch.Add(depthSearch[0].transform.GetChild(i).gameObject);
            }
            depthSearch.RemoveAt(0);
        }
    }

    // Use this for initialization
    void Start()
    {
        depthSearch = new List<GameObject>();
        depthSearch.Add(gameObject);
        DeepSearch();
    }
}
