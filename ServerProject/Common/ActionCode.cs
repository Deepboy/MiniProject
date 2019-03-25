using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,
        Register,
        Save,
        StartMatching,
        QuitMatching,
        StartGame,
        ShowTimer,
        OutCard,
        GameOver,
        EffectOver,
        UseSkill,
        GetHeroCard,
        TakeDamage,
    }
}