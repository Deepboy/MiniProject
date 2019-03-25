using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private Animator animator;
    GameObject previewPanel;
    GameObject PanelFight;
    GameObject Player;
    GameObject Enemy;
    public bool isPVP = false;
    public PVPFightPanel pvpFightPanel;
    public static bool isEnemy = false;//敌人特效的的标志位
    void Start()
    {
        if (isPVP) {
            Player = GameObject.Find("Canvas/PVPFightPanel/PanelFight/PanelFightInfo/player").transform.GetChild(0).gameObject;
            Enemy = GameObject.Find("Canvas/PVPFightPanel/PanelFight/PanelFightInfo/enemy").transform.GetChild(0).gameObject;
        }
        else {
            Enemy = GameObject.Find("Canvas/PanelFight/PanelFightInfo/enemy");
            Player = GameObject.Find("Canvas/PanelFight/PanelFightInfo/player");
            FightCtrl.isPlayingEffect = true;
        }
        //previewPanel = GameObject.Find("Canvas").transform.GetChild(3).gameObject;

        //if (FightCtrl.PreviewTopControlBool == true)//设置预览框的状态前都需判断是否全局开启
        //{
        //    previewPanel.SetActive(false);
        //}

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 1.0f)
        {
            //if (!isEnemy)
            //{
            //    if (FightCtrl.PreviewTopControlBool == true)//设置预览框的状态前都需判断是否全局开启
            //    {
            //        if (FightCtrl.playerSelectCardCount > 0)//若是玩家的特效，播放结束后有选中的卡牌则需要激活预览框
            //        {
            //            previewPanel.SetActive(true);
            //        }
            //        else
            //        {
            //            previewPanel.SetActive(false);
            //        }
            //    }
            //}
            if (isPVP)
            {
                pvpFightPanel.isEffectOver = true;
                
            }
            else {
                FightCtrl.isPlayingEffect = false;
            }
            
            //播放完毕，销毁自身
            Destroy(gameObject);
        }

    }
}
