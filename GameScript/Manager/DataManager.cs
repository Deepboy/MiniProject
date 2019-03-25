using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Model;
using SpecialBuffManager;
using Common;
using System.Text;
using System;

public class DataManager : BaseManager {

    public DataManager(GameFacade facade) : base(facade) { }

    private bool isFighting = false;


    //所有敌人角色卡
    private Dictionary<int, HeroCard> enemyInfoDic = new Dictionary<int, HeroCard>();
    //所有角色卡
    private Dictionary<int, HeroCard> cardInfoDic = new Dictionary<int, HeroCard>();
    //所有物品
    private Dictionary<int, Goods> goodsInfoDic = new Dictionary<int, Goods>();
    //技能ID对应的技能函数
    public Dictionary<int, Skill> skill_info_dict = new Dictionary<int, Skill>();
    //所有关卡
    public Dictionary<int, Stage> stage_info_dict = new Dictionary<int, Stage>();
    //所有buff
    public Dictionary<BuffCode, Buff> buff_info_dict = new Dictionary<BuffCode, Buff>();
    //所有卡牌特效
    public Dictionary<string, SpecialBuff> card_effect_dict = new Dictionary<string, SpecialBuff>();
    public List<Stage> all_stage_List = new List<Stage>();

    //玩家信息
    private UserInfo user_info;
    //玩家所持有的角色卡
    private List<HeroCard> userCardList = new List<HeroCard>();
    //玩家所持有的物品列表
    //public List<Goods> userGoodsList = new List<Goods>();
    private Dictionary<int, Goods> userGoodsDict = new Dictionary<int, Goods>();
    //玩家打通了的关卡
    public List<Stage> userStageList = new List<Stage>();


    private HeroCard PlayerUseHero;
    private HeroCard PlayerStaffHero;
    private Dictionary<string, bool> PlayerUseElement;

    private int StageIndex;

    private HeroCard EnemyUseHero;
    private HeroCard EnemyStaffHero;
    private Dictionary<string, bool> EnemyUseElement;

    private string PVPInfo=null;
    private string pvpPlayerInfo;


    public override void OnInit()
    {
        SpecialBuffHolder info_assets = Resources.Load<SpecialBuffHolder>("SpecialBuff");
        List<skillInfo_excel> skill_info_list = info_assets.skillInfo_excel;
        List<heroInfo_excel> hero_info_list = info_assets.heroInfo_excel;
        List<stageInfo_excel> stage_info_list = info_assets.stageInfo_excel;
        List<heroInfo_excel> enemy_info_list = info_assets.enemyInfo_excel;
        List<goodsInfo_excel> goods_info_list = info_assets.goodsInfo_excel;
        List<Buff> buff_info_list = info_assets.buffs;
        List<SpecialBuff> card_effect_list = info_assets.SpecialBuffs;

        foreach (SpecialBuff sb in card_effect_list) {
            card_effect_dict.Add(sb.ID, sb);
        }

        //获得所有技能列表
        foreach (skillInfo_excel si in skill_info_list) {
            string s = si.ToString();
            Skill skill = new Skill(s);
            skill_info_dict.Add(skill.Skill_id, skill);
        }
        //获得所有物品信息
        foreach (goodsInfo_excel gi in goods_info_list)
        {
            string s = gi.ToString();
            Goods goods = new Goods(s);
            goodsInfoDic.Add(goods.Goods_id, goods);
            Goods Usergoods = new Goods(s);
            userGoodsDict.Add(Usergoods.Goods_id, Usergoods);
        }

        //获得所有角色列表,并设置角色卡技能
        foreach (heroInfo_excel hi in hero_info_list) {
            string s = hi.ToString();
            HeroCard card = new HeroCard(s);
            card.Prossive_skill_1 = skill_info_dict[card.Prossive_skill_1_id];
            card.Prossive_skill_2 = skill_info_dict[card.Prossive_skill_2_id];
            card.Prossive_skill_3 = skill_info_dict[card.Prossive_skill_3_id];
            card.Action_skill = skill_info_dict[card.Active_skill_id];
            card.Staff_skill_self = skill_info_dict[card.Staff_skill_myself_id];
            cardInfoDic.Add(card.Hero_id, card);
        }

        foreach (heroInfo_excel enemy in enemy_info_list) {
            string s = enemy.ToString();
            HeroCard card = new HeroCard(s);
            card.Prossive_skill_1 = skill_info_dict[card.Prossive_skill_1_id];
            card.Prossive_skill_2 = skill_info_dict[card.Prossive_skill_2_id];
            card.Prossive_skill_3 = skill_info_dict[card.Prossive_skill_3_id];
            card.Action_skill = skill_info_dict[card.Active_skill_id];
            card.Staff_skill_self = skill_info_dict[card.Staff_skill_myself_id];
            enemyInfoDic.Add(card.Hero_id, card);
        }

        //获取所有关卡信息
        foreach (stageInfo_excel si in stage_info_list)
        {
            string s = si.ToString();
            Stage stage = new Stage(s);
            stage.Enemy_1_HeroCard = enemyInfoDic[stage.Enemy_ID_1];
            stage.Enemy_2_HeroCard = enemyInfoDic[stage.Enemy_ID_2];
            stage.Enemy_3_HeroCard = enemyInfoDic[stage.Enemy_ID_3];
            stage.SetStageRewardInfo(goodsInfoDic);
            stage_info_dict.Add(stage.Stage_ID, stage);
            all_stage_List.Add(stage);
        }

        foreach (Buff b in buff_info_list) {
            buff_info_dict.Add((BuffCode)Enum.Parse(typeof(BuffCode), b.BuffEngName), b);
        }

        base.OnInit();
    }

