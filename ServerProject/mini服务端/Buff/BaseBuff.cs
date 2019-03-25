using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using Common;
using GameServer.miniServer;

namespace GameServer.Buff
{
     public class BaseBuff
    {
        public int Buff_Id { get; set; }//buff的ID
        public string BuffName { get; set; }//buff的名称
        public BuffCode Buff_Ename { get; set; }//buff英文名以及对应的buffCode
        public ElementType Element_Type { get; set; }//buff元素类型
        public BuffDispelType Buff_Dispel_Type { get; set; }//buff的驱散类型
        public BuffType Buff_Type { get; set; }//buff的效果类型
        public BuffFlag Buff_Flag { get; set; }//buff的标记
        public float Value_1 { get; set; }//参数1
        public float Value_2 { get; set; }//参数2
        public float Value_3 { get; set; }//参数3
        public float Value_4 { get; set; }//参数4
        public float Value_5 { get; set; }//参数5
        public float Value_6 { get; set; }//参数6
        public float Value_7 { get; set; }//参数7
        public float Value_8 { get; set; }//参数8
        public float DurationTime { get; set; }//buff的存在时间
        public float OrgDurationTime { get; set; }//buff的存在时间
        public float OneActionTime { get; set; }//buff的每次生效时间
        public float NextActionTime { get; set; }//buff的下次生效时间
        public int Max_Tier { get; set; }//最大层数

        public bool isOneDamage { get; set; }
        public bool isOneAssist { get; set; }
        public DamageType Damage_Type { get; set; }//伤害类型（普通、真实、元素）
        public FightHero BuffSource { get; set; }//buff的来源
        public int Tier { get; set; }//buff的层数
        public Room fightRoom { get; set; }//对应战斗


        /// <summary>
        /// BaseBuff构造函数
        /// </summary>
        /// <param name="buff_id">ID</param>
        /// <param name="buff_name">中文名字</param>
        /// <param name="buff_ename">英文名字及对应BuffCode</param>
        /// <param name="element_type">元素类型</param>
        /// <param name="buff_dispel">驱散类型</param>
        /// <param name="buff_type">正面、负面效果</param>
        /// <param name="buffFlag">普通、高级、领域、技能</param>
        /// <param name="v1">参数1</param>
        /// <param name="v2">参数2</param>
        /// <param name="v3">参数3</param>
        /// <param name="v4">参数4</param>
        /// <param name="v5">参数5</param>
        /// <param name="v6">参数6</param>
        /// <param name="v7">参数7</param>
        /// <param name="v8">参数8</param>
        /// <param name="durationTime">持续时间</param>
        /// <param name="oneActionTime">跳一次时间</param>
        /// <param name="max_tier">最大层数</param>
        public BaseBuff(int buff_id, string buff_name, BuffCode buff_ename, ElementType element_type, BuffDispelType buff_dispel, BuffType buff_type, BuffFlag buffFlag,
                        float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float durationTime, float oneActionTime, int max_tier)
        {
            this.Buff_Id = buff_id;
            this.BuffName = buff_name;
            this.Buff_Ename = buff_ename;
            this.Element_Type = element_type;
            this.Buff_Dispel_Type = buff_dispel;
            this.Buff_Type = buff_type;
            this.Buff_Flag = buffFlag;
            this.Value_1 = v1;
            this.Value_2 = v2;
            this.Value_3 = v3;
            this.Value_4 = v4;
            this.Value_5 = v5;
            this.Value_6 = v6;
            this.Value_7 = v7;
            this.Value_8 = v8;
            this.DurationTime = durationTime;
            this.OrgDurationTime = durationTime;
            this.OneActionTime = oneActionTime;
            this.NextActionTime = oneActionTime;
            this.Max_Tier = max_tier;
            this.isOneDamage = true;
            this.isOneAssist = true;
        }
        public BaseBuff(BaseBuff buff)
        {
            this.Buff_Id = buff.Buff_Id;
            this.BuffName = buff.BuffName;
            this.Buff_Ename = buff.Buff_Ename;
            this.Element_Type = buff.Element_Type;
            this.Buff_Dispel_Type = buff.Buff_Dispel_Type;
            this.Buff_Type = buff.Buff_Type;
            this.Buff_Flag = buff.Buff_Flag;
            this.Value_1 = buff.Value_1;
            this.Value_2 = buff.Value_2;
            this.Value_3 = buff.Value_3;
            this.Value_4 = buff.Value_4;
            this.Value_5 = buff.Value_5;
            this.Value_6 = buff.Value_6;
            this.Value_7 = buff.Value_7;
            this.Value_8 = buff.Value_8;
            this.DurationTime = buff.DurationTime;
            this.OrgDurationTime = buff.OrgDurationTime;
            this.OneActionTime = buff.OneActionTime;
            this.NextActionTime = buff.NextActionTime;
            this.Max_Tier = buff.Max_Tier;
            this.isOneDamage = true;
            this.isOneAssist = true;
        }

