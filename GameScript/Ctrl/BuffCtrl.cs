using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewFightConfig;
using UnityEngine.UI;

public class BuffCtrl : MonoBehaviour
{
    public BuffClass buffClass = BuffClass.Random;
    public float originalDurationTime;
    public BaseBuff baseBuff;
    public bool isPVP=false;
    public string BuffName;
    public string BuffDescription;

    /// <summary>
    /// 指针移入事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseEnter(GameObject gameObject)
    {
        if (isPVP)
        {
            gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = BuffName + " " + BuffDescription;
            gameObject.transform.Find("Text").gameObject.SetActive(true);
        }
        else {
            gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = baseBuff.BuffName + " " + baseBuff.BuffDescription;
            gameObject.transform.Find("Text").gameObject.SetActive(true);
            // gameObject.transform.Find("bg").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 指针移出事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseExit(GameObject gameObject)
    {
        gameObject.transform.Find("Text").gameObject.SetActive(false);
        //gameObject.transform.Find("bg").gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        gameObject.transform.Find("Text").gameObject.SetActive(false);
        //gameObject.transform.Find("bg").gameObject.SetActive(false);
        EventTriggerListener.GetComponent(this.gameObject).onEnter = OnMouseEnter;
        EventTriggerListener.GetComponent(this.gameObject).onExit = OnMouseExit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPVP)
        {
            this.gameObject.transform.Find("ImageFill").GetComponent<Image>().fillAmount = (originalDurationTime - baseBuff.DurationTime) / originalDurationTime;
            this.gameObject.transform.Find("ImageFill").GetChild(0).GetComponent<Text>().text = baseBuff.Tier.ToString();
        }
        
    }
}
