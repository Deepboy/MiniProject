using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewFightConfig;
using System;
using Common;

public enum FightPhase
{
    None,
    BeforeEnter,
    InEnter,
    AfterEnter,
    BeforeStart,
    InStart,
    AfterStart,
    BeforeIntend,
    InIntend,
    AfterIntend,
    BeforeAction,
    InAction,
    AfterAction,
    BeforeDemage,
    InDemage,
    AfterDemage,
    BeforeEnd,
    InEnd,
    AfterEnd,
}

public class HeroCard
{
    //构造函数（string）
    public HeroCard(string data) {
        string[] strs = data.Split('|');

        if (strs.Length != 37) Debug.Log("数据错误");

        this.Hero_id = int.Parse(strs[0]);
        this.Hero_name = strs[1];
        this.Hero_describe = strs[2];
        this.Hero_rarity = strs[3];
        this.Hero_starLevel = int.Parse(strs[4]);
        this.Hero_level = int.Parse(strs[5]);
        this.Hero_exp = int.Parse(strs[6]);
        this.Hero_needEXP = int.Parse(strs[7]);
        this.Hero_type = (ElementType)Enum.Parse(typeof(ElementType), strs[8]);
        this.Hero_hp = float.Parse(strs[9]);
        this.OrgHp = float.Parse(strs[9]);
        this.Hero_hp_growth = float.Parse(strs[10]);
        this.Hero_speed = float.Parse(strs[11]);
        this.OrgSpeed= float.Parse(strs[11]);
        this.Hero_speed_growth = float.Parse(strs[12]);
        this.Wind = float.Parse(strs[13]);
        this.OrgWind= float.Parse(strs[13]);
        this.Wind_growth = float.Parse(strs[14]);
        this.Fire = float.Parse(strs[15]);
        this.OrgFire = float.Parse(strs[15]);
        this.Fire_growth = float.Parse(strs[16]);
        this.Water = float.Parse(strs[17]);
        this.OrgWater= float.Parse(strs[17]);
        this.Water_growth = float.Parse(strs[18]);
        this.Thunder = float.Parse(strs[19]);
        this.OrgThunder = float.Parse(strs[19]);
        this.Thunder_growth = float.Parse(strs[20]);
        this.Soil = float.Parse(strs[21]);
        this.OrgSoil= float.Parse(strs[21]);
        this.Soil_growth = float.Parse(strs[22]);
        this.Dark = float.Parse(strs[23]);
        this.OrgDark= float.Parse(strs[23]);
        this.Dark_growth = float.Parse(strs[24]);
        this.Prossive_skill_1_id = int.Parse(strs[25]);
        this.Prossive_skill_2_id = int.Parse(strs[26]);
        this.Prossive_skill_3_id = int.Parse(strs[27]);
        this.Active_skill_id = int.Parse(strs[28]);
        this.Staff_skill_myself_id = int.Parse(strs[29]);
        this.Gift_tree = strs[30];
        this.Model_path = strs[31];
        this.Texture_path = strs[32];
        this.Card_path = strs[33];
        this.Icon_path = strs[34];
        this.FightModel_path = strs[35];
        this.SkillIcon_path = strs[36];
    }
    public HeroCard(string data, Dictionary<int, Skill> skillDict)
    {
        string[] strs = data.Split('|');

        if (strs.Length != 37) Debug.Log("数据错误");

        this.Hero_id = int.Parse(strs[0]);
        this.Hero_name = strs[1];
        this.Hero_describe = strs[2];
        this.Hero_rarity = strs[3];
        this.Hero_starLevel = int.Parse(strs[4]);
        this.Hero_level = int.Parse(strs[5]);
        this.Hero_exp = int.Parse(strs[6]);
        this.Hero_needEXP = int.Parse(strs[7]);
        this.Hero_type = (ElementType)Enum.Parse(typeof(ElementType), strs[8]);
        this.Hero_hp = float.Parse(strs[9]);
        this.OrgHp = float.Parse(strs[9]);
        this.Hero_hp_growth = float.Parse(strs[10]);
        this.Hero_speed = float.Parse(strs[11]);
        this.OrgSpeed = float.Parse(strs[11]);
        this.Hero_speed_growth = float.Parse(strs[12]);
        this.Wind = float.Parse(strs[13]);
        this.OrgWind = float.Parse(strs[13]);
        this.Wind_growth = float.Parse(strs[14]);
        this.Fire = float.Parse(strs[15]);
        this.OrgFire = float.Parse(strs[15]);
        this.Fire_growth = float.Parse(strs[16]);
        this.Water = float.Parse(strs[17]);
        this.OrgWater = float.Parse(strs[17]);
        this.Water_growth = float.Parse(strs[18]);
        this.Thunder = float.Parse(strs[19]);
        this.OrgThunder = float.Parse(strs[19]);
        this.Thunder_growth = float.Parse(strs[20]);
        this.Soil = float.Parse(strs[21]);
        this.OrgSoil = float.Parse(strs[21]);
        this.Soil_growth = float.Parse(strs[22]);
        this.Dark = float.Parse(strs[23]);
        this.OrgDark = float.Parse(strs[23]);
        this.Dark_growth = float.Parse(strs[24]);
        this.Prossive_skill_1_id = int.Parse(strs[25]);
        this.Prossive_skill_2_id = int.Parse(strs[26]);
        this.Prossive_skill_3_id = int.Parse(strs[27]);
        this.Active_skill_id = int.Parse(strs[28]);
        this.Staff_skill_myself_id = int.Parse(strs[29]);
        this.Gift_tree = strs[30];
        this.Model_path = strs[31];
        this.Texture_path = strs[32];
        this.Card_path = strs[33];
        this.Icon_path = strs[34];
        this.FightModel_path = strs[35];
        this.SkillIcon_path = strs[36];
        this.Prossive_skill_1 = skillDict[Prossive_skill_1_id];
        this.Prossive_skill_2 = skillDict[Prossive_skill_2_id];
        this.Prossive_skill_3 = skillDict[Prossive_skill_3_id];
        this.Action_skill = skillDict[Active_skill_id];
        this.Staff_skill_self = skillDict[Staff_skill_myself_id];
    }


