using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardCtrl : MonoBehaviour
{
    private GameObject challengeName;
    private Text reward;
    private Text rewardChild;

    private Goods goods;

    /// <summary>
    /// 初始化物品信息
    /// </summary>
    public void InitReward(Goods goods)
    {
        this.goods = goods;
    }

    /// <summary>
    /// 指针移入事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseEnter(GameObject gameObject)
    {
        if(goods.Goods_describte != "0")
        {
            reward.gameObject.SetActive(true);
            reward.text = goods.Goods_describte;
            rewardChild.text = reward.text;
        }
    }

    /// <summary>
    /// 指针移出事件
    /// </summary>
    /// <param name="go"></param>
    public void OnMouseExit(GameObject gameObject)
    {
        reward.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        challengeName = gameObject.transform.parent.parent.Find("challengeName").gameObject;
        if(gameObject.name == "reward_1")
        {
            reward = challengeName.transform.Find("reward_1").GetComponent<Text>();
        }
        else if (gameObject.name == "reward_2")
        {
            reward = challengeName.transform.Find("reward_2").GetComponent<Text>();
        }
        else if (gameObject.name == "reward_3")
        {
            reward = challengeName.transform.Find("reward_3").GetComponent<Text>();
        }
        else
        {
            reward = challengeName.transform.Find("reward_4").GetComponent<Text>();
        }
        rewardChild = reward.gameObject.transform.Find("Text").GetComponent<Text>();
        EventTriggerListener.GetComponent(this.gameObject).onEnter = OnMouseEnter;
        EventTriggerListener.GetComponent(this.gameObject).onExit = OnMouseExit;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
