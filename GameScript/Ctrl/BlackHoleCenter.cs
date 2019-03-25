using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BlackHoleCenter : MonoBehaviour
{
    //引力范围
    private float Range = 5f;
    //元素球位置
    private Vector3[] elemBallPosition;
    //生成的元素球
    private string elemName;
    //元素卡图片
    private Sprite[] elemImage;
    //特效
    public GameObject effect;
    //是否开始移动
    private bool isMove;
    //是否已经生成元素球
    public bool isFinishElemBall;

    // Use this for initialization
    void Start()
    {
        elemBallPosition = new Vector3[5];
        elemBallPosition[0] = new Vector3(239, 172, 0);
        elemBallPosition[1] = new Vector3(-239, 172, 0);
        elemBallPosition[2] = new Vector3(0, 291, 0);
        elemBallPosition[3] = new Vector3(240, -115, 0);
        elemBallPosition[4] = new Vector3(-240, -115, 0);

        elemImage = new Sprite[6];
        elemImage[0] = Resources.Load<Sprite>("Image/Icon/fire");
        elemImage[1] = Resources.Load<Sprite>("Image/Icon/water");
        elemImage[2] = Resources.Load<Sprite>("Image/Icon/earth");
        elemImage[3] = Resources.Load<Sprite>("Image/Icon/wind");
        elemImage[4] = Resources.Load<Sprite>("Image/Icon/thunder");
        elemImage[5] = Resources.Load<Sprite>("Image/Icon/dark");

        if (gameObject.tag == "PlayerBlackHole")
        {
            elemName = "PlayerPVPCard";
        }
        else
        {
            elemName = "EnemyPVPCard";
            elemBallPosition[0] = new Vector3(-239, 172, 0);
            elemBallPosition[1] = new Vector3(239, 172, 0);
            elemBallPosition[2] = new Vector3(0, 291, 0);
            elemBallPosition[3] = new Vector3(-240, -115, 0);
            elemBallPosition[4] = new Vector3(240, -115, 0);
        }

        isMove = false;
        isFinishElemBall = true;
    }

    public void CreateElemBall(string id)
    {
        isFinishElemBall = false;
        isMove = false;
        char[] arr = id.ToCharArray();
        int[] intArr = new int[id.Length];
        int count = 0;
        for (int i = 0; i < id.Length; ++i)
        {
            intArr[i] = int.Parse(arr[i].ToString());
        }
        for (int i = 0; i < id.Length; ++i)
        {
            while (intArr[i] > 0)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/" + elemName));
                go.GetComponent<Image>().sprite = elemImage[i];
                go.transform.SetParent(transform);
                go.transform.localPosition = elemBallPosition[count++];
                go.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                intArr[i]--;
            }
        }
        StartCoroutine(CreateEffect());
    }

    private IEnumerator CreateEffect()
    {
        yield return new WaitForSeconds(0.3f);
        effect = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/BlackHole"));
        effect.transform.SetParent(transform);
        effect.transform.localPosition = new Vector3(0, 0, 0);
        effect.transform.localScale = new Vector3(100, 100, 100);
        isMove = true;
        StartCoroutine(TimeCount());
    }

    private IEnumerator TimeCount()
    {
        yield return new WaitForSeconds(1f);
        isFinishElemBall = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            //检测以玩家为球心半径是5的范围内的所有的带有碰撞器的游戏对象
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, Range);
            foreach (var item in colliders)
            {
                //如果是生成的卡牌
                if (item.tag.Equals("Card"))
                {
                    if (gameObject.tag == "PlayerBlackHole")
                    {
                        item.GetComponent<PlayerCardMove>().GetEffect(effect);
                        //让卡牌的开始移动
                        item.GetComponent<PlayerCardMove>().isCanMove = true;
                    }
                    else
                    {
                        item.GetComponent<EnemyCardMove>().GetEffect(effect);
                        //让卡牌的开始移动
                        item.GetComponent<EnemyCardMove>().isCanMove = true;
                    }
                }
            }
        }
    }
}
