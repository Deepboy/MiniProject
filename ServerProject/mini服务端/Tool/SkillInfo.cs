using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Buff;

public class SkillInfo
{
    //无
    public bool skill_100000(FightHero source, FightHero target) {
        Console.WriteLine("skill_100000");
        return false; }

    //镜湖
    public bool skill_100001(FightHero source, FightHero target)
    {
        //设置造成最终伤害系数+i，收到最终伤害+i
        float i = (((source.OrgHp - source.Hp) / source.OrgHp) / 0.1f) * 0.05f;
        source.Final_Damage += i;
        source.Final_Defense -= i;
        Console.WriteLine("skill_100001");
        return true;
    }

    //飞燕
    public bool skill_100002(FightHero source, FightHero target)
    {
        //获得出牌List，判断多少张非火属性牌count
        int count = source.UsedCardDict[ElementType.Water] + source.UsedCardDict[ElementType.Earth] + source.UsedCardDict[ElementType.Wind] + source.UsedCardDict[ElementType.Thunder] + source.UsedCardDict[ElementType.Dark];
        if (count <= 0)
            return true;
        source.User_Client.Room.TakeDamage(source.Hp*0.05f,DamageResource.CollateralDamage,DamageType.Real,ElementType.Fire,source,source);
        Console.WriteLine("skill_100002");
        return true;
    }
    //花舞
    public bool skill_100003(FightHero source, FightHero target)
    {
        //受到火属性伤害时，对敌人施加一层持续10S的弱化效果
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        if (source.UsedCardDict.ContainsKey(ElementType.Fire)) {
            source.User_Client.Room.SetCardEffectBuff(source,target,buffInfoDict[BuffCode.Weaken],1,BuffTarget.Other);
        }
        Console.WriteLine("skill_100003");
        return true;
    }

    //烈破
    public bool skill_100004(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100004");
        //20S内，造成最终伤害提升20%，受到最终伤害提升10%
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.BerserkersBlood], 1, BuffTarget.Self);
        return false;
    }

    //瞬光
    public bool skill_100005(FightHero source, FightHero target)
    {
        float i = (((source.OrgHp - source.Hp) / source.OrgHp) / 0.1f) * 0.03f;
        source.Final_Damage += i;
        Console.WriteLine("skill_100005");

        return false;

    }

    //彼岸花开
    public bool skill_100006(FightHero source, FightHero target)
    {
        //使用手牌时，赋予敌人一层标记，（标记持续时间10S，获得新的标记时会刷新持续时间），标记达到3层时赋予敌人10S的易伤，
        //自身获得10S的强化（最大层数时无法获得新的标记，无法刷新持续时间，该标记属于负面状态，可以被魔免清除）
        Console.WriteLine("skill_100006");
        if (source.UsedCardDict[ElementType.Fire] +
            source.UsedCardDict[ElementType.Water] +
            source.UsedCardDict[ElementType.Earth] +
            source.UsedCardDict[ElementType.Wind] +
            source.UsedCardDict[ElementType.Thunder] +
            source.UsedCardDict[ElementType.Dark] == 0) return true;
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.SnowDrift], 1, BuffTarget.Other);
        if (target.BuffDict[BuffCode.SnowDrift].Tier >= 3) {
            return false;
        }
        return true;
    }

    //纸吹雪
    public bool skill_100007(FightHero source, FightHero target)
    {
        //对敌人造成直接伤害会治疗自身15 % 的已损生命值
        Console.WriteLine("skill_100007");
        if (!source.BuffDict.ContainsKey(BuffCode.Strengthen))
            return true;
        source.User_Client.Room.AddHp((source.OrgHp - source.Hp) * 0.15f, source, source);
        return false;
    }

    //永堕无间
    public bool skill_100008(FightHero source, FightHero target)
    {
        //TODO-当自身拥有强化状态时，使用风元素牌会对敌人造成（风元素牌*15）风元素伤害
        Console.WriteLine("skill_100008");
        if (!source.BuffDict.ContainsKey(BuffCode.Strengthen))
            return true;
        if (source.UsedCardDict[ElementType.Wind] == 0) return true;
        source.User_Client.Room.TakeDamage(15 * source.UsedCardDict[ElementType.Wind], DamageResource.CollateralDamage, DamageType.Element, ElementType.Wind, source, target);
        return false;

    }

    //恭候黄泉路
    public bool skill_100009(FightHero source, FightHero target)
    {
        //对敌人造成其当前生命值10%的伤害并标记目标。10S后，印记会触发，造成
        source.User_Client.Room.TakeDamage(0.1f * target.Hp, DamageResource.CollateralDamage, DamageType.Real, ElementType.Wind, source, target);
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.Zed], 1, BuffTarget.Other);
        Console.WriteLine("skill_100009");
        return false;
    }

    //逢魔时刻
    public bool skill_100010(FightHero source, FightHero target)
    {
        //获得一层持续10S的强化
        Console.WriteLine("skill_100010");
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.Strengthen], 1, BuffTarget.Self);
        return false;
    }

    //倒影
    public bool skill_100011(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100011");
        if (source.UsedCardDict[ElementType.Dark] == 0)
            return true;
        float damage = source.Dark * 0.2f + target.Dark * 0.1f;
        source.User_Client.Room.TakeDamage(damage, DamageResource.CollateralDamage, DamageType.Element, ElementType.Dark, source, target);
        return false;
    }

    //祈灵术
    public bool skill_100012(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100012");
        if (source.UsedCardDict[ElementType.Dark] == 0)
            return true;
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.SpiritOfPrayer], 1, BuffTarget.Self);
        return false;
    }

    //变身
    public bool skill_100013(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100013");
        if (source.UsedCardDict[ElementType.Dark] == 0)
            return true;
        Dictionary<BuffCode, BaseBuff> buffInfoDict = source.User_Client.BaseInfoDict;
        source.User_Client.Room.SetCardEffectBuff(source, target, buffInfoDict[BuffCode.Translation], 1, BuffTarget.Self);
        return false;
    }

    //灵魂隔断
    public bool skill_100014(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100014");
        float sourcePercent = source.Hp / source.OrgHp;
        float targetPercent = target.Hp / target.OrgHp;
        source.Hp = targetPercent * source.OrgHp;
        target.Hp = sourcePercent * target.OrgHp;
        return false;
    }

    //暗夜降临
    public bool skill_100015(FightHero source, FightHero target)
    {
        Console.WriteLine("skill_100015");
        if (source.UsedCardDict[ElementType.Dark] == 0)
            return true;
        source.OrgDark *= 1.05f * source.UsedCardDict[ElementType.Dark];
        return false;
    }

}


