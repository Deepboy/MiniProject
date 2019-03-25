using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardMove : MonoBehaviour
{
    //移动的的目标
    GameObject target;
    //是否可以移动
    public bool isCanMove = false;
    //移动的速度
    public float speed = 3;
    //行星图片
    private Image image;
    //特效
    private GameObject effect;

    /// <summary>
    /// 获得特效
    /// </summary>
    public void GetEffect(GameObject go)
    {
        effect = go;
    }

    // Use this for initialization
    void Start()
    {
        //获取玩家黑洞
        target = GameObject.FindGameObjectWithTag("PlayerBlackHole");
        image = GetComponent<Image>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            //向玩家黑洞移动的速度
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), Time.deltaTime);
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0.4f), Time.deltaTime);
            effect.transform.localScale = Vector3.Lerp(effect.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 0.4f);
        }
    }
}
