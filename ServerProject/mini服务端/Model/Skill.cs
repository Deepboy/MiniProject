using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Common;
using GameServer.Tool;

namespace GameServer.Model
{
    public class Skill
    {

        public Skill(Skill skill)
        {
            this.Skill_id = skill.Skill_id;
            this.Skill_name = skill.Skill_name;
            this.isActionSkill = skill.isActionSkill;
            this.Skill_phase = skill.Skill_phase;
            this.Skill_cd = skill.OrgSkill_cd;
            this.OrgSkill_cd = skill.OrgSkill_cd;
            this.Skill_enable = skill.Skill_enable;
        }

        public Skill(string skill_id, string skill_name,int is_action_skill, FightPhase skill_phase, float skill_cd)
        {
            this.Skill_id = skill_id;
            this.Skill_name = skill_name;
            this.isActionSkill = is_action_skill>0;
            this.Skill_phase = skill_phase;
            this.Skill_cd = skill_cd;
            this.OrgSkill_cd = skill_cd;
            this.Skill_enable = true;
            
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", Skill_id, Skill_name, Convert.ToInt32(isActionSkill), Skill_phase.ToString(), Skill_cd);
        }

        public void skill_function(FightPhase now_fightPhase, FightHero source, FightHero target)
        {
            if (!Skill_enable) return;
            if (now_fightPhase != Skill_phase) return;
            SkillInfo skillInfo = new SkillInfo();
            MethodInfo mi = skillInfo.GetType().GetMethod("skill_"+Skill_id);
            if (mi == null)
            {
                Console.WriteLine("警告：在SkillInfo中没有对应的技能：[skill_" + Skill_id + "]");
                return;
            }
            object[] parameters = new object[] { source, target };
            Skill_enable = (bool)mi.Invoke(skillInfo, parameters);
            if (Skill_enable)
            {
                Skill_cd = OrgSkill_cd;
            }
            else {
                Skill_cd = 0;
            }
        }

        public string Skill_id { get; set; }
        public string Skill_name { get; set; }
        public bool isActionSkill { get; set; }
        public FightPhase Skill_phase { get; set; }
        public float Skill_cd { get; set; }
        public float OrgSkill_cd { get; set; }
        public bool Skill_enable { get; set; }
    }
}
