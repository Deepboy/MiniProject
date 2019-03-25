using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using SpecialBuffManager;
using System;
using Common;

namespace NewFightConfig
{
    #region DataConfig

    /// <summary>
    /// excel数据读入字典
    /// </summary>
    public static class ExcelDictionaryDefine
    {
        //卡牌组合技能字典
        public static Dictionary<string, SpecialBuff> CardGroupDict;
        //buff字典
        public static Dictionary<string, Buff> BuffDict;
        //asset文件内存模型
        private static SpecialBuffHolder specialBuffHolder;

        static ExcelDictionaryDefine()
        {
            CardGroupDict = new Dictionary<string, SpecialBuff>();
            BuffDict = new Dictionary<string, Buff>();
            SpecialBuffHolder specialBuffHolder = Resources.Load<SpecialBuffHolder>("SpecialBuff");

            foreach (SpecialBuff specialBuff in specialBuffHolder.SpecialBuffs)
            {
                CardGroupDict.Add(specialBuff.ID, specialBuff);
            }

            foreach (Buff buff in specialBuffHolder.buffs)
            {
                BuffDict.Add(buff.BuffEngName, buff);
                if (buff.BuffEngName == null || buff.BuffEngName == "")
                {
                    break;
                }
            }
        }

    }

    /// <summary>
    /// 飘字类型
    /// </summary>
    //public enum WordType : int
    //{
    //    Normal = 0,
    //    RealDamage = 1,
    //    CritDamage = 2,
    //    Dodge = 3,
    //    Recover = 4,
    //    SuckBlood = 5
    //}

    /// <summary>
    /// 伤害来源（直接/间接）
    /// </summary>
    //public enum DamageResource : int
    //{
    //    //直接伤害
    //    DirectDamage = 0,
    //    //间接伤害
    //    CollateralDamage = 1,
    //}

    /// <summary>
    /// 伤害类型（普通/元素/真实）
    /// </summary>
    //public enum DamageType : int
    //{
    //    //普通
    //    Normal = 0,
    //    //元素
    //    Element = 1,
    //    //真实
    //    Real = 2
    //}

    /// <summary>
    /// 用于记录历史伤害
    /// </summary>
    public class DamageRecord
    {
        //伤害来源（直接/间接）
        public DamageResource damageResource;
        //伤害类型（普通/元素/真实）
        public DamageType damageType;
        //伤害元素类型
        public ElementType elementType;
        //伤害数值
        public float damageValue;
    }

    /// <summary>
    /// buff状态（正面/负面）
    /// </summary>
    public enum BuffType : int
    {
        PositiveState = 0,
        NegativeState = 1
    }

    /// <summary>
    /// buff标记（普通/高级/领域/技能）
    /// </summary>
    public enum BuffFlag : int
    {
        Normal = 0,
        Senior = 1,
        Field = 2,
        Skill = 3
    }

    /// <summary>
    /// buff作用类型（伤害/辅助）
    /// </summary>
    public enum BuffEffectType : int
    {
        Damage = 0,
        Assit = 1
    }

    /// <summary>
    /// buff驱散类型（强驱散，弱驱散和不可驱散）
    /// </summary>
    public enum BuffDispelType : int
    {
        //强驱散
        Strong = 0,
        //弱驱散
        Weak = 1,
        //不可驱散
        Disable = 2
    }

    /// <summary>
    /// buff来源
    /// </summary>
    public enum BuffSource : int
    {
        Myself = 0,
        Enemy = 1
    }

    /// <summary>
    /// buff类
    /// </summary>
    public enum BuffClass : int
    {
        //随机
        Random = -1,
        //强化
        Strengthen = 0,
        //减伤
        DamageDecrement = 1,
        //魔免
        MagicImmune = 2,
        //无敌
        Invincible = 3,
        //加速
        SpeedUp = 4,
        //伤害抵抗
        DamageParry = 5,
        //防御元素强度增加
        ElementResistIncrement = 6,
        //伤害增加
        DamageIncrement = 7,
        //弱化
        Weaken = 8,
        //易伤
        EasyToHurt = 9,
        //燃烧
        Burn = 10,
        //麻痹
        Numb = 11,
        //减速
        SpeedCut = 12,
        //吸血
        SuckBlood = 13,
        //真实伤害
        RealDamage = 14,
        //闪避
        Dodge = 15,
        //侵蚀
        Erosion = 16,
        //风沙
        Sand = 17,
        //恢复
        Recover = 18,
        //穿透
        Pierce = 19,
        //微风
        Breeze = 20,
        //风刃
        WindBlade = 21,
        //烈破，狂战士之血
        BerserkersBlood = 22,
        //彼岸花开，风吹雪
        SnowDrift = 23,
        //劫，由恭候黄泉路产生
        Zed = 24,
        //是否可以出某张元素牌
        CanDrawElemCard = 25,
        //是否可以使用主动技能
        CanUseActiveSkill = 26,
        //是否可以使用被动技能
        CanUsePassiveSkill = 27,
        //暴烈火焰
        BlazingFire = 28,
        //不灭心炎
        Amaterasu = 29,
        //冰霜之力
        FrostPower = 30,
        //冰霜领域
        FrostField = 31,
        //圣盾庇护
        HolyShieldShelter = 32,
        //大地领域
        EarthFiled = 33,
        //疾风之力
        Hurricane = 34,
        //风之领域
        WindyFiled = 35,
        //邪恶之触 
        EvilTouch = 36,
        //暗夜领域
        DarkFiled = 37,
        //雷霆之怒
        ThunderWrath = 38,
        //雷霆领域
        ThunderField = 39,
        //驱散
        Disperse = 40,
        //神圣驱散
        HolyDispel = 41,
        //毒
        Poison = 42,
        //诅咒
        Curse = 43,
        //护盾
        Shield = 44,
        //祈灵术
        SpiritOfPrayer = 45,
        //变身
        Translation = 46,
        //暗夜降临
        DarknessFalls = 47
    }

    /// <summary>
    /// 战斗角色属性最大值限定
    /// </summary>
    public enum FightHeroMaxDefine : int
    {
        //最大生命
        MaxHp = 9999,
        //最大速度
        MaxSpeed = 1000,
        //最大防御
        MaxDamageResist = 200,
        //最大格挡
        MaxDamageParry = 100,

        //最大水元素强度
        MaxWaterStrength = 999,
        //最大火元素强度
        MaxFireStrength = 999,
        //最大雷元素强度
        MaxThunderStrenght = 999,
        //最大风元素强度
        MaxWindStrength = 999,
        //最大土元素强度
        MaxEarthStrength = 999,
        //最大暗元素强度
        MaxDarkStrength = 999,

        //最大水元素抵抗系数
        MaxWaterResistCoeficient = 100,
        //最大火元素抵抗系数
        MaxFireResistCoeficient = 100,
        //最大雷元素抵抗系数
        MaxThunderResistCoeficient = 100,
        //最大风元素抵抗系数
        MaxWindResistCoeficient = 100,
        //最大土元素抵抗系数
        MaxEarthResistCoeficient = 100,
        //最大暗元素抵抗系数
        MaxDarkResistCoeficient = 100,
        //最大非元素抵抗系数
        MaxChaosResistCoeficient = 100,
        //最大全元素抵抗系数
        MaxElemResistCoeficient = 100,

        //最大水元素增伤系数
        MaxWaterDamageCoeficient = 3,
        //最大火元素增伤系数
        MaxFireDamageCoeficient = 3,
        //最大雷元素增伤系数
        MaxThunderDamageCoeficient = 3,
        //最大风元素增伤系数
        MaxWindDamageCoeficient = 3,
        //最大土元素增伤系数
        MaxEarthDamageCoeficient = 3,
        //最大暗元素增伤系数
        MaxDarkDamageCoeficient = 3,
        //最大非元素增伤系数
        MaxChaosDamageCoeficient = 3,
        //最大全元素增伤系数
        MaxElemDamageCoeficient = 3,

        //最大水元素抽卡概率
        MaxWaterCardPerent = 1,
        //最大火元素抽卡概率
        MaxFireCardPerent = 1,
        //最大雷元素抽卡概率
        MaxThunderCardPerent = 1,
        //最大风元素抽卡概率
        MaxWindCardPerent = 1,
        //最大地元素抽卡概率
        MaxEarthCardPerent = 1,
        //最大暗元素抽卡概率
        MaxDarkCardPerent = 1,

        //最大闪避率
        MaxDodgeCoeficient = 1,
        //最大暴击率
        MaxCriticalCoeficient = 1,
        //最大暴击倍数
        MaxCritMutipleCoeficient = 3,
        //最大穿透系数
        MaxDamagePierceCoeficient = 1,
        //最大增伤数
        MaxDamageIncrementValue = 999,
        //最大吸血系数
        MaxSuckBloodCoeficient = 1,

        //最小主动技能冷却时间
        MinActiveSkillCoolingTime = 0,
        //最小被动技能冷却时间
        MinPassiveSkillCoolingTime = 0,

        //最大伤害
        MaxDamage = 9999

    }

    #endregion

    /// <summary>
    /// 卡牌类
    /// </summary>
    public class Card
    {
        #region field

        private ElementType elementType;

        #endregion

        #region attribute

        public ElementType ElementType
        {
            get
            {
                return elementType;
            }

            set
            {
                elementType = value;
            }
        }

        #endregion

        public Card(ElementType type)
        {
            elementType = type;
        }

    }

    #region Buff

    /// <summary>
    /// 基础Buff类
    /// 属性： 1.buff名称  2.buff描述  3.buff来源  4.buff层数
    ///        5.buff持续时间  6.buff的元素类型  7.buff的效果类型
    ///        8.buff类型  9.buff驱散类型  10.buff下次生效时间
    /// 
    /// 所有的基础buff类继承BaseBuff，通过多态来实现不同buff的计算
    /// </summary>
    public abstract class BaseBuff
    {
        #region Field

        //buff的名称
        private string buffName;
        //buff的描述
        private string buffDescription;
        //buff的来源
        private BuffSource buffSource;
        //buff的层数
        private int tier;
        //buff的持续时间
        private float durationTime;
        //buff元素类型
        private ElementType elementType;
        //buff的效果类型
        private BuffType buffType;
        //buff的标记
        private BuffFlag buffFlag;
        //buff的作用类型
        private BuffEffectType buffEffectType;
        //buff的驱散类型
        private BuffDispelType buffDispelType;
        //buff的下次生效时间
        private float nextActionTime;

        #endregion

        #region Attribute

        public string BuffName
        {
            get
            {
                return buffName;
            }

            set
            {
                buffName = value;
            }
        }

        public string BuffDescription
        {
            get
            {
                return buffDescription;
            }

            set
            {
                buffDescription = value;
            }
        }

        public BuffSource BuffSource
        {
            get
            {
                return buffSource;
            }
        }

        public int Tier
        {
            get
            {
                return tier;
            }

            set
            {
                tier = value;
            }
        }

        public float DurationTime
        {
            get
            {
                return durationTime;
            }

            set
            {
                durationTime = value;
            }
        }

        public ElementType ElementType
        {
            get
            {
                return elementType;
            }

            set
            {
                elementType = value;
            }
        }

        public BuffType BuffType
        {
            get
            {
                return buffType;
            }
        }

        public BuffFlag BuffFlag
        {
            get
            {
                return buffFlag;
            }
        }

        public BuffEffectType BuffEffectType
        {
            get
            {
                return buffEffectType;
            }
        }

        public BuffDispelType BuffDispelType
        {
            get
            {
                return buffDispelType;
            }
        }

        public float NextActionTime
        {
            get
            {
                return nextActionTime;
            }

            set
            {
                nextActionTime = value;
            }
        }

        #endregion

        public BaseBuff(BuffSource buffSource, BuffClass buffClass, BuffType buffType, BuffFlag buffFlag,
                        BuffEffectType buffEffectType, BuffDispelType buffDispelType, int cardCount)
        {
            this.buffName = "unknown";
            this.buffDescription = "unknown";
            this.buffSource = buffSource;
            tier = cardCount;
            if (tier < 1)
            {
                tier = 1;
            }
            this.buffType = buffType;
            this.buffFlag = buffFlag;
            this.buffEffectType = buffEffectType;
            this.buffDispelType = buffDispelType;
        }

        //buff更新效果
        public virtual void UpdateBuff(int cardCount = 1) {; }

        //buff在创建时产生的效果
        public virtual void ActionAtCreation(FightHero attacker, FightHero defender) {; }

        //buff在销毁产生的效果

        public virtual void ActionAtDestroy(FightHero attacker, FightHero defender) {; }

        //buff在帧开始时产生的效果
        public virtual void ActionAtBeginning(FightHero attacker, FightHero defender) {; }

        //buff在帧循环中产生的效果
        public virtual void ActionAtLoop(FightHero attacker, FightHero defender) {; }

        //buff在出牌前产生的效果
        public virtual void ActionBeforeDrawCard(FightHero attacker, FightHero defender) {; }

        //buff在出牌后产生的效果
        public virtual void ActionAfterDrawCard(FightHero attacker, FightHero defender) {; }

        //buff在伤害结算前产生的效果
        public virtual void ActionBeforeDamage(FightHero attacker, FightHero defender) {; }

        //buff在伤害结算后产生的效果
        public virtual void ActionAfterDamage(FightHero attacker, FightHero defender) {; }

    }

