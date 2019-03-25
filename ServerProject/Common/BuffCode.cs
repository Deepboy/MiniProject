using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 正面Buff/负面Buff
    /// </summary>
    public enum BuffType
    {
        PositiveState = 0,
        NegativeState = 1
    }

    /// <summary>
    /// 普通/高级/领域/技能
    /// </summary>
    public enum BuffFlag
    {
        Normal = 0,
        Senior = 1,
        Field = 2,
        Skill = 3
    }

    /// <summary>
    /// buff驱散类型（强驱散，弱驱散和不可驱散）
    /// </summary>
    public enum BuffDispelType
    {
        //强驱散
        Strong = 0,
        //弱驱散
        Weak = 1,
        //不可驱散
        Disable = 2
    }


    public enum BuffCode
    {
        //随机
        Random = -2,
        //无buff
        None = -1,
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
    /// buff挂的目标
    /// </summary>
    public enum BuffTarget
    {
        None,
        Self,
        Other,
    }

}
