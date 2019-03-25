using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCtrl
{
    /// <summary>
    /// 引导标志
    /// </summary>
    public enum MainGuideIndex
    {
        LevelGudie,
        ScrollerGuide,
        HeroInfoGuide,
        HeroCardGuide,
        LevelUpGuide,
        FinishGuide
    }

    public static class GuideIndex
    {
        public static bool isGuide;
        public static bool isNew = false;

        public static MainGuideIndex mainGuideIndex;

        public static bool IsUpstarDisplayButton = false;
        public static bool IsUpstarButton = false;
        public static bool IsFirstGuide = true;

        static GuideIndex()
        {
            isGuide = false;
            mainGuideIndex = MainGuideIndex.FinishGuide;
        }
    }

}
