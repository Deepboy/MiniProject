using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Model;
using GameServer.Buff;
using GameServer.miniServer;
using GameServer.Tool;

public class FightHero
{
    public FightHero(HeroCard heroCard,List<ElementType> choiceElementCard) {
        this.Hp = heroCard.OrgHp + heroCard.Hero_level * heroCard.Hero_hp_growth;
        //this.Hp = 100;
        this.OrgHp = heroCard.OrgHp + heroCard.Hero_level * heroCard.Hero_hp_growth;
        this.Speed = heroCard.OrgSpeed + heroCard.Hero_level * heroCard.Hero_speed_growth;
        this.OrgSpeed = heroCard.OrgSpeed + heroCard.Hero_level * heroCard.Hero_speed_growth;
        this.Fire = heroCard.OrgFire + heroCard.Hero_level * heroCard.Fire_growth*2.3f;
        this.OrgFire = heroCard.OrgFire + heroCard.Hero_level * heroCard.Fire_growth * 2.3f;
        this.Fire_Damage = 1;
        this.Fire_Defense = 1;
        this.Water = heroCard.OrgWater + heroCard.Hero_level * heroCard.Water_growth * 2.3f;
        this.OrgWater = heroCard.OrgWater + heroCard.Hero_level * heroCard.Water_growth * 2.3f;
        this.Water_Damage = 1;
        this.Water_Defense = 1;
        this.Earth = heroCard.OrgSoil + heroCard.Hero_level * heroCard.Soil_growth * 2.3f;
        this.OrgEarth = heroCard.OrgSoil + heroCard.Hero_level * heroCard.Soil_growth * 2.3f;
        this.Earth_Damage = 1;
        this.Earth_Defense = 1;
        this.Wind = heroCard.OrgWind + heroCard.Hero_level * heroCard.Wind_growth * 2.3f;
        this.OrgWind = heroCard.OrgWind + heroCard.Hero_level * heroCard.Wind_growth * 2.3f;
        this.Wind_Damage = 1;
        this.Wind_Defense = 1;
        this.Thunder = heroCard.OrgThunder + heroCard.Hero_level * heroCard.Thunder_growth * 2.3f;
        this.OrgThunder = heroCard.OrgThunder + heroCard.Hero_level * heroCard.Thunder_growth * 2.3f;
        this.Thunder_Damage = 1;
        this.Thunder_Defense = 1;
        this.Dark = heroCard.OrgDark + heroCard.Hero_level * heroCard.Dark_growth * 2.3f;
        this.OrgDark = heroCard.OrgDark + heroCard.Hero_level * heroCard.Dark_growth * 2.3f;
        this.Dark_Damage = 1;
        this.Dark_Defense = 1;
        this.All_Element_Damage = 1;
        this.All_Element_Defense = 1;
        this.Final_Damage = 1;
        this.Final_Defense = 1;

        this.Pass_through = 0;
        this.Increase_Damage = 0;
        this.Parry = 0;
        this.Defense = 0;
        this.RateOfAction = 0;
        this.ChoiceCardType = choiceElementCard;
        this.Shield = 0;
        this.Crit = 0;
        this.Crit_Damage = 2;
        this.Dodge = 0;
        this.SuckBlood = 0;
        this.IsInvincible = false;
        this.IsVertigo = false;
        this.IsOperate = true;
        this.IsInmmune = false;

        this.Hero_Card = heroCard;
        this.ElementCardDict = new Dictionary<ElementType, int>();
        this.BuffDict = new Dictionary<BuffCode, BaseBuff>();
        this.DamageCaculation = new Dictionary<ElementType, float>();
        this.UsedCardDict = new Dictionary<ElementType, int>();

        ElementCardDict.Add(ElementType.Fire, 0);
        ElementCardDict.Add(ElementType.Water, 0);
        ElementCardDict.Add(ElementType.Earth, 0);
        ElementCardDict.Add(ElementType.Wind, 0);
        ElementCardDict.Add(ElementType.Thunder, 0);
        ElementCardDict.Add(ElementType.Dark, 0);

        UsedCardDict.Add(ElementType.Fire, 0);
        UsedCardDict.Add(ElementType.Water, 0);
        UsedCardDict.Add(ElementType.Earth, 0);
        UsedCardDict.Add(ElementType.Wind, 0);
        UsedCardDict.Add(ElementType.Thunder, 0);
        UsedCardDict.Add(ElementType.Dark, 0);



    }