    //构造函数（传参）
    public HeroCard(int hero_id, string hero_name, string hero_describe, string hero_rarity, int hero_starLevel,
                int hero_level, int hero_exp, int hero_needEXP, ElementType hero_type, float hero_hp,
                float hero_hp_growth, float hero_speed, float hero_speed_growth, float wind, float wind_growth,
                float fire, float fire_growth, float water, float water_growth, float thunder, float thunder_growth,
                float soil, float soil_growth, float dark, float dark_growth, int prossive_skill_1_id,
                int prossive_skill_2_id, int prossive_skill_3_id, int active_skill_id, int staff_skill_id,
                string gift_tree, string model_path, string texture_path,string card_path,string icon_path,string fightModel_path, string skillIcon_path)
    {
        this.Hero_id = hero_id;
        this.Hero_name = hero_name;
        this.Hero_describe = hero_describe;
        this.Hero_rarity = hero_rarity;
        this.Hero_starLevel = hero_starLevel;
        this.Hero_level = hero_level;
        this.Hero_exp = hero_exp;
        this.Hero_needEXP = hero_needEXP;
        this.Hero_type = hero_type;
        this.Hero_hp = hero_hp;
        this.OrgHp = hero_hp;
        this.Hero_hp_growth = hero_hp_growth;
        this.Hero_speed = hero_speed;
        this.OrgSpeed = hero_speed;
        this.Hero_speed_growth = hero_speed_growth;
        this.Wind = wind;
        this.OrgWind = wind;
        this.Wind_growth = wind_growth;
        this.Fire = fire;
        this.OrgFire = fire;
        this.Fire_growth = fire_growth;
        this.Water = water;
        this.OrgWater = water;
        this.Water_growth = water_growth;
        this.Thunder = thunder;
        this.OrgThunder = thunder;
        this.Thunder_growth = thunder_growth;
        this.Soil = soil;
        this.OrgSoil = soil;
        this.Soil_growth = Soil_growth;
        this.Dark = dark;
        this.OrgDark = dark;
        this.Dark_growth = dark_growth;
        this.Prossive_skill_1_id = prossive_skill_1_id;
        this.Prossive_skill_2_id = prossive_skill_2_id;
        this.Prossive_skill_3_id = prossive_skill_3_id;
        this.Active_skill_id = active_skill_id;
        this.Staff_skill_myself_id = staff_skill_id;
        this.Gift_tree = gift_tree;
        this.Model_path = model_path;
        this.Texture_path = texture_path;
        this.Card_path = card_path;
        this.Icon_path = icon_path;
        this.FightModel_path = fightModel_path;
        this.SkillIcon_path = skillIcon_path;
    }