        public virtual void AddBuffRule(int tier)
        {
            NextActionTime = OneActionTime;
            DurationTime = OrgDurationTime;
            Tier = (Tier + tier) > Max_Tier ? Max_Tier : (Tier + tier);
        }//Buff添加规则
        public virtual void UpdateBuff(float time)
        {
            NextActionTime -= time;
            DurationTime -= time;
        }//Buff计时更新
        public virtual void OnceAssistEffect(FightHero target) { }//瞬时辅助效果
        public virtual void MoreAssistEffect(FightHero target) { }//持续辅助效果
        public virtual void LastAssistEffect(FightHero target) { }//结束辅助效果
        public virtual void OnceDamageEffect(FightHero target) { }//瞬时伤害效果
        public virtual void MoreDamageEffect(FightHero target) { }//持续伤害效果
        public virtual void LastDamageEffect(FightHero target) { }//结束伤害效果
        //集气阶段
        public virtual void IntendPhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase != FightPhase.BeforeIntend) return;
            if (NextActionTime <= 0)
            {
                MoreDamageEffect(target);
                NextActionTime = OneActionTime;
            }
        }
        //行动阶段
        public virtual void ActionPhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase == FightPhase.AfterAction && isOneAssist)
            {
                OnceAssistEffect(target);
                isOneAssist = false;
            }
            if (fightPhase == FightPhase.AfterAction && isOneDamage)
            {
                OnceDamageEffect(target);
                isOneDamage = false;
            }
        }
        //敌方行动阶段
        public virtual void OtherActionPhase(FightPhase fightPhase, FightHero target, FightHero other, string data) { }
        //技能阶段
        public virtual void SkillPhase(FightPhase fightPhase, FightHero target, FightHero other, string data) { }
        //伤害阶段
        public virtual void DamagePhase(FightPhase fightPhase, FightHero target, FightHero other, string data) { }
        //受击阶段
        public virtual void HitPhase(FightPhase fightPhase, FightHero target, FightHero other, string data) { }
        //结束阶段
        public virtual void EndPhase(FightPhase fightPhase, FightHero target, FightHero other, string data) { }

     }

    //每秒造成5点火属性伤害，持续8秒
    public class Buff_2001 : BaseBuff
    {
        public Buff_2001(BaseBuff baseBuff) : base(baseBuff) { }

        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //降低20%的速度，持续8秒。
    public class Buff_2002 : BaseBuff
    {
        public Buff_2002(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            float CurSpeed = target.OrgSpeed * v * Tier < 0 ? target.OrgSpeed : target.OrgSpeed * v * Tier;
            target.Speed -= CurSpeed;
            base.MoreAssistEffect(target);
        }
    }
    //防御值增加30，持续10秒。
    public class Buff_2003 : BaseBuff
    {
        public Buff_2003(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if(BuffSource==target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource,target);
            target.Defense += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //受到的伤害减少4点，持续8秒。
    public class Buff_2004 : BaseBuff
    {
        public Buff_2004(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Parry += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //增加5%速度，持续8秒，最多叠加8层。
    public class Buff_2005 : BaseBuff
    {
        public Buff_2005(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Speed *= (1 + v * Tier);
            base.MoreAssistEffect(target);
        }
    }
    //增加20%速度，持续5秒。
    public class Buff_2006 : BaseBuff
    {
        public Buff_2006(BaseBuff baseBuff) : base(baseBuff) { }

        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Speed *= (1 + v * Tier);
            base.MoreAssistEffect(target);
        }
    }
    //造成的伤害的30%无视防御，持续15秒。
    public class Buff_2007 : BaseBuff
    {
        public Buff_2007(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Pass_through += v * Tier;
            base.MoreAssistEffect(target);
        }

    }
    //行动点减少1点。
    public class Buff_2008 : BaseBuff
    {
        public Buff_2008(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            Console.WriteLine("电蚀");
            target.RateOfAction -= Value_1 * Tier;
            base.OnceAssistEffect(target);
        }

    }
    //每秒受到3点暗属性伤害，持续6秒
    public class Buff_2009 : BaseBuff
    {
        public Buff_2009(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //对敌伤害的25%转化为自己的生命，持续20秒。
    public class Buff_2010 : BaseBuff
    {
        public Buff_2010(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.SuckBlood += v * Tier;
            base.MoreAssistEffect(target);
        }

    }
    //造成的伤害增加5，持续10秒。
    public class Buff_2011 : BaseBuff
    {
        public Buff_2011(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Increase_Damage += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每秒造成6点真实伤害，持续5秒。
    public class Buff_2012 : BaseBuff
    {
        public Buff_2012(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Real, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }

    }
    //每秒受到3点土属性伤害，持续8秒
    public class Buff_2013 : BaseBuff
    {
        public Buff_2013(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //闪避率+20%，持续15秒。（真实伤害无法闪避）
    public class Buff_2014 : BaseBuff
    {
        public Buff_2014(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Pass_through += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //回复HP+5/2秒，持续12秒。
    public class Buff_2015 : BaseBuff
    {
        public Buff_2015(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.AddHp(v * Tier, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //生成一个抵挡20点敌方所造成伤害的护盾，被击破后消失。
    public class Buff_2016 : BaseBuff
    {
        public Buff_2016(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Shield += v * Tier;
            base.OnceAssistEffect(target);
        }
    }
    //每秒造成6点水属性伤害，持续5秒。
    public class Buff_2017 : BaseBuff
    {
        public Buff_2017(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //每秒受到6点风属性伤害，持续5秒
    public class Buff_2018 : BaseBuff
    {
        public Buff_2018(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //每层使最终元素伤害增加10%，持续20秒。
    public class Buff_2019 : BaseBuff
    {
        public Buff_2019(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.All_Element_Damage += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每层使受到的最终元素伤害增加10%，持续20秒。
    public class Buff_2020 : BaseBuff
    {
        public Buff_2020(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.All_Element_Defense -= v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每层使受到的计算后的最终伤害提升5%，持续20秒。
    public class Buff_2021 : BaseBuff
    {
        public Buff_2021(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Final_Defense -= v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每层使受到的计算后的最终伤害提升15%，持续20秒。
    public class Buff_2022 : BaseBuff
    {
        public Buff_2022(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Final_Defense -= v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每层使受到的计算后的最终伤害减少5%；持续20秒。
    public class Buff_2023 : BaseBuff
    {
        public Buff_2023(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Final_Defense += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //清除自身的所有负面状态，并获得对负面状态的15秒免疫时间。
    public class Buff_2024 : BaseBuff
    {
        public Buff_2024(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            target.IsInmmune = true;
            List<BuffCode> target_buffList = new List<BuffCode>();
            target_buffList.AddRange(target.BuffDict.Keys);
            foreach (BuffCode key in target_buffList)
                if (target.BuffDict[key].Buff_Type == BuffType.NegativeState)
                    target.BuffDict.Remove(key);
            base.OnceAssistEffect(target);
        }
        public override void MoreAssistEffect(FightHero target)
        {
            target.IsInmmune = true;
            base.MoreAssistEffect(target);
        }
        public override void LastAssistEffect(FightHero target)
        {
            target.IsInmmune = false;
            base.LastAssistEffect(target);
        }
    }
    //进入无敌状态，免疫任何伤害，持续20秒。
    public class Buff_2025 : BaseBuff
    {
        public Buff_2025(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            target.IsInvincible = true;
            base.MoreAssistEffect(target);
        }
        public override void LastAssistEffect(FightHero target)
        {
            target.IsInvincible = false;
            base.LastAssistEffect(target);
        }
    }
    //每5秒对敌人造成8点火属性伤害，持续时间60秒。
    public class Buff_2026 : BaseBuff
    {
        public Buff_2026(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //本场战斗，每5秒对敌人造成10点火属性伤害。
    public class Buff_2027 : BaseBuff
    {
        public Buff_2027(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreDamageEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            fightRoom.TakeDamage(v * Tier, DamageResource.CollateralDamage, DamageType.Element, Element_Type, BuffSource, target);
            base.MoreDamageEffect(target);
        }
    }
    //每次出牌对敌人施加1层减速buff，持续60秒，减速buff最多为5层。
    public class Buff_2028 : BaseBuff
    {
        public Buff_2028(BaseBuff baseBuff) : base(baseBuff) { }
        public override void ActionPhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase != FightPhase.AfterAction) return;
            Dictionary<BuffCode, BaseBuff> buffInfoDict = target.User_Client.BaseInfoDict;
            List<Client> clientList = fightRoom.GetRoomClient();
            fightRoom.SetCardEffectBuff(BuffSource, other, buffInfoDict[BuffCode.SpeedCut], 1, BuffTarget.Other);
            base.ActionPhase(fightPhase, target, other, data);
        }

    }
    //本场战斗降低敌人50%的速度。
    public class Buff_2029 : BaseBuff
    {
        public Buff_2029(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            FightHero other = fightRoom.GetRoomClient()[0].Fight_Hero == target ? fightRoom.GetRoomClient()[1].Fight_Hero: fightRoom.GetRoomClient()[0].Fight_Hero;
            other.Speed -= other.OrgSpeed*(1 - v * Tier);
            base.MoreAssistEffect(target);
        }
    }
    //受到的伤害减少4点，持续45秒。
    public class Buff_2030 : BaseBuff
    {
        public Buff_2030(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Parry += v * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //本局游戏内，角色死亡耗费4点行动点重生。
    public class Buff_2031 : BaseBuff
    {
        public Buff_2031(BaseBuff baseBuff) : base(baseBuff) { }
        public override void DamagePhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase != FightPhase.AfterDemage || target.Hp > 0) return;
            if (target.RateOfAction - Value_1 > 0)
            {
                target.RateOfAction -= Value_1;
                target.Hp = target.OrgHp;
            }
            base.DamagePhase(fightPhase, target, other, data);
        }
    }
    //每使用一张风元素牌叠加一层，每层增加自己5%闪避、5%暴击率，最多8层，不刷新时间，持续时间60秒。
    public class Buff_2032 : BaseBuff
    {
        public Buff_2032(BaseBuff baseBuff) : base(baseBuff) { }
        public override void ActionPhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase != FightPhase.AfterAction) return;
            
            //火水土风雷暗
            string[] strs = data.Split('|');
            if (int.Parse(strs[3]) > 0)
            {
                this.Tier += int.Parse(strs[3]);
            }
            base.ActionPhase(fightPhase, target, other, data);
        }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            float v2 = Value_1;
            if (BuffSource == target)
                v2 = fightRoom.EffectToSelfCaculation(Value_2, Element_Type, BuffSource);
            else
                v2 = fightRoom.EffectToEnemyCaculation(Value_2, Element_Type, BuffSource, target);
            target.Dodge += v * Tier;
            target.Crit += v2 * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //本场战斗，闪避+30%、暴击率+30%。
    public class Buff_2033 : BaseBuff
    {
        public Buff_2033(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            float v2 = Value_1;
            if (BuffSource == target)
                v2 = fightRoom.EffectToSelfCaculation(Value_2, Element_Type, BuffSource);
            else
                v2 = fightRoom.EffectToEnemyCaculation(Value_2, Element_Type, BuffSource, target);
            target.Dodge += v * Tier;
            target.Crit += v2 * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //每使用一张暗元素牌为自己施加一层吸血buff，持续时间60秒，最多为4层。
    public class Buff_2034 : BaseBuff
    {
        public Buff_2034(BaseBuff baseBuff) : base(baseBuff) { }
        public override void ActionPhase(FightPhase fightPhase, FightHero target, FightHero other, string data)
        {
            if (fightPhase != FightPhase.AfterAction) return;
            //火水土风雷暗
            string[] strs = data.Split('|');
            if (int.Parse(strs[5]) > 0)
            {
                Dictionary<BuffCode, BaseBuff> buffInfoDict = target.User_Client.BaseInfoDict;
                fightRoom.SetCardEffectBuff(BuffSource, target, buffInfoDict[BuffCode.SuckBlood], int.Parse(strs[5]), BuffTarget.Self);
            }
            base.ActionPhase(fightPhase, target, other, data);
        }
    }
    //本场战斗吸血+50%。
    public class Buff_2035 : BaseBuff
    {
        public Buff_2035(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.SuckBlood += Tier * v;
            base.MoreAssistEffect(target);
        }

    }
    //20秒眩晕（敌方不可出牌）
    public class Buff_2036 : BaseBuff
    {
        public Buff_2036(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            target.IsVertigo = true;
            base.OnceAssistEffect(target);
        }
        public override void MoreAssistEffect(FightHero target)
        {
            target.IsVertigo = true;
            base.MoreAssistEffect(target);
        }
        public override void LastAssistEffect(FightHero target)
        {
            target.IsVertigo = false;
            base.LastAssistEffect(target);
        }
    }
    //每20秒扣除敌人1行动点，清除自身3种弱负面效果，并有20%概率随机祛除敌人身上1种强正面效果或自己身上一种强负面效果。
    public class Buff_2037 : BaseBuff
    {
        public Buff_2037(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            List<Client> clientList = fightRoom.GetRoomClient();
            FightHero other = null;
            foreach (Client c in clientList)
            {
                if (c.Fight_Hero != target)
                    other = c.Fight_Hero;
            }
            List<BuffCode> other_buffList = new List<BuffCode>();
            List<BuffCode> target_buffList = new List<BuffCode>();
            other_buffList.AddRange(other.BuffDict.Keys);
            target_buffList.AddRange(target.BuffDict.Keys);
            other.RateOfAction -= Value_1 * Tier;
            for (int i = 0; i < Value_2; i++)
            {
                foreach (BuffCode key in target_buffList)
                {
                    if (target.BuffDict.ContainsKey(key))
                        if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type == BuffDispelType.Weak)
                        {
                            target.BuffDict.Remove(key);
                            break;
                        }
                }
            }
            Random r = new Random();
            float rate = r.Next(1, 100) / 100f;
            if (rate < Value_3)
            {
                bool isRemoveBuff = false;
                foreach (BuffCode key in other_buffList)
                {
                    if (other.BuffDict.ContainsKey(key))
                        if (other.BuffDict[key].Buff_Type == BuffType.PositiveState && other.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                        {
                            other.BuffDict.Remove(key);
                            isRemoveBuff = true;
                            break;
                        }
                }
                if (!isRemoveBuff)
                {
                    foreach (BuffCode key in target_buffList)
                    {
                        if (target.BuffDict.ContainsKey(key))
                            if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                            {
                                target.BuffDict.Remove(key);
                                break;
                            }
                    }
                }


            }
            base.MoreAssistEffect(target);
        }

    }
    //祛除自身3种弱负面效果。20%概率随机祛除敌人身上1种强正面效果或自己身上1种强负面效果。
    public class Buff_2038 : BaseBuff
    {
        public Buff_2038(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            List<Client> clientList = fightRoom.GetRoomClient();
            FightHero other = null;
            foreach (Client c in clientList)
            {
                if (c.Fight_Hero != target)
                    other = c.Fight_Hero;
            }
            List<BuffCode> other_buffList = new List<BuffCode>();
            List<BuffCode> target_buffList = new List<BuffCode>();
            other_buffList.AddRange(other.BuffDict.Keys);
            target_buffList.AddRange(target.BuffDict.Keys);
            for (int i = 0; i < Value_1; i++)
            {
                foreach (BuffCode key in target_buffList)
                {
                    if (target.BuffDict.ContainsKey(key))
                        if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type == BuffDispelType.Weak)
                        {
                            target.BuffDict.Remove(key);
                            break;
                        }
                }
            }
            Random r = new Random();
            float rate = r.Next(1, 100) / 100f;
            if (rate < Value_2)
            {
                bool isRemoveBuff = false;
                foreach (BuffCode key in other_buffList)
                {
                    if (other.BuffDict.ContainsKey(key))
                        if (other.BuffDict[key].Buff_Type == BuffType.PositiveState && other.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                        {
                            other.BuffDict.Remove(key);
                            isRemoveBuff = true;
                            break;
                        }
                }
                if (!isRemoveBuff)
                {
                    foreach (BuffCode key in target_buffList)
                    {
                        if (target.BuffDict.ContainsKey(key))
                            if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                            {
                                target.BuffDict.Remove(key);
                                break;
                            }
                    }
                }
                base.OnceAssistEffect(target);
            }
        }
    }
    //祛除自身所有弱负面效果。随机祛除敌人身上1种强正面效果或自己身上一种强负面效果。
    public class Buff_2039 : BaseBuff
    {
        public Buff_2039(BaseBuff baseBuff) : base(baseBuff) { }
        public override void OnceAssistEffect(FightHero target)
        {
            List<Client> clientList = fightRoom.GetRoomClient();
            FightHero other = null;
            foreach (Client c in clientList)
            {
                if (c.Fight_Hero != target)
                    other = c.Fight_Hero;
            }
            List<BuffCode> other_buffList = new List<BuffCode>();
            List<BuffCode> target_buffList = new List<BuffCode>();
            other_buffList.AddRange(other.BuffDict.Keys);
            target_buffList.AddRange(target.BuffDict.Keys);
            foreach (BuffCode key in target_buffList)
            {
                if (target.BuffDict.ContainsKey(key))
                    if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type == BuffDispelType.Weak)
                    {
                        target.BuffDict.Remove(key);
                    }
            }
            bool isRemoveBuff = false;
            foreach (BuffCode key in other_buffList)
            {
                if (other.BuffDict.ContainsKey(key))
                    if (other.BuffDict[key].Buff_Type == BuffType.PositiveState && other.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                    {
                        other.BuffDict.Remove(key);
                        isRemoveBuff = true;
                        break;
                    }
            }
            if (!isRemoveBuff)
            {
                foreach (BuffCode key in target_buffList)
                {
                    if (target.BuffDict.ContainsKey(key))
                        if (target.BuffDict[key].Buff_Type == BuffType.NegativeState && target.BuffDict[key].Buff_Dispel_Type != BuffDispelType.Disable)
                        {
                            target.BuffDict.Remove(key);
                            break;
                        }
                }
            }
            base.OnceAssistEffect(target);
            base.OnceAssistEffect(target);

        }
    }
    //20S内，造成最终伤害提升20%，受到最终伤害提升10%
    public class Buff_2040 : BaseBuff
    {
        public Buff_2040(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            float v2 = Value_2;
            if (BuffSource == target)
                v2 = fightRoom.EffectToSelfCaculation(Value_2, Element_Type, BuffSource);
            else
                v2 = fightRoom.EffectToEnemyCaculation(Value_2, Element_Type, BuffSource, target);
            target.Final_Damage += v * Tier;
            target.Final_Defense -= v2 * Tier;
            base.MoreAssistEffect(target);
        }
    }
    //敌人使用手牌时，赋予一层标记，持续时间10S，标记达到3层时赋予自己受到10S的易伤，敌人获得10S的强化。
    public class Buff_2041 : BaseBuff
    {
        public Buff_2041(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            if (Tier >= Value_1)
            {
                Dictionary<BuffCode, BaseBuff> buffInfoDict = target.User_Client.BaseInfoDict;
                fightRoom.SetCardEffectBuff(BuffSource, target, buffInfoDict[BuffCode.EasyToHurt], 1, BuffTarget.Other);
                fightRoom.SetCardEffectBuff(BuffSource, target, buffInfoDict[BuffCode.Strengthen], 1, BuffTarget.Self);
                DurationTime = 0;
                Tier = 0;
            }
            base.MoreAssistEffect(target);
        }
    }
    //10S后，印记会触发，造成（1加上在印记激活期间所造成的所有伤害总和的45%）风元素伤害。
    public class Buff_2042 : BaseBuff
    {
        public Buff_2042(BaseBuff baseBuff) : base(baseBuff) { }
        private float count = 0;
        public override void OnceAssistEffect(FightHero target)
        {
            count = target.Hp;
            base.OnceAssistEffect(target);
        }
        public override void LastDamageEffect(FightHero target)
        {
            Console.WriteLine("劫");
            float damage = (Value_1 + count - target.Hp) * Value_2;
            if (damage <= 0)
                damage = 1;
            fightRoom.TakeDamage(damage, DamageResource.CollateralDamage, DamageType.Element, ElementType.Wind, BuffSource, target);
            base.LastDamageEffect(target);
        }
    }
    //禁止使用某个元素牌
    public class Buff_2043 : BaseBuff
    {
        public Buff_2043(BaseBuff baseBuff) : base(baseBuff) { }
    }
    //禁止使用主动技能
    public class Buff_2044 : BaseBuff
    {
        public Buff_2044(BaseBuff baseBuff) : base(baseBuff) { }
    }
    //禁止使用被动技能
    public class Buff_2045 : BaseBuff
    {
        public Buff_2045(BaseBuff baseBuff) : base(baseBuff) { }
    }
    //伤害穿透+80%。持续30秒。
    public class Buff_2046 : BaseBuff
    {
        public Buff_2046(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Pass_through += v;
            base.MoreAssistEffect(target);
        }
    }
    //增加80%速度和暗元素强度。持续40秒。
    public class Buff_2047 : BaseBuff
    {
        public Buff_2047(BaseBuff baseBuff) : base(baseBuff) { }
        public override void MoreAssistEffect(FightHero target)
        {
            float v = Value_1;
            if (BuffSource == target)
                v = fightRoom.EffectToSelfCaculation(Value_1, Element_Type, BuffSource);
            else
                v = fightRoom.EffectToEnemyCaculation(Value_1, Element_Type, BuffSource, target);
            target.Speed *= (1 + v);
            target.Dark *= (1 + v);
            base.MoreAssistEffect(target);
        }
    }


}
