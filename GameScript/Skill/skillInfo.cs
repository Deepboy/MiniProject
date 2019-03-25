using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewFightConfig;
using UnityEngine;
using Common;

public class SkillInfo
{
    //无
    public bool skill_100000(FightHero source, FightHero target) { return false; }

    //镜湖
    public bool skill_100001(FightHero source,FightHero target) {
        //设置造成最终伤害系数+i，收到最终伤害+i
        float i = (((source.MaxHp - source.CurHp) / source.MaxHp) /0.1f)*0.05f;
        source.CurChaosDamageCoefficient += i;
        source.CurChaosDamageResistCoefficient -= i;
        return true;
    }

    //飞燕
    public bool skill_100002(FightHero source, FightHero target)
    {
        //获得出牌List，判断多少张非火属性牌count
        int count=source.CardGroup.DarkCount +source.CardGroup.WaterCount +source.CardGroup.ThunderCount +source.CardGroup.EarthCount +source.CardGroup.WindCount ;
        if(count <= 0)
        {
            return true;
        }
        FightCtrl.AllCaculation.DamageCaculation(source.CurHp * 0.05f * count, target, source, DamageResource.DirectDamage, DamageType.Element, ElementType.Fire);
        return false;
    }
    //花舞
    public bool skill_100003(FightHero source, FightHero target)
    {
        //受到火属性伤害时，对敌人施加一层持续10S的弱化效果
        if(source.FinalDamageElemType == ElementType.Fire)
        {
            BuffManager.AddAndUpdateBuff(source, target, BuffClass.Weaken);
            return false;
        }
        return true;
    }

    //烈破
    public bool skill_100004(FightHero source, FightHero target)
    {
        //20S内，造成最终伤害提升20%，受到最终伤害提升10%
        BuffManager.AddAndUpdateBuff(source, source, BuffClass.BerserkersBlood);
        return false;
    }

    //瞬光
    public bool skill_100005(FightHero source, FightHero target)
    {
        //设置造成最终伤害系数+i
        float i = (((source.MaxHp - source.CurHp) / source.MaxHp) / 0.1f) * 0.03f;
        source.CurChaosDamageCoefficient += i;
        return false;

    }

    //彼岸花开
    public bool skill_100006(FightHero source, FightHero target)
    {
        //使用手牌时，赋予敌人一层标记，（标记持续时间10S，获得新的标记时会刷新持续时间），标记达到3层时赋予敌人10S的易伤，
        //自身获得10S的强化（最大层数时无法获得新的标记，无法刷新持续时间，该标记属于负面状态，可以被魔免清除）
        
        if (source.CardGroup.DarkCount +
           source.CardGroup.FireCount +
           source.CardGroup.WaterCount +
           source.CardGroup.ThunderCount +
           source.CardGroup.EarthCount +
           source.CardGroup.WindCount == 0) return true;
        
        BuffManager.AddAndUpdateBuff(source, target, BuffClass.SnowDrift);
        if(target.BuffDictionary[BuffClass.SnowDrift].Tier >= 3)
        {
            return false;
        }
        return true ;
    }

    //纸吹雪
    public bool skill_100007(FightHero source, FightHero target)
    {
        //对敌人造成直接伤害会治疗自身15 % 的已损生命值
        if (!source.BuffDictionary.ContainsKey(BuffClass.Strengthen))
        {
            return true;
        }

        if (source.DamageToEnemyRecords[source.DamageToEnemyRecords.Count - 1].damageResource == DamageResource.DirectDamage)
        {
            source.CurHp += 0.15f * (source.MaxHp - source.CurHp);
        }

        return false;
    }

    //永堕无间
    public bool skill_100008(FightHero source, FightHero target)
    {
        //TODO-当自身拥有强化状态时，使用风元素牌会对敌人造成（风元素牌*15）风元素伤害
        if (!source.BuffDictionary.ContainsKey(BuffClass.Strengthen)) return true;
        if (source.CardGroup.WindCount == 0)
            return true;
        int count = source.CardGroup.WindCount * 15;
        FightCtrl.AllCaculation.DamageCaculation(count, source, target, DamageResource.CollateralDamage, DamageType.Element, ElementType.Wind);
        return false;

    }

    //恭候黄泉路
    public bool skill_100009(FightHero source, FightHero target)
    {
        //对敌人造成其当前生命值10%的伤害并标记目标。10S后，印记会触发，造成
        //（1加上在印记激活期间所造成的所有伤害总和的45%）风元素伤害。（该标记属于负面状态，可以被魔免清除）
        FightCtrl.AllCaculation.DamageCaculation(float.Parse(NewFightConfig.ExcelDictionaryDefine.BuffDict["Zed"].BuffValue2) * target.CurHp, source, target,
            DamageResource.CollateralDamage, DamageType.Real);
        BuffManager.AddAndUpdateBuff(source, target, BuffClass.Zed);
        return false;
    }