    public override string ToString()
    {
        StringBuilder buffInfo = new StringBuilder();
        buffInfo.Append("{");
        if (BuffDict != null) {
            bool hasInfo = false;
            List<BuffCode> bufflist = new List<BuffCode>();
            bufflist.AddRange(BuffDict.Keys);
            foreach (BuffCode c in bufflist)
            {
                buffInfo.Append("[" + BuffDict[c].Buff_Ename.ToString() + "," + BuffDict[c].Tier + "," + BuffDict[c].DurationTime+"];");
                hasInfo = true;
            }
            if(hasInfo)
                buffInfo.Remove(buffInfo.Length - 1, 1);
        }
        buffInfo.Append("}");

        //玩家ID,玩家角色ID,血量，行动条，buff列表.手牌由room决定传输自己的数据
        return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",User_Client.GetUserInfo().User_id,User_Client.GetUserInfo().Username,Hero_Card.Hero_id,Hp,Speed,RateOfAction,Shield, buffInfo.ToString());
    }
    #region field
    public float hp;//生命值
    public float speed;//速度值
    public float fire;//火属性值
    public float fire_Damage ;//火属性伤害系数
    public float fire_Defense ;//火属性抵抗系数
    public float water ;//水属性值
    public float water_Damage ;//水属性伤害系数
    public float water_Defense ;//水属性抵抗系数
    public float earth ;//土属性值
    public float earth_Damage ;//土属性伤害系数
    public float earth_Defense ;//土属性抵抗系数
    public float wind ;//风属性值
    public float wind_Damage ;//风属性伤害系数
    public float wind_Defense ;//风属性抵抗系数
    public float thunder ;//雷属性值
    public float thunder_Damage ;//雷属性值伤害系数
    public float thunder_Defense ;//雷属性值抵抗系数
    public float dark ;//暗属性值
    public float dark_Damage ;//暗属性伤害系数
    public float dark_Defense ;//暗属性抵抗系数
    public float all_Element_Damage ;//全属性伤害系数
    public float all_Element_Defense ;//全属性抵抗系数
    public float final_Damage ;//最终伤害系数
    public float final_Defense ;//最终伤害减免系数
    public float pass_through ;//穿透系数
    public float increase_Damage ;//增伤值
    public float parry ;//格挡值
    public float defense ;//防御值
    public float suckBlood ;//吸血值

    public float rateOfAction;//行动点比例
    public float shield ;//护盾
    public float crit ;//暴击率
    public float crit_Damage ;//暴击伤害
    public float dodge ;//闪避率
    #endregion

    public FightHeroMaxAndMinDefine define = new FightHeroMaxAndMinDefine();

