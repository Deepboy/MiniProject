using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Model
{
    public class HeroCard
    {
        //构造函数（string）
        public HeroCard(string data)
        {
            string[] strs = data.Split('|');
            this.Hero_id = strs[0];
            this.Hero_name = strs[1];
            this.Hero_starLevel = int.Parse(strs[2]);
            this.Hero_level = int.Parse(strs[3]);
            this.Hero_type = (ElementType)Enum.Parse(typeof(ElementType), strs[4]);
            this.Hero_hp = float.Parse(strs[5]);
            this.OrgHp = float.Parse(strs[5]);
            this.Hero_hp_growth = float.Parse(strs[6]);
            this.Hero_speed = float.Parse(strs[7]);
            this.OrgSpeed = float.Parse(strs[7]);
            this.Hero_speed_growth = float.Parse(strs[8]);
            this.Wind = float.Parse(strs[9]);
            this.OrgWind = float.Parse(strs[9]);
            this.Wind_growth = float.Parse(strs[10]);
            this.Fire = float.Parse(strs[11]);
            this.OrgFire = float.Parse(strs[11]);
            this.Fire_growth = float.Parse(strs[12]);
            this.Water = float.Parse(strs[13]);
            this.OrgWater = float.Parse(strs[13]);
            this.Water_growth = float.Parse(strs[14]);
            this.Thunder = float.Parse(strs[15]);
            this.OrgThunder = float.Parse(strs[15]);
            this.Thunder_growth = float.Parse(strs[16]);
            this.Soil = float.Parse(strs[17]);
            this.OrgSoil = float.Parse(strs[17]);
            this.Soil_growth = float.Parse(strs[18]);
            this.Dark = float.Parse(strs[19]);
            this.OrgDark = float.Parse(strs[19]);
            this.Dark_growth = float.Parse(strs[20]);
            this.Prossive_skill_1_id = strs[21];
            this.Prossive_skill_2_id = strs[22];
            this.Prossive_skill_3_id = strs[23];
            this.Active_skill_id = strs[24];
            this.Staff_skill_myself_id = strs[25];
        }

        public HeroCard(HeroCard heroCard)
        {
            this.Hero_id = heroCard.Hero_id;
            this.Hero_name = heroCard.Hero_name;
            this.Hero_starLevel = heroCard.Hero_starLevel;
            this.Hero_level = heroCard.Hero_level;
            this.Hero_type = heroCard.Hero_type;
            this.Hero_hp = heroCard.Hero_hp;
            this.OrgHp = heroCard.OrgHp;
            this.Hero_hp_growth = heroCard.Hero_hp_growth;
            this.Hero_speed = heroCard.Hero_speed;
            this.OrgSpeed = heroCard.OrgSpeed;
            this.Hero_speed_growth = heroCard.Hero_speed_growth;
            this.Wind = heroCard.Wind;
            this.OrgWind = heroCard.OrgWind;
            this.Wind_growth = heroCard.Wind_growth;
            this.Fire = heroCard.Fire;
            this.OrgFire = heroCard.OrgFire;
            this.Fire_growth = heroCard.Fire_growth;
            this.Water = heroCard.Water;
            this.OrgWater = heroCard.OrgWater;
            this.Water_growth = heroCard.Water_growth;
            this.Thunder = heroCard.Thunder;
            this.OrgThunder = heroCard.OrgThunder;
            this.Thunder_growth = heroCard.Thunder_growth;
            this.Soil = heroCard.Soil;
            this.OrgSoil = heroCard.OrgSoil;
            this.Soil_growth = heroCard.Soil_growth;
            this.Dark = heroCard.Dark;
            this.OrgDark = heroCard.OrgDark;
            this.Dark_growth = heroCard.Dark_growth;
            this.Prossive_skill_1_id = heroCard.Prossive_skill_1_id;
            this.Prossive_skill_2_id = heroCard.Prossive_skill_2_id;
            this.Prossive_skill_3_id = heroCard.Prossive_skill_3_id;
            this.Active_skill_id = heroCard.Active_skill_id;
            this.Staff_skill_myself_id = heroCard.Staff_skill_myself_id;
        }



        //构造函数（传参）
        public HeroCard(string hero_id, string hero_name, int hero_starLevel,int hero_level,
                        ElementType hero_type, float hero_hp,float hero_hp_growth, float hero_speed, float hero_speed_growth,
                        float wind, float wind_growth,float fire, float fire_growth, float water, float water_growth, float thunder, 
                        float thunder_growth,float soil, float soil_growth, float dark, float dark_growth, string prossive_skill_1_id,
                        string prossive_skill_2_id, string prossive_skill_3_id, string active_skill_id, string staff_skill_id)
        {
            this.Hero_id = hero_id;
            this.Hero_name = hero_name;
            this.Hero_starLevel = hero_starLevel;
            this.Hero_level = hero_level;
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
        }

        //类转成string
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}",
                Hero_id, Hero_name,  Hero_starLevel, Hero_level,((int)Hero_type).ToString(), OrgHp, Hero_hp_growth, OrgSpeed,
                Hero_speed_growth, OrgWind, Wind_growth, OrgFire, Fire_growth, OrgWater, Water_growth, OrgThunder, Thunder_growth, OrgSoil, Soil_growth, OrgDark, Dark_growth, Prossive_skill_1_id,
                Prossive_skill_2_id, Prossive_skill_3_id, Active_skill_id, Staff_skill_myself_id);
        }


        public string Hero_id { get; set; }//角色ID
        public string Hero_name { get; set; }//角色名字
        public int Hero_starLevel { get; set; }//角色当前星级
        public int Hero_level { get; set; }//角色当前等级
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
        public string Prossive_skill_1_id { get; set; }//被动技能1对应的ID
        public string Prossive_skill_2_id { get; set; }//被动技能2对应的ID
        public string Prossive_skill_3_id { get; set; }//被动技能3对应的ID
        public string Active_skill_id { get; set; }//主动技能对应的ID
        public string Staff_skill_myself_id { get; set; }//角色自己的助战技能对应的ID
        public Skill Prossive_skill_1 { get; set; }//被动技能对应的技能类1
        public Skill Prossive_skill_2 { get; set; }//被动技能对应的技能类2
        public Skill Prossive_skill_3 { get; set; }//被动技能对应的技能类3
        public Skill Action_skill { get; set; }//主动技能对应的技能类
        public Skill Staff_skill_self { get; set; }//自己的助战技能
        public Skill Staff_skill_other { get; set; }//进入战斗时获得的别人的助战技能

        //被动技能(技能来源，技能目标)
        public void Prossive_skill_function(FightPhase fightPhase, FightHero source, FightHero target)
        {
            if (Staff_skill_other !=null && Staff_skill_other.isActionSkill == false)
            {
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
        public void Staff_skill_function(FightPhase fightPhase, FightHero source, FightHero target)
        {
            if (Staff_skill_other.isActionSkill == false)
            {
                return;
            }
            Staff_skill_other.skill_function(fightPhase, source, target);
        }

        public void UpdataSkill_cd(float t) {
            SetSkillCD(Prossive_skill_1,t);
            SetSkillCD(Prossive_skill_2,t);
            SetSkillCD(Prossive_skill_3,t);
            SetSkillCD(Action_skill,t);
            SetSkillCD(Staff_skill_other,t);
        }

        private void SetSkillCD(Skill skill, float t) {
            if (!skill.Skill_enable)
            {
                skill.Skill_cd += t;
                if (skill.Skill_cd >= skill.OrgSkill_cd) skill.Skill_enable = true;
            }
        }


    }

}