    //逢魔时刻
    public bool skill_100010(FightHero source, FightHero target)
    {
        //获得一层持续10S的强化
        BuffManager.AddAndUpdateBuff(source, source, BuffClass.Strengthen);
        return false;
    }

    //倒影
    public bool skill_100011(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount == 0)
            return true;
        float damage = source.CurDarkStrength * 0.1f + target.CurDarkStrength * 0.2f;
        FightCtrl.AllCaculation.DamageCaculation(damage, source, target, DamageResource.CollateralDamage, DamageType.Element, ElementType.Dark);
        return false;
    }

    //祈灵术
    public bool skill_100012(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount == 0)
            return true;
        BuffManager.AddAndUpdateBuff(source, source, BuffClass.SpiritOfPrayer);
        return false;
    }

    //变身
    public bool skill_100013(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount == 0)
            return true;
        BuffManager.AddAndUpdateBuff(source, source, BuffClass.Translation);
        return false;
    }

    //灵魂隔断
    public bool skill_100014(FightHero source, FightHero target)
    {
        float sourcePercent = source.CurHp / source.MaxHp;
        float targetPercent = target.CurHp / target.MaxHp;
        source.CurHp = targetPercent * source.MaxHp;
        target.CurHp = sourcePercent * target.MaxHp;
        return false;
    }

    //暗夜降临
    public bool skill_100015(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount == 0)
            return true;
        source.OriginalDarkStrength *= 1.05f * source.CardGroup.DarkCount;
        return false;
    }

    //kawayi
    public bool skill_100016(FightHero source, FightHero target)
    {
        BuffManager.AddAndUpdateBuff(source, target, BuffClass.CanUsePassiveSkill);
        return true;
    }

    //等一下
    public bool skill_100017(FightHero source, FightHero target)
    {
        if(source.DamageFromEnemyRecords.Count <= 0)
        {
            return true;
        }
        ElementType elementType = ElementType.Water;
        float strength = source.CurWaterStrength;
        if(strength < source.CurFireStrength)
        {
            elementType = ElementType.Fire;
        }
        if (strength < source.CurThunderStrength)
        {
            elementType = ElementType.Thunder;
        }
        if (strength < source.CurWindStrength)
        {
            elementType = ElementType.Wind;
        }
        if (strength < source.CurEarthStrength)
        {
            elementType = ElementType.Earth;
        }
        if (strength < source.CurDarkStrength)
        {
            elementType = ElementType.Dark;
        }
        if(elementType == source.DamageFromEnemyRecords[source.DamageFromEnemyRecords.Count - 1].elementType)
        {
            FightCtrl.AllCaculation.DamageCaculation(source.DamageFromEnemyRecords[source.DamageFromEnemyRecords.Count - 1].damageValue, source, target,
                source.DamageFromEnemyRecords[source.DamageFromEnemyRecords.Count - 1].damageResource, source.DamageFromEnemyRecords[source.DamageFromEnemyRecords.Count - 1].damageType, elementType);
            return false;
        } 
        return true;
    }

    //吃饱啦
    public bool skill_100018(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount + source.CardGroup.FireCount + source.CardGroup.WaterCount 
            + source.CardGroup.ThunderCount + source.CardGroup.EarthCount +source.CardGroup.WindCount == 0)
            return true;
        FightCtrl.AllCaculation.DamageCaculation(0.1f * target.MaxHp, source, target, DamageResource.CollateralDamage, DamageType.Real);
        source.CurHp += (source.MaxHp - source.CurHp) * 0.5f;
        return false;
    }

    //嗯？
    public bool skill_100019(FightHero source, FightHero target)
    {
        float recoverValue = source.MaxHp - source.CurHp;
        source.CurHp = source.MaxHp;
        FightCtrl.AllCaculation.DamageCaculation(recoverValue / source.MaxHp, source, target, DamageResource.CollateralDamage, DamageType.Real);
        return false;
    }

    //喂？
    public bool skill_100020(FightHero source, FightHero target)
    {
        if (source.CardGroup.DarkCount + source.CardGroup.FireCount + source.CardGroup.WaterCount
            + source.CardGroup.ThunderCount + source.CardGroup.EarthCount + source.CardGroup.WindCount == 0)
            return true;
        source.CurHp += (source.MaxHp - source.CurHp) * 0.05f;
        FightCtrl.AllCaculation.DamageCaculation(target.MaxHp * 0.02f, source, target, DamageResource.CollateralDamage, DamageType.Real);
        return false;
    }

}