    public void setUserInfo(string data) {
        //string[] strs = data.Split('|');
        //int id=int.Parse(strs[1]);
        //int user_id=int.Parse(strs[2]);
        //string username=strs[3];
        //int user_Exp = int.Parse(strs[4]);
        //int user_ReserveExp = int.Parse(strs[5]);
        //int user_level=int.Parse(strs[6]);
        //int level_limit=int.Parse(strs[7]);
        //string goods=strs[8];
        //string card=strs[9];
        //int challenge=int.Parse(strs[10]);
        //int task_id=int.Parse(strs[11]);
        //int phase = int.Parse(strs[12]);
        //int minecraft = int.Parse(strs[13]); 
        //string[] strs = data.Split('|');
        int id = 1;
        int user_id = 1;
        string username = "test";
        int user_Exp = 0;
        int user_ReserveExp = 0;
        int user_level = 1;
        int level_limit = 60;
        string goods = "{[9001,50000];[9002,50000]}";
        string card = "{[10001,1,4];[10004,1,4];[10007,1,4]}";
        int challenge = 101;
        int task_id = 1;
        int phase = 0;
        int minecraft = 0;
        user_info = new UserInfo(id, user_id, username,user_Exp,user_ReserveExp, user_level, level_limit, goods, card, challenge, task_id, phase,minecraft,"");
        userCardList.Clear();
        userStageList.Clear();
        foreach(var key in userGoodsDict.Keys)
        {
            userGoodsDict[key].Goods_has = 0;
        }
        setUserCradList(card);
        setUserStageList(challenge);
        setUserGoodsList(goods);
    }

    /// <summary>
    /// 玩家所持物品列表
    /// </summary>
    /// <param name="data"></param>
    private void setUserGoodsList(string goods) {
        if (goods == "{}")
        {
            return;
        }
        string[] strs = goods.Split(';');
        foreach (string s in strs)
        {
            string data = s.Replace("{", "");
            data = data.Replace("[", "");
            data = data.Replace("]", "");
            data = data.Replace("}", "");
            string[] goodsInfo = data.Split(',');
            int goodsId = int.Parse(goodsInfo[0]);
            int goods_has = int.Parse(goodsInfo[1]);
            userGoodsDict[goodsId].Goods_has = goods_has;
        }
    }