    #region Atrribute
    public float Hp {
        get {
            if (hp < define.Min_hp)
                hp = define.Min_hp;
            if (hp > define.Max_hp)
                hp = define.Max_hp;
            return hp;
        }
        set {
            hp = value;
        }
    }//生命值
    public float OrgHp { get; set; }//初始生命值
    public float Speed {
        get {
            if (speed < define.Min_speed)
                speed = define.Min_speed;
            if (speed > define.Max_speed)
                speed = define.Max_speed;
            return speed;
        }
        set {
            speed = value;
        }
    }//速度值
    public float OrgSpeed { get; set; }//初始速度值
    public float Fire {
        get {
            if (fire < define.Min_fire)
                fire = define.Min_fire;
            if (fire > define.Max_fire)
                fire = define.Max_fire;
            return fire;
        }
        set { fire = value; } }//火属性值
    public float OrgFire { get; set; }//初始火属性值
    public float Fire_Damage { get {
            if (fire_Damage < define.Min_fire_Damage)
                fire_Damage = define.Min_fire_Damage;
            if (fire_Damage > define.Max_fire_Damage)
                fire_Damage = define.Max_fire_Damage;
            return fire_Damage;
        } set { fire_Damage = value; } }//火属性伤害系数
    public float Fire_Defense { get {
            if (fire_Defense < define.Min_fire_Defense)
                fire_Defense = define.Min_fire_Defense;
            if (fire_Defense > define.Max_fire_Defense)
                fire_Defense = define.Max_fire_Defense;
            return fire_Defense;
        } set { fire_Defense = value; } }//火属性抵抗系数
    public float Water { get {
            if (water < define.Min_water)
                water = define.Min_water;
            if (water > define.Max_water)
                water = define.Max_water;
            return water;
        } set { water = value; } }//水属性值
    public float OrgWater { get; set; }//初始水属性值
    public float Water_Damage { get {
            if (water_Damage < define.Min_water_Damage)
                water_Damage = define.Min_water_Damage;
            if (water_Damage > define.Max_water_Damage)
                water_Damage = define.Max_water_Damage;
            return water_Damage;
        } set { water_Damage = value; } }//水属性伤害系数
    public float Water_Defense { get {
            if (water_Defense < define.Min_water_Defense)
                water_Defense = define.Min_water_Defense;
            if (water_Defense > define.Max_water_Defense)
                water_Defense = define.Max_water_Defense;
            return water_Defense;
        } set { water_Defense = value; } }//水属性抵抗系数
    public float Earth { get {
            if (earth < define.Min_earth)
                earth = define.Min_earth;
            if (earth > define.Max_earth)
                earth = define.Max_earth;
            return earth;
        } set { earth = value; } }//土属性值
    public float OrgEarth { get; set; }//初始土属性值
    public float Earth_Damage { get {
            if (earth_Damage < define.Min_earth_Damage)
                earth_Damage = define.Min_earth_Damage;
            if (earth_Damage > define.Max_earth_Damage)
                earth_Damage = define.Max_earth_Damage;
            return earth_Damage;
        } set { earth_Damage = value; } }//土属性伤害系数
    public float Earth_Defense { get {
            if (earth_Defense < define.Min_earth_Defense)
                earth_Defense = define.Min_earth_Defense;
            if (earth_Defense > define.Max_earth_Defense)
                earth_Defense = define.Max_earth_Defense;
            return earth_Defense;
        } set { earth_Defense = value; } }//土属性抵抗系数
    public float Wind { get {
            if (wind < define.Min_wind)
                wind = define.Min_wind;
            if (wind > define.Max_wind)
                wind = define.Max_wind;
            return wind;
        } set { wind = value; } }//风属性值
    public float OrgWind { get; set; }//初始风属性值
    public float Wind_Damage { get {
            if (wind_Damage < define.Min_wind_Damage)
                wind_Damage = define.Min_wind_Damage;
            if (wind_Damage > define.Max_wind_Damage)
                wind_Damage = define.Max_wind_Damage;
            return wind_Damage;
        } set { wind_Damage = value; } }//风属性伤害系数
    public float Wind_Defense { get {
            if (wind_Defense < define.Min_wind_Defense)
                wind_Defense = define.Min_wind_Defense;
            if (wind_Defense > define.Max_wind_Defense)
                wind_Defense = define.Max_wind_Defense;
            return wind_Defense;
        } set { wind_Defense = value; } }//风属性抵抗系数
    public float Thunder { get {
            if (thunder < define.Min_thunder)
                thunder = define.Min_thunder;
            if (thunder > define.Max_thunder)
                thunder = define.Max_thunder;
            return thunder;
        } set { thunder = value; } }//雷属性值
    public float OrgThunder { get; set; }//初始雷属性值
    public float Thunder_Damage { get {
            if (thunder_Damage < define.Min_thunder_Damage)
                thunder_Damage = define.Min_thunder_Damage;
            if (thunder_Damage > define.Max_thunder_Damage)
                thunder_Damage = define.Max_thunder_Damage;
            return thunder_Damage;
        } set { thunder_Damage = value; } }//雷属性值伤害系数
    public float Thunder_Defense { get {
            if (thunder_Defense < define.Min_thunder_Defense)
                thunder_Defense = define.Min_thunder_Defense;
            if (thunder_Defense > define.Max_thunder_Defense)
                thunder_Defense = define.Max_thunder_Defense;
            return thunder_Defense;
        } set { thunder_Defense = value; } }//雷属性值抵抗系数
    public float Dark { get {
            if (dark < define.Min_dark)
                dark = define.Min_dark;
            if (dark > define.Max_dark)
                dark = define.Max_dark;
            return dark;
        } set { dark = value; } }//暗属性值
    public float OrgDark { get; set; }//初始暗属性值
    public float Dark_Damage { get {
            if (dark_Damage < define.Min_dark_Damage)
                dark_Damage = define.Min_dark_Damage;
            if (dark_Damage > define.Max_dark_Damage)
                dark_Damage = define.Max_dark_Damage;
            return dark_Damage;
        } set { dark_Damage = value; } }//暗属性伤害系数
    public float Dark_Defense { get {
            if (dark_Defense < define.Min_dark_Defense)
                dark_Defense = define.Min_dark_Defense;
            if (dark_Defense > define.Max_dark_Defense)
                dark_Defense = define.Max_dark_Defense;
            return dark_Defense;
        } set { dark_Defense = value; } }//暗属性抵抗系数
    public float All_Element_Damage { get {
            if (all_Element_Damage < define.Min_all_Element_Damage)
                all_Element_Damage = define.Min_all_Element_Damage;
            if (all_Element_Damage > define.Max_all_Element_Damage)
                all_Element_Damage = define.Max_all_Element_Damage;
            return all_Element_Damage;
        } set { all_Element_Damage = value; } }//全属性伤害系数
    public float All_Element_Defense { get {
            if (all_Element_Defense < define.Min_all_Element_Defense)
                all_Element_Defense = define.Min_all_Element_Defense;
            if (all_Element_Defense > define.Max_all_Element_Defense)
                all_Element_Defense = define.Max_all_Element_Defense;
            return all_Element_Defense;
        } set { all_Element_Defense = value; } }//全属性抵抗系数
    public float Final_Damage { get {
            if (final_Damage < define.Min_final_Damage)
                final_Damage = define.Min_final_Damage;
            if (final_Damage > define.Max_final_Damage)
                final_Damage = define.Max_final_Damage;
            return final_Damage;
        } set { final_Damage = value; } }//最终伤害系数
    public float Final_Defense { get {
            if (final_Defense < define.Min_final_Defense)
                final_Defense = define.Min_final_Defense;
            if (final_Defense > define.Max_final_Defense)
                final_Defense = define.Max_final_Defense;
            return final_Defense;
        } set { final_Defense = value; } }//最终伤害减免系数
    public float Pass_through { get {
            if (pass_through < define.Min_pass_through)
                pass_through = define.Min_pass_through;
            if (pass_through > define.Max_pass_through)
                pass_through = define.Max_pass_through;
            return pass_through;
        } set { pass_through = value; } }//穿透系数
    public float Increase_Damage { get {
            if (increase_Damage < define.Min_increase_Damage)
                increase_Damage = define.Min_increase_Damage;
            if (increase_Damage > define.Max_increase_Damage)
                increase_Damage = define.Max_increase_Damage;
            return increase_Damage;
        } set { increase_Damage = value; } }//增伤值
    public float Parry { get {
            if (parry < define.Min_parry)
                parry = define.Min_parry;
            if (parry > define.Max_parry)
                parry = define.Max_parry;
            return parry;
        } set { parry = value; } }//格挡值
    public float Defense { get {
            if (defense < define.Min_defense)
                defense = define.Min_defense;
            if (defense > define.Max_defense)
                defense = define.Max_defense;
            return defense;
        } set { defense = value; } }//防御值
    public float SuckBlood { get {
            if (suckBlood < define.Min_suckBlood)
                suckBlood = define.Min_suckBlood;
            if (suckBlood > define.Max_suckBlood)
                suckBlood = define.Max_suckBlood;
            return suckBlood;
        } set { suckBlood = value; } }//吸血值
    public float RateOfAction { get {
            if (rateOfAction < define.Min_rateOfAction)
                rateOfAction = define.Min_rateOfAction;
            if (rateOfAction > define.Max_rateOfAction)
                rateOfAction = define.Max_rateOfAction;
            return rateOfAction;
        } set { rateOfAction = value; } }//行动点比例
    public float Shield { get {
            if (shield < define.Min_shield)
                shield = define.Min_shield;
            if (shield > define.Max_shield)
                shield = define.Max_shield;
            return shield;
        } set { shield = value; } }//护盾
    public float Crit { get {
            if (crit < define.Min_crit)
                crit = define.Min_crit;
            if (crit > define.Max_crit)
                crit = define.Max_crit;
            return crit;
        } set { crit = value; } }//暴击率
    public float Crit_Damage { get {
            if (crit_Damage < define.Min_crit_Damage)
                crit_Damage = define.Min_crit_Damage;
            if (crit_Damage > define.Max_crit_Damage)
                crit_Damage = define.Max_crit_Damage;
            return crit_Damage;
        } set { crit_Damage = value; } }//暴击伤害
    public float Dodge { get {
            if (dodge < define.Min_dodge)
                dodge = define.Min_dodge;
            if (dodge > define.Max_dodge)
                 dodge = define.Max_dodge;
            return dodge;
        } set { dodge = value; } }//闪避率
    public bool IsInvincible { get; set; }//是否无敌
    public bool IsVertigo { get; set; }//是否眩晕
    public bool IsOperate { get; set; }//是否能操作
    public bool IsInmmune { get; set; }//是否魔免

