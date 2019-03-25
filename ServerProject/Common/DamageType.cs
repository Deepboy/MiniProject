using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 伤害类型（直接伤害/间接伤害）
    /// </summary>
    public enum DamageResource
    {
        //直接伤害
        DirectDamage = 0,
        //间接伤害
        CollateralDamage = 1,
    }

    /// <summary>
    /// 伤害类型（普通/元素/真实）
    /// </summary>
    public enum DamageType
    {
        //普通
        Normal = 0,
        //元素
        Element = 1,
        //真实
        Real = 2
    }
    public enum WordType : int
    {
        Normal = 0,
        RealDamage = 1,
        CritDamage = 2,
        Dodge = 3,
        Recover = 4,
        SuckBlood = 5,
    }
}
