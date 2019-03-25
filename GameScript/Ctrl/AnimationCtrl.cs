using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour
{

    private GameObject canvas;
    private GameObject fightPanel;
    private Animator Player;
    private Animator Enemy;
    public bool isPVP = false;
    private void AnimationStart()
    {
        FightCtrl.isPlayingAnimation = true;
    }

    private void AnimationEnd()
    {
        FightCtrl.isPlayingAnimation = false;
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas").gameObject;
        if (isPVP)
        {
            fightPanel = canvas.transform.Find("PVPFightPanel/PanelFight").gameObject;
        }
        else {
            fightPanel = canvas.transform.Find("PanelFight").gameObject;
        }
    }
    /// <summary>
    /// 播放受击动画
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPVP)
        {
            Player = GameObject.Find("Canvas/PVPFightPanel/PanelFight/PanelFightInfo/player").transform.GetChild(0).GetComponent<Animator>();
            Enemy = GameObject.Find("Canvas/PVPFightPanel/PanelFight/PanelFightInfo/enemy").transform.GetChild(0).GetComponent<Animator>();
        }
        else
        {
            Player = GameObject.Find("Canvas/PanelFight/PanelFightInfo/player").GetComponent<Animator>();
            Enemy = GameObject.Find("Canvas/PanelFight/PanelFightInfo/enemy").GetComponent<Animator>();
        }

        //此处判断是因为若是上次敌人放完特效将isEnemy改为true，然后玩家走过去攻击敌人（没有生成特效，没有改动isEnemy）进入OnTriggerEnter2D，玩家会播放受击动画
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") return;
        if (isPVP)
        {
            Enemy = GameObject.Find("Canvas/PVPFightPanel/PanelFight/PanelFightInfo/enemy").transform.GetChild(0).GetComponent<Animator>();
        }
        else {
            Enemy = GameObject.Find("Canvas/PanelFight/PanelFightInfo/enemy").GetComponent<Animator>();
        }
        GameObject effect = collision.gameObject as GameObject;
        effect.GetComponent<BoxCollider2D>().enabled = false;//防止再次进入碰撞，关闭特效的碰撞体
        if (effect != null)
        {
            if(FightCtrl.IsFightFinish == false)
            {
                switch (DestroySelf.isEnemy)
                {
                    case true:
                        Player.SetTrigger("hit");
                        break;
                    case false:
                        Enemy.SetTrigger("foxhit");
                        break;
                }
            }
            if (isPVP) {
                switch (DestroySelf.isEnemy)
                {
                    case true:
                        Player.SetTrigger("hit");
                        break;
                    case false:
                        Enemy.SetTrigger("hit");
                        break;
                }
            }
        }
    }

}
