using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Stage
{
    /// <summary>
    /// 关卡类
    /// </summary>
    /// <param name="id">关卡ID</param>
    /// <param name="challenge_name">关卡名字</param>
    /// <param name="challenge_reward">关卡奖励</param>
    /// <param name="challenge_boss_id">关卡boss对应的ID</param>
    /// 

    public Stage(string data) {
        string[] strs = data.Split('|');
        if (strs.Length < 22) Debug.Log("关卡数据格式异常");
        this.Stage_ID = int.Parse(strs[0]);
        this.Stage_Name = strs[1];
        this.Target_Name = strs[2];
        this.Recommended_level = int.Parse(strs[3]);
        this.Chapter = int.Parse(strs[4]);
        this.Stage_Reward_1 = strs[5];
        this.Stage_Reward_2 = strs[6];
        this.Stage_Reward_3 = strs[7];
        this.Stage_Reward_4 = strs[8];
        this.Exp_Reward = int.Parse(strs[9]);
        this.Special_reward = strs[10];
        this.Special_Stage = strs[11];
        this.Force_Hero = strs[12];
        this.Ban_Hero = strs[13];
        this.Force_Element = strs[14];
        this.Ban_Element = strs[15];
        this.Enemy_ID_1 = int.Parse(strs[16]);
        this.Enemy_ID_2 = int.Parse(strs[17]);
        this.Enemy_ID_3 = int.Parse(strs[18]);
        this.BG_1_path = strs[19];
        this.BG_2_path = strs[20];
        this.BG_3_path = strs[21];

    }

    public override string ToString()
    {
        return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}",
                              Stage_ID,Stage_Name,Target_Name,Recommended_level,Chapter,Stage_Reward_1,Stage_Reward_2,Stage_Reward_3,Stage_Reward_4,
                              Exp_Reward,Special_reward,Special_Stage,Force_Hero,Ban_Hero,Force_Element,Ban_Element,Enemy_ID_1,Enemy_ID_2,Enemy_ID_3,
                              BG_1_path,BG_2_path,BG_3_path);
    }


    public int Stage_ID { get; set; }//关卡ID
    public string Stage_Name { get; set; }//关卡名
    public string Target_Name { get; set; }//对应光标名称
    public int Recommended_level { get; set; }//建议等级
    public int Chapter { get; set; }//所属章节
    public string Stage_Reward_1 { get; set; }//关卡奖励一ID
    public string Stage_Reward_2 { get; set; }//关卡奖励二ID
    public string Stage_Reward_3 { get; set; }//关卡奖励三ID
    public string Stage_Reward_4 { get; set; }//关卡奖励四ID
    public int Exp_Reward { get; set; }//关卡经验奖励
    public string Special_reward { get; set; }//特殊奖励
    public string Special_Stage { get; set; }//特殊关卡
    public string Force_Hero { get; set; }//强制上场角色ID集
    public string Ban_Hero { get; set; }//禁止上场角色ID集
    public string Force_Element { get; set; }//强制上场元素牌集
    public string Ban_Element { get; set; }//禁止上场元素牌集
    public int Enemy_ID_1 { get; set; }//敌人一ID
    public int Enemy_ID_2 { get; set; }//敌人二ID
    public int Enemy_ID_3 { get; set; }//敌人三ID
    public string BG_1_path { get; set; }//战斗背景一路径
    public string BG_2_path { get; set; }//战斗背景二路径
    public string BG_3_path { get; set; }//战斗背景三路劲




    public HeroCard Enemy_1_HeroCard { get; set; }
    public HeroCard Enemy_2_HeroCard { get; set; }
    public HeroCard Enemy_3_HeroCard { get; set; }
    public Goods Reward_1 { get; set; }
    public Goods Reward_2 { get; set; }
    public Goods Reward_3 { get; set; }
    public Goods Reward_4 { get; set; }

    public void SetStageRewardInfo(Dictionary<int, Goods> goodsInfoDict)
    {
        string st1 = Stage_Reward_1.Replace("[", "");
        st1 = st1.Replace("]", "");
        string[] strs1 = st1.Split(',');
        int reward_1_id = int.Parse(strs1[0]);
        int reward_1_count = int.Parse(strs1[1]);
        Reward_1= goodsInfoDict[reward_1_id];
        Reward_1.Goods_has = reward_1_count;

        string st2 = Stage_Reward_2.Replace("[", "");
        st2 = st2.Replace("]", "");
        string[] strs2 = st2.Split(',');
        int reward_2_id = int.Parse(strs2[0]);
        int reward_2_count = int.Parse(strs2[1]);
        Reward_2 = goodsInfoDict[reward_2_id];
        Reward_2.Goods_has = reward_2_count;

        string st3 = Stage_Reward_3.Replace("[", "");
        st3 = st3.Replace("]", "");
        string[] strs3 = st3.Split(',');
        int reward_3_id = int.Parse(strs3[0]);
        int reward_3_count = int.Parse(strs3[1]);
        Reward_3 = goodsInfoDict[reward_3_id];
        Reward_3.Goods_has = reward_3_count;

        string st4 = Stage_Reward_4.Replace("[", "");
        st4 = st4.Replace("]", "");
        string[] strs4 = st4.Split(',');
        int reward_4_id = int.Parse(strs4[0]);
        int reward_4_count = int.Parse(strs4[1]);
        Reward_4 = goodsInfoDict[reward_4_id];
        Reward_4.Goods_has = reward_4_count;

    }

}