    /// <summary>
    /// 强化buff类
    /// 影响角色字段：ElemDamageCoefficient
    /// </summary>
    public class StrengthenBuff : BaseBuff
    {
        public StrengthenBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Strengthen,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Strengthen"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Strengthen"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Strengthen"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Strengthen"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Strengthen"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Strengthen"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Strengthen"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ElemDamageCoefficientDeviant +=
            FightCtrl.AllCaculation.EffectToEnemyCaculation(float.Parse(ExcelDictionaryDefine.BuffDict["Strengthen"].BuffValue1) * Tier, base.ElementType, attacker, defender);
        }

    }

    /// <summary>
    /// 减伤buff类
    /// 影响角色字段：ChaosDamageResistCoefficient
    /// </summary>
    public class DamageDecrementBuff : BaseBuff
    {
        public DamageDecrementBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.DamageDecrement,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["DamageDecrement"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ChaosDamageResistCoefficientDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation(float.Parse(ExcelDictionaryDefine.BuffDict["DamageDecrement"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 魔免buff类
    /// 影响角色字段：IsMagicImmune
    /// 魔免将使得不受任何buff影响
    /// </summary>
    public class MagicImmuneBuff : BaseBuff
    {

        public MagicImmuneBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.MagicImmune,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["MagicImmune"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["MagicImmune"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.ClearAllNegativeBuff(attacker, defender);
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.IsMagicImmuneDeviant = true;
        }

    }

    /// <summary>
    /// 无敌buff类
    /// 影响角色字段：IsInvinsible
    /// 无敌将使得不受任何伤害结算
    /// </summary>
    public class InvincibleBuff : BaseBuff
    {

        public InvincibleBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Invincible,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Invincible"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Invincible"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Invincible"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Invincible"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Invincible"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Invincible"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Invincible"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Invincible"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.ClearAllNegativeBuff(attacker, defender);
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.IsInvinsibleDeviant = true;
        }

    }

    /// <summary>
    /// 加速buff类
    /// 影响角色字段：CurSpeed
    /// </summary>
    public class SpeedUpBuff : BaseBuff
    {
        public SpeedUpBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.SpeedUp,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["SpeedUp"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SpeedDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (defender.OriginalSpeed * float.Parse(ExcelDictionaryDefine.BuffDict["SpeedUp"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 防御元素强度增加buff类
    /// 影响角色字段：CurDamageResist
    /// </summary>
    public class ElementResistIncrementBuff : BaseBuff
    {
        public ElementResistIncrementBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.ElementResistIncrement,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamageResistDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["ElementResistIncrement"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 格挡buff类
    /// 影响角色字段：DamageParry
    /// </summary>
    public class DamageParryBuff : BaseBuff
    {
        public DamageParryBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.DamageParry,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["DamageParry"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["DamageParry"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["DamageParry"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["DamageParry"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["DamageParry"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["DamageParry"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["DamageParry"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamageParryDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["DamageParry"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 加伤buff类
    /// 影响角色字段：DamageIncrementValue
    /// </summary>
    public class DamageIncrementBuff : BaseBuff
    {
        public DamageIncrementBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.DamageIncrement,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["DamageIncrement"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamageIncrementValueDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["DamageIncrement"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 弱化buff类
    /// 影响角色字段：ChaosDamageResistCoefficient
    /// </summary>
    public class WeakenBuff : BaseBuff
    {
        public WeakenBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Weaken,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Weaken"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Weaken"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Weaken"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Weaken"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Weaken"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Weaken"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Weaken"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ChaosDamageResistCoefficientDeviant -=
                FightCtrl.AllCaculation.EffectToEnemyCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Weaken"].BuffValue1) * Tier, base.ElementType, attacker, defender);
        }

    }

    /// <summary>
    /// 易伤buff类
    /// 影响角色字段：ElemResistCoefficient
    /// </summary>
    public class EasyToHurtBuff : BaseBuff
    {
        public EasyToHurtBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.EasyToHurt,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["EasyToHurt"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ElemResistCoefficientDeviant -=
                FightCtrl.AllCaculation.EffectToEnemyCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["EasyToHurt"].BuffValue1) * Tier, base.ElementType, attacker, defender);
        }

    }

    /// <summary>
    /// 灼烧buff类
    /// 属性：1.灼烧值
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class BurnBuff : BaseBuff
    {
        public BurnBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Burn,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Burn"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Burn"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Burn"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Burn"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Burn"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Burn"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Burn"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Burn"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Burn"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 麻痹buff类
    /// 属性：1.麻痹系数
    /// 影响角色字段： 1.CurSliderValue  2.CurApPoint
    /// </summary>
    public class NumbBuff : BaseBuff
    {
        //麻痹系数
        private float numbCoefficient = 0;

        public NumbBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Numb,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Numb"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Numb"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Numb"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Numb"].BuffDispelType),
                  cardCount)
        {
            numbCoefficient = float.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffValue1);
            BuffName = ExcelDictionaryDefine.BuffDict["Numb"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Numb"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Numb"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Numb"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            numbCoefficient = FightCtrl.AllCaculation.EffectToEnemyCaculation(numbCoefficient * Tier, base.ElementType, attacker, defender);
            while (numbCoefficient >= 1 && defender.CurApPoint > 0)
            {
                defender.CurApPoint -= 1;
                numbCoefficient -= 1;
            }
            if(numbCoefficient >= 1)
            {
                defender.CurApPoint = 0;
                defender.CurSliderValue = 0;
                return;
            }
            if (defender.CurSliderValue < numbCoefficient)
            {
                if (defender.CurApPoint <= 0)
                {
                    defender.CurSliderValue = 0;
                    return;
                }
                defender.CurApPoint -= 1;
                numbCoefficient = numbCoefficient - defender.CurSliderValue;
                defender.CurSliderValue = 0;
                defender.CurSliderValue += 1 - numbCoefficient;
                return;
            }
            defender.CurSliderValue -= numbCoefficient;
        }

    }

    /// <summary>
    /// 减速buff类
    /// 影响角色字段：CurSpeed
    /// </summary>
    public class SpeedCutBuff : BaseBuff
    {
        public SpeedCutBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.SpeedCut,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["SpeedCut"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SpeedDeviant -=
                FightCtrl.AllCaculation.EffectToEnemyCaculation
                (defender.OriginalSpeed * float.Parse(ExcelDictionaryDefine.BuffDict["SpeedCut"].BuffValue1) * Tier, base.ElementType, attacker, defender);
        }

    }

    /// <summary>
    /// 吸血buff
    /// 影响角色字段：SuckBloodCoefficient
    /// </summary>
    public class SuckBloodBuff : BaseBuff
    {
        public SuckBloodBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.SuckBlood,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["SuckBlood"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SuckBloodCoefficientDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation(float.Parse(ExcelDictionaryDefine.BuffDict["SuckBlood"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 真实伤害buff
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class RealDamageBuff : BaseBuff
    {
        public RealDamageBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.RealDamage,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["RealDamage"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["RealDamage"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["RealDamage"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["RealDamage"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["RealDamage"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["RealDamage"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["RealDamage"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["RealDamage"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["RealDamage"].BuffDamageType),
                DamageType.Real);
        }
    }

    /// <summary>
    /// 闪避buff类
    /// 影响角色字段：CurSpeed
    /// </summary>
    public class DodgeBuff : BaseBuff
    {
        public DodgeBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Dodge,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Dodge"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Dodge"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Dodge"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Dodge"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Dodge"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Dodge"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Dodge"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DodgeCoefficientDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Dodge"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 侵蚀buff类
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class ErosionBuff : BaseBuff
    {
        public ErosionBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Erosion,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Erosion"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Erosion"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Erosion"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Erosion"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Erosion"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Erosion"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Erosion"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Erosion"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Erosion"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 风沙buff类
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class SandBuff : BaseBuff
    {
        public SandBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Sand,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Sand"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Sand"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Sand"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Sand"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Sand"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Sand"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Sand"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Sand"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Sand"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 恢复buff类
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class RecoverBuff : BaseBuff
    {
        public RecoverBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Recover,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Recover"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Recover"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Recover"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Recover"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Recover"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Recover"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Recover"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            float value = FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Recover"].BuffValue1) * Tier, base.ElementType, defender);
            value = Mathf.Floor(value);
            FightCtrl.ShowWord(defender, value, WordType.Recover);
            defender.CurHp += value;
        }

    }

    /// <summary>
    /// 穿透buff
    /// 属性： 1.穿透系数  
    /// 影响角色字段：DamagePierceCoefficient
    /// buff消失时会恢复原数值
    /// </summary>
    public class PierceBuff : BaseBuff
    {
        public PierceBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Pierce,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Pierce"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Pierce"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Pierce"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Pierce"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Pierce"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Pierce"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Pierce"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamagePierceCoefficientDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Pierce"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 微风buff类
    /// 影响角色字段：CurSpeed
    /// </summary>
    public class BreezeBuff : BaseBuff
    {
        public BreezeBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Breeze,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Breeze"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Breeze"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Breeze"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Breeze"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Breeze"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Breeze"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Breeze"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SpeedDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation
                (defender.OriginalSpeed * float.Parse(ExcelDictionaryDefine.BuffDict["Breeze"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 风暴buff类
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class WindBladeBuff : BaseBuff
    {
        public WindBladeBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.WindBlade,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["WindBlade"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["WindBlade"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["WindBlade"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["WindBlade"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["WindBlade"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["WindBlade"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["WindBlade"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["WindBlade"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["WindBlade"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 毒buff类
    /// 影响角色字段：CurHp
    /// 持续性效果
    /// </summary>
    public class PoisonBuff : BaseBuff
    {
        public PoisonBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Erosion,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Poison"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Poison"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Poison"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Poison"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Poison"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Poison"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Poison"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Poison"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Poison"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 狂战士之血buff类，由烈破产生，主动技能
    /// 影响角色字段：1.ChaosDamageCoefficient
    /// </summary>
    public class BerserkersBloodBuff : BaseBuff
    {
        public BerserkersBloodBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.BerserkersBlood,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["BerserkersBlood"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ChaosDamageCoefficientDeviant +=
                FightCtrl.AllCaculation.EffectToSelfCaculation(float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffValue1) * Tier, base.ElementType, defender);
            defender.ChaosDamageResistCoefficientDeviant -=
               FightCtrl.AllCaculation.EffectToSelfCaculation(float.Parse(ExcelDictionaryDefine.BuffDict["BerserkersBlood"].BuffValue2) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 风吹雪buff类，由彼岸花开产生，第一个被动技能
    /// </summary>
    public class SnowDriftBuff : BaseBuff
    {
        public SnowDriftBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.SnowDrift,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["SnowDrift"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            if (Tier >= int.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SnowDrift"].BuffMaxTier);
                BuffManager.AddAndUpdateBuff(defender, attacker, BuffClass.Strengthen);
                BuffManager.AddAndUpdateBuff(attacker, defender, BuffClass.Weaken);
                DurationTime = 0;
            }
        }

    }

    /// <summary>
    /// 劫buff类，由恭候黄泉路产生，主动技能
    /// 属性： 1.累计伤害值  
    /// </summary>
    public class ZedBuff : BaseBuff
    {
        #region field

        //累计伤害值
        private float damageRecord;

        #endregion

        #region attribute

        public float DamageRecord
        {
            get
            {
                return damageRecord;
            }
        }

        #endregion

        public ZedBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.Zed,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Zed"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Zed"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Zed"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Zed"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Zed"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Zed"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Zed"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Zed"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Zed"].BuffActionTime);
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            damageRecord = defender.TotalRecivedDamage;
        }

        public override void ActionAtDestroy(FightHero attacker, FightHero defender)
        {
            float damage = defender.TotalRecivedDamage - damageRecord;
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Zed"].BuffValue1) + damage * float.Parse(ExcelDictionaryDefine.BuffDict["Zed"].BuffValue2),
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Zed"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 全领域沉默，禁止使用某个元素牌
    /// </summary>
    public class CanDrawElemCardBuff : BaseBuff
    {
        public CanDrawElemCardBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.CanDrawElemCard,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffDispelType),
                  cardCount)
        {
            string elem;
            ElementType elementType = new ElementType();
            int rand = UnityEngine.Random.Range(1, 7);
            switch (rand)
            {
                case 1:
                    elementType = ElementType.Water;
                    break;
                case 2:
                    elementType = ElementType.Fire;
                    break;
                case 3:
                    elementType = ElementType.Thunder;
                    break;
                case 4:
                    elementType = ElementType.Wind;
                    break;
                case 5:
                    elementType = ElementType.Earth;
                    break;
                case 6:
                    elementType = ElementType.Dark;
                    break;
            }
            ElementType = elementType;
            switch (ElementType)
            {
                case ElementType.Water:
                    elem = "水";
                    break;
                case ElementType.Fire:
                    elem = "火";
                    break;
                case ElementType.Thunder:
                    elem = "雷";
                    break;
                case ElementType.Wind:
                    elem = "风";
                    break;
                case ElementType.Earth:
                    elem = "地";
                    break;
                case ElementType.Dark:
                    elem = "暗";
                    break;
                default:
                    elem = "混沌";
                    break;
            }
            BuffName = ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffDes + "（" + elem + "）";
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanDrawElemCard"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            switch (base.ElementType)
            {
                case ElementType.Water:
                    defender.CanDrawWaterCardDeviant = false;
                    break;
                case ElementType.Fire:
                    defender.CanDrawFireCardDeviant = false;
                    break;
                case ElementType.Thunder:
                    defender.CanDrawThunderCardDeviant = false;
                    break;
                case ElementType.Wind:
                    defender.CanDrawWindCardDeviant = false;
                    break;
                case ElementType.Earth:
                    defender.CanDrawEarthCardDeviant = false;
                    break;
                case ElementType.Dark:
                    defender.CanDrawDarkCardDeviant = false;
                    break;
                default:
                    break;
            }
        }

    }

    /// <summary>
    /// 奥术诅咒，禁止使用主动技能
    /// </summary>
    public class CanUseActiveSkillBuff : BaseBuff
    {
        public CanUseActiveSkillBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.CanUseActiveSkill,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanUseActiveSkill"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.heroCard.Action_skill.Skill_enable = false;
            defender.IsActiveSkill = false;
            //FightCtrl.playerActionSkill.GetComponent<Button>().interactable = false;
        }

        public override void ActionAtDestroy(FightHero attacker, FightHero defender)
        {
            defender.heroCard.Action_skill.Skill_enable = true;
            defender.IsActiveSkill = true;
            //FightCtrl.playerActionSkill.GetComponent<Button>().interactable = true;
        }

    }

    /// <summary>
    /// 幽冥剧毒，禁止使用被动技能
    /// </summary>
    public class CanUsePassiveSkillBuff : BaseBuff
    {
        public CanUsePassiveSkillBuff(BuffSource buffSource, int cardCount = 1)
            : base(buffSource, BuffClass.CanUsePassiveSkill,
                  (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffFlag),
                  (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffEffectType),
                  (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffDispelType),
                  cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["CanUsePassiveSkill"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.heroCard.Prossive_skill_1.Skill_enable = false;
            defender.IsFirstPassiveSkill = false;
            //FightCtrl.playerFirstPassiveSkill.GetComponent<Button>().interactable = false;
            defender.heroCard.Prossive_skill_2.Skill_enable = false;
            defender.IsSecondPassiveSkill = false;
            //FightCtrl.playerSecondPassiveSkill.GetComponent<Button>().interactable = false;
            defender.heroCard.Prossive_skill_3.Skill_enable = false;
            defender.IsThirdPassiveSkill = false;
            //FightCtrl.playerThirdPassiveSkill.GetComponent<Button>().interactable = false;
        }

        public override void ActionAtDestroy(FightHero attacker, FightHero defender)
        {
            defender.heroCard.Prossive_skill_1.Skill_enable = true;
            defender.IsFirstPassiveSkill = true;
            //FightCtrl.playerFirstPassiveSkill.GetComponent<Button>().interactable = true;
            defender.heroCard.Prossive_skill_2.Skill_enable = true;
            defender.IsSecondPassiveSkill = true;
            //FightCtrl.playerSecondPassiveSkill.GetComponent<Button>().interactable = true;
            defender.heroCard.Prossive_skill_3.Skill_enable = true;
            defender.IsThirdPassiveSkill = true;
            //FightCtrl.playerThirdPassiveSkill.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// 暴烈火焰
    /// </summary> 
    public class BlazingFireBuff : BaseBuff
    {
        public BlazingFireBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.BlazingFire,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["BlazingFire"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffValue1) * Tier,
                attacker, defender, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["BlazingFire"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 不死心炎
    /// </summary> 
    public class AmaterasuBuff : BaseBuff
    {
        public AmaterasuBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Amaterasu,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffType),
		  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Amaterasu"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.RemoveAllFieldBuff(defender, BuffClass.Amaterasu);
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            FightCtrl.AllCaculation.DamageCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffValue1) * Tier,
                defender, attacker, (DamageResource)Enum.Parse(typeof(DamageResource), ExcelDictionaryDefine.BuffDict["Amaterasu"].BuffDamageType),
                DamageType.Element, base.ElementType);
        }

    }

    /// <summary>
    /// 冰霜之力
    /// </summary> 
    public class FrostPowerBuff : BaseBuff
    {
        public FrostPowerBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.FrostPower,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["FrostPower"].BuffType),
		  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["FrostPower"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["FrostPower"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["FrostPower"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["FrostPower"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["FrostPower"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["FrostPower"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffActionTime);
            }
        }

        public override void ActionAfterDrawCard(FightHero attacker, FightHero defender)
        {
            BuffManager.AddAndUpdateBuff(defender, attacker, BuffClass.SpeedCut, int.Parse(ExcelDictionaryDefine.BuffDict["FrostPower"].BuffValue1));
        }

    }

    /// <summary>
    /// 冰霜领域
    /// </summary> 
    public class FrostFieldBuff : BaseBuff
    {
        public FrostFieldBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.FrostField,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["FrostField"].BuffType),
		  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["FrostField"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["FrostField"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["FrostField"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["FrostField"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["FrostField"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["FrostField"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.RemoveAllFieldBuff(defender, BuffClass.FrostField);
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            attacker.SpeedDeviant -=
                FightCtrl.AllCaculation.EffectToEnemyCaculation
                (attacker.OriginalSpeed * float.Parse(ExcelDictionaryDefine.BuffDict["FrostField"].BuffValue1) * Tier, base.ElementType, defender, attacker);
        }

    }

    /// <summary>
    /// 圣盾庇护
    /// </summary> 
    public class HolyShieldShelterBuff : BaseBuff
    {
        public HolyShieldShelterBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.HolyShieldShelter,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamageParryDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["HolyShieldShelter"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 大地领域
    /// </summary> 
    public class EarthFiledBuff : BaseBuff
    {
        public EarthFiledBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.EarthFiled,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["EarthFiled"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
             BuffManager.RemoveAllFieldBuff(defender, BuffClass.EarthFiled);
        }

        public override void ActionAfterDamage(FightHero attacker, FightHero defender)
        {
            if(defender.CurApPoint >= int.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffValue1))
            {
                if(defender.CurHp <= 0)
                {
                    defender.CurApPoint -= int.Parse(ExcelDictionaryDefine.BuffDict["EarthFiled"].BuffValue1);
                    defender.CurHp = defender.MaxHp;
                    DurationTime = 0;
                }
            }
        }

    }

    /// <summary>
    /// 疾风之力
    /// </summary> 
    public class HurricaneBuff : BaseBuff
    {
        #region field

        private int count;

        #endregion

        #region attribute

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
                if (count < 0)
                {
                    count = 0;
                }
            }
        }

        #endregion

        public HurricaneBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Hurricane,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Hurricane"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Hurricane"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Hurricane"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Hurricane"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Hurricane"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Hurricane"].BuffDes;
            Count = 0;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Hurricane"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DodgeCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue1) * Tier, base.ElementType, defender);
            if(defender.DodgeCoefficientDeviant > float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue3))
            {
                defender.DodgeCoefficientDeviant = float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue3);
            }
            defender.CriticalCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue2) * Tier, base.ElementType, defender);
            if(defender.CriticalCoefficientDeviant > float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue4))
            {
                defender.CriticalCoefficientDeviant = float.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffValue4);
            }
        }

        public override void ActionAfterDrawCard(FightHero attacker, FightHero defender)
        {
            Count += defender.CardGroup.WindCount;
            if(defender.CardGroup.WindCount > 0)
            {
                Tier += defender.CardGroup.WindCount;
                if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffMaxTier))
                {
                    Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Hurricane"].BuffMaxTier);
                }
            }
        }

    }

    /// <summary>
    /// 风之领域
    /// </summary> 
    public class WindyFiledBuff : BaseBuff
    {
        public WindyFiledBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.WindyFiled,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["WindyFiled"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.RemoveAllFieldBuff(defender, BuffClass.WindyFiled);
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DodgeCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffValue1) * Tier, base.ElementType, defender);
            defender.CriticalCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
               (float.Parse(ExcelDictionaryDefine.BuffDict["WindyFiled"].BuffValue2) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 邪恶之触
    /// </summary> 
    public class EvilTouchBuff : BaseBuff
    {
        public EvilTouchBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.EvilTouch,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["EvilTouch"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffActionTime);
            }
        }

        public override void ActionAfterDrawCard(FightHero attacker, FightHero defender)
        {
            if(defender.CardGroup.DarkCount > 0)
            {
                BuffManager.AddAndUpdateBuff(attacker, defender, BuffClass.SuckBlood, defender.CardGroup.DarkCount * int.Parse(ExcelDictionaryDefine.BuffDict["EvilTouch"].BuffValue1));
            }
        }

    }

    /// <summary>
    /// 暗夜领域
    /// </summary> 
    public class DarkFiledBuff : BaseBuff
    {
        public DarkFiledBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.DarkFiled,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["DarkFiled"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.RemoveAllFieldBuff(defender, BuffClass.DarkFiled);
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SuckBloodCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
               (float.Parse(ExcelDictionaryDefine.BuffDict["DarkFiled"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 雷霆之怒
    /// </summary> 
    public class ThunderWrathBuff : BaseBuff
    {
        public ThunderWrathBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.ThunderWrath,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["ThunderWrath"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ThunderWrath"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.CanDrawCardDeviant = false;
        }

    }

    /// <summary>
    /// 雷霆领域
    /// </summary> 
    public class ThunderFieldBuff : BaseBuff
    {
        public ThunderFieldBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.ThunderField,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["ThunderField"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["ThunderField"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["ThunderField"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["ThunderField"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["ThunderField"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["ThunderField"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["ThunderField"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffActionTime);
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.RemoveAllFieldBuff(defender, BuffClass.ThunderField);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffActionTime);
            }
        }

        public override void ActionAtLoop(FightHero attacker, FightHero defender)
        {
            attacker.CurApPoint -= int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffValue1);
            BuffManager.ClearAllNegativeBuff(attacker, defender, int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffValue2));
            int rand = UnityEngine.Random.Range(1, 100);
            int randObject = UnityEngine.Random.Range(1, 100);
            if (rand <= 100 * float.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffValue3))
            {
                if(randObject <= 50)
                {
                    BuffManager.ClearAllStrongNegativeBuff(attacker, defender, int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffValue4));
                }
                else
                {
                    BuffManager.ClearAllStrongPositiveBuff(defender, attacker, int.Parse(ExcelDictionaryDefine.BuffDict["ThunderField"].BuffValue4));
                }
            }
        }

    }

    /// <summary>
    /// 驱散
    /// </summary> 
    public class DisperseBuff : BaseBuff
    {
        public DisperseBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Disperse,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Disperse"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Disperse"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Disperse"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Disperse"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Disperse"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Disperse"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Disperse"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.ClearAllNegativeBuff(attacker, defender, int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue1));
            int rand = UnityEngine.Random.Range(1, 100);
            int randObject = UnityEngine.Random.Range(1, 100);
            if (rand <= 100 * float.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue2))
            {
                if (randObject <= 50)
                {
                    BuffManager.ClearAllStrongNegativeBuff(attacker, defender, int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue3));
                }
                else
                {
                    BuffManager.ClearAllStrongPositiveBuff(defender, attacker, int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue3));
                }
            }
        }

    }

    /// <summary>
    /// 神圣驱散
    /// </summary> 
    public class HolyDispelBuff : BaseBuff
    {
        public HolyDispelBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.HolyDispel,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffType),
				  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["HolyDispel"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["HolyDispel"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            BuffManager.ClearAllNegativeBuff(attacker, defender);
            int randObject = UnityEngine.Random.Range(1, 100);
            if (randObject <= 50)
            {
                BuffManager.ClearAllStrongNegativeBuff(attacker, defender, int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue1));
            }
            else
            {
                BuffManager.ClearAllStrongPositiveBuff(defender, attacker, int.Parse(ExcelDictionaryDefine.BuffDict["Disperse"].BuffValue1));
            }
        }

    }

    /// <summary>
    /// 诅咒
    /// </summary>
    public class CurseBuff : BaseBuff
    {
        public CurseBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Curse,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Curse"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Curse"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Curse"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Curse"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Curse"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Curse"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Curse"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ElemResistCoefficientDeviant -=
                FightCtrl.AllCaculation.EffectToEnemyCaculation
                (float.Parse(ExcelDictionaryDefine.BuffDict["Curse"].BuffValue1) * Tier, base.ElementType, attacker, defender);
        }
    }

    /// <summary>
    /// 护盾
    /// </summary> 
    public class ShieldBuff : BaseBuff
    {
        #region field

        float shieldValue;

        #endregion

        public ShieldBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Shield,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Shield"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Shield"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Shield"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Shield"].BuffDispelType),
          cardCount)
        {
            shieldValue = 0;
            BuffName = ExcelDictionaryDefine.BuffDict["Shield"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Shield"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Shield"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffActionTime);
            shieldValue = float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffValue1) * Tier;
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                shieldValue += float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffValue1);
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Shield"].BuffActionTime);
            }
        }

        public override void ActionAtCreation(FightHero attacker, FightHero defender)
        {
            defender.MaxShield = shieldValue;
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.ShieldDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (shieldValue, base.ElementType, defender);
        }

        public override void ActionAfterDamage(FightHero attacker, FightHero defender)
        {
            shieldValue -= defender.DamageFromEnemyRecords[defender.DamageFromEnemyRecords.Count - 1].damageValue;
            Debug.Log(defender.heroCard.Hero_name);
            if(shieldValue <= 0)
            {
                shieldValue = 0;
                DurationTime = 0;
            }
        }

    }

    /// <summary>
    /// 祈灵术
    /// </summary>
    public class SpiritOfPrayerBuff : BaseBuff
    {
        public SpiritOfPrayerBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.SpiritOfPrayer,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DamagePierceCoefficientDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (float.Parse(ExcelDictionaryDefine.BuffDict["SpiritOfPrayer"].BuffValue1) * Tier, base.ElementType, defender);
        }

    }

    /// <summary>
    /// 变身
    /// </summary>
    public class TranslationBuff : BaseBuff
    {
        public TranslationBuff(BuffSource buffSource, int cardCount = 1)
                : base(buffSource, BuffClass.Translation,
          (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["Translation"].BuffType),
                  (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["Translation"].BuffFlag),
          (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["Translation"].BuffEffectType),
          (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["Translation"].BuffDispelType),
          cardCount)
        {
            BuffName = ExcelDictionaryDefine.BuffDict["Translation"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["Translation"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["Translation"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.SpeedDeviant += defender.OriginalSpeed * float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffValue1) * Tier;
            defender.DarkStrengthDeviant += defender.OriginalDarkStrength * float.Parse(ExcelDictionaryDefine.BuffDict["Translation"].BuffValue1) * Tier;
        }
    }

    /// <summary>
    /// 暗夜降临
    /// </summary>
    public class DarknessFallsBuff : BaseBuff
    {
        #region field

        int count;

        #endregion

        public DarknessFallsBuff(BuffSource buffSource, int cardCount = 1)
               : base(buffSource, BuffClass.DarknessFalls,
         (BuffType)Enum.Parse(typeof(BuffType), ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffType),
                 (BuffFlag)Enum.Parse(typeof(BuffFlag), ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffFlag),
         (BuffEffectType)Enum.Parse(typeof(BuffEffectType), ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffEffectType),
         (BuffDispelType)Enum.Parse(typeof(BuffDispelType), ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffDispelType),
         cardCount)
        {
            count = 0;
            BuffName = ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffName;
            BuffDescription = ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffDes;
            ElementType = (ElementType)Enum.Parse(typeof(ElementType), ExcelDictionaryDefine.BuffDict["DarknessFalls"].ElemType);
            DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffDuration);
            NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffActionTime);
        }

        public override void UpdateBuff(int cardCount = 1)
        {
            Tier += cardCount;
            if (Tier > int.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffMaxTier))
            {
                Tier = int.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffMaxTier);
            }
            if (cardCount >= 0)
            {
                DurationTime = float.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffDuration);
                NextActionTime = DurationTime - float.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffActionTime);
            }
        }

        public override void ActionAtBeginning(FightHero attacker, FightHero defender)
        {
            defender.DarkStrengthDeviant += FightCtrl.AllCaculation.EffectToSelfCaculation
                 (float.Parse(ExcelDictionaryDefine.BuffDict["DarknessFalls"].BuffValue1) * count, base.ElementType, defender);
        }

        public override void ActionAfterDrawCard(FightHero attacker, FightHero defender)
        {
            count += defender.CardGroup.DarkCount;
        }

    }

    #endregion


    /// <summary>
    /// BuffManager
    /// </summary>
    public static class BuffManager
    {

        /// <summary>
        /// 由attacker向defender添加或更新buff
        /// </summary>
        /// <param name="attacker">buff的进攻者</param>
        /// <param name="defender">buff的防御者</param>
        /// <param name="buffClass">buff类</param>
        /// <param name="elementType">buff元素类型</param>
        public static void AddAndUpdateBuff(FightHero attacker, FightHero defender, BuffClass buffClass, int cardCount = 1)
        {
            if (attacker == null || defender == null || cardCount < 0)
            {
                return;
            }
            //如果进攻者和防守者相同，则buff来源为Myself，否则为Enemy
            BuffSource buffSource = new BuffSource();
            if (attacker.Equals(defender))
            {
                buffSource = BuffSource.Myself;
            }
            else
            {
                buffSource = BuffSource.Enemy;
            }
            //当buff来源于Enemy并且敌人存在魔免时，不能添加或更新buff
            if (buffSource == BuffSource.Enemy && defender.CurIsMagicImmune == true)
            {
                return;

            }
            //已存在buff时，更新。不存在时为添加buff
            if (defender.BuffDictionary.ContainsKey(buffClass))
            {
                defender.BuffDictionary[buffClass].UpdateBuff(cardCount);
                //触发一次buff的唯一效果
                defender.BuffDictionary[buffClass].ActionAtCreation(attacker, defender);
                //产生buff时瞬间造成一次效果
                defender.BuffDictionary[buffClass].ActionAtLoop(attacker, defender);
            }
            else
            {
                //利用反射来构建buff类
                //获取当前程序集
                Assembly assembly = Assembly.GetExecutingAssembly();
                //参数列表，第一个为buff来源，第二个为元素类型，第三个为卡牌计数，对应buff的构造函数
                object[] parameters = new object[2];
                parameters[0] = buffSource;
                parameters[1] = cardCount;
                object obj = assembly.CreateInstance("NewFightConfig." + buffClass.ToString() + "Buff", true, System.Reflection.BindingFlags.Default, null, parameters, null, null);
                //将buff加入敌人的buff字典
                defender.BuffDictionary.Add(buffClass, obj as BaseBuff);
                //触发一次buff的唯一效果
                defender.BuffDictionary[buffClass].ActionAtCreation(attacker, defender);
                //产生buff时瞬间造成一次效果
                defender.BuffDictionary[buffClass].ActionAtLoop(attacker, defender);
                //当敌人身上有瞬间结算的buff时，负面伤害buff瞬间结算
                if (attacker.CurIsBuffInstant == true && defender.BuffDictionary[buffClass].BuffEffectType == BuffEffectType.Damage && defender.BuffDictionary[buffClass].BuffType == BuffType.NegativeState)
                {
                    //不执行最后一次
                    while (defender.BuffDictionary[buffClass].DurationTime >= 1)
                    {
                        defender.BuffDictionary[buffClass].DurationTime -= 1;
                        defender.BuffDictionary[buffClass].ActionAtLoop(attacker, defender);
                    }
                    defender.BuffDictionary[buffClass].ActionAtDestroy(attacker, defender);
                    defender.BuffDictionary.Remove(buffClass);
                    return;
                }
                //添加buff图标
                FightCtrl.AddBuffIcon(defender, buffClass);
            }
        }

        /// <summary>
        /// 更新所有的buff持续时间并触发循环效果，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        /// <param name="reduceTime">更新的时间</param>
        public static void UpdateBuffDurationTime(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                if(defender.BuffDictionary.ContainsKey(buffKey))
                {
                    defender.BuffDictionary[buffKey].DurationTime -= Time.fixedDeltaTime;
                    if (defender.BuffDictionary[buffKey].DurationTime <= defender.BuffDictionary[buffKey].NextActionTime)
                    {
                        //不执行最后一次
                        if (defender.BuffDictionary[buffKey].NextActionTime > 0)
                        {
                            defender.BuffDictionary[buffKey].ActionAtLoop(attacker, defender);
                        }
                        defender.BuffDictionary[buffKey].NextActionTime -= float.Parse(ExcelDictionaryDefine.BuffDict[buffKey.ToString()].BuffActionTime);
                    }
                    if (defender.BuffDictionary[buffKey].DurationTime <= 0)
                    {
                        defender.BuffDictionary[buffKey].ActionAtDestroy(attacker, defender);
                        //删除buff图标
                        FightCtrl.DeleteBuffIcon(defender, buffKey);
                        defender.BuffDictionary.Remove(buffKey);
                    }
                }
            }
        }

        /// <summary>
        /// 触发全部帧开始效果
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetActionAtBeginning(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                defender.BuffDictionary[buffKey].ActionAtBeginning(attacker, defender);
            }
        }

        /// <summary>
        /// 触发全部出牌前的buff
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetActionBeforeDrawCard(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                defender.BuffDictionary[buffKey].ActionBeforeDrawCard(attacker, defender);
            }
        }

        /// <summary>
        /// 触发全部出牌后的buff
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetActionAfterDrawCard(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                defender.BuffDictionary[buffKey].ActionAfterDrawCard(attacker, defender);
            }
        }

        /// <summary>
        /// 触发全部伤害结算前的buff
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetActionBeforeDamage(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                defender.BuffDictionary[buffKey].ActionBeforeDamage(attacker, defender);
            }
        }

        /// <summary>
        /// 触发全部伤害结算后的buff
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetActionAfterDamage(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                defender.BuffDictionary[buffKey].ActionAfterDamage(attacker, defender);
            }
        }

        /// <summary>
        /// 清除所有的正面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllPositiveBuff(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //正面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.PositiveState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Weak)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 清除固定数量的正面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllPositiveBuff(FightHero attacker, FightHero defender, int count)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0 || count <= 0)
            {
                return;
            }
            int amount = 0;
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //正面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.PositiveState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Weak)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                    amount++;
                    if (amount >= count)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有的强驱散正面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllStrongPositiveBuff(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //正面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.PositiveState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Strong)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 清除固定数量的强驱散正面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllStrongPositiveBuff(FightHero attacker, FightHero defender, int count)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0 || count <= 0)
            {
                return;
            }
            int amount = 0;
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //正面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.PositiveState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Strong)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                    amount++;
                    if (amount >= count)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有的负面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllNegativeBuff(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //负面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Weak)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 清除固定数量的负面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllNegativeBuff(FightHero attacker, FightHero defender, int count)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0 || count <= 0)
            {
                return;
            }
            int amount = 0;
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //负面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Weak)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                    amount++;
                    if(amount >= count)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有的强驱散负面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllStrongNegativeBuff(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //负面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Strong)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 清除固定数量的强驱散负面buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void ClearAllStrongNegativeBuff(FightHero attacker, FightHero defender, int count)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0 || count <= 0)
            {
                return;
            }
            int amount = 0;
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //负面状态并且是弱驱散
                if (defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState && defender.BuffDictionary[buffKey].BuffDispelType == BuffDispelType.Strong)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                    amount++;
                    if (amount >= count)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 触发一次所有负面伤害buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void SetAllDamageBuffAction(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //buff为伤害类型并且为负面状态
                if (defender.BuffDictionary[buffKey].BuffEffectType == BuffEffectType.Damage && defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState)
                {
                    if (defender.BuffDictionary[buffKey].DurationTime < 1)
                    {
                        //删除buff图标
                        FightCtrl.DeleteBuffIcon(defender, buffKey);
                        defender.BuffDictionary.Remove(buffKey);
                        continue;
                    }
                    defender.BuffDictionary[buffKey].DurationTime -= 1;
                    defender.BuffDictionary[buffKey].NextActionTime -= 1;
                    defender.BuffDictionary[buffKey].ActionAtLoop(attacker, defender);
                }
            }
        }

        /// <summary>
        /// 瞬间结算身上的所有负面伤害buff，目标是defender
        /// </summary>
        /// <param name="attacker">进攻者</param>
        /// <param name="defender">防守者</param>
        public static void FinishAllDamageBuffAction(FightHero attacker, FightHero defender)
        {
            if (defender.BuffDictionary == null || defender.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(defender.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //buff为伤害类型并且为负面状态
                if (defender.BuffDictionary[buffKey].BuffEffectType == BuffEffectType.Damage && defender.BuffDictionary[buffKey].BuffType == BuffType.NegativeState)
                {
                    if (defender.BuffDictionary[buffKey].DurationTime < 1)
                    {
                        //删除buff图标
                        FightCtrl.DeleteBuffIcon(defender, buffKey);
                        defender.BuffDictionary.Remove(buffKey);
                        continue;
                    }
                    //不执行最后一次
                    while (defender.BuffDictionary[buffKey].DurationTime >= 1)
                    {
                        defender.BuffDictionary[buffKey].DurationTime -= 1;
                        defender.BuffDictionary[buffKey].ActionAtLoop(attacker, defender);
                    }
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(defender, buffKey);
                    defender.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 刷新一次身上的所有正面buff
        /// </summary>
        /// <param name="fightHero">作用对象</param>
        public static void UpdateOnceAllPositiveBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //正面buff
                if (fightHero.BuffDictionary[buffKey].BuffType == BuffType.PositiveState)
                {
                    fightHero.BuffDictionary[buffKey].UpdateBuff();
                }
            }
        }

        /// <summary>
        /// 刷新一次身上的所有负面buff
        /// </summary>
        /// <param name="fightHero">作用对象</param>
        public static void UpdateOnceAllNegativeBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //负面buff
                if (fightHero.BuffDictionary[buffKey].BuffType == BuffType.NegativeState)
                {
                    fightHero.BuffDictionary[buffKey].UpdateBuff();
                }
            }
        }

        /// <summary>
        /// 是否具有高级buff
        /// </summary>
        /// <param name="fightHero">作用对象</param>
        /// <returns></returns>
        public static bool IsHaveSeniorBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return false;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //高级buff
                if (fightHero.BuffDictionary[buffKey].BuffFlag == BuffFlag.Senior)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除所有的高级buff
        /// </summary>
        /// <param name="fightHero"></param>
        public static void RemoveAllSeniorBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //高级buff
                if (fightHero.BuffDictionary[buffKey].BuffFlag == BuffFlag.Senior)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(fightHero, buffKey);
                    fightHero.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 是否具有领域buff
        /// </summary>
        /// <param name="fightHero">作用对象</param>
        /// <returns></returns>
        public static bool IsHaveFieldBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return false;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //领域buff
                if (fightHero.BuffDictionary[buffKey].BuffFlag == BuffFlag.Field)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除领域buff
        /// </summary>
        /// <param name="fightHero"></param>
        public static void RemoveAllFieldBuff(FightHero fightHero, BuffClass buffClass)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //领域buff
                if (fightHero.BuffDictionary[buffKey].BuffFlag == BuffFlag.Field && buffKey != buffClass)
                {
                    //删除buff图标
                    FightCtrl.DeleteBuffIcon(fightHero, buffKey);
                    fightHero.BuffDictionary.Remove(buffKey);
                }
            }
        }

        /// <summary>
        /// 移除所有buff
        /// </summary>
        /// <param name="fightHero"></param>
        public static void RemoveAllBuff(FightHero fightHero)
        {
            if (fightHero.BuffDictionary == null || fightHero.BuffDictionary.Count <= 0)
            {
                return;
            }
            List<BuffClass> dictList = new List<BuffClass>();
            dictList.AddRange(fightHero.BuffDictionary.Keys);
            foreach (BuffClass buffKey in dictList)
            {
                //删除buff图标
                FightCtrl.DeleteBuffIcon(fightHero, buffKey);
                fightHero.BuffDictionary.Remove(buffKey);
            }
        }

    }

    #region FightHero

    /// <summary>
    /// 人物战斗手牌
    /// </summary>
    public class CardGroup
    {
        #region Field

        private int waterCount;
        private int fireCount;
        private int thunderCount;
        private int windCount;
        private int earthCount;
        private int darkCount;
        private int chaosCount;

        private float getWaterPercent;
        private float getFirePercent;
        private float getThunderPercent;
        private float getWindPercent;
        private float getEarthPercent;
        private float getDarkPercent;
        private float getChaosPercent;

        //手牌卡组
        private Dictionary<GameObject, ElementType> handCardGroup;
        //历史出牌卡组
        private Dictionary<GameObject, ElementType> cardHistory;

        #endregion

        #region Attribute

        public int WaterCount
        {
            get
            {
                return waterCount;
            }
        }

        public int FireCount
        {
            get
            {
                return fireCount;
            }
        }

        public int ThunderCount
        {
            get
            {
                return thunderCount;
            }
        }

        public int WindCount
        {
            get
            {
                return windCount;
            }
        }

        public int EarthCount
        {
            get
            {
                return earthCount;
            }
        }

        public int DarkCount
        {
            get
            {
                return darkCount;
            }
        }

        public int ChaosCount
        {
            get
            {
                return chaosCount;
            }
        }

        public float GetWaterPercent
        {
            get
            {
                return getWaterPercent;
            }

            set
            {
                getWaterPercent = value;
                if (getWaterPercent < 0)
                {
                    getWaterPercent = 0;
                }
                if (getWaterPercent > 1)
                {
                    getWaterPercent = 1;
                }
            }
        }

        public float GetFirePercent
        {
            get
            {
                return getFirePercent;
            }

            set
            {
                getFirePercent = value;
                if (getFirePercent < 0)
                {
                    getFirePercent = 0;
                }
                if (getFirePercent > 1)
                {
                    getFirePercent = 1;
                }
            }
        }

        public float GetThunderPercent
        {
            get
            {
                return getThunderPercent;
            }

            set
            {
                getThunderPercent = value;
                if (getThunderPercent < 0)
                {
                    getThunderPercent = 0;
                }
                if (getThunderPercent > 1)
                {
                    getThunderPercent = 1;
                }
            }
        }

        public float GetWindPercent
        {
            get
            {
                return getWindPercent;
            }

            set
            {
                getWindPercent = value;
                if (getWindPercent < 0)
                {
                    getWindPercent = 0;
                }
                if (getWindPercent > 1)
                {
                    getWindPercent = 1;
                }
            }
        }

        public float GetEarthPercent
        {
            get
            {
                return getEarthPercent;
            }

            set
            {
                getEarthPercent = value;
                if (getEarthPercent < 0)
                {
                    getEarthPercent = 0;
                }
                if (getEarthPercent > 1)
                {
                    getEarthPercent = 1;
                }
            }
        }

        public float GetDarkPercent
        {
            get
            {
                return getDarkPercent;
            }

            set
            {
                getDarkPercent = value;
                if (getDarkPercent < 0)
                {
                    getDarkPercent = 0;
                }
                if (getDarkPercent > 1)
                {
                    getDarkPercent = 1;
                }
            }
        }

        public float GetChaosPercent
        {
            get
            {
                return getChaosPercent;
            }

            set
            {
                getChaosPercent = value;
                if (getChaosPercent < 0)
                {
                    getChaosPercent = 0;
                }
                if (getChaosPercent > 1)
                {
                    getChaosPercent = 1;
                }
            }
        }

        public Dictionary<GameObject, ElementType> HandCardGroup
        {
            get
            {
                return handCardGroup;
            }
        }

        public Dictionary<GameObject, ElementType> CardHistory
        {
            get
            {
                return cardHistory;
            }
        }

        #endregion

        public CardGroup()
        {
            waterCount = 0;
            fireCount = 0;
            thunderCount = 0;
            windCount = 0;
            earthCount = 0;
            darkCount = 0;
            chaosCount = 0;
            getWaterPercent = 0;
            getFirePercent = 0;
            getThunderPercent = 0;
            getWindPercent = 0;
            getEarthPercent = 0;
            getDarkPercent = 0;
            getChaosPercent = 0;
            handCardGroup = new Dictionary<GameObject, ElementType>();
            cardHistory = new Dictionary<GameObject, ElementType>();
        }

        /// <summary>
        /// 抽卡阶段，玩家抽卡，获得一张随机产生的元素卡
        /// 元素卡的创建逻辑在FightCtrl中完成，此过程只负责将白卡转化成相应的随机元素卡
        /// </summary>
        /// <param name="gameObject">玩家抽卡的对象</param>
        /// <returns></returns>
        public void GetShuffedCard(GameObject gameObject)
        {
            int random = UnityEngine.Random.Range(1, (int)(10000 * (getWaterPercent + getFirePercent + getThunderPercent + getWindPercent + getEarthPercent + getDarkPercent)));
            if (random <= 10000 * getWaterPercent)
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Water;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/water");
                HandCardGroup.Add(gameObject, ElementType.Water);
            }
            else if (random <= 10000 * (getWaterPercent + getFirePercent))
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Fire;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/fire");
                HandCardGroup.Add(gameObject, ElementType.Fire);
            }
            else if (random <= 10000 * (getWaterPercent + getFirePercent + getThunderPercent))
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Thunder;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/thunder");
                HandCardGroup.Add(gameObject, ElementType.Thunder);
            }
            else if (random <= 10000 * (getWaterPercent + getFirePercent + getThunderPercent + getWindPercent))
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Wind;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/wind");
                HandCardGroup.Add(gameObject, ElementType.Wind);
            }
            else if (random <= 10000 * (getWaterPercent + getFirePercent + getThunderPercent + getWindPercent + getEarthPercent))
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Earth;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/earth");
                HandCardGroup.Add(gameObject, ElementType.Earth);
            }
            else if (random <= 10000 * (getWaterPercent + getFirePercent + getThunderPercent + getWindPercent + getEarthPercent + getDarkPercent))
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Dark;
                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/dark");
                HandCardGroup.Add(gameObject, ElementType.Dark);
            }
            else
            {
                gameObject.GetComponent<CardCtrl>().elemType = ElementType.Chaos;
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/random");
                HandCardGroup.Add(gameObject, ElementType.Chaos);
            }
        }

        /// <summary>
        /// 准备出牌阶段，选择牌，即玩家点击该牌但未选择出牌时
        /// 增加玩家准备出牌的元素计数
        /// </summary>
        /// <param name="gameObject">玩家点击牌的对象</param>
        public void PrepareShowCard(GameObject gameObject)
        {
            if (handCardGroup.ContainsKey(gameObject) == false)
                return;
            switch (handCardGroup[gameObject])
            {
                case ElementType.Water:
                    waterCount++;
                    break;
                case ElementType.Fire:
                    fireCount++;
                    break;
                case ElementType.Thunder:
                    thunderCount++;
                    break;
                case ElementType.Wind:
                    windCount++;
                    break;
                case ElementType.Earth:
                    earthCount++;
                    break;
                case ElementType.Dark:
                    darkCount++;
                    break;
                case ElementType.Chaos:
                    chaosCount++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 准备出牌阶段，放弃选择的牌，即玩家再次点击已选择的牌
        /// 减少玩家放弃的牌的元素计数
        /// </summary>
        /// <param name="gameObject">玩家点击牌的对象</param>
        public void AbandomPrepareCard(GameObject gameObject)
        {
            if (handCardGroup.ContainsKey(gameObject) == false)
                return;
            switch (handCardGroup[gameObject])
            {
                case ElementType.Water:
                    waterCount--;
                    break;
                case ElementType.Fire:
                    fireCount--;
                    break;
                case ElementType.Thunder:
                    thunderCount--;
                    break;
                case ElementType.Wind:
                    windCount--;
                    break;
                case ElementType.Earth:
                    earthCount--;
                    break;
                case ElementType.Dark:
                    darkCount--;
                    break;
                case ElementType.Chaos:
                    chaosCount--;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 出牌阶段，出所有选择的牌
        /// 增加玩家出牌的元素计数，用于选择卡牌逻辑
        /// </summary>
        /// <param name="gameObject">玩家出牌的对象</param>
        public void ShowCard(GameObject gameObject)
        {
            ElementType elemType = new ElementType();
            if (HandCardGroup.TryGetValue(gameObject, out elemType) == false)
                return;
            CardHistory.Add(gameObject, elemType);
            HandCardGroup.Remove(gameObject);
            switch (elemType)
            {
                case ElementType.Water:
                    waterCount++;
                    break;
                case ElementType.Fire:
                    fireCount++;
                    break;
                case ElementType.Thunder:
                    thunderCount++;
                    break;
                case ElementType.Wind:
                    windCount++;
                    break;
                case ElementType.Earth:
                    earthCount++;
                    break;
                case ElementType.Dark:
                    darkCount++;
                    break;
                case ElementType.Chaos:
                    chaosCount++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重置所有元素计数
        /// </summary>
        public void ResetAllCount()
        {
            waterCount = 0;
            fireCount = 0;
            thunderCount = 0;
            windCount = 0;
            earthCount = 0;
            darkCount = 0;
            chaosCount = 0;
        }

        /// <summary>
        /// 重置手牌，并清空所有元素计数
        /// </summary>
        public void ClearHandCardGroup()
        {
            HandCardGroup.Clear();
            ResetAllCount();
        }

        /// <summary>
        /// 清空出牌记录
        /// </summary>
        public void ClearShowCardGroup()
        {
            CardHistory.Clear();
        }

    }

    /// <summary>
    /// 战斗人物
    /// </summary>
    public class FightHero
    {

        public HeroCard heroCard;

        #region Field

        #region PublicParams

        //初始生命
        private float originalHp;
        //最大生命
        private float maxHp;
        //最大生命值偏移
        private float maxHpDeviant;
        //当前生命
        private float curHp;

        //初始护盾
        private float originalShield;
        //最大护盾
        private float maxShield;
        //当前护盾
        private float curShield;
        //护盾偏移
        private float shieldDeviant;

        //初始速度
        private float originalSpeed;
        //当前速度
        private float curSpeed;
        //速度偏移值
        private float speedDeviant;

        //初始防御
        private float originalDamageResist;
        //当前防御
        private float curDamageResist;
        //防御偏移值
        private float damageResistDeviant;

        //初始防御格挡
        private float originalDamageParry;
        //当前防御格挡
        private float curDamageParry;
        //防御格挡偏移值
        private float damageParryDeviant;

        //初始伤害抵抗系数
        private float originalChaosDamageResistCoefficient;
        //当前伤害抵抗系数
        private float curChaosDamageResistCoefficient;
        //伤害抵抗系数偏移值
        private float chaosDamageResistCoefficientDeviant;

        //初始伤害系数
        private float originalChaosDamageCoefficient;
        //当前伤害系数
        private float curChaosDamageCoefficient;
        //伤害系数偏移值
        private float chaosDamageCoefficientDeviant;

        //当前的行动条值
        private float curSliderValue;
        //当前的行动点
        private int curApPoint;
        //最大行动点
        private int maxApPoint;
        //最大行动点偏移
        private int maxApPointDeviant;

        //初始闪避率
        private float originalDodgeCoefficient;
        //当前闪避率
        private float curDodgeCoefficient;
        //闪避率偏移值
        private float dodgeCoefficientDeviant;

        //初始暴击率
        private float originalCriticalCoefficient;
        //当前暴击率
        private float curCriticalCoefficient;
        //暴击率偏移值
        private float criticalCoefficientDeviant;

        //初始暴击系数
        private float originalCritMutipleCoefficient;
        //当前暴击系数
        private float curCritMutipleCoefficient;
        //暴击系数偏移值
        private float critMutipleCoefficientDeviant;

        //初始穿透系数
        private float originalDamagePierceCoefficient;
        //当前穿透系数
        private float curDamagePierceCoefficient;
        //穿透系数偏移值
        private float damagePierceCoefficientDeviant;

        //初始增伤数值
        private float originalDamageIncrementValue;
        //当前增伤数值
        private float curDamageIncrementValue;
        //增伤数值偏移值
        private float damageIncrementValueDeviant;

        //初始吸血系数
        private float originalSuckBloodCoefficient;
        //当前吸血系数
        private float curSuckBloodCoefficient;
        //吸血系数偏移值
        private float suckBloodCoefficientDeviant;

        //初始是否buff瞬间结算
        private bool originalIsBuffInstant;
        //当前是否buff瞬间结算
        private bool curIsBuffInstant;
        //buff瞬间结算偏移
        private bool isBuffInstantDeviant;

        //初始是否无敌
        private bool originalIsInvinsible;
        //当前是否无敌
        private bool curIsInvinsible;
        //是否无敌偏移
        private bool isInvinsibleDeviant;

        //初始是否魔免
        private bool originalIsMagicImmune;
        //当前是否魔免
        private bool curIsMagicImmune;
        //是否魔免偏移
        private bool isMagicImmuneDeviant;

        //初始是否可以出牌
        private bool originalCanDrawCard;
        //当前是否可以出牌
        private bool curCanDrawCard;
        //是否可以出牌偏移
        private bool canDrawCardDeviant;

        //初始是否可以出水牌
        private bool originalCanDrawWaterCard;
        //当前是否可以出水牌
        private bool curCanDrawWaterCard;
        //是否可以出水牌偏移
        private bool canDrawWaterCardDeviant;

        //初始是否可以出火牌
        private bool originalCanDrawFireCard;
        //当前是否可以出火牌
        private bool curCanDrawFireCard;
        //是否可以出火牌偏移
        private bool canDrawFireCardDeviant;

        //初始是否可以出雷牌
        private bool originalCanDrawThunderCard;
        //当前是否可以出雷牌
        private bool curCanDrawThunderCard;
        //是否可以出雷牌偏移
        private bool canDrawThunderCardDeviant;

        //初始是否可以出风牌
        private bool originalCanDrawWindCard;
        //当前是否可以出风牌
        private bool curCanDrawWindCard;
        //是否可以出风牌偏移
        private bool canDrawWindCardDeviant;

        //初始是否可以出地牌
        private bool originalCanDraweEarthCard;
        //当前是否可以出地牌
        private bool curCanDrawEarthCard;
        //是否可以出地牌偏移
        private bool canDrawEarthCardDeviant;

        //初始是否可以出暗牌
        private bool originalCanDrawDarkCard;
        //当前是否可以出暗牌
        private bool curCanDrawDarkCard;
        //是否可以出暗牌偏移
        private bool canDrawDarkCardDeviant;

        //玩家当前拥有的手牌卡组
        private CardGroup cardGroup;
        //玩家造成的对敌伤害记录
        private List<DamageRecord> damageToEnemyRecords;
        //玩家收到的伤害记录
        private List<DamageRecord> damageFromEnemyRecords;
        //控制所有的buff字典
        private Dictionary<BuffClass, BaseBuff> buffDictionary;

        //主动技能是否可用
        private bool isActiveSkill;
        //初始主动技能冷却时间
        private float originalActiveSkillCoolingTime;
        //最大主动技能冷却时间
        private float maxActiveSkillCoolingTime;
        //当前主动技能冷却时间
        private float curActiveSkillCoolingTime;

        //助战技能是否可用
        private bool isAssistSkill;
        //初始助战技能冷却时间
        private float originalAssistSkillCoolingTime;
        //最大助战技能冷却时间
        private float maxAssistSkillCoolingTime;
        //当前助战技能冷却时间
        private float curAssistSkillCoolingTime;

        //第一个被动技能是否可用
        private bool isFirstPassiveSkill;
        //初始第一个被动技能冷却时间
        private float originalFirstPassiveSkillCoolingTime;
        //最大第一个被动技能冷却时间
        private float maxFirstPassiveSkillCoolingTime;
        //当前第一个被动技能冷却时间
        private float curFirstPassiveSkillCoolingTime;

        //第二个被动技能是否可用
        private bool isSecondPassiveSkill;
        //初始第二个被动技能冷却时间
        private float originalSecondPassiveSkillCoolingTime;
        //最大第二个被动技能冷却时间
        private float maxSecondPassiveSkillCoolingTime;
        //当前第二个被动技能冷却时间
        private float curSecondPassiveSkillCoolingTime;

        //第三个被动技能是否可用
        private bool isThirdPassiveSkill;
        //初始第三个被动技能冷却时间
        private float originalThirdPassiveSkillCoolingTime;
        //最大第三个被动技能冷却时间
        private float maxThirdPassiveSkillCoolingTime;
        //当前第三个被动技能冷却时间
        private float curThirdPassiveSkillCoolingTime;

        //记录总共收到的伤害
        private float totalRecivedDamage;
        //最后一次受到的伤害元素类型
        private ElementType finalDamageElemType;

        #endregion

        #region Water

        //初始水元素强度
        private float originalWaterStrength;
        //当前水元素强度
        private float curWaterStrength;
        //水元素强度偏移值
        private float waterStrengthDeviant;

        //初始水元素伤害抵抗系数
        private float originalWaterResistCoefficient;
        //当前水元素伤害抵抗系数
        private float curWaterResistCoefficient;
        //水元素伤害抵抗系数偏移值
        private float waterResistCoefficientDeviant;

        //初始水元素伤害系数
        private float originalWaterDamageCoefficient;
        //当前水元素伤害系数
        private float curWaterDamageCoefficient;
        //水元素伤害系数偏移值
        private float waterDamageCoefficientDeviant;

        //水元素受到的总伤害值
        private float waterTotalRecivedDamage;

        #endregion

        #region Fire

        //初始火元素强度
        private float originalFireStrength;
        //当前火元素强度
        private float curFireStrength;
        //火元素强度偏移值
        private float fireStrengthDeviant;

        //初始火元素伤害抵抗系数
        private float originalFireResistCoefficient;
        //当前火元素伤害抵抗系数
        private float curFireResistCoefficient;
        //火元素伤害抵抗系数偏移值
        private float fireResistCoefficientDeviant;

        //初始火元素伤害系数
        private float originalFireDamageCoefficient;
        //当前火元素伤害系数
        private float curFireDamageCoefficient;
        //火元素伤害系数偏移值
        private float fireDamageCoefficientDeviant;

        //火元素受到的总伤害值
        private float fireTotalRecivedDamage;

        #endregion

        #region Thunder

        //初始雷元素强度
        private float originalThunderStrength;
        //当前雷元素强度
        private float curThunderStrength;
        //雷元素强度偏移值
        private float thunderStrengthDeviant;

        //初始雷元素伤害抵抗系数
        private float originalThunderResistCoefficient;
        //当前雷元素伤害抵抗系数
        private float curThunderResistCoefficient;
        //雷元素伤害抵抗系数偏移值
        private float thunderResistCoefficientDeviant;

        //初始雷元素伤害系数
        private float originalThunderDamageCoefficient;
        //当前雷元素伤害系数
        private float curThunderDamageCoefficient;
        //雷元素伤害系数偏移值
        private float thunderDamageCoefficientDeviant;

        //雷元素受到的总伤害值
        private float thunderTotalRecivedDamage;

        #endregion

        #region Wind

        //初始风元素强度
        private float originalWindStrength;
        //当前风元素强度
        private float curWindStrength;
        //风元素强度偏移值
        private float windStrengthDeviant;

        //初始风元素伤害抵抗系数
        private float originalWindResistCoefficient;
        //当前风元素伤害抵抗系数
        private float curWindResistCoefficient;
        //风元素伤害抵抗系数偏移值
        private float windResistCoefficientDeviant;

        //初始风元素伤害系数
        private float originalWindDamageCoefficient;
        //当前风元素伤害系数
        private float curWindDamageCoefficient;
        //风元素伤害系数偏移值
        private float windDamageCoefficientDeviant;

        //风元素受到的总伤害值
        private float windTotalRecivedDamage;

        #endregion

        #region Earth

        //初始地元素强度
        private float originalEarthStrength;
        //当前地元素强度
        private float curEarthStrength;
        //地元素强度偏移值
        private float earthStrengthDeviant;

        //初始地元素伤害抵抗系数
        private float originalEarthResistCoefficient;
        //当前地元素伤害抵抗系数
        private float curEarthResistCoefficient;
        //地元素伤害抵抗系数偏移值
        private float earthResistCoefficientDeviant;

        //初始地元素伤害系数
        private float originalEarthDamageCoefficient;
        //当前地元素伤害系数
        private float curEarthDamageCoefficient;
        //地元素伤害系数偏移值
        private float earthDamageCoefficientDeviant;

        //地元素受到的总伤害值
        private float earthTotalRecivedDamage;

        #endregion

        #region Dark

        //初始暗元素强度
        private float originalDarkStrength;
        //当前暗元素强度
        private float curDarkStrength;
        //暗元素强度偏移值
        private float darkStrengthDeviant;

        //初始暗元素伤害抵抗系数
        private float originalDarkResistCoefficient;
        //当前暗元素伤害抵抗系数
        private float curDarkResistCoefficient;
        //暗元素伤害抵抗系数偏移值
        private float darkResistCoefficientDeviant;

        //初始暗元素伤害系数
        private float originalDarkDamageCoefficient;
        //当前暗元素伤害系数
        private float curDarkDamageCoefficient;
        //暗元素伤害系数偏移值
        private float darkDamageCoefficientDeviant;

        //暗元素受到的总伤害值
        private float darkTotalRecivedDamage;

        #endregion

        #region AllElem

        //初始全元素伤害抵抗系数
        private float originalElemResistCoefficient;
        //当前全元素伤害抵抗系数
        private float curElemResistCoefficient;
        //全元素伤害抵抗系数偏移值
        private float elemResistCoefficientDeviant;

        //初始全元素伤害系数
        private float originalElemDamageCoefficient;
        //当前全元素伤害系数
        private float curElemDamageCoefficient;
        //全元素伤害系数偏移值
        private float elemDamageCoefficientDeviant;

        #endregion

        #endregion

        #region Attribute

        #region PublicParams

        public float OriginalHp
        {
            get
            {
                return originalHp;
            }

            set
            {
                originalHp = value;
            }
        }

        public float MaxHp
        {
            get
            {
                return maxHp;
            }

            set
            {
                maxHp = value;
                if (maxHp < 0)
                {
                    maxHp = 1;
                }
                if (maxHp < curHp)
                {
                    curHp = maxHp;
                }
                if (maxHp > (float)FightHeroMaxDefine.MaxHp)
                {
                    maxHp = (float)FightHeroMaxDefine.MaxHp;
                }
            }
        }

        public float MaxHpDeviant
        {
            get
            {
                return maxHpDeviant;
            }

            set
            {
                maxHpDeviant = value;
            }
        }

        public float CurHp
        {
            get
            {
                return curHp;
            }

            set
            {
                curHp = value;
                if (curHp < 0)
                {
                    curHp = 0;
                }
                if (curHp > maxHp)
                {
                    curHp = maxHp;
                }
            }
        }

        public float OriginalShield
        {
            get
            {
                return originalShield;
            }

            set
            {
                originalShield = value;
                if(originalShield < 0)
                {
                    originalShield = 0;
                }
            }
        }

        public float MaxShield
        {
            get
            {
                return maxShield;
            }

            set
            {
                maxShield = value;
                if (maxShield < 0)
                {
                    maxShield = 0;
                }
            }
        }

        public float CurShield
        {
            get
            {
                return curShield;
            }

            set
            {
                curShield = value;
                if (curShield < 0)
                {
                    curShield = 0;
                }
            }
        }

        public float ShieldDeviant
        {
            get
            {
                return shieldDeviant;
            }

            set
            {
                shieldDeviant = value;
            }
        }


        public float OriginalSpeed
        {
            get
            {
                return originalSpeed;
            }

            set
            {
                originalSpeed = value;
            }
        }

        public float CurSpeed
        {
            get
            {
                return curSpeed;
            }

            set
            {
                curSpeed = value;
                if (curSpeed <= 0)
                {
                    curSpeed = 1;
                }
                if (curSpeed > (float)FightHeroMaxDefine.MaxSpeed)
                {
                    curSpeed = (float)FightHeroMaxDefine.MaxSpeed;
                }
            }
        }

        public float SpeedDeviant
        {
            get
            {
                return speedDeviant;
            }

            set
            {
                speedDeviant = value;
            }
        }

        public float OriginalDamageResist
        {
            get
            {
                return originalDamageResist;
            }

            set
            {
                originalDamageResist = value;
            }
        }

        public float CurDamageResist
        {
            get
            {
                return curDamageResist;
            }

            set
            {
                curDamageResist = value;
                if (curDamageResist < 0)
                {
                    curDamageResist = 0;
                }
                if (curDamageResist > (float)FightHeroMaxDefine.MaxDamageResist)
                {
                    curDamageResist = (float)FightHeroMaxDefine.MaxDamageResist;
                }
            }
        }

        public float DamageResistDeviant
        {
            get
            {
                return damageResistDeviant;
            }

            set
            {
                damageResistDeviant = value;
            }
        }

        public float OriginalDamageParry
        {
            get
            {
                return originalDamageParry;
            }

            set
            {
                originalDamageParry = value;
            }
        }

        public float CurDamageParry
        {
            get
            {
                return curDamageParry;
            }

            set
            {
                curDamageParry = value;
                if (curDamageParry < 0)
                {
                    curDamageParry = 0;
                }
                if (curDamageParry > (float)FightHeroMaxDefine.MaxDamageParry)
                {
                    curDamageParry = (float)FightHeroMaxDefine.MaxDamageParry;
                }
            }
        }

        public float DamageParryDeviant
        {
            get
            {
                return damageParryDeviant;
            }

            set
            {
                damageParryDeviant = value;
            }
        }

        public float OriginalChaosDamageResistCoefficient
        {
            get
            {
                return originalChaosDamageResistCoefficient;
            }

            set
            {
                originalChaosDamageResistCoefficient = value;
                if (originalChaosDamageResistCoefficient <= 0)
                {
                    originalChaosDamageResistCoefficient = 0.1f;
                }
            }
        }

        public float CurChaosDamageResistCoefficient
        {
            get
            {
                return curChaosDamageResistCoefficient;
            }

            set
            {
                curChaosDamageResistCoefficient = value;
                if (curChaosDamageResistCoefficient < 0)
                {
                    curChaosDamageResistCoefficient = 0.1f;
                }
                if (curChaosDamageResistCoefficient > (float)FightHeroMaxDefine.MaxChaosResistCoeficient)
                {
                    curChaosDamageResistCoefficient = (float)FightHeroMaxDefine.MaxChaosResistCoeficient;
                }
            }
        }

        public float ChaosDamageResistCoefficientDeviant
        {
            get
            {
                return chaosDamageResistCoefficientDeviant;
            }

            set
            {
                chaosDamageResistCoefficientDeviant = value;
            }
        }

        public float OriginalChaosDamageCoefficient
        {
            get
            {
                return originalChaosDamageCoefficient;
            }

            set
            {
                originalChaosDamageCoefficient = value;
                if (originalChaosDamageCoefficient < 0)
                {
                    originalChaosDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurChaosDamageCoefficient
        {
            get
            {
                return curChaosDamageCoefficient;
            }

            set
            {
                curChaosDamageCoefficient = value;
                if (curChaosDamageCoefficient < 0)
                {
                    curChaosDamageCoefficient = 0.1f;
                }
                if (curChaosDamageCoefficient > (float)FightHeroMaxDefine.MaxChaosDamageCoeficient)
                {
                    curChaosDamageCoefficient = (float)FightHeroMaxDefine.MaxChaosDamageCoeficient;
                }
            }
        }

        public float ChaosDamageCoefficientDeviant
        {
            get
            {
                return chaosDamageCoefficientDeviant;
            }

            set
            {
                chaosDamageCoefficientDeviant = value;
            }
        }

        public float CurSliderValue
        {
            get
            {
                return curSliderValue;
            }

            set
            {
                curSliderValue = value;
                if (curSliderValue < 0)
                {
                    curSliderValue = 0;
                }
            }
        }

        public int CurApPoint
        {
            get
            {
                return curApPoint;
            }

            set
            {
                curApPoint = value;
                if (curApPoint < 0)
                {
                    curApPoint = 0;
                }
                if (curApPoint > 5)
                {
                    curApPoint = 5;
                }
            }
        }

        public int MaxApPoint
        {
            get
            {
                return maxApPoint;
            }

            set
            {
                maxApPoint = value;
                if (maxApPoint < 1)
                {
                    maxApPoint = 1;
                }
                if (maxApPoint > 5)
                {
                    maxApPoint = 5;
                }
            }
        }

        public int MaxApPointDeviant
        {
            get
            {
                return maxApPointDeviant;
            }

            set
            {
                maxApPointDeviant = value;
            }
        }

        public float OriginalDodgeCoefficient
        {
            get
            {
                return originalDodgeCoefficient;
            }

            set
            {
                originalDodgeCoefficient = value;
            }
        }

        public float CurDodgeCoefficient
        {
            get
            {
                return curDodgeCoefficient;
            }

            set
            {
                curDodgeCoefficient = value;
                if (curDodgeCoefficient < 0)
                {
                    curDodgeCoefficient = 0;
                }
                if (curDodgeCoefficient > (float)FightHeroMaxDefine.MaxDodgeCoeficient)
                {
                    curDodgeCoefficient = (float)FightHeroMaxDefine.MaxDodgeCoeficient;
                }
            }
        }

        public float DodgeCoefficientDeviant
        {
            get
            {
                return dodgeCoefficientDeviant;
            }

            set
            {
                dodgeCoefficientDeviant = value;
            }
        }

        public float OriginalCriticalCoefficient
        {
            get
            {
                return originalCriticalCoefficient;
            }

            set
            {
                originalCriticalCoefficient = value;
            }
        }

        public float CurCriticalCoefficient
        {
            get
            {
                return curCriticalCoefficient;
            }

            set
            {
                curCriticalCoefficient = value;
                if (curCriticalCoefficient < 0)
                {
                    curCriticalCoefficient = 0;
                }
                if (curCriticalCoefficient > (float)FightHeroMaxDefine.MaxCriticalCoeficient)
                {
                    curCriticalCoefficient = (float)FightHeroMaxDefine.MaxCriticalCoeficient;
                }
            }
        }

        public float CriticalCoefficientDeviant
        {
            get
            {
                return criticalCoefficientDeviant;
            }

            set
            {
                criticalCoefficientDeviant = value;
            }
        }

        public float OriginalCritMutipleCoefficient
        {
            get
            {
                return originalCritMutipleCoefficient;
            }

            set
            {
                originalCritMutipleCoefficient = value;
            }
        }

        public float CurCritMutipleCoefficient
        {
            get
            {
                return curCritMutipleCoefficient;
            }

            set
            {
                curCritMutipleCoefficient = value;
                if (curCritMutipleCoefficient < 0)
                {
                    curCritMutipleCoefficient = 0;
                }
                if (curCritMutipleCoefficient > (float)FightHeroMaxDefine.MaxCritMutipleCoeficient)
                {
                    curCritMutipleCoefficient = (float)FightHeroMaxDefine.MaxCritMutipleCoeficient;
                }
            }
        }

        public float CritMutipleCoefficientDeviant
        {
            get
            {
                return critMutipleCoefficientDeviant;
            }

            set
            {
                critMutipleCoefficientDeviant = value;
            }
        }

        public float OriginalDamagePierceCoefficient
        {
            get
            {
                return originalDamagePierceCoefficient;
            }

            set
            {
                originalDamagePierceCoefficient = value;
            }
        }

        public float CurDamagePierceCoefficient
        {
            get
            {
                return curDamagePierceCoefficient;
            }

            set
            {
                curDamagePierceCoefficient = value;
                if (curDamagePierceCoefficient < 0)
                {
                    curDamagePierceCoefficient = 0;
                }
                if (curDamagePierceCoefficient > (float)FightHeroMaxDefine.MaxDamagePierceCoeficient)
                {
                    curDamagePierceCoefficient = (float)FightHeroMaxDefine.MaxDamagePierceCoeficient;
                }
            }
        }

        public float DamagePierceCoefficientDeviant
        {
            get
            {
                return damagePierceCoefficientDeviant;
            }

            set
            {
                damagePierceCoefficientDeviant = value;
            }
        }

        public float OriginalDamageIncrementValue
        {
            get
            {
                return originalDamageIncrementValue;
            }

            set
            {
                originalDamageIncrementValue = value;
            }
        }

        public float CurDamageIncrementValue
        {
            get
            {
                return curDamageIncrementValue;
            }

            set
            {
                curDamageIncrementValue = value;
                if (curDamageIncrementValue < 0)
                {
                    curDamageIncrementValue = 0;
                }
                if (curDamageIncrementValue > (float)FightHeroMaxDefine.MaxDamageIncrementValue)
                {
                    curDamageIncrementValue = (float)FightHeroMaxDefine.MaxDamageIncrementValue;
                }
            }
        }

        public float DamageIncrementValueDeviant
        {
            get
            {
                return damageIncrementValueDeviant;
            }

            set
            {
                damageIncrementValueDeviant = value;
            }
        }

        public float OriginalSuckBloodCoefficient
        {
            get
            {
                return originalSuckBloodCoefficient;
            }

            set
            {
                originalSuckBloodCoefficient = value;
            }
        }

        public float CurSuckBloodCoefficient
        {
            get
            {
                return curSuckBloodCoefficient;
            }

            set
            {
                curSuckBloodCoefficient = value;
                if (curSuckBloodCoefficient < 0)
                {
                    curSuckBloodCoefficient = 0;
                }
                if (curSuckBloodCoefficient > (float)FightHeroMaxDefine.MaxSuckBloodCoeficient)
                {
                    curSuckBloodCoefficient = (float)FightHeroMaxDefine.MaxSuckBloodCoeficient;
                }
            }
        }

        public float SuckBloodCoefficientDeviant
        {
            get
            {
                return suckBloodCoefficientDeviant;
            }

            set
            {
                suckBloodCoefficientDeviant = value;
            }
        }

        public bool OriginalIsBuffInstant
        {
            get
            {
                return originalIsBuffInstant;
            }

            set
            {
                originalIsBuffInstant = value;
            }
        }

        public bool CurIsBuffInstant
        {
            get
            {
                return curIsBuffInstant;
            }

            set
            {
                curIsBuffInstant = value;
            }
        }

        public bool IsBuffInstantDeviant
        {
            get
            {
                return isBuffInstantDeviant;
            }

            set
            {
                isBuffInstantDeviant = value;
            }
        }

        public bool OriginalIsInvinsible
        {
            get
            {
                return originalIsInvinsible;
            }

            set
            {
                originalIsInvinsible = value;
            }
        }

        public bool CurIsInvinsible
        {
            get
            {
                return curIsInvinsible;
            }

            set
            {
                curIsInvinsible = value;
            }
        }

        public bool IsInvinsibleDeviant
        {
            get
            {
                return isInvinsibleDeviant;
            }

            set
            {
                isInvinsibleDeviant = value;
            }
        }

        public bool OriginalIsMagicImmune
        {
            get
            {
                return originalIsMagicImmune;
            }

            set
            {
                originalIsMagicImmune = value;
            }
        }

        public bool CurIsMagicImmune
        {
            get
            {
                return curIsMagicImmune;
            }

            set
            {
                curIsMagicImmune = value;
            }
        }

        public bool IsMagicImmuneDeviant
        {
            get
            {
                return isMagicImmuneDeviant;
            }

            set
            {
                isMagicImmuneDeviant = value;
            }
        }

        public bool OriginalCanDrawCard
        {
            get
            {
                return originalCanDrawCard;
            }

            set
            {
                originalCanDrawCard = value;
            }
        }

        public bool CurCanDrawCard
        {
            get
            {
                return curCanDrawCard;
            }

            set
            {
                curCanDrawCard = value;
            }
        }

        public bool CanDrawCardDeviant
        {
            get
            {
                return canDrawCardDeviant;
            }

            set
            {
                canDrawCardDeviant = value;
            }
        }

        public bool OriginalCanDrawWaterCard
        {
            get
            {
                return originalCanDrawWaterCard;
            }

            set
            {
                originalCanDrawWaterCard = value;
            }
        }

        public bool CurCanDrawWaterCard
        {
            get
            {
                return curCanDrawWaterCard;
            }

            set
            {
                curCanDrawWaterCard = value;
            }
        }

        public bool CanDrawWaterCardDeviant
        {
            get
            {
                return canDrawWaterCardDeviant;
            }

            set
            {
                canDrawWaterCardDeviant = value;
            }
        }

        public bool OriginalCanDrawFireCard
        {
            get
            {
                return originalCanDrawFireCard;
            }

            set
            {
                originalCanDrawFireCard = value;
            }
        }

        public bool CurCanDrawFireCard
        {
            get
            {
                return curCanDrawFireCard;
            }

            set
            {
                curCanDrawFireCard = value;
            }
        }

        public bool CanDrawFireCardDeviant
        {
            get
            {
                return canDrawFireCardDeviant;
            }

            set
            {
                canDrawFireCardDeviant = value;
            }
        }

        public bool OriginalCanDrawThunderCard
        {
            get
            {
                return originalCanDrawThunderCard;
            }

            set
            {
                originalCanDrawThunderCard = value;
            }
        }

        public bool CurCanDrawThunderCard
        {
            get
            {
                return curCanDrawThunderCard;
            }

            set
            {
                curCanDrawThunderCard = value;
            }
        }

        public bool CanDrawThunderCardDeviant
        {
            get
            {
                return canDrawThunderCardDeviant;
            }

            set
            {
                canDrawThunderCardDeviant = value;
            }
        }

        public bool OriginalCanDrawWindCard
        {
            get
            {
                return originalCanDrawWindCard;
            }

            set
            {
                originalCanDrawWindCard = value;
            }
        }

        public bool CurCanDrawWindCard
        {
            get
            {
                return curCanDrawWindCard;
            }

            set
            {
                curCanDrawWindCard = value;
            }
        }

        public bool CanDrawWindCardDeviant
        {
            get
            {
                return canDrawWindCardDeviant;
            }

            set
            {
                canDrawWindCardDeviant = value;
            }
        }

        public bool OriginalCanDraweEarthCard
        {
            get
            {
                return originalCanDraweEarthCard;
            }

            set
            {
                originalCanDraweEarthCard = value;
            }
        }

        public bool CurCanDrawEarthCard
        {
            get
            {
                return curCanDrawEarthCard;
            }

            set
            {
                curCanDrawEarthCard = value;
            }
        }

        public bool CanDrawEarthCardDeviant
        {
            get
            {
                return canDrawEarthCardDeviant;
            }

            set
            {
                canDrawEarthCardDeviant = value;
            }
        }

        public bool OriginalCanDrawDarkCard
        {
            get
            {
                return originalCanDrawDarkCard;
            }

            set
            {
                originalCanDrawDarkCard = value;
            }
        }

        public bool CurCanDrawDarkCard
        {
            get
            {
                return curCanDrawDarkCard;
            }

            set
            {
                curCanDrawDarkCard = value;
            }
        }

        public bool CanDrawDarkCardDeviant
        {
            get
            {
                return canDrawDarkCardDeviant;
            }

            set
            {
                canDrawDarkCardDeviant = value;
            }
        }

        public CardGroup CardGroup
        {
            get
            {
                return cardGroup;
            }
        }

        public List<DamageRecord> DamageToEnemyRecords
        {
            get
            {
                return damageToEnemyRecords;
            }
        }

        public List<DamageRecord> DamageFromEnemyRecords
        {
            get
            {
                return damageFromEnemyRecords;
            }
        }

        public Dictionary<BuffClass, BaseBuff> BuffDictionary
        {
            get
            {
                return buffDictionary;
            }
        }

        public bool IsActiveSkill
        {
            get
            {
                return isActiveSkill;
            }

            set
            {
                isActiveSkill = value;
            }
        }

        public float OriginalActiveSkillCoolingTime
        {
            get
            {
                return originalActiveSkillCoolingTime;
            }

            set
            {
                originalActiveSkillCoolingTime = value;
            }
        }

        public float MaxActiveSkillCoolingTime
        {
            get
            {
                return maxActiveSkillCoolingTime;
            }

            set
            {
                maxActiveSkillCoolingTime = value;
                if (maxActiveSkillCoolingTime < 0)
                {
                    maxActiveSkillCoolingTime = 1;
                }
                if (maxActiveSkillCoolingTime > curActiveSkillCoolingTime)
                {
                    maxActiveSkillCoolingTime = curActiveSkillCoolingTime;
                }
                if (maxActiveSkillCoolingTime < curActiveSkillCoolingTime)
                {
                    curActiveSkillCoolingTime = maxFirstPassiveSkillCoolingTime;
                }
            }
        }

        public float CurActiveSkillCoolingTime
        {
            get
            {
                return curActiveSkillCoolingTime;
            }

            set
            {
                curActiveSkillCoolingTime = value;
                if (curActiveSkillCoolingTime < 0)
                {
                    curActiveSkillCoolingTime = 0;
                }
                if (curActiveSkillCoolingTime > maxActiveSkillCoolingTime)
                {
                    curActiveSkillCoolingTime = maxActiveSkillCoolingTime;
                }
            }
        }

        public bool IsFirstPassiveSkill
        {
            get
            {
                return isFirstPassiveSkill;
            }

            set
            {
                isFirstPassiveSkill = value;
            }
        }

        public float OriginalFirstPassiveSkillCoolingTime
        {
            get
            {
                return originalFirstPassiveSkillCoolingTime;
            }

            set
            {
                originalFirstPassiveSkillCoolingTime = value;
            }
        }

        public float MaxFirstPassiveSkillCoolingTime
        {
            get
            {
                return maxFirstPassiveSkillCoolingTime;
            }

            set
            {
                maxFirstPassiveSkillCoolingTime = value;
                if (maxFirstPassiveSkillCoolingTime < 0)
                {
                    maxFirstPassiveSkillCoolingTime = 1;
                }
                if (maxFirstPassiveSkillCoolingTime > originalFirstPassiveSkillCoolingTime)
                {
                    maxFirstPassiveSkillCoolingTime = originalFirstPassiveSkillCoolingTime;
                }
                if (maxFirstPassiveSkillCoolingTime < curFirstPassiveSkillCoolingTime)
                {
                    curFirstPassiveSkillCoolingTime = maxFirstPassiveSkillCoolingTime;
                }
            }
        }

        public float CurFirstPassiveSkillCoolingTime
        {
            get
            {
                return curFirstPassiveSkillCoolingTime;
            }

            set
            {
                curFirstPassiveSkillCoolingTime = value;
                if (curFirstPassiveSkillCoolingTime < 0)
                {
                    curFirstPassiveSkillCoolingTime = 0;
                }
                if (curFirstPassiveSkillCoolingTime > maxFirstPassiveSkillCoolingTime)
                {
                    curFirstPassiveSkillCoolingTime = maxFirstPassiveSkillCoolingTime;
                }
            }
        }

        public bool IsSecondPassiveSkill
        {
            get
            {
                return isSecondPassiveSkill;
            }

            set
            {
                isSecondPassiveSkill = value;
            }
        }

        public float OriginalSecondPassiveSkillCoolingTime
        {
            get
            {
                return originalSecondPassiveSkillCoolingTime;
            }

            set
            {
                originalSecondPassiveSkillCoolingTime = value;
            }
        }

        public float MaxSecondPassiveSkillCoolingTime
        {
            get
            {
                return maxSecondPassiveSkillCoolingTime;
            }

            set
            {
                maxSecondPassiveSkillCoolingTime = value;
                if (maxSecondPassiveSkillCoolingTime < 0)
                {
                    maxSecondPassiveSkillCoolingTime = 1;
                }
                if (maxSecondPassiveSkillCoolingTime > originalSecondPassiveSkillCoolingTime)
                {
                    maxSecondPassiveSkillCoolingTime = originalSecondPassiveSkillCoolingTime;
                }
                if (maxSecondPassiveSkillCoolingTime < originalSecondPassiveSkillCoolingTime)
                {
                    originalSecondPassiveSkillCoolingTime = maxSecondPassiveSkillCoolingTime;
                }
            }
        }

        public float CurSecondPassiveSkillCoolingTime
        {
            get
            {
                return curSecondPassiveSkillCoolingTime;
            }

            set
            {
                curSecondPassiveSkillCoolingTime = value;
                if (curSecondPassiveSkillCoolingTime < 0)
                {
                    curSecondPassiveSkillCoolingTime = 0;
                }
                if (curSecondPassiveSkillCoolingTime > maxSecondPassiveSkillCoolingTime)
                {
                    curSecondPassiveSkillCoolingTime = maxSecondPassiveSkillCoolingTime;
                }
            }
        }

        public bool IsThirdPassiveSkill
        {
            get
            {
                return isThirdPassiveSkill;
            }

            set
            {
                isThirdPassiveSkill = value;
            }
        }

        public float OriginalThirdPassiveSkillCoolingTime
        {
            get
            {
                return originalThirdPassiveSkillCoolingTime;
            }

            set
            {
                originalThirdPassiveSkillCoolingTime = value;
            }
        }

        public float MaxThirdPassiveSkillCoolingTime
        {
            get
            {
                return maxThirdPassiveSkillCoolingTime;
            }

            set
            {
                maxThirdPassiveSkillCoolingTime = value;
                if (maxThirdPassiveSkillCoolingTime < 0)
                {
                    maxThirdPassiveSkillCoolingTime = 1;
                }
                if (maxThirdPassiveSkillCoolingTime > originalThirdPassiveSkillCoolingTime)
                {
                    maxThirdPassiveSkillCoolingTime = originalThirdPassiveSkillCoolingTime;
                }
                if (maxThirdPassiveSkillCoolingTime < originalSecondPassiveSkillCoolingTime)
                {
                    originalThirdPassiveSkillCoolingTime = maxThirdPassiveSkillCoolingTime;
                }
            }
        }

        public float CurThirdPassiveSkillCoolingTime
        {
            get
            {
                return curThirdPassiveSkillCoolingTime;
            }

            set
            {
                curThirdPassiveSkillCoolingTime = value;
                if (curThirdPassiveSkillCoolingTime < 0)
                {
                    curThirdPassiveSkillCoolingTime = 0;
                }
                if (curThirdPassiveSkillCoolingTime > maxThirdPassiveSkillCoolingTime)
                {
                    curThirdPassiveSkillCoolingTime = maxThirdPassiveSkillCoolingTime;
                }
            }
        }

        public float TotalRecivedDamage
        {
            get
            {
                return totalRecivedDamage;
            }

            set
            {
                totalRecivedDamage = value;
                if (totalRecivedDamage < 0)
                {
                    totalRecivedDamage = 0;
                }
            }
        }

        public ElementType FinalDamageElemType
        {
            get
            {
                return finalDamageElemType;
            }

            set
            {
                finalDamageElemType = value;
            }
        }

        #endregion

        #region Water

        public float OriginalWaterStrength
        {
            get
            {
                return originalWaterStrength;
            }

            set
            {
                originalWaterStrength = value;
            }
        }

        public float CurWaterStrength
        {
            get
            {
                return curWaterStrength;
            }

            set
            {
                curWaterStrength = value;
                if (curWaterStrength < 0)
                {
                    curWaterStrength = 1;
                }
                if (curWaterStrength > (float)FightHeroMaxDefine.MaxWaterStrength)
                {
                    curWaterStrength = (float)FightHeroMaxDefine.MaxWaterStrength;
                }
            }
        }

        public float WaterStrengthDeviant
        {
            get
            {
                return waterStrengthDeviant;
            }

            set
            {
                waterStrengthDeviant = value;
            }
        }

        public float OriginalWaterResistCoefficient
        {
            get
            {
                return originalWaterResistCoefficient;
            }

            set
            {
                originalWaterResistCoefficient = value;
                if (originalWaterResistCoefficient < 0)
                {
                    originalWaterResistCoefficient = 0.1f;
                }
            }
        }

        public float CurWaterResistCoefficient
        {
            get
            {
                return curWaterResistCoefficient;
            }

            set
            {
                curWaterResistCoefficient = value;
                if (curWaterResistCoefficient < 0)
                {
                    curWaterResistCoefficient = 0.1f;
                }
                if (curWaterResistCoefficient > (float)FightHeroMaxDefine.MaxWaterResistCoeficient)
                {
                    curWaterResistCoefficient = (float)FightHeroMaxDefine.MaxWaterResistCoeficient;
                }
            }
        }

        public float WaterResistCoefficientDeviant
        {
            get
            {
                return waterResistCoefficientDeviant;
            }

            set
            {
                waterResistCoefficientDeviant = value;
            }
        }

        public float OriginalWaterDamageCoefficient
        {
            get
            {
                return originalWaterDamageCoefficient;
            }

            set
            {
                originalWaterDamageCoefficient = value;
                if (originalWaterDamageCoefficient < 0)
                {
                    originalWaterDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurWaterDamageCoefficient
        {
            get
            {
                return curWaterDamageCoefficient;
            }

            set
            {
                curWaterDamageCoefficient = value;
                if (curWaterDamageCoefficient < 0)
                {
                    curWaterDamageCoefficient = 0.1f;
                }
                if (curWaterDamageCoefficient > (float)FightHeroMaxDefine.MaxWaterDamageCoeficient)
                {
                    curWaterDamageCoefficient = (float)FightHeroMaxDefine.MaxWaterDamageCoeficient;
                }
            }
        }

        public float WaterDamageCoefficientDeviant
        {
            get
            {
                return waterDamageCoefficientDeviant;
            }

            set
            {
                waterDamageCoefficientDeviant = value;
            }
        }

        public float WaterTotalRecivedDamage
        {
            get
            {
                return waterTotalRecivedDamage;
            }

            set
            {
                waterTotalRecivedDamage = value;
                if (waterTotalRecivedDamage < 0)
                {
                    waterTotalRecivedDamage = 0;
                }
            }
        }

        #endregion

        #region Fire

        public float OriginalFireStrength
        {
            get
            {
                return originalFireStrength;
            }

            set
            {
                originalFireStrength = value;
            }
        }

        public float CurFireStrength
        {
            get
            {
                return curFireStrength;
            }

            set
            {
                curFireStrength = value;
                if (curFireStrength < 0)
                {
                    curFireStrength = 1;
                }
                if (curFireStrength > (float)FightHeroMaxDefine.MaxFireStrength)
                {
                    curFireStrength = (float)FightHeroMaxDefine.MaxFireStrength;
                }
            }
        }

        public float FireStrengthDeviant
        {
            get
            {
                return fireStrengthDeviant;
            }

            set
            {
                fireStrengthDeviant = value;
            }
        }

        public float OriginalFireResistCoefficient
        {
            get
            {
                return originalFireResistCoefficient;
            }

            set
            {
                originalFireResistCoefficient = value;
                if (originalFireResistCoefficient < 0)
                {
                    originalFireResistCoefficient = 0.1f;
                }
            }
        }

        public float CurFireResistCoefficient
        {
            get
            {
                return curFireResistCoefficient;
            }

            set
            {
                curFireResistCoefficient = value;
                if (curFireResistCoefficient < 0)
                {
                    curFireResistCoefficient = 0.1f;
                }
                if (curFireResistCoefficient > (float)FightHeroMaxDefine.MaxFireResistCoeficient)
                {
                    curFireResistCoefficient = (float)FightHeroMaxDefine.MaxFireResistCoeficient;
                }
            }
        }

        public float FireResistCoefficientDeviant
        {
            get
            {
                return fireResistCoefficientDeviant;
            }

            set
            {
                fireResistCoefficientDeviant = value;
            }
        }

        public float OriginalFireDamageCoefficient
        {
            get
            {
                return originalFireDamageCoefficient;
            }

            set
            {
                originalFireDamageCoefficient = value;
                if (originalFireDamageCoefficient < 0)
                {
                    originalFireDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurFireDamageCoefficient
        {
            get
            {
                return curFireDamageCoefficient;
            }

            set
            {
                curFireDamageCoefficient = value;
                if (curFireDamageCoefficient < 0)
                {
                    curFireDamageCoefficient = 0.1f;
                }
                if (curFireDamageCoefficient > (float)FightHeroMaxDefine.MaxFireDamageCoeficient)
                {
                    curFireDamageCoefficient = (float)FightHeroMaxDefine.MaxFireDamageCoeficient;
                }
            }
        }

        public float FireDamageCoefficientDeviant
        {
            get
            {
                return fireDamageCoefficientDeviant;
            }

            set
            {
                fireDamageCoefficientDeviant = value;
            }
        }

        public float FireTotalRecivedDamage
        {
            get
            {
                return fireTotalRecivedDamage;
            }

            set
            {
                fireTotalRecivedDamage = value;
            }
        }

        #endregion

        #region Thunder

        public float OriginalThunderStrength
        {
            get
            {
                return originalThunderStrength;
            }

            set
            {
                originalThunderStrength = value;
            }
        }

        public float CurThunderStrength
        {
            get
            {
                return curThunderStrength;
            }

            set
            {
                curThunderStrength = value;
                if (curThunderStrength < 0)
                {
                    curThunderStrength = 1;
                }
                if (curThunderStrength > (float)FightHeroMaxDefine.MaxThunderStrenght)
                {
                    curThunderStrength = (float)FightHeroMaxDefine.MaxThunderStrenght;
                }
            }
        }

        public float ThunderStrengthDeviant
        {
            get
            {
                return thunderStrengthDeviant;
            }

            set
            {
                thunderStrengthDeviant = value;
            }
        }

        public float OriginalThunderResistCoefficient
        {
            get
            {
                return originalThunderResistCoefficient;
            }

            set
            {
                originalThunderResistCoefficient = value;
                if (originalThunderResistCoefficient < 0)
                {
                    originalThunderResistCoefficient = 0.1f;
                }
            }
        }

        public float CurThunderResistCoefficient
        {
            get
            {
                return curThunderResistCoefficient;
            }

            set
            {
                curThunderResistCoefficient = value;
                if (curThunderResistCoefficient < 0)
                {
                    curThunderResistCoefficient = 0.1f;
                }
                if (curThunderResistCoefficient > (float)FightHeroMaxDefine.MaxThunderResistCoeficient)
                {
                    curThunderResistCoefficient = (float)FightHeroMaxDefine.MaxThunderResistCoeficient;
                }
            }
        }

        public float ThunderResistCoefficientDeviant
        {
            get
            {
                return thunderResistCoefficientDeviant;
            }

            set
            {
                thunderResistCoefficientDeviant = value;
            }
        }

        public float OriginalThunderDamageCoefficient
        {
            get
            {
                return originalThunderDamageCoefficient;
            }

            set
            {
                originalThunderDamageCoefficient = value;
                if (originalThunderDamageCoefficient < 0)
                {
                    originalThunderDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurThunderDamageCoefficient
        {
            get
            {
                return curThunderDamageCoefficient;
            }

            set
            {
                curThunderDamageCoefficient = value;
                if (curThunderDamageCoefficient < 0)
                {
                    curThunderDamageCoefficient = 0.1f;
                }
                if (curThunderDamageCoefficient > (float)FightHeroMaxDefine.MaxThunderDamageCoeficient)
                {
                    curThunderDamageCoefficient = (float)FightHeroMaxDefine.MaxThunderDamageCoeficient;
                }
            }
        }

        public float ThunderDamageCoefficientDeviant
        {
            get
            {
                return thunderDamageCoefficientDeviant;
            }

            set
            {
                thunderDamageCoefficientDeviant = value;
            }
        }

        public float ThunderTotalRecivedDamage
        {
            get
            {
                return thunderTotalRecivedDamage;
            }

            set
            {
                thunderTotalRecivedDamage = value;
            }
        }

        #endregion

        #region Wind

        public float OriginalWindStrength
        {
            get
            {
                return originalWindStrength;
            }

            set
            {
                originalWindStrength = value;
            }
        }

        public float CurWindStrength
        {
            get
            {
                return curWindStrength;
            }

            set
            {
                curWindStrength = value;
                if (curWindStrength < 0)
                {
                    curWindStrength = 1;
                }
                if (curWindStrength > (float)FightHeroMaxDefine.MaxWindStrength)
                {
                    curWindStrength = (float)FightHeroMaxDefine.MaxWindStrength;
                }
            }
        }

        public float WindStrengthDeviant
        {
            get
            {
                return windStrengthDeviant;
            }

            set
            {
                windStrengthDeviant = value;
            }
        }

        public float OriginalWindResistCoefficient
        {
            get
            {
                return originalWindResistCoefficient;
            }

            set
            {
                originalWindResistCoefficient = value;
                if (originalWindResistCoefficient < 0)
                {
                    originalWindResistCoefficient = 0.1f;
                }
            }
        }

        public float CurWindResistCoefficient
        {
            get
            {
                return curWindResistCoefficient;
            }

            set
            {
                curWindResistCoefficient = value;
                if (curWindResistCoefficient < 0)
                {
                    curWindResistCoefficient = 0.1f;
                }
                if (curWindResistCoefficient > (float)FightHeroMaxDefine.MaxWindResistCoeficient)
                {
                    curWindResistCoefficient = (float)FightHeroMaxDefine.MaxWindResistCoeficient;
                }
            }
        }

        public float WindResistCoefficientDeviant
        {
            get
            {
                return windResistCoefficientDeviant;
            }

            set
            {
                windResistCoefficientDeviant = value;
            }
        }

        public float OriginalWindDamageCoefficient
        {
            get
            {
                return originalWindDamageCoefficient;
            }

            set
            {
                originalWindDamageCoefficient = value;
                if (originalWindDamageCoefficient < 0)
                {
                    originalWindDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurWindDamageCoefficient
        {
            get
            {
                return curWindDamageCoefficient;
            }

            set
            {
                curWindDamageCoefficient = value;
                if (curWindDamageCoefficient < 0)
                {
                    curWindDamageCoefficient = 0.1f;
                }
                if (curWindDamageCoefficient > (float)FightHeroMaxDefine.MaxWindDamageCoeficient)
                {
                    curWindDamageCoefficient = (float)FightHeroMaxDefine.MaxWindDamageCoeficient;
                }
            }
        }

        public float WindDamageCoefficientDeviant
        {
            get
            {
                return windDamageCoefficientDeviant;
            }

            set
            {
                windDamageCoefficientDeviant = value;
            }
        }

        public float WindTotalRecivedDamage
        {
            get
            {
                return windTotalRecivedDamage;
            }

            set
            {
                windTotalRecivedDamage = value;
            }
        }

        #endregion

        #region Earth

        public float OriginalEarthStrength
        {
            get
            {
                return originalEarthStrength;
            }

            set
            {
                originalEarthStrength = value;
            }
        }

        public float CurEarthStrength
        {
            get
            {
                return curEarthStrength;
            }

            set
            {
                curEarthStrength = value;
                if (curEarthStrength < 0)
                {
                    curEarthStrength = 1;
                }
                if (curEarthStrength > (float)FightHeroMaxDefine.MaxEarthStrength)
                {
                    curEarthStrength = (float)FightHeroMaxDefine.MaxEarthStrength;
                }
            }
        }

        public float EarthStrengthDeviant
        {
            get
            {
                return earthStrengthDeviant;
            }

            set
            {
                earthStrengthDeviant = value;
            }
        }

        public float OriginalEarthResistCoefficient
        {
            get
            {
                return originalEarthResistCoefficient;
            }

            set
            {
                originalEarthResistCoefficient = value;
                if (originalEarthResistCoefficient < 0)
                {
                    originalEarthResistCoefficient = 0.1f;
                }
            }
        }

        public float CurEarthResistCoefficient
        {
            get
            {
                return curEarthResistCoefficient;
            }

            set
            {
                curEarthResistCoefficient = value;
                if (curEarthResistCoefficient < 0)
                {
                    curEarthResistCoefficient = 0.1f;
                }
                if (curEarthResistCoefficient > (float)FightHeroMaxDefine.MaxEarthResistCoeficient)
                {
                    curEarthResistCoefficient = (float)FightHeroMaxDefine.MaxEarthResistCoeficient;
                }
            }
        }

        public float EarthResistCoefficientDeviant
        {
            get
            {
                return earthResistCoefficientDeviant;
            }

            set
            {
                earthResistCoefficientDeviant = value;
            }
        }

        public float OriginalEarthDamageCoefficient
        {
            get
            {
                return originalEarthDamageCoefficient;
            }

            set
            {
                originalEarthDamageCoefficient = value;
                if (originalEarthDamageCoefficient < 0)
                {
                    originalEarthDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurEarthDamageCoefficient
        {
            get
            {
                return curEarthDamageCoefficient;
            }

            set
            {
                curEarthDamageCoefficient = value;
                if (curEarthDamageCoefficient < 0)
                {
                    curEarthDamageCoefficient = 0.1f;
                }
                if (curEarthDamageCoefficient > (float)FightHeroMaxDefine.MaxEarthDamageCoeficient)
                {
                    curEarthDamageCoefficient = (float)FightHeroMaxDefine.MaxEarthDamageCoeficient;
                }
            }
        }

        public float EarthDamageCoefficientDeviant
        {
            get
            {
                return earthDamageCoefficientDeviant;
            }

            set
            {
                earthDamageCoefficientDeviant = value;
            }
        }

        public float EarthTotalRecivedDamage
        {
            get
            {
                return earthTotalRecivedDamage;
            }

            set
            {
                earthTotalRecivedDamage = value;
            }
        }

        #endregion

        #region Dark

        public float OriginalDarkStrength
        {
            get
            {
                return originalDarkStrength;
            }

            set
            {
                originalDarkStrength = value;
            }
        }

        public float CurDarkStrength
        {
            get
            {
                return curDarkStrength;
            }

            set
            {
                curDarkStrength = value;
                if (curDarkStrength < 0)
                {
                    curDarkStrength = 1;
                }
                if (curDarkStrength > (float)FightHeroMaxDefine.MaxDarkStrength)
                {
                    curDarkStrength = (float)FightHeroMaxDefine.MaxDarkStrength;
                }
            }
        }

        public float DarkStrengthDeviant
        {
            get
            {
                return darkStrengthDeviant;
            }

            set
            {
                darkStrengthDeviant = value;
            }
        }

        public float OriginalDarkResistCoefficient
        {
            get
            {
                return originalDarkResistCoefficient;
            }

            set
            {
                originalDarkResistCoefficient = value;
                if (originalDarkResistCoefficient < 0)
                {
                    originalDarkResistCoefficient = 0.1f;
                }
            }
        }

        public float CurDarkResistCoefficient
        {
            get
            {
                return curDarkResistCoefficient;
            }

            set
            {
                curDarkResistCoefficient = value;
                if (curDarkResistCoefficient < 0)
                {
                    curDarkResistCoefficient = 0.1f;
                }
                if (curDarkResistCoefficient > (float)FightHeroMaxDefine.MaxDarkResistCoeficient)
                {
                    curDarkResistCoefficient = (float)FightHeroMaxDefine.MaxDarkResistCoeficient;
                }
            }
        }

        public float DarkResistCoefficientDeviant
        {
            get
            {
                return darkResistCoefficientDeviant;
            }

            set
            {
                darkResistCoefficientDeviant = value;
            }
        }

        public float OriginalDarkDamageCoefficient
        {
            get
            {
                return originalDarkDamageCoefficient;
            }

            set
            {
                originalDarkDamageCoefficient = value;
                if (originalDarkDamageCoefficient < 0)
                {
                    originalDarkDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurDarkDamageCoefficient
        {
            get
            {
                return curDarkDamageCoefficient;
            }

            set
            {
                curDarkDamageCoefficient = value;
                if (curDarkDamageCoefficient < 0)
                {
                    curDarkDamageCoefficient = 0.1f;
                }
                if (curDarkDamageCoefficient > (float)FightHeroMaxDefine.MaxDarkDamageCoeficient)
                {
                    curDarkDamageCoefficient = (float)FightHeroMaxDefine.MaxDarkDamageCoeficient;
                }
            }
        }

        public float DarkDamageCoefficientDeviant
        {
            get
            {
                return darkDamageCoefficientDeviant;
            }

            set
            {
                darkDamageCoefficientDeviant = value;
            }
        }

        public float DarkTotalRecivedDamage
        {
            get
            {
                return darkTotalRecivedDamage;
            }

            set
            {
                darkTotalRecivedDamage = value;
            }
        }

        #endregion

        #region AllElem

        public float OriginalElemResistCoefficient
        {
            get
            {
                return originalElemResistCoefficient;
            }

            set
            {
                originalElemResistCoefficient = value;
                if (originalElemResistCoefficient < 0)
                {
                    originalElemResistCoefficient = 0.1f;
                }
            }
        }

        public float CurElemResistCoefficient
        {
            get
            {
                return curElemResistCoefficient;
            }

            set
            {
                curElemResistCoefficient = value;
                if (curElemResistCoefficient < 0)
                {
                    curElemResistCoefficient = 0.1f;
                }
                if (curElemResistCoefficient > (float)FightHeroMaxDefine.MaxElemResistCoeficient)
                {
                    curElemResistCoefficient = (float)FightHeroMaxDefine.MaxElemResistCoeficient;
                }
            }
        }

        public float ElemResistCoefficientDeviant
        {
            get
            {
                return elemResistCoefficientDeviant;
            }

            set
            {
                elemResistCoefficientDeviant = value;
            }
        }

        public float OriginalElemDamageCoefficient
        {
            get
            {
                return originalElemDamageCoefficient;
            }

            set
            {
                originalElemDamageCoefficient = value;
                if (originalElemDamageCoefficient < 0)
                {
                    originalElemDamageCoefficient = 0.1f;
                }
            }
        }

        public float CurElemDamageCoefficient
        {
            get
            {
                return curElemDamageCoefficient;
            }

            set
            {
                curElemDamageCoefficient = value;
                if (curElemDamageCoefficient < 0)
                {
                    curElemDamageCoefficient = 0.1f;
                }
                if (curElemDamageCoefficient > (float)FightHeroMaxDefine.MaxElemDamageCoeficient)
                {
                    curElemDamageCoefficient = (float)FightHeroMaxDefine.MaxElemDamageCoeficient;
                }
            }
        }

        public float ElemDamageCoefficientDeviant
        {
            get
            {
                return elemDamageCoefficientDeviant;
            }

            set
            {
                elemDamageCoefficientDeviant = value;
            }
        }

        public bool IsAssistSkill
        {
            get
            {
                return isAssistSkill;
            }

            set
            {
                isAssistSkill = value;
            }
        }

        public float OriginalAssistSkillCoolingTime
        {
            get
            {
                return originalAssistSkillCoolingTime;
            }

            set
            {
                originalAssistSkillCoolingTime = value;
            }
        }

        public float MaxAssistSkillCoolingTime
        {
            get
            {
                return maxAssistSkillCoolingTime;
            }

            set
            {
                maxAssistSkillCoolingTime = value;
            }
        }

        public float CurAssistSkillCoolingTime
        {
            get
            {
                return curAssistSkillCoolingTime;
            }

            set
            {
                curAssistSkillCoolingTime = value;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 重置所有偏移值
        /// </summary>
        public void ResetAllDeviant()
        {
            maxHpDeviant = 0;
            shieldDeviant = 0;
            speedDeviant = 0;
            damageResistDeviant = damageParryDeviant = chaosDamageResistCoefficientDeviant = 0;
            chaosDamageCoefficientDeviant = dodgeCoefficientDeviant = criticalCoefficientDeviant = 0;
            critMutipleCoefficientDeviant = damagePierceCoefficientDeviant = damageIncrementValueDeviant = 0;
            suckBloodCoefficientDeviant = 0;
            maxApPointDeviant = 0;
            isBuffInstantDeviant = isInvinsibleDeviant = isMagicImmuneDeviant = false;
            canDrawCardDeviant = canDrawWaterCardDeviant = canDrawFireCardDeviant = canDrawThunderCardDeviant = true;
            canDrawWindCardDeviant = canDrawEarthCardDeviant = canDrawDarkCardDeviant = true;

            waterStrengthDeviant = waterResistCoefficientDeviant = waterDamageCoefficientDeviant = 0;
            fireStrengthDeviant = fireResistCoefficientDeviant = fireDamageCoefficientDeviant = 0;
            thunderStrengthDeviant = thunderResistCoefficientDeviant = thunderDamageCoefficientDeviant = 0;
            windStrengthDeviant = windResistCoefficientDeviant = windDamageCoefficientDeviant = 0;
            earthStrengthDeviant = earthResistCoefficientDeviant = earthDamageCoefficientDeviant = 0;
            darkStrengthDeviant = darkResistCoefficientDeviant = darkDamageCoefficientDeviant = 0;
            elemResistCoefficientDeviant = elemDamageCoefficientDeviant = 0;
        }

        /// <summary>
        /// 触发所有偏移值效果
        /// </summary>
        public void SetAllDeviantAction()
        {
            MaxHp = OriginalHp + MaxHpDeviant;
            CurShield = OriginalShield + ShieldDeviant;
            CurSpeed = originalSpeed + SpeedDeviant;
            CurDamageResist = originalDamageResist + DamageResistDeviant;
            CurDamageParry = originalDamageParry + DamageParryDeviant;
            CurChaosDamageResistCoefficient = originalChaosDamageResistCoefficient + ChaosDamageResistCoefficientDeviant;
            CurChaosDamageCoefficient = originalChaosDamageCoefficient + ChaosDamageCoefficientDeviant;
            CurDodgeCoefficient = originalDodgeCoefficient + DodgeCoefficientDeviant;
            CurCriticalCoefficient = originalCriticalCoefficient + CriticalCoefficientDeviant;
            CurCritMutipleCoefficient = originalCritMutipleCoefficient + CritMutipleCoefficientDeviant;
            CurDamagePierceCoefficient = originalDamagePierceCoefficient + DamagePierceCoefficientDeviant;
            CurDamageIncrementValue = originalDamageIncrementValue + DamageIncrementValueDeviant;
            CurSuckBloodCoefficient = originalSuckBloodCoefficient + SuckBloodCoefficientDeviant;
            MaxApPoint = 5 + MaxApPointDeviant;
            CurIsBuffInstant = isBuffInstantDeviant;
            CurIsInvinsible = isInvinsibleDeviant;
            CurIsMagicImmune = isMagicImmuneDeviant;
            CurCanDrawCard = canDrawCardDeviant;
            CurCanDrawWaterCard = canDrawWaterCardDeviant;
            CurCanDrawFireCard = canDrawFireCardDeviant;
            CurCanDrawThunderCard = canDrawThunderCardDeviant;
            CurCanDrawWindCard = canDrawWindCardDeviant;
            CurCanDrawEarthCard = canDrawEarthCardDeviant;
            CurCanDrawDarkCard = canDrawDarkCardDeviant;

            CurWaterStrength = originalWaterStrength + WaterStrengthDeviant;
            CurWaterResistCoefficient = originalWaterResistCoefficient + WaterResistCoefficientDeviant;
            CurWaterDamageCoefficient = originalWaterDamageCoefficient + WaterDamageCoefficientDeviant;

            CurFireStrength = originalFireStrength + FireStrengthDeviant;
            CurFireResistCoefficient = originalFireResistCoefficient + FireResistCoefficientDeviant;
            CurFireDamageCoefficient = originalFireDamageCoefficient + FireDamageCoefficientDeviant;

            CurThunderStrength = originalThunderStrength + ThunderStrengthDeviant;
            CurThunderResistCoefficient = originalThunderResistCoefficient + ThunderResistCoefficientDeviant;
            CurThunderDamageCoefficient = originalThunderDamageCoefficient + ThunderDamageCoefficientDeviant;

            CurWindStrength = originalWindStrength + WindStrengthDeviant;
            CurWindResistCoefficient = originalWindResistCoefficient + WindResistCoefficientDeviant;
            CurWindDamageCoefficient = originalWindDamageCoefficient + WindDamageCoefficientDeviant;

            CurEarthStrength = originalEarthStrength + EarthStrengthDeviant;
            CurEarthResistCoefficient = originalEarthResistCoefficient + EarthResistCoefficientDeviant;
            CurEarthDamageCoefficient = originalEarthDamageCoefficient + EarthDamageCoefficientDeviant;

            CurDarkStrength = originalDarkStrength + DarkStrengthDeviant;
            CurDarkResistCoefficient = originalDarkResistCoefficient + DarkResistCoefficientDeviant;
            CurDarkDamageCoefficient = originalDarkDamageCoefficient + DarkDamageCoefficientDeviant;

            CurElemResistCoefficient = originalElemResistCoefficient + ElemResistCoefficientDeviant;
            CurElemDamageCoefficient = originalElemDamageCoefficient + ElemDamageCoefficientDeviant;
        }

        /// <summary>
        /// 获得选取的元素卡
        /// </summary>
        private void SetLevelCard(Dictionary<string, bool> dict)
        {
            if(dict == null)
            {
                return;
            }
            if(dict["water"] == true)
            {
                cardGroup.GetWaterPercent = 1;
            }
            if (dict["fire"] == true)
            {
                cardGroup.GetFirePercent = 1;
            }
            if (dict["wind"] == true)
            {
                cardGroup.GetWindPercent = 1;
            }
            if (dict["thunder"] == true)
            {
                cardGroup.GetThunderPercent = 1;
            }
            if (dict["earth"] == true)
            {
                cardGroup.GetEarthPercent = 1;
            }
            if (dict["dark"] == true)
            {
                cardGroup.GetDarkPercent = 1;
            }
        }

        public FightHero()
        {
            originalHp = maxHp = curHp = 200;
            maxHpDeviant = 0;
            originalShield = maxShield = curShield = 0;
            shieldDeviant = 0;
            curSpeed = originalSpeed = 100;
            speedDeviant = 0;
            originalDamageResist = curDamageResist = 0;
            damageResistDeviant = 0;
            originalDamageParry = curDamageParry = 0;
            damageParryDeviant = 0;
            originalChaosDamageResistCoefficient = curChaosDamageResistCoefficient = 1;
            chaosDamageResistCoefficientDeviant = 0;
            originalChaosDamageCoefficient = curChaosDamageCoefficient = 1;
            chaosDamageCoefficientDeviant = 0;
            curSliderValue = 0;
            curApPoint = 0;
            maxApPoint = 5;
            maxApPointDeviant = 0;
            originalDodgeCoefficient = curDodgeCoefficient = 0;
            dodgeCoefficientDeviant = 0;
            originalCriticalCoefficient = curCriticalCoefficient = 0;
            criticalCoefficientDeviant = 0;
            originalCritMutipleCoefficient = curCritMutipleCoefficient = 2;
            critMutipleCoefficientDeviant = 0;
            originalDamagePierceCoefficient = curDamagePierceCoefficient = 0;
            damagePierceCoefficientDeviant = 0;
            originalDamageIncrementValue = curDamageIncrementValue = 0;
            damageIncrementValueDeviant = 0;
            originalSuckBloodCoefficient = curSuckBloodCoefficient = 0;
            suckBloodCoefficientDeviant = 0;
            originalIsBuffInstant = curIsBuffInstant = false;
            isBuffInstantDeviant = false;
            originalIsInvinsible = curIsInvinsible = false;
            isInvinsibleDeviant = false;
            originalIsMagicImmune = curIsMagicImmune = false;
            isMagicImmuneDeviant = false;
            CurCanDrawCard = canDrawCardDeviant = true;
            CurCanDrawWaterCard = canDrawWaterCardDeviant = true;
            CurCanDrawFireCard = canDrawFireCardDeviant = true;
            CurCanDrawThunderCard = canDrawThunderCardDeviant = true;
            CurCanDrawWindCard = canDrawWindCardDeviant = true;
            CurCanDrawEarthCard = canDrawEarthCardDeviant = true;
            CurCanDrawDarkCard = canDrawDarkCardDeviant = true;

            isActiveSkill = true;
            originalActiveSkillCoolingTime = maxActiveSkillCoolingTime = curActiveSkillCoolingTime = 20;
            isAssistSkill = true;
            originalAssistSkillCoolingTime = maxAssistSkillCoolingTime = curAssistSkillCoolingTime = 10;
            isFirstPassiveSkill = true;
            originalFirstPassiveSkillCoolingTime = maxFirstPassiveSkillCoolingTime = curFirstPassiveSkillCoolingTime = 10;
            isSecondPassiveSkill = true;
            originalSecondPassiveSkillCoolingTime = maxSecondPassiveSkillCoolingTime = curSecondPassiveSkillCoolingTime = 10;
            isThirdPassiveSkill = true;
            originalThirdPassiveSkillCoolingTime = maxThirdPassiveSkillCoolingTime = curThirdPassiveSkillCoolingTime = 10;
            totalRecivedDamage = 0;
            finalDamageElemType = ElementType.Chaos;

            cardGroup = new CardGroup();
            damageToEnemyRecords = new List<DamageRecord>();
            damageFromEnemyRecords = new List<DamageRecord>();
            buffDictionary = new Dictionary<BuffClass, BaseBuff>();
            //SetLevelCard(dict);

            originalWaterStrength = curWaterStrength = 100;
            waterStrengthDeviant = 0;
            originalWaterResistCoefficient = curWaterResistCoefficient = 1;
            waterResistCoefficientDeviant = 0;
            originalWaterDamageCoefficient = curWaterDamageCoefficient = 1;
            waterDamageCoefficientDeviant = 0;
            waterTotalRecivedDamage = 0;

            originalFireStrength = curFireStrength = 100;
            fireStrengthDeviant = 0;
            originalFireResistCoefficient = curFireResistCoefficient = 1;
            fireResistCoefficientDeviant = 0;
            originalFireDamageCoefficient = curFireDamageCoefficient = 1;
            fireDamageCoefficientDeviant = 0;
            fireTotalRecivedDamage = 0;

            originalThunderStrength = curThunderStrength = 100;
            thunderStrengthDeviant = 0;
            originalThunderResistCoefficient = curThunderResistCoefficient = 1;
            thunderResistCoefficientDeviant = 0;
            originalThunderDamageCoefficient = curThunderDamageCoefficient = 1;
            thunderDamageCoefficientDeviant = 0;
            thunderTotalRecivedDamage = 0;

            originalWindStrength = curWindStrength = 100;
            windStrengthDeviant = 0;
            originalWindResistCoefficient = curWindResistCoefficient = 1;
            windResistCoefficientDeviant = 0;
            originalWindDamageCoefficient = curWindDamageCoefficient = 1;
            windDamageCoefficientDeviant = 0;
            windTotalRecivedDamage = 0;

            originalEarthStrength = curEarthStrength = 100;
            earthStrengthDeviant = 0;
            originalEarthResistCoefficient = curEarthResistCoefficient = 1;
            earthResistCoefficientDeviant = 0;
            originalEarthDamageCoefficient = curEarthDamageCoefficient = 1;
            earthDamageCoefficientDeviant = 0;
            earthTotalRecivedDamage = 0;

            originalDarkStrength = curDarkStrength = 100;
            darkStrengthDeviant = 0;
            originalDarkResistCoefficient = curDarkResistCoefficient = 1;
            darkResistCoefficientDeviant = 0;
            originalDarkDamageCoefficient = curDarkDamageCoefficient = 1;
            darkDamageCoefficientDeviant = 0;
            darkTotalRecivedDamage = 0;

            originalElemResistCoefficient = curElemResistCoefficient = 1;
            elemResistCoefficientDeviant = 0;
            originalElemDamageCoefficient = curElemDamageCoefficient = 1;
            elemDamageCoefficientDeviant = 0;
        }

        public FightHero(HeroCard heroCard, Dictionary<string, bool> dict = null)
        {
            this.heroCard = heroCard;

            originalHp = maxHp = curHp = heroCard.Hero_hp;
            maxHpDeviant = 0;
            originalShield = maxShield = curShield = 0;
            shieldDeviant = 0;
            curSpeed = originalSpeed = heroCard.Hero_speed;
            speedDeviant = 0;
            originalDamageResist = curDamageResist = 0;
            damageResistDeviant = 0;
            originalDamageParry = curDamageParry = 0;
            damageParryDeviant = 0;
            originalChaosDamageResistCoefficient = curChaosDamageResistCoefficient = 1;
            chaosDamageResistCoefficientDeviant = 0;
            originalChaosDamageCoefficient = curChaosDamageCoefficient = 1;
            chaosDamageCoefficientDeviant = 0;
            curSliderValue = 0;
            curApPoint = 0;
            maxApPoint = 5;
            maxApPointDeviant = 0;
            originalDodgeCoefficient = curDodgeCoefficient = 0;
            dodgeCoefficientDeviant = 0;
            originalCriticalCoefficient = curCriticalCoefficient = 0;
            criticalCoefficientDeviant = 0;
            originalCritMutipleCoefficient = curCritMutipleCoefficient = 2;
            critMutipleCoefficientDeviant = 0;
            originalDamagePierceCoefficient = curDamagePierceCoefficient = 0;
            damagePierceCoefficientDeviant = 0;
            originalDamageIncrementValue = curDamageIncrementValue = 0;
            damageIncrementValueDeviant = 0;
            originalSuckBloodCoefficient = curSuckBloodCoefficient = 0;
            suckBloodCoefficientDeviant = 0;
            originalIsBuffInstant = curIsBuffInstant = false;
            isBuffInstantDeviant = false;
            originalIsInvinsible = curIsInvinsible = false;
            isInvinsibleDeviant = false;
            originalIsMagicImmune = curIsMagicImmune = false;
            isMagicImmuneDeviant = false;
            CurCanDrawCard = canDrawCardDeviant = true;
            CurCanDrawWaterCard = canDrawWaterCardDeviant = true;
            CurCanDrawFireCard = canDrawFireCardDeviant = true;
            CurCanDrawThunderCard = canDrawThunderCardDeviant = true;
            CurCanDrawWindCard = canDrawWindCardDeviant = true;
            CurCanDrawEarthCard = canDrawEarthCardDeviant = true;
            CurCanDrawDarkCard = canDrawDarkCardDeviant = true;

            isActiveSkill = true;
            originalActiveSkillCoolingTime = maxActiveSkillCoolingTime = curActiveSkillCoolingTime = heroCard.Action_skill.Skill_cd;
            isAssistSkill = true;
            originalAssistSkillCoolingTime = maxAssistSkillCoolingTime = curAssistSkillCoolingTime = heroCard.Staff_skill_self.Skill_cd;
            isFirstPassiveSkill = true;
            originalFirstPassiveSkillCoolingTime = maxFirstPassiveSkillCoolingTime = curFirstPassiveSkillCoolingTime = heroCard.Prossive_skill_1.Skill_cd;
            isSecondPassiveSkill = true;
            originalSecondPassiveSkillCoolingTime = maxSecondPassiveSkillCoolingTime = curSecondPassiveSkillCoolingTime = heroCard.Prossive_skill_2.Skill_cd;
            isThirdPassiveSkill = true;
            originalThirdPassiveSkillCoolingTime = maxThirdPassiveSkillCoolingTime = curThirdPassiveSkillCoolingTime = heroCard.Prossive_skill_3.Skill_cd;
            totalRecivedDamage = 0;
            finalDamageElemType = ElementType.Chaos;

            cardGroup = new CardGroup();
            damageToEnemyRecords = new List<DamageRecord>();
            damageFromEnemyRecords = new List<DamageRecord>();
            buffDictionary = new Dictionary<BuffClass, BaseBuff>();
            SetLevelCard(dict);

            originalWaterStrength = curWaterStrength = heroCard.Water;
            waterStrengthDeviant = 0;
            originalWaterResistCoefficient = curWaterResistCoefficient = 1;
            waterResistCoefficientDeviant = 0;
            originalWaterDamageCoefficient = curWaterDamageCoefficient = 1;
            waterDamageCoefficientDeviant = 0;
            waterTotalRecivedDamage = 0;

            originalFireStrength = curFireStrength = heroCard.Fire;
            fireStrengthDeviant = 0;
            originalFireResistCoefficient = curFireResistCoefficient = 1;
            fireResistCoefficientDeviant = 0;
            originalFireDamageCoefficient = curFireDamageCoefficient = 1;
            fireDamageCoefficientDeviant = 0;
            fireTotalRecivedDamage = 0;

            originalThunderStrength = curThunderStrength = heroCard.Thunder;
            thunderStrengthDeviant = 0;
            originalThunderResistCoefficient = curThunderResistCoefficient = 1;
            thunderResistCoefficientDeviant = 0;
            originalThunderDamageCoefficient = curThunderDamageCoefficient = 1;
            thunderDamageCoefficientDeviant = 0;
            thunderTotalRecivedDamage = 0;

            originalWindStrength = curWindStrength = heroCard.Wind;
            windStrengthDeviant = 0;
            originalWindResistCoefficient = curWindResistCoefficient = 1;
            windResistCoefficientDeviant = 0;
            originalWindDamageCoefficient = curWindDamageCoefficient = 1;
            windDamageCoefficientDeviant = 0;
            windTotalRecivedDamage = 0;

            originalEarthStrength = curEarthStrength = heroCard.Soil;
            earthStrengthDeviant = 0;
            originalEarthResistCoefficient = curEarthResistCoefficient = 1;
            earthResistCoefficientDeviant = 0;
            originalEarthDamageCoefficient = curEarthDamageCoefficient = 1;
            earthDamageCoefficientDeviant = 0;
            earthTotalRecivedDamage = 0;

            originalDarkStrength = curDarkStrength = heroCard.Dark;
            darkStrengthDeviant = 0;
            originalDarkResistCoefficient = curDarkResistCoefficient = 1;
            darkResistCoefficientDeviant = 0;
            originalDarkDamageCoefficient = curDarkDamageCoefficient = 1;
            darkDamageCoefficientDeviant = 0;
            darkTotalRecivedDamage = 0;

            originalElemResistCoefficient = curElemResistCoefficient = 1;
            elemResistCoefficientDeviant = 0;
            originalElemDamageCoefficient = curElemDamageCoefficient = 1;
            elemDamageCoefficientDeviant = 0;

        }

    }

    #endregion

}
