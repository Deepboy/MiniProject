using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewFightConfig;
using System.Reflection;
using UnityEngine;

public class Skill
{
    public Skill() { }

    public Skill(string data) {
        string[] strs = data.Split('|');

        if (strs.Length != 8) Debug.Log("数据格式错误"+strs.Length);

        this.Skill_id = int.Parse(strs[0]);
        this.Skill_name = strs[1];
        this.Skill_function_name = strs[2];
        this.Skill_describe = strs[3];
        this.isActionSkill = int.Parse(strs[4])>0;
        this.Skill_phase = (FightPhase)Enum.Parse(typeof(FightPhase), strs[5]);
        this.Skill_cd = float.Parse(strs[6]);
        this.Skill_effect_path = strs[7];
        this.Skill_enable = true;
    }

    public Skill(int skill_id, string skill_name, string skill_function_name, string skill_describe, bool is_action_skill , FightPhase skill_phase,float skill_cd,string skill_effect_path)
    {
        this.Skill_id = skill_id;
        this.Skill_name = skill_name;
        this.Skill_describe = skill_describe;
        this.isActionSkill = is_action_skill;
        this.Skill_phase = skill_phase;
        this.Skill_function_name = skill_function_name;
        this.Skill_cd = skill_cd;
        this.Skill_effect_path = skill_effect_path;
        this.Skill_enable = true;
    }

    public override string ToString()
    {
        return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", Skill_id, Skill_name, Skill_describe, Convert.ToInt32(isActionSkill),((int)Skill_phase).ToString(),Skill_function_name,Skill_cd);
    }

    public void skill_function(FightPhase now_fightPhase, FightHero source,FightHero target) {
        if (!Skill_enable) return;
        if (now_fightPhase != Skill_phase) return;
        SkillInfo skillInfo = new SkillInfo();
        MethodInfo mi = skillInfo.GetType().GetMethod(Skill_function_name);
        if (mi == null) {
            Debug.Log("警告：在SkillInfo中没有对应的技能：[" + Skill_function_name + "]");
            return;
        }
        object[] parameters = new object[] { source, target};
        Skill_enable=(bool)mi.Invoke(skillInfo, parameters);
    }


    public int Skill_id { get; set; }
    public string Skill_name { get; set; }
    public string Skill_describe { get; set; }
    public FightPhase Skill_phase { get; set; }
    public string Skill_function_name { get; set; }
    public float Skill_cd { get; set; }
    public bool isActionSkill { get; set; }
    public string Skill_effect_path { get; set; }
    public bool Skill_enable { get; set; }
}