    /// <summary>
    /// 玩家持有角色卡牌
    /// </summary>
    /// <param name="card">卡牌集合</param>
    private void setUserCradList(string card) {
        if (card == "{}") {
            return;
        }
        string[] strs = card.Split(';');
        foreach (string s in strs)
        {
            string data = s.Replace("{", "");
            data = data.Replace("[", "");
            data = data.Replace("]", "");
            data = data.Replace("}", "");
            string[] cardInfo = data.Split(',');
            int cardId = int.Parse(cardInfo[0]);
            int hero_level = int.Parse(cardInfo[1]);
            //int hero_Star = int.Parse(cardInfo[2]);
            HeroCard c = cardInfoDic[cardId];
            HeroCard userCard = new HeroCard(c.ToString());
            userCard.Prossive_skill_1 = c.Prossive_skill_1;
            userCard.Prossive_skill_2 = c.Prossive_skill_2;
            userCard.Prossive_skill_3 = c.Prossive_skill_3;
            userCard.Action_skill = c.Action_skill;
            userCard.Staff_skill_self = c.Staff_skill_self;
            userCard.Hero_level = hero_level;

            userCard.Fire = (int)(userCard.OrgFire + userCard.Hero_level * userCard.Fire_growth * 2.3f);
            userCard.Water = (int)(userCard.OrgWater + userCard.Hero_level * userCard.Water_growth * 2.3f);
            userCard.Soil = (int)(userCard.OrgSoil + userCard.Hero_level * userCard.Soil_growth * 2.3f);
            userCard.Wind = (int)(userCard.OrgWind + userCard.Hero_level * userCard.Wind_growth * 2.3f);
            userCard.Thunder = (int)(userCard.OrgThunder + userCard.Hero_level * userCard.Thunder_growth * 2.3f);
            userCard.Dark = (int)(userCard.OrgDark + userCard.Hero_level * userCard.Dark_growth * 2.3f);
            userCard.Hero_hp = (int)(userCard.OrgHp + userCard.Hero_level * userCard.Hero_hp_growth);
            userCard.Hero_speed = (int)(userCard.OrgSpeed + userCard.Hero_level * userCard.Hero_speed_growth);
            
            userCardList.Add(userCard);
        }
    }
    /// <summary>
    /// 设置玩家当前的关卡
    /// </summary>
    /// <param name="index"></param>
    private void setUserStageList(int index) {
        foreach (Stage st in all_stage_List) {
            if (st.Stage_ID == index)
            {
                userStageList.Add(st);
                break;
            }
            else {
                userStageList.Add(st);
            }
        }
    }




    //获取所有角色卡牌信息
    public Dictionary<int, HeroCard> GetCardInfoDict()
    {
        return cardInfoDic;
    }

    //获取所有物品信息
    public Dictionary<int, Goods> GetGoodsInfoDict() {
        return goodsInfoDic;
    }

    //获取所有物品信息
    public Dictionary<int, Stage> GetStageInfoDict() {
        return stage_info_dict;
    }



    //获取玩家信息
    public UserInfo getUserInfo()
    {
        return user_info;
    }

    //获取玩家角色卡牌信息
    public List<HeroCard> GetUserCardList() {
        return userCardList;
    }

    //获取玩家物品信息
    public Dictionary<int,Goods> GetUserGoodsDict() {
        return userGoodsDict;
    }

    public List<Stage> GetUserStageList() {
        return userStageList;
    }

    public Dictionary<int, Skill> GetAllSkillInfoDict() {
        return skill_info_dict;
    }


    //设置玩家所选出战信息（出战人物，助战人物，所选的元素牌）
    public void SetPlayerFightInfo(HeroCard mainHero,HeroCard staffHero, Dictionary<string, bool> useElement) {
        PlayerUseHero = mainHero;
        PlayerStaffHero = staffHero;
        if (staffHero != null)
        {
            PlayerUseHero.Staff_skill_other = staffHero.Staff_skill_self;
        }
        else {
            PlayerUseHero.Staff_skill_other = skill_info_dict[100000];
        }
        this.PlayerUseElement = useElement;

    }

    //设置敌人所选出战信息（出战人物，助战人物，所选的元素牌）
    public void SetEnemyFightInfo(HeroCard mainHero, HeroCard staffHero, Dictionary<string, bool> useElement)
    {
        EnemyUseHero = mainHero;
        if (staffHero != null)
        {
            EnemyUseHero.Staff_skill_other = staffHero.Staff_skill_self;
        }
        else
        {
            EnemyUseHero.Staff_skill_other = skill_info_dict[100000];
        }
        this.EnemyUseElement = useElement;
    }

    //获得玩家所选角色
    public HeroCard GetPlayerUseHero()
    {
        return PlayerUseHero;
    }

    public HeroCard GetPlayerUseStaffHero()
    {
        return PlayerStaffHero;
    }


    //获得敌人所选角色
    public HeroCard GetEnemyUseHero()
    {
        return EnemyUseHero;
    }

    //获得玩家所使用的元素牌
    public Dictionary<string, bool> GetPlayerUseElement()
    {
        return PlayerUseElement;
    }

    //获得敌人所使用的元素牌
    public Dictionary<string, bool> GetEnemyUseElement()
    {
        return EnemyUseElement;
    }

    public void SetChallengeStageIndex(int index) {
        StageIndex = index;
    }