    //类转成string
    public override string ToString()
    {
        return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}|{26}|{27}|{28}|{29}|{30}|{31}|{32}|{33}|{34}|{35}|{36}",
            Hero_id, Hero_name, Hero_describe, Hero_rarity, Hero_starLevel, Hero_level, Hero_exp, Hero_needEXP, ((int)Hero_type).ToString(), OrgHp, Hero_hp_growth, OrgSpeed,
            Hero_speed_growth, OrgWind, Wind_growth, OrgFire, Fire_growth, OrgWater, Water_growth, OrgThunder, Thunder_growth, OrgSoil, Soil_growth, OrgDark, Dark_growth, Prossive_skill_1_id,
            Prossive_skill_2_id, Prossive_skill_3_id, Active_skill_id, Staff_skill_myself_id, Gift_tree, Model_path, Texture_path,Card_path,Icon_path, FightModel_path, SkillIcon_path);
    }


    public int Hero_id { get; set; }//角色ID
    public string Hero_name { get; set; }//角色名字
    public string Hero_describe { get; set; }//角色背景描述
    public string Hero_rarity { get; set; }//角色稀有度
    public int Hero_starLevel { get; set; }//角色当前星级
    public int Hero_level { get; set; }//角色当前等级
    public int Hero_exp { get; set; }//角色当前经验
    public int Hero_needEXP { get; set; }//角色到下一级所需的经验
    public ElementType Hero_type { get; set; }//角色属性
    public float Hero_hp { get; set; }//角色的生命值
    public float OrgHp { get; set; }
    public float Hero_hp_growth { get; set; }//角色的生命成长
    public float Hero_speed { get; set; }//角色的速度
    public float OrgSpeed { get; set; }
    public float Hero_speed_growth { get; set; }//速度的成长
    public float Wind { get; set; }//风属性值
    public float OrgWind { get; set; }
    public float Wind_growth { get; set; }//风属性成长
    public float Fire { get; set; }//火属性值
    public float OrgFire { get; set; }
    public float Fire_growth { get; set; }//火属性成长
    public float Water { get; set; }//水属性值
    public float OrgWater { get; set; }
    public float Water_growth { get; set; }//水属性成长
    public float Thunder { get; set; }//雷属性值
    public float OrgThunder { get; set; }
    public float Thunder_growth { get; set; }//雷属性成长
    public float Soil { get; set; }//地属性值
    public float OrgSoil { get; set; }
    public float Soil_growth { get; set; }//地属性成长
    public float Dark { get; set; }//暗属性值 
    public float OrgDark { get; set; }
    public float Dark_growth { get; set; }//暗属性成长
    public int Prossive_skill_1_id { get; set; }//被动技能1对应的ID
    public int Prossive_skill_2_id { get; set; }//被动技能2对应的ID
    public int Prossive_skill_3_id { get; set; }//被动技能3对应的ID
    public int Active_skill_id { get; set; }//主动技能对应的ID
    public int Staff_skill_myself_id { get; set; }//角色自己的助战技能对应的ID
    public string Gift_tree { get; set; }//角色的天赋树
    public string Model_path { get; set; }//角色模型路径
    public string Texture_path { get; set; }//角色立绘路径
    public string Card_path { get; set; }//角色卡牌模型路径
    public string Icon_path { get; set; }//角色头像模型路径
    public string SkillIcon_path { get; set; }
    public string FightModel_path { get; set; }//角色战斗时的模型路径
    public Skill Prossive_skill_1 { get; set; }//被动技能对应的技能类1
    public Skill Prossive_skill_2 { get; set; }//被动技能对应的技能类2
    public Skill Prossive_skill_3 { get; set; }//被动技能对应的技能类3
    public Skill Action_skill { get; set; }//主动技能对应的技能类
    public Skill Staff_skill_self { get; set; }//自己的助战技能
    public Skill Staff_skill_other { get; set; }//进入战斗时获得的别人的助战技能

    //被动技能
    public void Prossive_skill_function(FightPhase fightPhase,FightHero source,FightHero target) {
        if (Staff_skill_other != null && Staff_skill_other.isActionSkill == false) {
            Staff_skill_other.skill_function(fightPhase, source, target);
        }
        Prossive_skill_1.skill_function(fightPhase, source, target);
        Prossive_skill_2.skill_function(fightPhase, source, target);
        Prossive_skill_3.skill_function(fightPhase, source, target);
    }

    //主动技能
    public void Action_skill_function(FightPhase fightPhase, FightHero source, FightHero target)
    {
        Action_skill.skill_function(fightPhase, source, target);
    }

    //助战技能
    public void Staff_skill_function(FightPhase fightPhase, FightHero source, FightHero target) {
        if (Staff_skill_self.isActionSkill == false) {
            return;
        }
        Staff_skill_self.skill_function(fightPhase, source, target);
    }

}