    #endregion

    public float GetAttributeByEnum(ElementType elementType)
    {
        float ans = 0;
        switch (elementType)
        {
            case ElementType.Fire:
                ans = Fire;
                break;
            case ElementType.Water:
                ans = Water;
                break;
            case ElementType.Earth:
                ans = Earth;
                break;
            case ElementType.Wind:
                ans = Wind;
                break;
            case ElementType.Thunder:
                ans = Thunder;
                break;
            case ElementType.Dark:
                ans = Dark;
                break;
            default:
                ans = -1;
                break;
        }
        if (ans == -1) { Console.WriteLine("数据出错"); ans = 0; }
        return ans;
    }
    public float GetAttributeDamageByEnum(ElementType elementType)
    {
        float ans = 0;
        switch (elementType)
        {
            case ElementType.Fire:
                ans = Fire_Damage;
                break;
            case ElementType.Water:
                ans = Water_Damage;
                break;
            case ElementType.Earth:
                ans = Earth_Damage;
                break;
            case ElementType.Wind:
                ans = Wind_Damage;
                break;
            case ElementType.Thunder:
                ans = Thunder_Damage;
                break;
            case ElementType.Dark:
                ans = Dark_Damage;
                break;
            default:
                ans = -1;
                break;
        }
        if (ans == -1) { Console.WriteLine("数据出错"); ans = 0; }
        return ans;
    }
    public float GetAttributeDefenseByEnum(ElementType elementType)
    {
        float ans = 0;
        switch (elementType)
        {
            case ElementType.Fire:
                ans = Fire_Defense;
                break;
            case ElementType.Water:
                ans = Water_Defense;
                break;
            case ElementType.Earth:
                ans = Earth_Defense;
                break;
            case ElementType.Wind:
                ans = Wind_Defense;
                break;
            case ElementType.Thunder:
                ans = Thunder_Defense;
                break;
            case ElementType.Dark:
                ans = Dark_Defense;
                break;
            default:
                ans = -1;
                break;
        }
        if (ans == -1) { Console.WriteLine("数据出错"); ans = 0; }
        return ans;
    }
    public string GetCardInfo() {

        StringBuilder cardInfo = new StringBuilder();
        cardInfo.Append("{");
        if (ElementCardDict != null)
        {
            foreach (ElementType e in ElementCardDict.Keys)
            {
                cardInfo.Append("[" + e.ToString() + "," + ElementCardDict[e] + "];");
            }
            cardInfo.Remove(cardInfo.Length - 1, 1);
        }
        cardInfo.Append("}");

        return cardInfo.ToString();
    }
    public string GetSkillInfo() {
        StringBuilder skillInfo = new StringBuilder();
        skillInfo.Append("{");
        if (Hero_Card.Prossive_skill_1.Skill_cd < Hero_Card.Prossive_skill_1.OrgSkill_cd)
            skillInfo.Append(Hero_Card.Prossive_skill_1.Skill_cd / Hero_Card.Prossive_skill_1.OrgSkill_cd + ";");
        else
            skillInfo.Append("1;");
        if (Hero_Card.Prossive_skill_2.Skill_cd < Hero_Card.Prossive_skill_2.OrgSkill_cd)
            skillInfo.Append(Hero_Card.Prossive_skill_2.Skill_cd / Hero_Card.Prossive_skill_2.OrgSkill_cd + ";");
        else
            skillInfo.Append("1;");
        if (Hero_Card.Prossive_skill_3.Skill_cd < Hero_Card.Prossive_skill_3.OrgSkill_cd)
            skillInfo.Append(Hero_Card.Prossive_skill_3.Skill_cd / Hero_Card.Prossive_skill_3.OrgSkill_cd + ";");
        else
            skillInfo.Append("1;");
        if (Hero_Card.Action_skill.Skill_cd < Hero_Card.Action_skill.OrgSkill_cd)
            skillInfo.Append(Hero_Card.Action_skill.Skill_cd / Hero_Card.Action_skill.OrgSkill_cd + ";");
        else
            skillInfo.Append("1;");
        if (Hero_Card.Staff_skill_other.Skill_cd < Hero_Card.Staff_skill_other.OrgSkill_cd)
            skillInfo.Append(Hero_Card.Staff_skill_other.Skill_cd / Hero_Card.Staff_skill_other.OrgSkill_cd + ";");
        else
            skillInfo.Append("1;");

        skillInfo.Remove(skillInfo.Length - 1, 1);
        skillInfo.Append("}");

        return skillInfo.ToString();
    }
    public void OrgFightHeroInfo()
    {
        this.OrgHp = Hero_Card.OrgHp + Hero_Card.Hero_level * Hero_Card.Hero_hp_growth;
        this.Speed = Hero_Card.OrgSpeed + Hero_Card.Hero_level * Hero_Card.Hero_speed_growth;
        this.OrgSpeed = Hero_Card.OrgSpeed + Hero_Card.Hero_level * Hero_Card.Hero_speed_growth;
        this.Fire = Hero_Card.OrgFire + Hero_Card.Hero_level * Hero_Card.Fire_growth * 2.3f;
        this.OrgFire = Hero_Card.OrgFire + Hero_Card.Hero_level * Hero_Card.Fire_growth * 2.3f;
        this.Fire_Damage = 1;
        this.Fire_Defense = 1;
        this.Water = Hero_Card.OrgWater + Hero_Card.Hero_level * Hero_Card.Water_growth * 2.3f;
        this.OrgWater = Hero_Card.OrgWater + Hero_Card.Hero_level * Hero_Card.Water_growth * 2.3f;
        this.Water_Damage = 1;
        this.Water_Defense = 1;
        this.Earth = Hero_Card.OrgSoil + Hero_Card.Hero_level * Hero_Card.Soil_growth * 2.3f;
        this.OrgEarth = Hero_Card.OrgSoil + Hero_Card.Hero_level * Hero_Card.Soil_growth * 2.3f;
        this.Earth_Damage = 1;
        this.Earth_Defense = 1;
        this.Wind = Hero_Card.OrgWind + Hero_Card.Hero_level * Hero_Card.Wind_growth * 2.3f;
        this.OrgWind = Hero_Card.OrgWind + Hero_Card.Hero_level * Hero_Card.Wind_growth * 2.3f;
        this.Wind_Damage = 1;
        this.Wind_Defense = 1;
        this.Thunder = Hero_Card.OrgThunder + Hero_Card.Hero_level * Hero_Card.Thunder_growth * 2.3f;
        this.OrgThunder = Hero_Card.OrgThunder + Hero_Card.Hero_level * Hero_Card.Thunder_growth * 2.3f;
        this.Thunder_Damage = 1;
        this.Thunder_Defense = 1;
        this.Dark = Hero_Card.OrgDark + Hero_Card.Hero_level * Hero_Card.Dark_growth * 2.3f;
        this.OrgDark = Hero_Card.OrgDark + Hero_Card.Hero_level * Hero_Card.Dark_growth * 2.3f;
        this.Dark_Damage = 1;
        this.Dark_Defense = 1;
        this.All_Element_Damage = 1;
        this.All_Element_Defense = 1;
        this.Final_Damage = 1;
        this.Final_Defense = 1;

        this.Pass_through = 0;
        this.Increase_Damage = 0;
        this.Parry = 0;
        this.Defense = 0;
        this.Crit = 0;
        this.Crit_Damage = 2;
        this.Dodge = 0;
        this.SuckBlood = 0;
        this.IsInvincible = false;
        this.IsVertigo = false;
        this.IsOperate = true;
        this.IsInmmune = false;
    }

    public Client User_Client { get; set; }//客户端
    public HeroCard Hero_Card { get; set; }//出战角色
    public List<ElementType> ChoiceCardType { get; set; }//选择使用的元素牌
    public Dictionary<ElementType,int> ElementCardDict { get; set; }//持有手牌
    public Dictionary<BuffCode,BaseBuff> BuffDict { get; set; }//角色身上的buff
    public Dictionary<ElementType, int> UsedCardDict { get; set; }//玩家历史出牌
    public Dictionary<ElementType, float> DamageCaculation { get; set; }//最后一次受到的伤害

    
}