    public Stage GetChallengeStage() {
        return stage_info_dict[StageIndex];
    }

    public void StageFightResult(Stage stage,ReturnCode returnCode) {
        if (returnCode == ReturnCode.Success)
        {
            Debug.Log("win");
            Debug.Log(all_stage_List.Count);
            for (int i = 0; i < all_stage_List.Count; i++) {
                if (stage == all_stage_List[i]&&i!= all_stage_List.Count-1) {
                    if (userStageList.Contains(all_stage_List[i + 1]))
                        break;
                    else
                        userStageList.Add(all_stage_List[i + 1]);
                }
            }
            userGoodsDict[stage.Reward_1.Goods_id].Goods_has = userGoodsDict[stage.Reward_1.Goods_id].Goods_has + stage.Reward_1.Goods_has > stage.Reward_1.Goods_max ? stage.Reward_1.Goods_max : userGoodsDict[stage.Reward_1.Goods_id].Goods_has + stage.Reward_1.Goods_has;
            userGoodsDict[stage.Reward_2.Goods_id].Goods_has = userGoodsDict[stage.Reward_2.Goods_id].Goods_has + stage.Reward_2.Goods_has > stage.Reward_2.Goods_max ? stage.Reward_2.Goods_max : userGoodsDict[stage.Reward_2.Goods_id].Goods_has + stage.Reward_2.Goods_has;
            userGoodsDict[stage.Reward_3.Goods_id].Goods_has = userGoodsDict[stage.Reward_3.Goods_id].Goods_has + stage.Reward_3.Goods_has > stage.Reward_3.Goods_max ? stage.Reward_3.Goods_max : userGoodsDict[stage.Reward_3.Goods_id].Goods_has + stage.Reward_3.Goods_has;
            userGoodsDict[stage.Reward_4.Goods_id].Goods_has = userGoodsDict[stage.Reward_4.Goods_id].Goods_has + stage.Reward_4.Goods_has > stage.Reward_4.Goods_max ? stage.Reward_4.Goods_max : userGoodsDict[stage.Reward_4.Goods_id].Goods_has + stage.Reward_4.Goods_has;
            user_info.User_ReserveExp += stage.Exp_Reward;
            //自动升级
            //if(user_info.User_ReserveExp >= user_info.User_level * 300)
            //{
            //    if(user_info.User_level < user_info.Level_limit)
            //    {
            //        user_info.User_ReserveExp -= user_info.User_level * 300;
            //        user_info.User_level++;
            //    }
            //}
        }
    }


    public UserInfo SaveUserInfo() {
        StringBuilder ci = new StringBuilder();
        ci.Append("{");
        if (userCardList != null) {
            foreach (HeroCard card in userCardList) {
                ci.Append("["+card.Hero_id.ToString()+","+card.Hero_level+ ","+card.Hero_starLevel+ "]" + ";");
            }
            ci.Remove(ci.Length - 1, 1);
        }
        ci.Append("}");
        user_info.Card = ci.ToString();

        StringBuilder gi = new StringBuilder();
        gi.Append("{");
        if (userGoodsDict != null)
        {
            bool hasInfo = false;
            foreach (var key in userGoodsDict.Keys)
            {
                if (userGoodsDict[key].Goods_has != 0) {
                    gi.Append("[" + key + "," + userGoodsDict[key].Goods_has + "]" + ";");
                    hasInfo = true;
                }
            }
            if (hasInfo) {
                gi.Remove(gi.Length - 1, 1);
            }
        }
        gi.Append("}");
        user_info.Goods = gi.ToString();
       
        user_info.Challenge = userStageList[userStageList.Count - 1].Stage_ID;
        Debug.Log(user_info.ToString());
        return user_info;
    }

    public void SetPVPInfo(string pvpcardInfo) {
        PVPInfo = pvpcardInfo;
    }

    public void SetPVPPlayerInfo(string pvpPlayerInfo) {
        this.pvpPlayerInfo = pvpPlayerInfo;
    }

    public string GetPVPPlayerInfo() {
        return pvpPlayerInfo;
    }

    public string getPVPInfo() {
        return PVPInfo;
    }
    public Dictionary<BuffCode, Buff> GetBuffInfoDict() {
        return buff_info_dict;
    }

    public void SetPVPState(bool pvpstate) {
        isFighting = pvpstate;
    }
    public bool GetPVPState() {
        return isFighting;
    }


    public Dictionary<string, SpecialBuff> GetCardEffectDict() {
        return card_effect_dict;
    }

}
