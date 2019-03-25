using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum FightPhase
    {
        None,
        BeforeEnter,
        InEnter,
        AfterEnter,
        BeforeStart,
        InStart,
        AfterStart,
        BeforeIntend,
        InIntend,
        AfterIntend,
        BeforeAction,
        BeforeOtherAction,
        InAction,
        InOtherAction,
        AfterAction,
        AfterOtherAction,
        BeforeSkill,
        BeforeOtherSkill,
        InSkill,
        InOtherSkill,
        AfterSkill,
        AfterOtherSkill,
        BeforeDemage,
        InDemage,
        AfterDemage,
        BeforeHit,
        InHit,
        AfterHit,
        BeforeEnd,
        InEnd,
        AfterEnd,
    }

    public enum FightState
    {
        Prepare,
        Start,
        Intend,
        Action,
        Skill,
        Damage,
        End,
    }
}
