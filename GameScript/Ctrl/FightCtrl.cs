using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NewFightConfig;
using Excel;
using System.IO;
using System.Data;
using SpecialBuffManager;
using System;
using System.Reflection;
using Common;

public class FightCtrl : BasePanel
{

    #region PlayerInfo

    //玩家所出战的角色
    private static FightHero playerHero;
    //玩家助战角色
    private FightHero playerAssitHero;
    //玩家初始位置
    private Vector3 playerHeroOriginalPosition;
    //玩家角色位置
    private Vector3 playerHeroPosition;
    //玩家助战角色位置
    private Vector3 playerAssitHeroPosition;
    //玩家角色护盾
    private SpriteRenderer playerShieldSpriteRender;

    //玩家当前的手牌计数
    private int curPlayerHandCardCount;
    //玩家当前的行动点计数
    private int curPlayerAPCount;
    //玩家当前积累的行动点时间计数
    private float curPlayerSaveAPTime;
    //玩家当前的选择卡牌计数
    private int playerSelectCardCount;

    //玩家信息面板
    private GameObject playerInfoPanel;
    //玩家生命条
    private Slider playerHpSlider;
    //玩家行动条
    private Slider playerActionSlider;
    //玩家行动点content
    private GameObject playerAPContent;
    //存放玩家buff信息content
    private static GameObject playerBuffContent;
    //玩家手牌信息面板
    private GameObject playerCardInfoPanel;
    //玩家手牌ScrollView
    private GameObject playerScrollViewCard;
    //存放玩家当前手牌content
    private GameObject playerCardContent;
    //存放所有玩家显示手牌的对象
    private List<GameObject> playerCardGroupContent;
    //玩家手牌初始大小
    private Vector2 playerCardOriginalSize;
    //玩家手牌大小变化系数
    private float playerCardScaleRatio;

    //玩家对象
    private GameObject playerGameObject;
    //助战对象
    private GameObject playerAssitGameObject;
    //玩家动画控制器
    private Animator playerAnimator;
    //玩家助战角色动画控制器
    private Animator playerAssistAnimator;

    //记录玩家行动点
    private int playerSaveApCount;
    //记录玩家行动条值
    private float playerSaveApSliderCount;

    //记录玩家的buff
    private int playerSaveBuffCount;

    //预览框
    private GameObject PreviewPanel;

    private Text PlayerHp_Text;

    private List<BuffClass> playerPrepareBuffClass;

    #endregion

    #region EnemyInfo

    //敌人所出战的角色
    private static FightHero enemyHero;
    //敌人角色位置
    private Vector3 enemyHeroPosition;
    //敌人角色护盾
    private SpriteRenderer enemyShieldSpriteRender;

    //敌人当前的手牌计数
    private int curEnemyCardCount;

    //敌人随机出牌的行动点
    private int curRandEnemyApCount;
    //敌人当前积累的行动点时间计数
    private float curEnemySaveAPTime;
    //敌人伤害系数
    private float enemyDamageRatio;

    //敌人信息面板
    private GameObject enemyInfoPanel;
    //敌人生命条
    private Slider enemyHpSlider;
    //敌人行动条
    private Slider enemyActionSlider;
    //敌人行动点content
    private GameObject enemyAPContent;
    //存放敌人buff信息content
    private static GameObject enemyBuffContent;

    //敌人对象
    private GameObject enemyGameObject;
    //敌人动画控制器
    private Animator enemyAnimator;

    //记录敌人行动点
    private int enemySaveApCount;
    //记录敌人行动条值
    private float enemySaveApSliderCount;
    private Text EnemyHP_Text;

    //敌人卡组
    private List<Card> enemyCard;

    //敌人伤害系数
    private float enemyDamageCoefficient;

    #endregion

    #region PublicParmas

    //检测当前是否有攻击动画播放
    public static bool isPlayingAnimation;
    //当前是否有特效在播放
    public static bool isPlayingEffect;
    //未获得时的行动点图片
    private Sprite actionOrigin;
    //获得后的行动点图片
    private Sprite actionFinish;

    //出牌按钮
    private Button buttonDrawCard;
    //点击卡牌前的卡牌图片
    private Sprite chooseSprite;
    //点击卡牌后的卡牌显示
    private Sprite unchooseSprite;

    //战斗信息面板
    private GameObject fightInfoPanel;

    private GameObject SettingPanel;
    //当前时间
    private float curTime;
    //计时器
    private float timeCount;

    //游戏对象的canvas
    private GameObject canvas;

    //卡牌前景遮罩
    private GameObject maskCardForeground;
    //阴阳图
    private Sprite[] YingYangImage;
    //卡牌放回按钮
    private GameObject cancelSelectCardButton;
    //预览框是否激活的按钮
    private GameObject PreviewButton;
    //打开设置界面的按钮
    private Button SettingButton;
    private Button ContinueButton;
    //用于控制预览框是否能开启的顶层标志位
    public static bool PreviewTopControlBool = true;
    
    //卡牌组合技能字典
    private Dictionary<string, SpecialBuff> cardGroupDict;
    //buff字典
    private Dictionary<string, Buff> buffDict;

    //卡牌组合名称
    private Text cardCombineName;
    //卡牌组合效果描述
    private Text cardCombineDescription;

    //调试窗口按钮
    private Button debugSettingButton;
    //调试窗口面板
    private static GameObject debugSettingPanel;

    //当前关卡
    private Stage curStage;
    //当前的怪物
    private int curEnemyFlag;
    //怪物列表
    private List<FightHero> enemyList;

    //奖励图片
    private GameObject imageReward1;
    private GameObject imageReward2;
    private GameObject imageReward3;
    private GameObject imageReward4;

    //立即胜利按钮
    public static Button winButton;

    //技能评级图片
    private Image gradeImage;

    //字体结构
    private struct Word
    {
        public GameObject gameObject;
        public bool isDelay;

        public Word(GameObject go, bool delay)
        {
            gameObject = go;
            isDelay = delay;
        }
    }
    //玩家飘字列表
    private static List<Word> playerWordList;
    //敌人飘字列表
    private static List<Word> enemyWordList;
    //飘字面板
    private GameObject showWordPanel;

    #endregion

    //战斗集气标志
    private bool IsFightPrepare;
    //战斗伤害结算标志
    private bool IsDamageDamageSettlement;
    //战斗结束标志
    public static bool IsFightFinish;
    //辅助第一次战斗结束标志
    private bool IsFirstFightFinish;
    //新手指引标志
    private bool IsGuide;

    //是否为第一次碰撞
    private bool isFirstOnTrigger;

    //技能
    private GameObject playerSkillPanel;
    private GameObject playerActionSkill;
    private GameObject playerFirstPassiveSkill;
    private GameObject playerSecondPassiveSkill;
    private GameObject playerThirdPassiveSkill;
    private GameObject playerAssistSkill;
    private GameObject ActionSkillEffect;
    private GameObject AssistSkillEffect;
    private GameObject AssistPassiveSkillEffect;
    private bool IsFirstAssistSkillEffect;

    private Text ExpText;
    public static  Button Tips_Button;
    private GameObject Tips_Image;
    private Button BackToSettingButton;

    //音乐设置滑动条
    public static Slider SliderMusic;
    //音乐滑动头图片
    private Image SliderMusicImage;
    //音效设置滑动条
    public static Slider SliderSoundEffect;
    //音效滑动条图片
    private Image SliderSoundEffectImage;
    //音乐
    public static GameObject TextMusic;
    //音效
    public static GameObject TextSoundEffect;

    //玩家黑洞
    private GameObject playerBlackHole;
    //敌人黑洞
    private GameObject enemyBlackHole;

    #region 新手指引相关函数及变量

    //流程索引，随着玩家的操作递增从而控制流程向下进行
    private int GuideIndex = 0;
    private Text GuideText;
    private GameObject GuidePanel;
    private GameObject Finger;
    GameObject Ap_tips;
    //生成一张特定元素的卡牌并加入手牌
    public void GenerateOneCard(ElementType type)
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load("Prefab/card") as GameObject);
        gameObject.transform.SetParent(playerCardContent.transform);
        gameObject.transform.localScale = new Vector3(1, 1, 0);
        gameObject.GetComponent<CardCtrl>().elemType = type;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/" + type);
        playerHero.CardGroup.HandCardGroup.Add(gameObject, type);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate () { PlayerChooseCard(gameObject); });
        curPlayerHandCardCount++;
    }

    public void OnGuidePanelClick()
    {
        facade.PlayUIAudio("close");
        GuideIndex++;
    }
    /// <summary>
    /// 控制整个新手指引流程的协程
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllCommand()
    {
        playerAssistSkill.GetComponent<Button>().interactable = false;
        enemyHero.MaxHp = 500;
        enemyHero.CurHp = 500;
        GameObject.Find("Canvas/PanelFight").GetComponent<BoxCollider2D>().enabled = false;
        PreviewButton.GetComponent<Button>().interactable = false;
        SettingButton.GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas/white_image").GetComponent<Button>().interactable = false;
        IsGuide = true;

        if (GuideIndex == 0)
        {
            buttonDrawCard.interactable = false;
            playerActionSkill.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            playerHero.CurHp -= 100;
            enemyGameObject.GetComponent<Animator>().SetTrigger("foxSpell");
            playerGameObject.GetComponent<Animator>().SetTrigger("hit");
            buttonDrawCard.interactable = false;
            yield return new WaitForSeconds(1f);
            ShowWord(playerHero, 100, WordType.Normal, true);
            yield return new WaitForSeconds(1f);
            if (playerHero.CurApPoint < 1)
            {
                playerHero.CurApPoint = 1;
                playerHero.CurSliderValue = 0;
            }
            GuidePanel.SetActive(true);
            GuidePanel.transform.Find("GuideImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/nuowa");
            GuideText.text = "诺瓦：不准伤害召唤师哥哥！";
            GuidePanel.SetActive(true);
            yield return new WaitForSeconds(0.04f);
            IsFightPrepare = false;
        }
        while (GuideIndex == 0)
        {
            IsFightPrepare = false;
            yield return null;
            playerActionSkill.GetComponent<Button>().interactable = false;
        }
        if (GuideIndex == 1)
        {
            playerActionSkill.GetComponent<Button>().interactable = true;
            IsFightPrepare = false;
            Finger.SetActive(true);
            GuidePanel.transform.Find("GuideImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/ainuomiya");
            GuidePanel.transform.Find("GuideImage").localPosition = new Vector3(-GuidePanel.transform.Find("GuideImage").localPosition.x, GuidePanel.transform.Find("GuideImage").localPosition.y, 0);
            GuideText.text = "欧诺弥亚：释放主动技能以提高伤害！";

            GuidePanel.GetComponent<Button>().interactable = false;
            buttonDrawCard.interactable = false;
        }
        while (GuideIndex == 1)
        {
            IsFightPrepare = false;
            yield return null;
        }
        if (GuideIndex == 2)
        {
            yield return new WaitForSeconds(2f);
            GuideText.text = "欧诺弥亚：已积累1行动点，点击施放一张风牌以加速行动点的积累";
            Ap_tips = Instantiate(Resources.Load<GameObject>("Prefab/AP_Tips"));
            Ap_tips.transform.SetParent(playerAPContent.transform.GetChild(0).transform);
            Ap_tips.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1);
            Ap_tips.GetComponent<RectTransform>().localPosition = new Vector3(0, -80, 0);

            GuidePanel.GetComponent<Button>().interactable = false;
            buttonDrawCard.interactable = true;

            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            playerCardContent.transform.GetChild(0).GetComponent<Button>().interactable = true;
            Finger.GetComponent<Animator>().SetTrigger("Step2");
            Finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);


        }
        while (GuideIndex == 2)
        {
            playerActionSkill.GetComponent<Button>().interactable = false;
            yield return null;
        }
        if (GuideIndex == 3)
        {
            Ap_tips.SetActive(false);
            Finger.SetActive(false);
            playerHero.CurSpeed = 100000;
            IsFightPrepare = true;
            GuidePanel.GetComponent<Button>().interactable = false;
            buttonDrawCard.interactable = false;

            GuidePanel.SetActive(false);
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            GenerateOneCard(ElementType.Thunder);
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            yield return new WaitForSeconds(3);
            if (playerHero.CurApPoint < 2)
            {
                playerHero.CurApPoint = 2;
                playerHero.CurSliderValue = 0;
            }
            yield return new WaitForSeconds(2);
            Finger.SetActive(true);
            yield return new WaitForSeconds(2f);
            //Finger.GetComponent<Animator>().SetBool("Tip_DrawCard", true);
            GuidePanel.SetActive(true);
            GuideText.text = "欧诺弥亚：你已经大于两个行动点了，让我们使用两张火牌来释放更强力的攻击吧！";
            buttonDrawCard.interactable = true;
            IsFightPrepare = false;
            PlayerChooseCard(playerCardContent.transform.GetChild(0).gameObject);
            PlayerChooseCard(playerCardContent.transform.GetChild(1).gameObject);
        }
        while (GuideIndex == 3)
        {
            playerActionSkill.GetComponent<Button>().interactable = false;
            if (playerHero.CurApPoint >= 3)
            {
                IsFightPrepare = false;
            }
            yield return null;
        }
        if (GuideIndex == 4)
        {
            Finger.SetActive(false);
            GuidePanel.GetComponent<Button>().interactable = false;
            GuidePanel.SetActive(false);
            buttonDrawCard.interactable = false;
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }

            GenerateOneCard(ElementType.Fire);
            GenerateOneCard(ElementType.Fire);

            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            yield return new WaitForSeconds(5);
            BuffManager.AddAndUpdateBuff(enemyHero, playerHero, BuffClass.Burn, 3);
            IsFightPrepare = true;
            for (int i = 0; i < playerBuffContent.transform.childCount; i++)//给灼烧buff高亮显示
            {
                if (playerBuffContent.transform.GetChild(i).GetComponent<BuffCtrl>().buffClass == BuffClass.Burn)
                {
                    GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/HighLine"));
                    effect.transform.SetParent(playerBuffContent.transform.GetChild(i).transform);
                    effect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    effect.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    effect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                }

            }
            yield return new WaitForSeconds(2);
            playerSaveApCount = 0;
            if (playerHero.CurApPoint < 3)
            {
                playerHero.CurApPoint = 3;
                playerHero.CurSliderValue = 0;
            }
            yield return new WaitForSeconds(0.04f);
            Finger.SetActive(true);
            PlayerChooseCard(playerCardContent.transform.GetChild(0).gameObject);
            PlayerChooseCard(playerCardContent.transform.GetChild(1).gameObject);
            PlayerChooseCard(playerCardContent.transform.GetChild(2).gameObject);
            GuidePanel.SetActive(true);


            GuideText.text = "欧诺弥亚：不好，你被敌人施加了灼烧效果，快净化掉它！";
            buttonDrawCard.interactable = true;
            IsFightPrepare = false;
        }
        while (GuideIndex == 4)
        {
            playerActionSkill.GetComponent<Button>().interactable = false;
            if (playerHero.CurApPoint >= 3)
            {
                IsFightPrepare = false;
            }
            yield return null;
        }

        if (GuideIndex == 5)
        {

            Finger.SetActive(false);
            GenerateOneCard(ElementType.Fire);
            GenerateOneCard(ElementType.Fire);
            GenerateOneCard(ElementType.Fire);
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            GuidePanel.GetComponent<Button>().interactable = false;
            GuidePanel.SetActive(false);
            yield return new WaitForSeconds(3);
            if (playerHero.CurApPoint < 4)
            {
                playerHero.CurApPoint = 4;
                playerHero.CurSliderValue = 0;
            }
            IsFightPrepare = false;
            yield return new WaitForSeconds(0.04f);
            Finger.SetActive(true);
            GuidePanel.SetActive(true);
            GuideText.text = "欧诺弥亚：出三张火烧死敌人！";
            PlayerChooseCard(playerCardContent.transform.GetChild(0).gameObject);
            PlayerChooseCard(playerCardContent.transform.GetChild(1).gameObject);
            PlayerChooseCard(playerCardContent.transform.GetChild(2).gameObject);
            buttonDrawCard.interactable = true;
        }
        while (GuideIndex == 5)
        {
            playerActionSkill.GetComponent<Button>().interactable = false;
            yield return null;
        }
        if (GuideIndex == 6)
        {
            PlayerGetCard();
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            GuidePanel.SetActive(false);
            //IsFightPrepare = true;
            Finger.SetActive(false);
            playerActionSkill.GetComponent<Button>().interactable = false;
            GuidePanel.GetComponent<Button>().interactable = true;

            yield return new WaitForSeconds(9);
            if (enemyHero.CurHp > 0)
            {
                enemyHero.CurHp = 0;
            }
            GuidePanel.SetActive(true);
            GuideText.text = "欧诺弥亚：召唤师，你和 “诺瓦” 的感情得到了我的认可！";

        }
        while (GuideIndex == 6)
        {
            yield return null;
        }
        if (GuideIndex == 7)
        {
            GuideText.text = "欧诺弥亚：但你的力量太过弱小，这是我创造这个世界留下的秩序绘卷，希望你能继承秩序的意志，守护美丽的卡其亚大陆！";
            GuideCtrl.GuideIndex.isNew = false;
        }
        while (GuideIndex == 7)
        {
            yield return null;
        }
        if (GuideIndex == 8)
        {
            LoadAsyncScene.nextSceneName = "Main_Scene";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
        }

    }

    #endregion

    #region FightLogic

    #region 战斗开始阶段

    /// <summary>
    /// 开场动画
    /// </summary>
    public void ShowStartGameAnim()
    {
        //播放战斗开场动画
        cancelSelectCardButton.GetComponent<Animation>().Play();
        //播放战斗开场动画
        buttonDrawCard.GetComponent<Animation>().Play();
        //播放战斗开场动画
        playerScrollViewCard.GetComponent<Animation>().Play();
        StartCoroutine("WaitForStartAnimFinish", 1);
        if (GuideCtrl.GuideIndex.isNew)
        {
            StartCoroutine("AllCommand");
        }

    }

    /// <summary>
    /// 等待开场动画结束动作
    /// </summary>
    /// <param name="delay">等待时间</param>
    private IEnumerator WaitForStartAnimFinish(float delay)
    {

        yield return new WaitForSeconds(delay);
        cancelSelectCardButton.GetComponent<Image>().sprite = YingYangImage[0];
        buttonDrawCard.GetComponent<Image>().sprite = YingYangImage[1];
        IsFightPrepare = true;
        IsDamageDamageSettlement = false;

        IsFightFinish = false;
        IsFirstFightFinish = true;
        //为怪物随机一个出牌行动点
        if (GuideCtrl.GuideIndex.isNew)
        {
            GenerateOneCard(ElementType.Wind);
            GenerateOneCard(ElementType.Fire);
            GenerateOneCard(ElementType.Fire);
            GenerateOneCard(ElementType.Wind);
            GenerateOneCard(ElementType.Wind);
            //GenerateOneCard(ElementType.Thunder);
            curRandEnemyApCount = 1;
            EnemyGetCard();
        }
        else
        {
            curRandEnemyApCount = UnityEngine.Random.Range(1, 4);
            //玩家抽卡
            PlayerGetCard();
            //敌人抽卡
            EnemyGetCard();
        }
    }

    #endregion

    #region 集气阶段

    /// <summary>
    /// 玩家获得行动点
    /// </summary>
    private void PlayerGetAP()
    {
        //当前行动点小于最大行动点时，获得新的行动点
        if (playerHero.CurApPoint < 5)
        {
            playerHero.CurApPoint++;
            //满行动点时，行动条不再清0
            if (playerHero.CurApPoint <= 5)
            {
                playerHero.CurSliderValue = 0;
            }
        }
    }

    /// <summary>
    /// 敌人获得行动点
    /// </summary>
    private void EnemyGetAP()
    {
        //当前行动点小于最大行动点时，获得新的行动点
        if (enemyHero.CurApPoint < 5)
        {
            enemyHero.CurApPoint++;
            //满行动点时，行动条不再清0
            if (enemyHero.CurApPoint <= 5)
            {
                enemyHero.CurSliderValue = 0;
            }
        }
    }

    #endregion

    #region 伤害结算阶段

    /// <summary>
    /// 判断当前准备出牌的卡牌逻辑是否合理
    /// </summary>
    /// <returns>true为合理，可以出牌</returns>
    private bool PrepareShowCardLegal()
    {
        //未选择任何牌时或选择的牌大于最大行动点时
        if (playerSelectCardCount <= 0 || playerSelectCardCount > playerHero.CurApPoint)
        {
            return false;
        }
        if (isPlayingAnimation == true && isPlayingEffect == true)
        {
            return false;
        }
        //非集气阶段不能出牌
        if (IsFightPrepare == false && IsGuide == false)
        {
            return false;
        }
        //禁止出牌
        if (playerHero.CanDrawCardDeviant == false)
        {
            return false;
        }
        //禁止出某个元素牌
        if (playerHero.CurCanDrawWaterCard == false && playerHero.CardGroup.WaterCount > 0)
        {
            return false;
        }
        if (playerHero.CurCanDrawFireCard == false && playerHero.CardGroup.FireCount > 0)
        {
            return false;
        }
        if (playerHero.CurCanDrawThunderCard == false && playerHero.CardGroup.ThunderCount > 0)
        {
            return false;
        }
        if (playerHero.CurCanDrawWindCard == false && playerHero.CardGroup.WindCount > 0)
        {
            return false;
        }
        if (playerHero.CurCanDrawEarthCard == false && playerHero.CardGroup.EarthCount > 0)
        {
            return false;
        }
        if (playerHero.CurCanDrawDarkCard == false && playerHero.CardGroup.DarkCount > 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 出牌按钮的绑定函数，用于实现玩家出牌逻辑
    /// 在出牌时，消耗玩家的所有行动点
    /// </summary>
    public void PlayerDrawCardOnClick()
    {
        Time.timeScale = 1;
        //准备出牌不合逻辑时
        if (PrepareShowCardLegal() == false)
            return;
        for (int i = 0; i < 10000; i++)
        {
            if (isPlayingAnimation == true || isPlayingEffect == true)
                return;
        }
        playerHero.heroCard.Prossive_skill_function(FightPhase.AfterIntend, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.AfterIntend, playerHero, enemyHero);
        playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeAction, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.BeforeAction, playerHero, enemyHero);
        playerHero.heroCard.Prossive_skill_function(FightPhase.InAction, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.InAction, playerHero, enemyHero);
        BuffManager.SetActionAfterDrawCard(enemyHero, playerHero);
        //重置出牌卡组的元素计数
        playerHero.CardGroup.ResetAllCount();
        //playerFightCardGroupManager.ResetAllCount();

        //将玩家行动点标记为未获得状态
        for (int i = playerHero.CurApPoint - 1; i > playerHero.CurApPoint - playerSelectCardCount - 1; i--)
        {
            GameObject gameObject = playerAPContent.transform.GetChild(i).gameObject;
            gameObject.GetComponent<Image>().sprite = actionOrigin;
        }
        //删除相应数量的行动点
        playerHero.CurApPoint -= playerSelectCardCount;
        playerSaveApCount -= playerSelectCardCount;
        //将选择的手牌删除
        for (int i = 0; i < playerCardContent.transform.childCount; i++)
        {
            if (playerCardContent.transform.GetChild(i).GetComponent<CardCtrl>().ctrl == true)
            {
                playerHero.CardGroup.ShowCard(playerCardContent.transform.GetChild(i).gameObject);
                //playerFightCardGroupManager.ShowCard(playerCardContent.transform.GetChild(i).gameObject);
                Destroy(playerCardContent.transform.GetChild(i).gameObject);
                playerCardGroupContent.Remove(playerCardContent.transform.GetChild(i).gameObject);
                curPlayerHandCardCount--;
            }
        }
        playerHero.heroCard.Prossive_skill_function(FightPhase.AfterAction, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.AfterAction, playerHero, enemyHero);
        //将玩家选择手牌计数重置为0
        playerSelectCardCount = 0;
        //反射获得构造buff
        string id = playerHero.CardGroup.FireCount.ToString() + playerHero.CardGroup.WaterCount.ToString() + playerHero.CardGroup.EarthCount.ToString()
                      + playerHero.CardGroup.WindCount.ToString() + playerHero.CardGroup.ThunderCount.ToString() + playerHero.CardGroup.DarkCount.ToString();
        int count = playerHero.CardGroup.FireCount + playerHero.CardGroup.WaterCount + playerHero.CardGroup.EarthCount
                        + playerHero.CardGroup.WindCount + playerHero.CardGroup.ThunderCount + playerHero.CardGroup.DarkCount;
        //展示玩家出牌的元素
        playerBlackHole.GetComponent<BlackHoleCenter>().CreateElemBall(id);
        if (cardGroupDict.ContainsKey(id))
        {
            facade.PlaySkillAudio(cardGroupDict[id].EffectSoundPath, float.Parse(cardGroupDict[id].EffectSoundDelay));
            bool isPlayingAnimation = true;
            ElementType elementType = (ElementType)Enum.Parse(typeof(ElementType), cardGroupDict[id].FixedDamageAttribute);
            float damage = float.Parse(cardGroupDict[id].FixedDamage);
            if (count <= 4)
            {
                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
            }
            if (cardGroupDict[id].buffID1 != "0")
            {
                if (count <= 4)
                {
                    int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                    BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID1);
                    if (cardGroupDict[id].buff1Object == "Enemy")
                    {
                        BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                    }
                    else
                    {
                        BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                    }
                }
                else
                {
                    int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                    BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID1);
                    switch (buffClass)
                    {
                        case BuffClass.Amaterasu:
                            if (enemyHero.BuffDictionary.ContainsKey(BuffClass.BlazingFire))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            //
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        case BuffClass.FrostField:
                            if (playerHero.BuffDictionary.ContainsKey(BuffClass.FrostPower))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        case BuffClass.EarthFiled:
                            if (playerHero.BuffDictionary.ContainsKey(BuffClass.HolyShieldShelter))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        case BuffClass.WindyFiled:
                            if (playerHero.BuffDictionary.ContainsKey(BuffClass.Hurricane))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        case BuffClass.DarkFiled:
                            if (playerHero.BuffDictionary.ContainsKey(BuffClass.EvilTouch))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        case BuffClass.ThunderField:
                            if (enemyHero.BuffDictionary.ContainsKey(BuffClass.ThunderWrath))
                            {
                                AllCaculation.DamageCaculation(damage, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Element, elementType);
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            //shengjian
                            else
                            {
                                isPlayingAnimation = false;
                                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
                            }
                            break;
                        default:
                            if (cardGroupDict[id].buff1Object == "Enemy")
                            {
                                BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                            }
                            else
                            {
                                BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                            }
                            break;
                    }
                }

            }
            if (cardGroupDict[id].buffID2 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber2);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID2);
                if (cardGroupDict[id].buff2Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
            }
            if (cardGroupDict[id].buffID3 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber3);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID3);
                if (cardGroupDict[id].buff3Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
            }
            if (cardGroupDict[id].buffID4 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber4);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID4);
                if (cardGroupDict[id].buff4Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
            }
            if (isPlayingAnimation)
            {
                //生成特效
                string islongRange = cardGroupDict[id].isLongRange;
                string effectName = cardGroupDict[id].Effect;
                StartCoroutine(WaitForCreateElemBall(true, islongRange, effectName));
            }
            //玩家抽卡
            if (!GuideCtrl.GuideIndex.isNew)
            {
                PlayerGetCard();
            }
            UpdateShowPreviewPanel(false);//出牌时更新预览框的显示
            cardCombineName.text = "";
            cardCombineDescription.text = "";
            //新手指引部分
            GuideIndex++;

        }
        else
        {
            facade.PlaySkillAudio("scratch");
            //shengzheyiwu
            if (count == 4)
            {
                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengZheYiWu"));
                AllCaculation.DamageCaculation(40, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
            }
            //shengjian
            else if (count == 5)
            {
                StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
                AllCaculation.DamageCaculation(65, playerHero, enemyHero, DamageResource.DirectDamage, DamageType.Normal);
            }
            //playerAnimator.SetTrigger("attack");
            StartCoroutine("waitforEnemyHit");
            UpdateShowPreviewPanel(false);//出牌时更新预览框的显示
            cardCombineName.text = "组合名称：";
            cardCombineDescription.text = "组合效果：";
            //玩家抽卡
            if (!GuideCtrl.GuideIndex.isNew)
            {
                PlayerGetCard();
            }
        }
        //重置出牌卡组的元素计数
        playerHero.CardGroup.ResetAllCount();

    }

    /// <summary>
    /// 等待敌人受击
    /// </summary>
    /// <returns></returns>
    public IEnumerator waitforEnemyHit()
    {
        yield return new WaitForSeconds(1);
        enemyAnimator.SetTrigger("foxhit");
    }

    /// <summary>
    /// 等待生成元素球
    /// isPlayer为： true:玩家 false:敌人
    /// EffectType 1：普通特效 2：圣者遗物 3：圣剑
    /// </summary>
    private IEnumerator WaitForCreateElemBall(bool isPlayer, string islongRange = "", string effectName = "")
    {
        if(isPlayer)
        {
            while(!playerBlackHole.GetComponent<BlackHoleCenter>().isFinishElemBall)
            {
                isPlayingAnimation = true;
                isPlayingEffect = true;
                yield return null;
            }
            isPlayingAnimation = false;
            isPlayingEffect = false;
            GenerateEffect(islongRange, effectName);
        }
        else
        {
            while (!enemyBlackHole.GetComponent<BlackHoleCenter>().isFinishElemBall)
            {
                isPlayingAnimation = true;
                isPlayingEffect = true;
                yield return null;
            }
            isPlayingAnimation = false;
            isPlayingEffect = false;
            GenerateEnemyEffect(islongRange, effectName);
        }
    }

    /// <summary>
    /// 生成玩家特效
    /// </summary>
    /// <param name="isLongRange">是否远程攻击</param>
    public void GenerateEffect(string isLongRange, string EffectName)
    {
        if (isLongRange == "0")
        {
            playerAnimator.SetTrigger("attack");
            if (EffectName != "0")
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                DestroySelf.isEnemy = false;
            }
        }
        else
        {
            playerAnimator.SetTrigger("spell");
            if (EffectName != "0")
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                DestroySelf.isEnemy = false;
            }
            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(100, 100, 1);
        }
    }

    /// <summary>
    /// 生成敌人特效
    /// </summary>
    /// <param name="isLongRange">是否远程攻击</param>
    public void GenerateEnemyEffect(string isLongRange, string EffectName)
    {

        if (isLongRange == "0")
        {
            enemyAnimator.SetTrigger("foxAttack");
            if (EffectName != "0")
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                effect.transform.localPosition = new Vector3(-effect.transform.localPosition.x, effect.transform.localPosition.y, effect.transform.localPosition.z);
                DestroySelf.isEnemy = true;
            }
        }
        else
        {
            enemyAnimator.SetTrigger("foxSpell");
            if (EffectName != "0")
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                effect.transform.localPosition = new Vector3(-effect.transform.localPosition.x, effect.transform.localPosition.y, effect.transform.localPosition.z);
                DestroySelf.isEnemy = true;
            }
            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(1).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(-100, 100, 1);
        }
    }

    /// <summary>
    /// 敌人出牌函数
    /// 出牌的伤害为传递参数randomPoint * enemyDamageRatio（敌人伤害系数）
    /// 在出牌时会消耗敌人积攒的所有行动点，并且重置行动条
    /// </summary>
    /// <param name="randomPoint">敌人随机获得的行动点</param>
    private void EnemyDrawCard(int randomPoint)
    {
        //禁止出牌
        if (enemyHero.CanDrawCardDeviant == false)
        {
            return;
        }
        //禁止出某个元素牌
        if (enemyHero.CurCanDrawWaterCard == false && enemyHero.CardGroup.WaterCount > 0)
        {
            return;
        }
        if (enemyHero.CurCanDrawFireCard == false && enemyHero.CardGroup.FireCount > 0)
        {
            return;
        }
        if (enemyHero.CurCanDrawThunderCard == false && enemyHero.CardGroup.ThunderCount > 0)
        {
            return;
        }
        if (enemyHero.CurCanDrawWindCard == false && enemyHero.CardGroup.WindCount > 0)
        {
            return;
        }
        if (enemyHero.CurCanDrawEarthCard == false && enemyHero.CardGroup.EarthCount > 0)
        {
            return;
        }
        if (enemyHero.CurCanDrawDarkCard == false && enemyHero.CardGroup.DarkCount > 0)
        {
            return;
        }
        for (int i = 0; i < 10000; i++)
        {
            if (isPlayingAnimation == true)
                return;
        }
        //将敌人行动条和行动点清0
        enemyHero.CurApPoint = 0;

        //将所有行动点设为未获得状态
        for (int i = 0; i < enemyAPContent.transform.childCount; i++)
        {
            GameObject gameObject = enemyAPContent.transform.GetChild(i).gameObject;
            gameObject.GetComponent<Image>().sprite = actionOrigin;
        }
        int[] count = new int[6];
        for (int i = 0; i < 6; i++)
        {
            count[i] = 0;
        }
        for (int i = 0; i < randomPoint; i++)
        {
            switch ((int)enemyCard[i].ElementType)
            {
                case 1:
                    count[1]++;
                    break;
                case 2:
                    count[0]++;
                    break;
                case 3:
                    count[4]++;
                    break;
                case 4:
                    count[3]++;
                    break;
                case 5:
                    count[2]++;
                    break;
                case 6:
                    count[5]++;
                    break;
            }
        }
        string id = count[0].ToString() + count[1].ToString() + count[2].ToString() + count[3].ToString() + count[4].ToString() + count[5].ToString();
        //展示玩家出牌的元素
        enemyBlackHole.GetComponent<BlackHoleCenter>().CreateElemBall(id);
        if (cardGroupDict.ContainsKey(id))
        {
            ElementType elementType = (ElementType)Enum.Parse(typeof(ElementType), cardGroupDict[id].FixedDamageAttribute);
            float damage = float.Parse(cardGroupDict[id].FixedDamage);
            AllCaculation.DamageCaculation(damage * enemyDamageCoefficient, enemyHero, playerHero, DamageResource.DirectDamage, DamageType.Element, elementType);
            if (cardGroupDict[id].buffID1 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID1);
                if (cardGroupDict[id].buff1Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
            }
            if (cardGroupDict[id].buffID2 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber2);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID2);
                if (cardGroupDict[id].buff2Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
            }
            if (cardGroupDict[id].buffID3 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber3);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID3);
                if (cardGroupDict[id].buff3Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
            }
            if (cardGroupDict[id].buffID4 != "0")
            {
                int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber4);
                BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID4);
                if (cardGroupDict[id].buff4Object == "Enemy")
                {
                    BuffManager.AddAndUpdateBuff(enemyHero, playerHero, buffClass, fixedCoefficient);
                }
                else
                {
                    BuffManager.AddAndUpdateBuff(playerHero, enemyHero, buffClass, fixedCoefficient);
                }
            }
            enemyCard.Clear();
            curEnemyCardCount = 0;
            EnemyGetCard();
            string islongRange = cardGroupDict[id].isLongRange;
            string effectName = cardGroupDict[id].Effect;
            StartCoroutine(WaitForCreateElemBall(false, islongRange, effectName));
        }
        else
        {
            enemyCard.Clear();
            curEnemyCardCount = 0;
            EnemyGetCard();
            enemyAnimator.SetTrigger("foxAttack");
            StartCoroutine("waitforHit");
            StartCoroutine(WaitForCreateElemBall(false, "1", "Chaos_ShengZheYiWu"));
            AllCaculation.DamageCaculation(100, enemyHero, playerHero, DamageResource.DirectDamage, DamageType.Element, ElementType.Water);
        }

        curRandEnemyApCount = UnityEngine.Random.Range(1, 4);

    }

    public IEnumerator waitforHit()
    {
        yield return new WaitForSeconds(1);
        playerAnimator.SetTrigger("hit");
    }
    #endregion

    #region 战斗结束阶段

    /// <summary>
    /// 玩家碰撞碰撞体
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.transform.Find("PanelFight").GetComponent<BoxCollider2D>().enabled = true;
        if (isFirstOnTrigger == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine("FadeImage");
            }
        }
    }

    public IEnumerator FadeImage()
    {
        canvas.transform.Find("FadeOut_Image").gameObject.SetActive(true);
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.Find("FadeOut_Image").gameObject.GetComponent<Animator>().SetTrigger("fadeOut");
        isFirstOnTrigger = false;
        yield return new WaitForSeconds(2.0f);
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartANewGame();
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.Find("FadeOut_Image").gameObject.GetComponent<Animator>().SetTrigger("fadeIn");
        if (curEnemyFlag >= 2)
        {
            facade.PlayBGM("fight_boss");
        }
        yield return new WaitForSeconds(0.8f);
        playerGameObject.transform.GetChild(0).gameObject.SetActive(true);
        isFirstOnTrigger = true;
        canvas.transform.Find("FadeOut_Image").gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入下一关
    /// </summary>
    private void StartANewGame()
    {
        BuffManager.RemoveAllBuff(enemyHero);
        Destroy(playerGameObject);
        playerGameObject = GameObject.Instantiate(Resources.Load<GameObject>(playerHero.heroCard.FightModel_path));
        playerShieldSpriteRender = playerGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerGameObject.transform.SetParent(fightInfoPanel.transform);
        playerGameObject.gameObject.GetComponent<RectTransform>().localPosition = playerHeroPosition;
        playerGameObject.tag = "Player";
        playerGameObject.name = "player";
        if (playerGameObject.GetComponent<AnimationCtrl>() == null)
            playerGameObject.AddComponent<AnimationCtrl>();
        playerAnimator = playerGameObject.GetComponent<Animator>();
        Destroy(playerAssitGameObject);
        if (playerAssitHero != null)
        {
            playerAssitGameObject = GameObject.Instantiate(Resources.Load<GameObject>(playerAssitHero.heroCard.FightModel_path + "_assist"));
            playerAssitGameObject.transform.SetParent(fightInfoPanel.transform);
            playerAssitGameObject.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            playerAssitGameObject.gameObject.GetComponent<RectTransform>().localPosition = playerAssitHeroPosition;
            playerAssitGameObject.name = "PlayerAssist";
            if (playerGameObject.GetComponent<AnimationCtrl>() == null)
                playerGameObject.AddComponent<AnimationCtrl>();
            playerAssistAnimator = playerAssitGameObject.GetComponent<Animator>();
        }
        Destroy(enemyGameObject);
        enemyHero = enemyList[curEnemyFlag];
        enemyGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HeroPrefabs/enemy/" + FightCtrl.enemyHero.heroCard.Hero_id));
        enemyShieldSpriteRender = enemyGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyGameObject.transform.SetParent(fightInfoPanel.transform);
        enemyGameObject.gameObject.GetComponent<RectTransform>().localPosition = enemyHeroPosition;
        enemyGameObject.tag = "Enemy";
        enemyGameObject.name = "enemy";
        if (enemyGameObject.GetComponent<AnimationCtrl>() == null)
            enemyGameObject.AddComponent<AnimationCtrl>();
        enemyAnimator = enemyGameObject.GetComponent<Animator>();
        IsFightPrepare = true;
        IsDamageDamageSettlement = false;
        IsFightFinish = false;
        IsFirstFightFinish = true;
        buttonDrawCard.GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/PanelFight/ImageBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(curStage.BG_1_path + curEnemyFlag);

    }

    /// <summary>
    /// 战斗结束，玩家获胜
    /// </summary>
    public void PlayerWin()
    {
        canvas.transform.Find("PanelFight").GetComponent<BoxCollider2D>().enabled = true;
        curEnemyFlag++;
        if (curEnemyFlag >= 3 || enemyList[curEnemyFlag] == null)
        {
            enemyAnimator.SetTrigger("foxdie");
            StartCoroutine("waitToOpenPlayerWin");
        }
        else
        {
            StartCoroutine("EnemyDie");
        }
    }

    /// <summary>
    /// 战斗结束，敌人获胜
    /// </summary>
    public void EnemyWin()
    {
        StartCoroutine("PlayerDie");
        StartCoroutine("waitToOpenEnemyWin");
    }

    /// <summary>
    /// 战斗结束确认按钮的绑定函数
    /// </summary>
    public void OnSureButtonClick()
    {
        LoadAsyncScene.nextSceneName = "world_scene";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
    }

    /// <summary>
    /// 玩家死亡处理
    /// </summary>
    private IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1.2f);
        playerAnimator.SetTrigger("die");
    }

    /// <summary>
    /// 敌人死亡处理
    /// </summary>
    private IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(1.2f);
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
        {
            Debug.Log(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("stand"));
            yield return null;
        }
        enemyAnimator.SetTrigger("foxdie");
        yield return new WaitForSeconds(1);
        if (!GuideCtrl.GuideIndex.isNew)
        {
            playerAnimator.SetTrigger("PlayerExit");

        }
        yield return new WaitForSeconds(0.5f);
        if (!GuideCtrl.GuideIndex.isNew)
        {
            if (playerAssistAnimator != null && playerAssitHero != null)
                playerAssistAnimator.SetTrigger("PlayerExit");
        }
    }

    /// <summary>
    /// 玩家胜利时等待弹框
    /// </summary>
    private IEnumerator waitToOpenPlayerWin()
    {
        yield return new WaitForSeconds(2.5f);
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        maskCardForeground = canvas.transform.Find("Mask").gameObject;
        maskCardForeground.SetActive(true);
        canvas.transform.Find("PanelWin").gameObject.SetActive(true);
        imageReward1.GetComponent<Image>().sprite = Resources.Load<Sprite>(curStage.Reward_2.Goods_path);
        imageReward1.transform.GetChild(0).GetComponent<Text>().text = curStage.Reward_2.Goods_has == 0 ? "" : curStage.Reward_2.Goods_has.ToString();
        imageReward2.GetComponent<Image>().sprite = Resources.Load<Sprite>(curStage.Reward_1.Goods_path);
        imageReward2.transform.GetChild(0).GetComponent<Text>().text = curStage.Reward_1.Goods_has == 0 ? "" : curStage.Reward_1.Goods_has.ToString();
        imageReward3.GetComponent<Image>().sprite = Resources.Load<Sprite>(curStage.Reward_3.Goods_path);
        imageReward3.transform.GetChild(0).GetComponent<Text>().text = curStage.Reward_3.Goods_has == 0 ? "" : curStage.Reward_3.Goods_has.ToString();
        imageReward4.GetComponent<Image>().sprite = Resources.Load<Sprite>(curStage.Reward_4.Goods_path);
        imageReward4.transform.GetChild(0).GetComponent<Text>().text = curStage.Reward_4.Goods_has == 0 ? "" : curStage.Reward_4.Goods_has.ToString();
        facade.StageFightResult(curStage, ReturnCode.Success);
        ExpText.text = "（经验x" + curStage.Exp_Reward.ToString() + "）";
        facade.SaveUserInfo();

    }

    /// <summary>
    /// 敌人胜利时等待弹框
    /// </summary>
    private IEnumerator waitToOpenEnemyWin()
    {
        yield return new WaitForSeconds(2.5f);
        enemyGameObject.transform.GetChild(0).gameObject.SetActive(false);
        maskCardForeground = canvas.transform.Find("Mask").gameObject;
        maskCardForeground.SetActive(true);
        canvas.transform.Find("PanelLost").gameObject.SetActive(true);
        facade.StageFightResult(curStage, ReturnCode.Fail);
    }

    #endregion

    #region 全局函数

    public void OnTipsButtonClick()
    {
        SettingPanel.SetActive(false);
        Tips_Image.transform.gameObject.SetActive(true);
    }
    public void OnTipsBackTosettingClick()
    {
        SettingPanel.SetActive(true);
        Tips_Image.transform.gameObject.SetActive(false);
    }
    #region 预览框相关
    public void OnPreViewButtonClick()//预览框是否开启的按钮
    {
        PreviewTopControlBool = !PreviewTopControlBool;
        if (PreviewTopControlBool == true)
        {
            PlayerPrefs.SetInt("PreviewTopControlBool_Save", 0);
            
        }
        else
        {
            PlayerPrefs.SetInt("PreviewTopControlBool_Save", 1);
          
        }


        if (PreviewTopControlBool == true)
        {
            PreviewButton.GetComponent<Image>().sprite = PreviewButtonOnOrOff[0];
            if (playerSelectCardCount > 0)
            {
                facade.PlayUIAudio("open");
                PreviewPanel.SetActive(true);
            }
        }
        else
        {
            facade.PlayUIAudio("close");
            PreviewButton.GetComponent<Image>().sprite = PreviewButtonOnOrOff[1];
            PreviewPanel.SetActive(false);
        }
        if (playerSelectCardCount > 0)
            UpdateShowPreviewPanel(true);
    }

    //用于判断是否新手期，需要开启预览框
    //private bool IsShowPreviewPanel = true;
    private Sprite[] PreviewButtonOnOrOff = new Sprite[2];
    //放回按钮
    public void OnPutBackButtonClock()
    {
        for (int i = playerCardGroupContent.Count - 1; i >= 0; i--)
        {

            playerCardGroupContent[i].gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            playerCardGroupContent[i].gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            playerCardGroupContent[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
            playerCardGroupContent[i].gameObject.GetComponent<CardCtrl>().ctrl = false;
            playerSelectCardCount--;
            playerCardGroupContent.RemoveAt(i);
        }
        playerSelectCardCount = 0;
        playerHero.CardGroup.ResetAllCount();
        cardCombineName.text = "组合名称：";
        cardCombineDescription.text = "组合效果：";
        UpdateShowPreviewPanel(false);//隐藏预览框，因为此时没有选择牌


    }

    /// <summary>
    /// 隐藏或显示预览框
    /// </summary>
    /// 
    public void UpdateShowPreviewPanel(bool isActive)
    {
        if (PreviewTopControlBool == true)
        {
            if (isActive == true)
            {
                PreviewPanel.SetActive(true);
            }
            else
            {
                PreviewPanel.SetActive(false);
            }

        }
        else
        {
            PreviewPanel.SetActive(false);
        }

    }

    #endregion

    #region 音量设置

    /// <summary>
    /// 音乐设置滑动条值改变
    /// </summary>
    private void SliderMusicOnChange()
    {
        facade.SetBGMVolumn(SliderMusic.value);
        if (SliderMusic.value == 0)
        {
            SliderMusicImage.sprite = Resources.Load<Sprite>("UI/MusicOff");
        }
        else
        {
            SliderMusicImage.sprite = Resources.Load<Sprite>("UI/MusicOn");
        }
    }

    /// <summary>
    /// 音效设置滑动条值改变
    /// </summary>
    private void SliderSoundEffectOnChange()
    {
        facade.SetDrawCardAudioVolumn(SliderSoundEffect.value);
        facade.SetSkillAudioVolumn(SliderSoundEffect.value);
        facade.SetUIAudioVolumn(SliderSoundEffect.value);
        facade.PlayUIAudio("button");
        if (SliderSoundEffect.value == 0)
        {
            SliderSoundEffectImage.sprite = Resources.Load<Sprite>("UI/SoundEffectOff");
        }
        else
        {
            SliderSoundEffectImage.sprite = Resources.Load<Sprite>("UI/SoundEffectOn");
        }
    }

    #endregion

    public void OnWinButtonClick()
    {
        facade.PlayUIAudio("button");
        LoadAsyncScene.nextSceneName = "world_scene";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
    }

    /// <summary>
    /// 元素类型转文字描述
    /// </summary>
    /// <param name="elemType">元素类型</param>
    /// <returns></returns>
    private string ElemtypeToString(string elemType)
    {
        if (elemType == "Water")
        {
            return "水";
        }
        else if (elemType == "Fire")
        {
            return "火";
        }
        else if (elemType == "Thunder")
        {
            return "雷";
        }
        else if (elemType == "Wind")
        {
            return "风";
        }
        else if (elemType == "Earth")
        {
            return "地";
        }
        else if (elemType == "Dark")
        {
            return "暗";
        }
        else
        {
            return "普通";
        }
    }

    /// <summary>
    /// 玩家抽卡，抽卡时判定，如果当前选择卡数小于当前行动点数时，隐藏未选择的卡
    /// </summary>
    private void PlayerGetCard()
    {
        for (int i = curPlayerHandCardCount; i < 5; i++)
        {
            GameObject gameObject = GameObject.Instantiate(Resources.Load("Prefab/card") as GameObject);
            gameObject.transform.SetParent(playerCardContent.transform);
            gameObject.transform.localScale = new Vector3(1, 1, 0);
            playerHero.CardGroup.GetShuffedCard(gameObject);
            //playerFightCardGroupManager.GetShuffledCard(gameObject);
            gameObject.GetComponent<Button>().onClick.AddListener(delegate () { PlayerChooseCard(gameObject); });
            curPlayerHandCardCount++;
        }
        if (!GuideCtrl.GuideIndex.isNew)
        {
            for (int i = 0; i < playerCardContent.transform.childCount; i++)
            {
                playerCardContent.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>
    /// 随机一个元素类型
    /// </summary>
    /// <returns></returns>
    private string RandomElementType()
    {
        int randValue = UnityEngine.Random.Range(1, 7);
        switch (randValue)
        {
            case 1:
                return "Water";
            case 2:
                return "Fire";
            case 3:
                return "Thunder";
            case 4:
                return "Wind";
            case 5:
                return "Earth";
            case 6:
                return "Dark";
            default:
                return "Chaos";
        }
    }

    /// <summary>
    /// 评级的图片
    /// </summary>
    private Sprite GradeToSprite(string grade)
    {
        if (grade == "D")
        {
            return Resources.Load<Sprite>("Image/Icon/D");
        }
        else if (grade == "C")
        {
            return Resources.Load<Sprite>("Image/Icon/C");
        }
        else if (grade == "B")
        {
            return Resources.Load<Sprite>("Image/Icon/B");
        }
        else if (grade == "A")
        {
            return Resources.Load<Sprite>("Image/Icon/A");
        }
        else
        {
            return Resources.Load<Sprite>("Image/Icon/S");
        }
    }

    /// <summary>
    /// 敌人抽卡，抽卡时判定，如果当前选择卡数小于当前行动点数时，隐藏未选择的卡
    /// </summary>
    private void EnemyGetCard()
    {
        for (int i = curEnemyCardCount; i < 5; i++)
        {
            ElementType elementType = (ElementType)Enum.Parse(typeof(ElementType), RandomElementType());
            Card card = new Card(elementType);
            enemyCard.Add(card);
            curEnemyCardCount++;
        }
    }

    /// <summary>
    /// 玩家选择卡牌
    /// </summary>
    /// <param name="gameObject">卡牌对象</param>
    private void PlayerChooseCard(GameObject gameObject)
    {
        if (gameObject.GetComponent<CardCtrl>().ctrl == false)
        {
            if (GuideCtrl.GuideIndex.isNew)
            {
                Finger.GetComponent<Animator>().SetBool("Tip_DrawCard", true);
                gameObject.GetComponent<Button>().interactable = false;
            }
            playerHero.CardGroup.PrepareShowCard(gameObject);
            //playerFightCardGroupManager.PrepareShowCard(gameObject);
            gameObject.GetComponent<CardCtrl>().ctrl = true;
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = chooseSprite;
            //显示选择框
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            playerSelectCardCount++;
            //将选择的牌上移
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
            //将当前选择的牌加入出牌列表
            playerCardGroupContent.Add(gameObject);
            UpdateShowPreviewPanel(true);
            //用于记录组合id
            string id = playerHero.CardGroup.FireCount.ToString() + playerHero.CardGroup.WaterCount.ToString() + playerHero.CardGroup.EarthCount.ToString()
                      + playerHero.CardGroup.WindCount.ToString() + playerHero.CardGroup.ThunderCount.ToString() + playerHero.CardGroup.DarkCount.ToString();
            int count = playerHero.CardGroup.FireCount + playerHero.CardGroup.WaterCount + playerHero.CardGroup.EarthCount
                      + playerHero.CardGroup.WindCount + playerHero.CardGroup.ThunderCount + playerHero.CardGroup.DarkCount;
            cardCombineName.text = "";
            cardCombineDescription.text = "";
            if (cardGroupDict.ContainsKey(id))
            {
                cardCombineName.text = "";
                cardCombineName.text = cardGroupDict[id].CombineName + "(" + cardGroupDict[id].FixedDamage + "点" + ElemtypeToString(cardGroupDict[id].FixedDamageAttribute.ToString()) + "伤害)";
                gradeImage.sprite = GradeToSprite(cardGroupDict[id].Grade);
                if (cardGroupDict[id].buffID1 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID1))
                    {
                        if (count <= 4)
                        {
                            int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                            cardCombineDescription.text = "";
                            cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                              + buffDict[cardGroupDict[id].buffID1].BuffDes;
                        }
                        else
                        {
                            int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                            BuffClass buffClass = (BuffClass)Enum.Parse(typeof(BuffClass), cardGroupDict[id].buffID1);
                            switch (buffClass)
                            {
                                case BuffClass.Amaterasu:
                                    if (enemyHero.BuffDictionary.ContainsKey(BuffClass.BlazingFire))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(不灭心炎之域未满足开启条件)";
                                    }
                                    break;
                                case BuffClass.FrostField:
                                    if (playerHero.BuffDictionary.ContainsKey(BuffClass.FrostPower))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(冰霜领域未满足开启条件)";
                                    }
                                    break;
                                case BuffClass.EarthFiled:
                                    if (playerHero.BuffDictionary.ContainsKey(BuffClass.HolyShieldShelter))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(大地领域未满足开启条件)";
                                    }
                                    break;
                                case BuffClass.WindyFiled:
                                    if (playerHero.BuffDictionary.ContainsKey(BuffClass.Hurricane))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(风之领域未满足开启条件)";
                                    }
                                    break;
                                case BuffClass.DarkFiled:
                                    if (playerHero.BuffDictionary.ContainsKey(BuffClass.EvilTouch))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(暗夜领域未满足开启条件)";
                                    }
                                    break;
                                case BuffClass.ThunderField:
                                    if (enemyHero.BuffDictionary.ContainsKey(BuffClass.ThunderWrath))
                                    {
                                        cardCombineDescription.text = "";
                                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    }
                                    else
                                    {
                                        cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                                        cardCombineDescription.text = "" + "\n(雷霆领域未满足开启条件)";
                                    }
                                    break;
                                default:
                                    cardCombineDescription.text = "";
                                    cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                                      + buffDict[cardGroupDict[id].buffID1].BuffDes;
                                    break;
                            }
                        }
                    }
                }
                if (cardGroupDict[id].buffID2 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID2))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber2);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID2].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID2].BuffDes;
                    }
                }
                if (cardGroupDict[id].buffID3 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID3))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber3);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID3].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID3].BuffDes;
                    }
                }
                if (cardGroupDict[id].buffID4 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID4))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber4);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID4].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID4].BuffDes;
                    }
                }
            }
            else
            {
                if (count == 4)
                {
                    cardCombineName.text = "圣者遗物" + "(" + 40 + "点" + "普通伤害)";
                    cardCombineDescription.text = "";
                    gradeImage.sprite = GradeToSprite("B");
                }
                else
                {
                    cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                    cardCombineDescription.text = "";
                    gradeImage.sprite = GradeToSprite("A");
                }
            }
        }
        else
        {
            playerHero.CardGroup.AbandomPrepareCard(gameObject);
            //用于记录组合id
            string id = playerHero.CardGroup.FireCount.ToString() + playerHero.CardGroup.WaterCount.ToString() + playerHero.CardGroup.EarthCount.ToString()
                      + playerHero.CardGroup.WindCount.ToString() + playerHero.CardGroup.ThunderCount.ToString() + playerHero.CardGroup.DarkCount.ToString();
            cardCombineName.text = "暂无此组合，请联系开发人员";
            cardCombineDescription.text = "";
            if (cardGroupDict.ContainsKey(id))
            {
                gradeImage.sprite = GradeToSprite(cardGroupDict[id].Grade);
                cardCombineName.text = cardGroupDict[id].CombineName + "(" + cardGroupDict[id].FixedDamage + "点" + ElemtypeToString(cardGroupDict[id].FixedDamageAttribute.ToString()) + "伤害)";
                if (cardGroupDict[id].buffID1 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID1))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber1);
                        cardCombineDescription.text = "";
                        cardCombineDescription.text = buffDict[cardGroupDict[id].buffID1].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID1].BuffDes;
                    }
                }
                if (cardGroupDict[id].buffID2 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID2))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber2);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID2].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID2].BuffDes;
                    }
                }
                if (cardGroupDict[id].buffID3 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID3))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber3);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID3].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID3].BuffDes;
                    }
                }
                if (cardGroupDict[id].buffID4 != "0")
                {
                    if (buffDict.ContainsKey(cardGroupDict[id].buffID4))
                    {
                        int fixedCoefficient = Convert.ToInt32(cardGroupDict[id].addNumber4);
                        cardCombineDescription.text += "\n" + buffDict[cardGroupDict[id].buffID4].BuffName + "(+ " + fixedCoefficient.ToString() + ")" + " :" + "\n"
                                                          + buffDict[cardGroupDict[id].buffID4].BuffDes;
                    }
                }
            }
            else
            {
                int count = playerHero.CardGroup.FireCount + playerHero.CardGroup.WaterCount + playerHero.CardGroup.EarthCount
                      + playerHero.CardGroup.WindCount + playerHero.CardGroup.ThunderCount + playerHero.CardGroup.DarkCount;
                if (count == 4)
                {
                    cardCombineName.text = "圣者遗物" + "(" + 40 + "点" + "普通伤害)";
                    cardCombineDescription.text = "";
                    gradeImage.sprite = GradeToSprite("B");
                }
                else
                {
                    cardCombineName.text = "圣剑" + "(" + 65 + "点" + "普通伤害)";
                    cardCombineDescription.text = "";
                    gradeImage.sprite = GradeToSprite("A");
                }
            }
            gameObject.GetComponent<CardCtrl>().ctrl = false;
            //将取消选择的牌下移
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            playerSelectCardCount--;
            //将取消选择的牌从出牌列表移除
            playerCardGroupContent.Remove(gameObject);
            if (playerSelectCardCount <= 0)
            {
                UpdateShowPreviewPanel(false);
            }
        }
    }

    /// <summary>
    /// 当选择的出牌数大于行动点时，自动将最先选择的一张卡下移并取消选择
    /// </summary>
    private void PlayerUnchooseCard()
    {
        playerSelectCardCount--;
        playerCardGroupContent[0].transform.GetChild(1).gameObject.SetActive(false);
        playerCardGroupContent[0].GetComponent<CardCtrl>().ctrl = false;
        playerCardGroupContent[0].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        playerCardGroupContent[0].transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        playerCardGroupContent.RemoveAt(0);

    }

    /// <summary>
    /// 添加buff图标
    /// </summary>
    /// <param name="fightHero">作用对象</param>
    /// <param name="buffClass">buff类型</param>
    public static void AddBuffIcon(FightHero fightHero, BuffClass buffClass)
    {
        if (fightHero.Equals(playerHero))
        {
            GameObject gameObject = GameObject.Instantiate(Resources.Load("Prefab/buff") as GameObject);
            gameObject.transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/" + buffClass.ToString() + "Buff");
            gameObject.GetComponent<BuffCtrl>().buffClass = buffClass;
            gameObject.GetComponent<BuffCtrl>().originalDurationTime = fightHero.BuffDictionary[buffClass].DurationTime;
            gameObject.GetComponent<BuffCtrl>().baseBuff = fightHero.BuffDictionary[buffClass];
            gameObject.transform.SetParent(playerBuffContent.transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject gameObject = GameObject.Instantiate(Resources.Load("Prefab/buff") as GameObject);
            gameObject.transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/" + buffClass.ToString() + "Buff");
            gameObject.GetComponent<BuffCtrl>().buffClass = buffClass;
            gameObject.GetComponent<BuffCtrl>().originalDurationTime = fightHero.BuffDictionary[buffClass].DurationTime;
            gameObject.GetComponent<BuffCtrl>().baseBuff = fightHero.BuffDictionary[buffClass];
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.SetParent(enemyBuffContent.transform);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            gameObject.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(-363, -260);
            //gameObject.transform.Find("bg").GetComponent<RectTransform>().anchoredPosition = new Vector2(-363, -200);
        }
    }

    /// <summary>
    /// 删除buff图标
    /// </summary>
    /// <param name="fightHero">作用对象</param>
    /// <param name="buffClass">buff类型</param>
    public static void DeleteBuffIcon(FightHero fightHero, BuffClass buffClass)
    {
        if (fightHero.Equals(playerHero))
        {
            for (int i = 0; i < playerBuffContent.transform.childCount; i++)
            {
                if (playerBuffContent.transform.GetChild(i).gameObject.GetComponent<BuffCtrl>().buffClass == buffClass)
                {
                    Destroy(playerBuffContent.transform.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            for (int i = 0; i < enemyBuffContent.transform.childCount; i++)
            {
                if (enemyBuffContent.transform.GetChild(i).gameObject.GetComponent<BuffCtrl>().buffClass == buffClass)
                {
                    Destroy(enemyBuffContent.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    //打开设置界面的按钮
    public void OnSettingPanelButtonClick()
    {
        facade.PlayUIAudio("open");
        //TODO游戏时间停止流动
        playerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        SettingPanel.SetActive(true);
        SettingPanel.GetComponent<Animator>().Play("SettingPanelScale");
        Time.timeScale = 0;
        canvas.transform.Find("Mask").gameObject.SetActive(true);
        for (int i = 0; i < GameObject.Find("Effect").transform.childCount; i++)
        {
            GameObject.Find("Effect").transform.GetChild(i).gameObject.SetActive(false);
        }

    }
    //继续游戏的按钮
    public void OnContinueButtonClick()
    {
        facade.PlayUIAudio("close");
        playerGameObject.transform.GetChild(0).gameObject.SetActive(true);
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
        canvas.transform.Find("Mask").gameObject.SetActive(false);
        for (int i = 0; i < GameObject.Find("Effect").transform.childCount; i++)
        {
            GameObject.Find("Effect").transform.GetChild(i).gameObject.SetActive(true);
        }
        //TODO游戏时间开始流动
    }

    ////音乐设置滑动条
    //private Slider SliderMusic;
    ////音乐滑动头图片
    //private Image SliderMusicImage;
    ////音效设置滑动条
    //private Slider SliderSoundEffect;

    /// <summary>
    /// 调试窗口按钮
    /// </summary>
    private void DebugSettingButtonOnClick()
    {
        facade.PlayUIAudio("open");
        winButton.gameObject.SetActive(false);
        debugSettingPanel.SetActive(true);
        Tips_Button.gameObject.SetActive(false);
        SliderMusic.gameObject.SetActive(false);
        SliderSoundEffect.gameObject.SetActive(false);
        TextSoundEffect.SetActive(false);
        TextMusic.SetActive(false);
    }

    /// <summary>
    /// 调试面板初始化
    /// </summary>
    public static void DebugSettingPanelInit()
    {
        FightCtrl.debugSettingPanel.GetComponent<DebugPanelCtrl>().Init(playerHero);
    }

    /// <summary>
    /// 立即胜利按钮
    /// </summary>
    private void WinButtonOnClick()
    {
        facade.PlayUIAudio("button");
        IsFightPrepare = false;
        IsDamageDamageSettlement = false;
        Time.timeScale = 1;
        SettingPanel.SetActive(false);
        canvas.transform.Find("Mask").gameObject.SetActive(false);
        curEnemyFlag = 3;
        PlayerWin();
    }

    /// <summary>
    /// 主动技能点击
    /// </summary>
    private void ButtonSkillOnClick()
    {
        if (GuideCtrl.GuideIndex.isNew)
        {
            GuideIndex++;
            Finger.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Finger.GetComponent<Animator>().SetTrigger("Step2");
            playerActionSkill.GetComponent<Button>().interactable = false;
            playerHero.heroCard.Action_skill_function(FightPhase.InIntend, playerHero, enemyHero);
            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/ActiveEffect")) as GameObject;
            effect.transform.SetParent(playerGameObject.transform);
            effect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            effect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            DestroySelf.isEnemy = false;

            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(100, 100, 1);
        }
        if (IsFightPrepare)
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    if (isPlayingAnimation == true || isPlayingEffect == true)
            //        return;
            //}
            playerHero.heroCard.Action_skill_function(FightPhase.InIntend, playerHero, enemyHero);
            GameObject effect = Instantiate(Resources.Load<GameObject>(playerHero.heroCard.Action_skill.Skill_effect_path)) as GameObject;
            effect.transform.SetParent(playerGameObject.transform);
            effect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            effect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            DestroySelf.isEnemy = false;

            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(100, 100, 1);
        }
    }

    /// <summary>
    /// 助战技能点击
    /// </summary>
    private void ButtonAssistSkillOnClick()
    {
        if (IsFightPrepare)
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    if (isPlayingAnimation == true || isPlayingEffect == true)
            //        return;
            //}
            playerAssitHero.heroCard.Staff_skill_function(FightPhase.InIntend, playerHero, enemyHero);
            if (playerAssitHero.heroCard.Staff_skill_self.isActionSkill == true)
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/assist_Effect")) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                DestroySelf.isEnemy = false;

                GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
                spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
                spell.GetComponent<RectTransform>().localPosition = new Vector3(-286, 0, 0);
                spell.transform.localScale = new Vector3(100, 100, 1);
            }
        }
    }

    #region 飘字

    /// <summary>
    /// 飘字函数，flag为false时伤害飘字，true为回血飘字
    /// </summary>
    public static void ShowWord(FightHero fightHero, float value, WordType wordType, bool isDelay = false)
    {
        //普通伤害
        if (wordType == WordType.Normal)
        {
            if (fightHero.Equals(enemyHero))
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.GetComponent<Text>().color = new Color(1, 0, 0);
                EnemyShowWordAdd(zi, isDelay);
            }
            else
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.GetComponent<Text>().color = new Color(1, 0, 0);
                PlayerShowWordAdd(zi, isDelay);
            }
        }
        //吸血
        else if (wordType == WordType.SuckBlood)
        {
            if (fightHero.Equals(playerHero))
            {
                if (playerHero.CurSuckBloodCoefficient > 0)
                {
                    if (playerHero.CurHp + value <= playerHero.MaxHp)
                    {
                        value = Mathf.Floor(value);
                        GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                        zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                        zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                        zi.GetComponent<Text>().color = new Color(0, 1, 0);
                        PlayerShowWordAdd(zi, isDelay);
                    }
                    else
                    {
                        value = playerHero.MaxHp - playerHero.CurHp;
                        value = Mathf.Floor(value);
                        if (value <= 0)
                        {
                            return;
                        }
                        GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                        zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                        zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                        zi.GetComponent<Text>().color = new Color(0, 1, 0);
                        PlayerShowWordAdd(zi, isDelay);
                    }
                }
            }
            else
            {
                if (enemyHero.CurSuckBloodCoefficient > 0)
                {
                    if (enemyHero.CurHp + value <= enemyHero.MaxHp)
                    {
                        value = Mathf.Floor(value);
                        GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                        zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                        zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                        zi.GetComponent<Text>().color = new Color(0, 1, 0);
                        EnemyShowWordAdd(zi, isDelay);
                    }
                    else
                    {
                        value = enemyHero.MaxHp - enemyHero.CurHp;
                        value = Mathf.Floor(value);
                        if (value <= 0)
                        {
                            return;
                        }
                        GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                        zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                        zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                        zi.GetComponent<Text>().color = new Color(0, 1, 0);
                        EnemyShowWordAdd(zi, isDelay);
                    }
                }
            }
        }
        //回血
        else if (wordType == WordType.Recover)
        {
            if (fightHero.Equals(playerHero))
            {
                if (playerHero.CurHp + value <= playerHero.MaxHp)
                {
                    value = Mathf.Floor(value);
                    GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                    zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                    zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                    zi.GetComponent<Text>().color = new Color(0, 1, 0);
                    PlayerShowWordAdd(zi, isDelay);
                }
                else
                {
                    value = playerHero.MaxHp - playerHero.CurHp;
                    value = Mathf.Floor(value);
                    GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                    zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                    zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                    zi.GetComponent<Text>().color = new Color(0, 1, 0);
                    PlayerShowWordAdd(zi, isDelay);
                }
            }
            else
            {
                if (enemyHero.CurHp + value <= enemyHero.MaxHp)
                {
                    value = Mathf.Floor(value);
                    GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                    zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                    zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                    zi.GetComponent<Text>().color = new Color(0, 1, 0);
                    EnemyShowWordAdd(zi, isDelay);
                }
                else
                {
                    value = enemyHero.MaxHp - enemyHero.CurHp;
                    value = Mathf.Floor(value);
                    GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                    zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                    zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    zi.GetComponent<Text>().text = "+" + Math.Round(value, 1).ToString();
                    zi.GetComponent<Text>().color = new Color(0, 1, 0);
                    EnemyShowWordAdd(zi, isDelay);
                }
            }
        }
        //暴击
        else if (wordType == WordType.CritDamage)
        {
            if (fightHero.Equals(playerHero))
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/CritCritDamage"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.transform.GetChild(0).GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 116 / 255, 0);
                PlayerShowWordAdd(zi, isDelay);
            }
            else
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyCritDamage"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.transform.GetChild(0).GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 116 / 255, 0);
                EnemyShowWordAdd(zi, isDelay);
            }
        }
        //闪避
        else if (wordType == WordType.Dodge)
        {
            if (fightHero.Equals(playerHero))
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "miss";
                zi.GetComponent<Text>().color = new Color(0.9f, 0.9f, 0.9f);
                PlayerShowWordAdd(zi, isDelay);
            }
            else
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "miss";
                zi.GetComponent<Text>().color = new Color(0.9f, 0.9f, 0.9f);
                EnemyShowWordAdd(zi, isDelay);
            }
        }
        //真实伤害
        else if (wordType == WordType.RealDamage)
        {
            if (fightHero.Equals(playerHero))
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.GetComponent<Text>().color = new Color(1, 1, 1);
                PlayerShowWordAdd(zi, isDelay);
            }
            else
            {
                GameObject zi = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/EnemyDamage_Text"));
                zi.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelShowWordInfo").transform);
                zi.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                zi.GetComponent<Text>().text = "-" + Math.Round(value, 1).ToString();
                zi.GetComponent<Text>().color = new Color(1, 1, 1);
                EnemyShowWordAdd(zi, isDelay);
            }
        }
    }

    /// <summary>
    /// 添加玩家飘字
    /// </summary>
    private static void PlayerShowWordAdd(GameObject gameObject, bool delay)
    {
        if (playerWordList != null)
        {
            if (gameObject != null)
            {
                Word word = new Word(gameObject, delay);
                gameObject.SetActive(false);
                playerWordList.Add(word);
            }
        }
    }

    /// <summary>
    /// 添加敌人飘字
    /// </summary>
    private static void EnemyShowWordAdd(GameObject gameObject, bool delay)
    {
        if (enemyWordList != null)
        {
            if (gameObject != null)
            {
                Word word = new Word(gameObject, delay);
                gameObject.SetActive(false);
                enemyWordList.Add(word);
            }
        }
    }

    /// <summary>
    /// 开启飘字协程
    /// </summary>
    private void StartShowWord()
    {
        StartCoroutine(PlayerShowWord());
        StartCoroutine(EnemytShowWord());
    }

    /// <summary>
    /// 玩家飘字协程
    /// </summary>
    private IEnumerator PlayerShowWord()
    {
        while (true)
        {
            if (playerWordList.Count <= 0)
            {
                yield return null;
                continue;
            }
            if (playerWordList[0].gameObject != null)
            {
                if(playerWordList[0].isDelay)
                {
                    yield return new WaitForSeconds(1.4f);
                }
                playerWordList[0].gameObject.SetActive(true);
                playerWordList.RemoveAt(0);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 玩家飘字协程
    /// </summary>
    private IEnumerator EnemytShowWord()
    {
        while (true)
        {
            if (enemyWordList.Count <= 0)
            {
                yield return null;
                continue;
            }
            if (enemyWordList[0].gameObject != null)
            {
                if (enemyWordList[0].isDelay)
                {
                    yield return new WaitForSeconds(1.4f);
                }
                enemyWordList[0].gameObject.SetActive(true);
                enemyWordList.RemoveAt(0);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 重置飘字列表
    /// </summary>
    private void Clear()
    {
        if (playerWordList.Count <= 0)
        {
            return;
        }
        while (playerWordList[0].gameObject != null)
        {
            playerWordList.RemoveAt(0);
        }
    }

    #endregion

    /// <summary>
    /// 计算类
    /// </summary>
    public class AllCaculation
    {
        /// <summary>
        /// 伤害计算函数
        /// </summary>
        /// <param name="damage">伤害数值</param>
        /// <param name="attacker">进攻方</param>
        /// <param name="defender">防守方</param>
        /// <param name="damageResource">伤害来源</param>
        /// <param name="damageType">伤害类型</param>
        /// <param name="elementType">元素类型</param>
        public static void DamageCaculation(float damage, FightHero attacker, FightHero defender, DamageResource damageResource,
                                            DamageType damageType, ElementType elementType = ElementType.Chaos)
        {
            if (damageResource == DamageResource.DirectDamage)
            {
                playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeDemage, attacker, defender);
                playerHero.heroCard.Staff_skill_function(FightPhase.BeforeDemage, playerHero, enemyHero);
                playerHero.heroCard.Prossive_skill_function(FightPhase.InDemage, attacker, defender);
                playerHero.heroCard.Staff_skill_function(FightPhase.InDemage, playerHero, enemyHero);
            }
            //最终伤害
            float finalDamage = 0;
            //伤害记录
            DamageRecord damageRecord = new DamageRecord();

            //敌人无敌
            if (defender.CurIsInvinsible == true)
            {
                return;
            }
            //无伤害
            if (damage <= 0)
            {
                return;
            }
            //真实伤害
            if (damageType == DamageType.Real)
            {
                damage = Mathf.Floor(damage);
                defender.CurHp -= damage;
                defender.TotalRecivedDamage += damage;
                //填充双方的伤害记录
                damageRecord.damageResource = damageResource;
                damageRecord.damageType = damageType;
                damageRecord.elementType = elementType;
                damageRecord.damageValue = damage;
                attacker.DamageToEnemyRecords.Add(damageRecord);
                defender.DamageFromEnemyRecords.Add(damageRecord);
                //飘字
                if (damageResource == DamageResource.CollateralDamage)
                {
                    ShowWord(defender, damage, WordType.RealDamage);
                }
                else
                {
                    ShowWord(defender, damage, WordType.RealDamage, true);
                }
                return;
            }
            //闪避
            float dodgeValue = UnityEngine.Random.Range(1, 10000);
            if (dodgeValue < defender.CurDodgeCoefficient * 10000)
            {
                //飘字
                if (damageResource == DamageResource.CollateralDamage)
                {
                    ShowWord(defender, damage, WordType.Dodge);
                }
                else
                {
                    ShowWord(defender, damage, WordType.Dodge, true);
                }
                return;
            }
            //暴击数值
            float critCorfficient = 1;
            if (damageResource == DamageResource.DirectDamage)
            {
                float critialValue = UnityEngine.Random.Range(1, 10000);
                if (critialValue < attacker.CurCriticalCoefficient * 10000)
                {
                    critCorfficient = attacker.CurCritMutipleCoefficient;
                }
            }
            //普通伤害
            if (damageType == DamageType.Normal || elementType == ElementType.Chaos)
            {
                finalDamage = (damage + attacker.CurDamageIncrementValue - defender.CurDamageParry)
                              * attacker.CurChaosDamageCoefficient
                              * defender.CurChaosDamageResistCoefficient
                              * critCorfficient;
                finalDamage = Mathf.Floor(finalDamage);
                finalDamage -= defender.CurShield;
                if (finalDamage < 0)
                {
                    finalDamage = 0;
                }
                defender.CurHp -= finalDamage;
                defender.TotalRecivedDamage += finalDamage * critCorfficient;
                //吸血飘字
                ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                attacker.CurHp += finalDamage * attacker.SuckBloodCoefficientDeviant;
                //更新防守者的受到伤害类型
                defender.FinalDamageElemType = ElementType.Chaos;
                //填充双方的伤害记录
                damageRecord.damageResource = damageResource;
                damageRecord.damageType = damageType;
                damageRecord.elementType = elementType;
                damageRecord.damageValue = damage;
                attacker.DamageToEnemyRecords.Add(damageRecord);
                defender.DamageFromEnemyRecords.Add(damageRecord);
                //触发伤害技能
                if (attacker.Equals(playerHero) && damageResource == DamageResource.DirectDamage)
                {
                    BuffManager.SetActionAfterDamage(playerHero, enemyHero);
                    playerHero.heroCard.Prossive_skill_function(FightPhase.AfterDemage, attacker, defender);
                    playerHero.heroCard.Staff_skill_function(FightPhase.AfterDemage, playerHero, enemyHero);
                }
                if (attacker.Equals(enemyHero) && damageResource == DamageResource.DirectDamage)
                {
                    BuffManager.SetActionAfterDamage(enemyHero, playerHero);
                    enemyHero.heroCard.Prossive_skill_function(FightPhase.AfterDemage, attacker, defender);
                    enemyHero.heroCard.Staff_skill_function(FightPhase.AfterDemage, playerHero, enemyHero);
                }
                //飘字
                if (critCorfficient > 1)
                {
                    if (damageResource == DamageResource.CollateralDamage)
                    {
                        ShowWord(defender, finalDamage, WordType.CritDamage);
                    }
                    else
                    {
                        ShowWord(defender, finalDamage, WordType.CritDamage, true);
                    }
                    return;
                }
                if (damageResource == DamageResource.CollateralDamage)
                {
                    ShowWord(defender, finalDamage, WordType.Normal);
                }
                else
                {
                    ShowWord(defender, finalDamage, WordType.Normal, true);
                }
                return;
            }
            //元素伤害
            else
            {
                float damageIncrement = 0;
                if (damageResource == DamageResource.DirectDamage)
                {
                    damageIncrement = attacker.CurDamageIncrementValue;
                }
                else
                {
                    damageIncrement = attacker.CurDamageIncrementValue / 5;
                }
                switch (elementType)
                {
                    case ElementType.Water:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurWaterStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurWaterStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurWaterStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurWaterDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurWaterResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurWaterStrength / 100, 2) + 0.4f)
                                    * attacker.CurWaterDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        Debug.Log(finalDamage);
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Water;
                        //更新防守者的元素受到的总伤害
                        defender.WaterTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发

                        break;
                    case ElementType.Fire:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurFireStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurFireStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurFireStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurFireDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurFireResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurFireStrength / 100, 2) + 0.4f)
                                    * attacker.CurFireDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Fire;
                        //更新防守者的元素受到的总伤害
                        defender.FireTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发
                        break;
                    case ElementType.Thunder:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurThunderStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurThunderStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurThunderStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurThunderDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurThunderResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurThunderStrength / 100, 2) + 0.4f)
                                    * attacker.CurThunderDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Thunder;
                        //更新防守者的元素受到的总伤害
                        defender.ThunderTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发
                        break;
                    case ElementType.Wind:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurWindStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurWindStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurWindStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurWindDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurWindResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurWaterStrength / 100, 2) + 0.4f)
                                    * attacker.CurWindDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Wind;
                        //更新防守者的元素受到的总伤害
                        defender.WindTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发
                        break;
                    case ElementType.Earth:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurEarthStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurEarthStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurEarthStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurEarthDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurEarthResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurEarthStrength / 100, 2) + 0.4f)
                                    * attacker.CurEarthDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Earth;
                        //更新防守者的元素受到的总伤害
                        defender.EarthTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发

                        break;
                    case ElementType.Dark:
                        finalDamage = ((1 - attacker.CurDamagePierceCoefficient) * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurDarkStrength / 100, 2) + 0.4f)
                                    * (1 - (defender.CurDarkStrength + defender.CurDamageResist) / 600)
                                    / (1 + (defender.CurDarkStrength + defender.CurDamageResist) / 600)
                                    * attacker.CurDarkDamageCoefficient * attacker.CurElemDamageCoefficient
                                    / defender.CurDarkResistCoefficient / defender.CurElemResistCoefficient
                                    + attacker.CurDamagePierceCoefficient * (damage + damageIncrement)
                                    * (Mathf.Pow(attacker.CurDarkStrength / 100, 2) + 0.4f)
                                    * attacker.CurDarkDamageCoefficient * attacker.CurElemDamageCoefficient
                                    - defender.CurDamageParry)
                                    * attacker.CurChaosDamageCoefficient
                                    / defender.CurChaosDamageResistCoefficient
                                    * critCorfficient;
                        finalDamage = Mathf.Floor(finalDamage);
                        finalDamage -= defender.CurShield;
                        if (finalDamage < 0)
                        {
                            finalDamage = 0;
                        }
                        defender.CurHp -= finalDamage;
                        defender.TotalRecivedDamage += finalDamage;
                        //更新防守者的受到伤害类型
                        defender.FinalDamageElemType = ElementType.Dark;
                        //更新防守者的元素受到的总伤害
                        defender.DarkTotalRecivedDamage += finalDamage;
                        //吸血飘字
                        ShowWord(attacker, finalDamage * attacker.CurSuckBloodCoefficient, WordType.SuckBlood, true);
                        attacker.CurHp += finalDamage * attacker.CurSuckBloodCoefficient;
                        //这里写技能触发
                        break;

                }
                //填充双方的伤害记录
                damageRecord.damageResource = damageResource;
                damageRecord.damageType = damageType;
                damageRecord.elementType = elementType;
                damageRecord.damageValue = damage;
                attacker.DamageToEnemyRecords.Add(damageRecord);
                defender.DamageFromEnemyRecords.Add(damageRecord);
                if (attacker.Equals(playerHero) && damageResource == DamageResource.DirectDamage)
                {
                    BuffManager.SetActionAfterDamage(playerHero, enemyHero);
                    playerHero.heroCard.Prossive_skill_function(FightPhase.AfterDemage, attacker, defender);
                    playerHero.heroCard.Staff_skill_function(FightPhase.AfterDemage, playerHero, enemyHero);
                }
                if (attacker.Equals(enemyHero) && damageResource == DamageResource.DirectDamage)
                {
                    BuffManager.SetActionAfterDamage(enemyHero, playerHero);
                    enemyHero.heroCard.Prossive_skill_function(FightPhase.AfterDemage, attacker, defender);
                    enemyHero.heroCard.Staff_skill_function(FightPhase.AfterDemage, playerHero, enemyHero);
                }
                //飘字
                if (critCorfficient > 1)
                {
                    if (damageResource == DamageResource.CollateralDamage)
                    {
                        ShowWord(defender, finalDamage, WordType.CritDamage);
                    }
                    else
                    {
                        ShowWord(defender, finalDamage, WordType.CritDamage, true);
                    }
                    return;
                }
                if(damageResource == DamageResource.CollateralDamage)
                {
                    ShowWord(defender, finalDamage, WordType.Normal);
                }
                else
                {
                    ShowWord(defender, finalDamage, WordType.Normal, true);
                }
            }
        }

        /// <summary>
        /// 对敌效果计算
        /// </summary>
        /// <param name="effectValue">效果数值</param>
        /// <param name="elementType">效果元素类型</param>
        /// <param name="attacker">进攻方</param>
        /// <param name="defender">防守方</param>
        public static float EffectToEnemyCaculation(float effectValue, ElementType elementType,
                                                    FightHero attacker, FightHero defender)
        {
            switch (elementType)
            {
                case ElementType.Water:
                    effectValue = effectValue * (attacker.CurWaterStrength / defender.CurWaterStrength)
                                  / defender.CurWaterResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Fire:
                    effectValue = effectValue * (attacker.CurFireStrength / defender.CurFireStrength)
                                  / defender.CurFireResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Thunder:
                    effectValue = effectValue * (attacker.CurThunderStrength / defender.CurThunderStrength)
                                  / defender.CurThunderResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Wind:
                    effectValue = effectValue * (attacker.CurWindStrength / defender.CurWindStrength)
                                  / defender.CurWindResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Earth:
                    effectValue = effectValue * (attacker.CurEarthStrength / defender.CurEarthStrength)
                                  / defender.CurEarthResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Dark:
                    effectValue = effectValue * (attacker.CurDarkStrength / defender.CurDarkStrength)
                                  / defender.CurDarkResistCoefficient / defender.CurElemResistCoefficient;
                    break;
                case ElementType.Chaos:
                    break;
            }
            return effectValue;
        }

        /// <summary>
        /// 对己效果计算
        /// </summary>
        /// <param name="effectValue">效果数值</param>
        /// <param name="elementType">效果元素类型</param>
        /// <param name="attacker">自己</param>
        public static float EffectToSelfCaculation(float effectValue, ElementType elementType, FightHero attacker)
        {
            switch (elementType)
            {
                case ElementType.Water:
                    effectValue = effectValue * (attacker.CurWaterStrength / 80);
                    break;
                case ElementType.Fire:
                    effectValue = effectValue * (attacker.CurFireStrength / 80);
                    break;
                case ElementType.Thunder:
                    effectValue = effectValue * (attacker.CurThunderStrength / 80);
                    break;
                case ElementType.Wind:
                    effectValue = effectValue * (attacker.CurWindStrength / 80);
                    break;
                case ElementType.Earth:
                    effectValue = effectValue * (attacker.CurEarthStrength / 80);
                    break;
                case ElementType.Dark:
                    effectValue = effectValue * (attacker.CurDarkStrength / 80);
                    break;
            }
            return effectValue;
        }

    }

    #endregion

    #endregion

    void Awake()
    {

        #region 战斗开始前
        Time.timeScale = 1;
        
        
        isFirstOnTrigger = true;

        #region InitBaseGameObject

        canvas = GameObject.Find("Canvas").gameObject;
        canvas.transform.Find("PanelFight").GetComponent<BoxCollider2D>().enabled = false;

        fightInfoPanel = gameObject.transform.Find("PanelFightInfo").gameObject;
        curTime = 0;
        timeCount = 0;

        playerInfoPanel = gameObject.transform.Find("PanelPlayerInfo").gameObject;
        playerCardInfoPanel = gameObject.transform.Find("PanelCardInfo").gameObject;

        enemyInfoPanel = gameObject.transform.Find("PanelEnemyInfo").gameObject;

        cancelSelectCardButton = GameObject.Find("Canvas/white_image");
        //设置面板按钮注册及获得引用
        SettingPanel = canvas.transform.Find("settingPanel").gameObject;
        SettingPanel.SetActive(false);
        SettingButton = GameObject.Find("Setting_Button").GetComponent<Button>();
        SettingButton.onClick.AddListener(OnSettingPanelButtonClick);
        ContinueButton = canvas.transform.Find("settingPanel").Find("Continue_Button").GetComponent<Button>();
        ContinueButton.onClick.AddListener(OnContinueButtonClick);

        debugSettingButton = SettingPanel.transform.Find("ButtonDebug").gameObject.GetComponent<Button>();
        debugSettingButton.onClick.AddListener(DebugSettingButtonOnClick);
        debugSettingPanel = SettingPanel.transform.Find("PanelDebug").gameObject;

        cancelSelectCardButton.GetComponent<Button>().onClick.AddListener(OnPutBackButtonClock);
        PreviewButton = GameObject.Find("Preview_Button");
        PreviewButton.GetComponent<Button>().onClick.AddListener(OnPreViewButtonClick);

        playerSkillPanel = gameObject.transform.Find("PanelPlayerSkill").gameObject;
        playerActionSkill = playerSkillPanel.transform.Find("ActiveSkill").gameObject;
        playerFirstPassiveSkill = playerSkillPanel.transform.Find("PassiveSkillFirst").gameObject;
        playerSecondPassiveSkill = playerSkillPanel.transform.Find("PassiveSkillSecond").gameObject;
        playerThirdPassiveSkill = playerSkillPanel.transform.Find("PassiveSkillThird").gameObject;
        playerAssistSkill = playerSkillPanel.transform.Find("AssistSkill").gameObject;
        playerAssistSkill.SetActive(false);

        canvas.transform.Find("PanelWin").transform.Find("Button").GetComponent<Button>().onClick.AddListener(OnWinButtonClick);
        canvas.transform.Find("PanelLost").transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(OnWinButtonClick);
        ActionSkillEffect = Instantiate(Resources.Load<GameObject>("Prefab/ActionSkill"));
        ActionSkillEffect.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelPlayerSkill/ActiveSkill").transform);
        ActionSkillEffect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        ActionSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        AssistSkillEffect = Instantiate(Resources.Load<GameObject>("Prefab/AssistSkill"));
        AssistPassiveSkillEffect = Instantiate(Resources.Load<GameObject>("Prefab/AssistPassiveSkill"));
        AssistSkillEffect.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelPlayerSkill/AssistSkill").transform);
        AssistSkillEffect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        AssistSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        AssistPassiveSkillEffect.transform.SetParent(GameObject.Find("Canvas/PanelFight/PanelPlayerSkill/AssistSkill").transform);
        AssistPassiveSkillEffect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        AssistPassiveSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        #endregion

        #region InitPublicParmas

        isPlayingAnimation = false;
        isPlayingEffect = false;
        actionOrigin = Resources.Load<Sprite>("Image/Icon/actionOrigin");
        actionFinish = Resources.Load<Sprite>("Image/Icon/actionFinish");

        buttonDrawCard = GameObject.Find("DrawCard").GetComponent<Button>();
        buttonDrawCard.onClick.AddListener(PlayerDrawCardOnClick);
        chooseSprite = Resources.Load<Sprite>("Image/Icon/choose");
        unchooseSprite = Resources.Load<Sprite>("Image/Icon/unchoose");

        YingYangImage = new Sprite[2];
        YingYangImage[0] = Resources.Load<Sprite>("Image/Icon/white");
        YingYangImage[1] = Resources.Load<Sprite>("Image/Icon/black");


        PreviewButtonOnOrOff[0] = Resources.Load<Sprite>("Image/Icon/previewWait");
        PreviewButtonOnOrOff[1] = Resources.Load<Sprite>("Image/Icon/preview");
        //新手指引相关变量初始化
        GuideText = GameObject.Find("Canvas/PanelFight").transform.Find("GuidePanel/GuideText").GetComponent<Text>();
        GuideText.text = "";
        GuidePanel = GameObject.Find("Canvas/PanelFight").transform.Find("GuidePanel").gameObject;
        GuidePanel.GetComponent<Button>().onClick.AddListener(OnGuidePanelClick);
        GuidePanel.SetActive(false);
        Finger = GameObject.Find("Canvas").transform.Find("Finger_Image").gameObject;


        curStage = facade.GetChallengeStage();
        curEnemyFlag = 0;
        enemyList = new List<FightHero>();
        if (curStage.Enemy_ID_1 != 0)
        {
            FightHero fightHero = new FightHero(curStage.Enemy_1_HeroCard);
            enemyList.Add(fightHero);

        }
        if (curStage.Enemy_ID_2 != 0)
        {
            FightHero fightHero = new FightHero(curStage.Enemy_2_HeroCard);
            enemyList.Add(fightHero);
        }
        if (curStage.Enemy_ID_3 != 0)
        {
            FightHero fightHero = new FightHero(curStage.Enemy_3_HeroCard);
            enemyList.Add(fightHero);
        }
        GameObject.Find("Canvas/PanelFight/ImageBackground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(curStage.BG_1_path + curEnemyFlag);

        imageReward1 = canvas.transform.Find("PanelWin").Find("ImageReward1").gameObject;
        imageReward2 = canvas.transform.Find("PanelWin").Find("ImageReward2").gameObject;
        imageReward3 = canvas.transform.Find("PanelWin").Find("ImageReward3").gameObject;
        imageReward4 = canvas.transform.Find("PanelWin").Find("ImageReward4").gameObject;

        ExpText = canvas.transform.Find("PanelWin").Find("TextExp").GetComponent<Text>();

        winButton = SettingPanel.transform.Find("ButtonWin").gameObject.GetComponent<Button>();
        winButton.onClick.AddListener(WinButtonOnClick);

        showWordPanel = transform.Find("PanelShowWordInfo").gameObject;
        playerWordList = new List<Word>();
        enemyWordList = new List<Word>();
        StartShowWord();
        Tips_Button = SettingPanel.transform.Find("Tips_Button").transform.GetComponent<Button>();
        Tips_Button.onClick.AddListener(OnTipsButtonClick);

        Tips_Image = GameObject.Find("Canvas").transform.Find("Tips_Image").gameObject;
        BackToSettingButton = Tips_Image.transform.Find("Back_Button").GetComponent<Button>();
        BackToSettingButton.onClick.AddListener(OnTipsBackTosettingClick);

        #endregion

        #region InitPlayerInfo

        playerHero = new FightHero(facade.GetPlayerUseHero(), facade.GetPlayerUseElement());
        if (facade.GetPlayerUseStaffHero() != null)
        {
            playerAssitHero = new FightHero(facade.GetPlayerUseStaffHero());
        }

        playerActionSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>(playerHero.heroCard.SkillIcon_path);

        playerHero.heroCard.Action_skill.Skill_enable = true;
        playerHero.heroCard.Prossive_skill_1.Skill_enable = true;
        playerHero.heroCard.Prossive_skill_2.Skill_enable = true;
        playerHero.heroCard.Prossive_skill_3.Skill_enable = true;
        playerHeroOriginalPosition = new Vector3(-1390, 18.5f, 0);
        playerHeroPosition = new Vector3(-691, 0, 0);
        playerAssitHeroPosition = new Vector3(-830, 0, 0);

        curPlayerHandCardCount = 0;
        playerSelectCardCount = 0;

        playerHpSlider = playerInfoPanel.gameObject.transform.Find("SliderPlayerHp").GetComponent<Slider>();
        playerActionSlider = playerInfoPanel.gameObject.transform.Find("SliderPlayer").GetComponent<Slider>();
        playerAPContent = playerInfoPanel.gameObject.transform.Find("ScrollViewPlayer").transform.GetChild(0).GetChild(0).gameObject;
        playerBuffContent = playerInfoPanel.gameObject.transform.Find("ScrollViewPlayerBuff").transform.GetChild(0).GetChild(0).gameObject;
        playerScrollViewCard = GameObject.Find("Canvas/PanelFight/PanelCardInfo/ScrollViewCard");
        playerCardContent = playerCardInfoPanel.gameObject.transform.Find("ScrollViewCard").transform.GetChild(0).GetChild(0).gameObject;




        playerCardGroupContent = new List<GameObject>();
        playerCardOriginalSize = new Vector2(210, 234);
        playerCardScaleRatio = 1.3f;
        playerGameObject = GameObject.Instantiate(Resources.Load<GameObject>(playerHero.heroCard.FightModel_path));
        playerGameObject.transform.SetParent(fightInfoPanel.transform);
        playerGameObject.gameObject.GetComponent<RectTransform>().localPosition = playerHeroPosition;
        playerGameObject.tag = "Player";
        playerGameObject.name = "player";
        if (playerGameObject.GetComponent<AnimationCtrl>() == null)
            playerGameObject.AddComponent<AnimationCtrl>();

        playerAnimator = playerGameObject.GetComponent<Animator>();

        if (playerAssitHero != null)
        {
            playerAssistSkill.SetActive(true);
            playerAssistSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>(playerAssitHero.heroCard.SkillIcon_path);
            playerAssistSkill.GetComponent<AssistSkillCtrl>().skill = playerAssitHero.heroCard.Staff_skill_self;
            playerAssitHero.heroCard.Staff_skill_self.Skill_enable = true;
            playerAssistSkill.GetComponent<Button>().onClick.AddListener(ButtonAssistSkillOnClick);
            playerAssitGameObject = GameObject.Instantiate(Resources.Load<GameObject>(playerAssitHero.heroCard.FightModel_path + "_assist"));
            playerAssitGameObject.transform.SetParent(fightInfoPanel.transform);
            playerAssitGameObject.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            playerAssitGameObject.gameObject.GetComponent<RectTransform>().localPosition = playerAssitHeroPosition;
            playerAssitGameObject.name = "PlayerAssist";
            if (playerGameObject.GetComponent<AnimationCtrl>() == null)
                playerGameObject.AddComponent<AnimationCtrl>();
            playerAssistAnimator = playerAssitGameObject.GetComponent<Animator>();
        }

        playerShieldSpriteRender = playerGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        playerSaveApCount = playerHero.CurApPoint;
        playerSaveApSliderCount = playerHero.CurSliderValue;

        playerSaveBuffCount = playerHero.BuffDictionary.Count;

        PreviewPanel = canvas.transform.Find("CardPreview_Panel").gameObject;
        PlayerHp_Text = GameObject.Find("PlayerHP_Text").GetComponent<Text>();

        cardCombineName = PreviewPanel.transform.Find("CombineName_Text").GetComponent<Text>();
        cardCombineDescription = PreviewPanel.transform.Find("CombineDes_Text").GetComponent<Text>();

        gradeImage = PreviewPanel.transform.Find("ImageGrade").GetComponent<Image>();

        SliderMusic = SettingPanel.transform.Find("SliderMusic").GetComponent<Slider>();
        SliderMusic.onValueChanged.AddListener(delegate (float volumn) { SliderMusicOnChange(); });
        SliderMusicImage = SliderMusic.gameObject.transform.Find("Handle Slide Area/Handle/Image").GetComponent<Image>();
        SliderMusic.value = facade.GetBGMVolumn();
        SliderSoundEffect = SettingPanel.transform.Find("SliderSoundEffect").GetComponent<Slider>();
        SliderSoundEffect.onValueChanged.AddListener(delegate (float volumn) { SliderSoundEffectOnChange(); });
        SliderSoundEffectImage = SliderSoundEffect.gameObject.transform.Find("Handle Slide Area/Handle/Image").GetComponent<Image>();
        SliderSoundEffect.value = facade.GetUIAudioVolumn();
        TextMusic = SettingPanel.transform.Find("TextMusic").gameObject;
        TextSoundEffect = SettingPanel.transform.Find("TextSoundEffect").gameObject;

        playerBlackHole = transform.Find("PanelCardEffect/PlayerCard").gameObject;
        enemyBlackHole = transform.Find("PanelCardEffect/EnemyCard").gameObject;

        #endregion

        #region InitEnemyInfo

        enemyHero = enemyList[curEnemyFlag];

        enemyHeroPosition = new Vector3(691, 0, 0);

        curEnemyCardCount = 0;
        enemyDamageRatio = 0.08f;

        enemyActionSlider = enemyInfoPanel.gameObject.transform.Find("SliderEnemy").GetComponent<Slider>();
        enemyHpSlider = enemyInfoPanel.gameObject.transform.Find("SliderEnemyHp").GetComponent<Slider>();
        enemyAPContent = enemyInfoPanel.gameObject.transform.Find("ScrollViewEnemy").transform.GetChild(0).GetChild(0).gameObject;
        enemyBuffContent = enemyInfoPanel.gameObject.transform.Find("ScrollViewEnemyBuff").transform.GetChild(0).GetChild(0).gameObject;

        enemyGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HeroPrefabs/enemy/" + enemyHero.heroCard.Hero_id));
        enemyGameObject.transform.SetParent(fightInfoPanel.transform);
        enemyGameObject.gameObject.GetComponent<RectTransform>().localPosition = enemyHeroPosition;
        enemyGameObject.tag = "Enemy";
        enemyGameObject.name = "enemy";
        if (enemyGameObject.GetComponent<AnimationCtrl>() == null)
            enemyGameObject.AddComponent<AnimationCtrl>();

        enemyAnimator = enemyGameObject.GetComponent<Animator>();

        enemyShieldSpriteRender = enemyGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        enemySaveApCount = enemyHero.CurApPoint;
        enemySaveApSliderCount = enemyHero.CurSliderValue;

        enemyCard = new List<Card>();

        enemyDamageCoefficient = 1.5f;

        #endregion

        #region additional

        PreviewPanel.SetActive(false);
        EnemyHP_Text = GameObject.Find("EnemyHP_Text").GetComponent<Text>();

        playerPrepareBuffClass = new List<BuffClass>();

        cardGroupDict = new Dictionary<string, SpecialBuff>();
        buffDict = new Dictionary<string, Buff>();
        SpecialBuffHolder specialBuffHolder = Resources.Load<SpecialBuffHolder>("SpecialBuff");

        foreach (SpecialBuff specialBuff in specialBuffHolder.SpecialBuffs)
        {
            cardGroupDict.Add(specialBuff.ID, specialBuff);
        }

        foreach (Buff buff in specialBuffHolder.buffs)
        {
            buffDict.Add(buff.BuffEngName, buff);
            if (buff.BuffEngName == null || buff.BuffEngName == "")
            {
                break;
            }
        }

        playerActionSkill.GetComponent<SkillCtrl>().skill = playerHero.heroCard.Action_skill;
        playerActionSkill.GetComponent<Button>().onClick.AddListener(ButtonSkillOnClick);
        playerFirstPassiveSkill.GetComponent<SkillCtrl>().skill = playerHero.heroCard.Prossive_skill_1;
        playerSecondPassiveSkill.GetComponent<SkillCtrl>().skill = playerHero.heroCard.Prossive_skill_2;
        playerThirdPassiveSkill.GetComponent<SkillCtrl>().skill = playerHero.heroCard.Prossive_skill_3;

        #endregion

        #region PlayerAssist

        playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeEnter, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.BeforeEnter, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.InEnter, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.InEnter, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.AfterEnter, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.AfterEnter, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeStart, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.BeforeStart, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.InStart, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.InStart, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.AfterStart, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.AfterStart, playerHero, enemyHero);

        playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeIntend, playerHero, enemyHero);
        playerHero.heroCard.Staff_skill_function(FightPhase.BeforeIntend, playerHero, enemyHero);

        IsFirstAssistSkillEffect = true;

        AssistSkillEffect.SetActive(false);
        AssistPassiveSkillEffect.SetActive(false);

        #endregion

        facade.PlayBGM("fight");
        if (PlayerPrefs.GetInt("PreviewTopControlBool_Save") == 0)
        {
            PreviewTopControlBool = true;
            PreviewButton.GetComponent<Image>().sprite = PreviewButtonOnOrOff[0];
        }
        else
        {
            PreviewTopControlBool = false;
            PreviewButton.GetComponent<Image>().sprite = PreviewButtonOnOrOff[1];
        }
        #endregion

    }

    private void Start()
    {

        #region 战斗开始时

        #endregion

        #region 战斗开始后

        playerActionSlider.value = 0;
        enemyActionSlider.value = 0;

        //播放开场动画
        ShowStartGameAnim();
        #endregion

    }

    private void Update()
    {
        playerHero.ResetAllDeviant();
        enemyHero.ResetAllDeviant();
        BuffManager.SetActionAtBeginning(playerHero, enemyHero);
        BuffManager.SetActionAtBeginning(enemyHero, playerHero);
        playerHero.SetAllDeviantAction();
        enemyHero.SetAllDeviantAction();

        playerHpSlider.value = playerHero.CurHp / playerHero.MaxHp;
        enemyHpSlider.value = enemyHero.CurHp / enemyHero.MaxHp;
        PlayerHp_Text.text = ((int)playerHero.CurHp).ToString();
        EnemyHP_Text.text = ((int)enemyHero.CurHp).ToString();

        if (playerHero.CurSpeed > playerHero.OriginalSpeed)
        {
            playerActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(32f / 255, 227f / 255, 175f / 255);
        }
        else if (playerHero.CurSpeed == playerHero.OriginalSpeed)
        {
            playerActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(42f / 255, 184f / 255, 18 / 255);
        }
        else
        {
            playerActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(4f / 255, 158f / 255, 248f / 255);
        }

        if (enemyHero.CurSpeed > enemyHero.OriginalSpeed)
        {
            enemyActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(32f / 255, 227f / 255, 175f / 255);
        }
        else if (enemyHero.CurSpeed == enemyHero.OriginalSpeed)
        {
            enemyActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(42f / 255, 184f / 255, 18 / 255);
        }
        else
        {
            enemyActionSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(4f / 255, 158f / 255, 248f / 255);
        }

        if (playerHero.CurShield > 0)
        {
            playerShieldSpriteRender.color = new Color(playerShieldSpriteRender.color.r, playerShieldSpriteRender.color.g, playerShieldSpriteRender.color.b, playerHero.CurShield / playerHero.MaxShield + 0.35f);
        }
        else
        {
            playerShieldSpriteRender.color = new Color(playerShieldSpriteRender.color.r, playerShieldSpriteRender.color.g, playerShieldSpriteRender.color.b, 0);
        }
        if (enemyHero.CurShield > 0)
        {
            enemyShieldSpriteRender.color = new Color(enemyShieldSpriteRender.color.r, enemyShieldSpriteRender.color.g, enemyShieldSpriteRender.color.b, enemyHero.CurShield / enemyHero.MaxShield + 0.35f);
        }
        else
        {
            enemyShieldSpriteRender.color = new Color(enemyShieldSpriteRender.color.r, enemyShieldSpriteRender.color.g, enemyShieldSpriteRender.color.b, 0);
        }

        //集气阶段
        if (IsFightPrepare)
        {
            playerHero.heroCard.Prossive_skill_function(FightPhase.InIntend, playerHero, enemyHero);
            playerHero.heroCard.Staff_skill_function(FightPhase.InDemage, playerHero, enemyHero);
            // IsShowPreviewPanel = true;
            //若在播放动画过程中选择了牌，则动画结束后预览框自动激活
            if (playerSelectCardCount > 0)
            {
                UpdateShowPreviewPanel(true);
            }
            //播放战斗动画时，跳转到伤害结算阶段
            if (isPlayingAnimation == true || isPlayingEffect == true)
            {
                //防止播放动画过程中点牌造成预览框激活
                //IsShowPreviewPanel = false;
                //播放动画时不显示预览框
                //UpdateShowPreviewPanel(false);
                IsFightPrepare = false;
                IsDamageDamageSettlement = true;
            }
        }
        //伤害结算阶段
        if (IsDamageDamageSettlement)
        {
            playerHero.heroCard.Prossive_skill_function(FightPhase.InDemage, playerHero, enemyHero);
            playerHero.heroCard.Staff_skill_function(FightPhase.InDemage, playerHero, enemyHero);
            //不播放战斗动画时，跳转到集气阶段
            if (isPlayingAnimation == false && isPlayingEffect == false)
            {
                IsFightPrepare = true;
                IsDamageDamageSettlement = false;
            }
        }
        //战斗结束阶段
        if (IsFightFinish)
        {
            if (IsFirstFightFinish)
            {
                playerHero.heroCard.Prossive_skill_function(FightPhase.InEnd, playerHero, enemyHero);
                playerHero.heroCard.Staff_skill_function(FightPhase.InEnd, playerHero, enemyHero);
                playerHero.heroCard.Prossive_skill_function(FightPhase.AfterEnd, playerHero, enemyHero);
                playerHero.heroCard.Staff_skill_function(FightPhase.AfterEnd, playerHero, enemyHero);
                //敌人胜利
                if (playerHero.CurHp <= 0)
                {
                    IsFirstFightFinish = false;
                    EnemyWin();
                }
                //玩家胜利
                if (enemyHero.CurHp <= 0)
                {
                    IsFirstFightFinish = false;
                    PlayerWin();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //集气阶段
        if (IsFightPrepare)
        {
            //一方血量小于0时，跳转到战斗结束状态
            if (playerHero.CurHp <= 0 || enemyHero.CurHp <= 0)
            {
                playerHero.heroCard.Prossive_skill_function(FightPhase.BeforeEnd, playerHero, enemyHero);
                playerHero.heroCard.Staff_skill_function(FightPhase.BeforeEnd, playerHero, enemyHero);
                buttonDrawCard.GetComponent<Button>().interactable = false;
                IsFightPrepare = false;
                IsDamageDamageSettlement = false;
                IsFightFinish = true;
            }

            //计时器更新
            timeCount += Time.fixedDeltaTime;
            //当计时器大于1时，进行时间更新
            if (timeCount >= 1)
            {
                timeCount = 0;
                curTime++;
            }
            if ((int)curTime % 20 == 0)
            {
                //BuffManager.AddAndUpdateBuff(enemyHero, playerHero, BuffClass.CanDrawElemCard);
            }
            if ((int)curTime % 25 == 0)
            {
                //BuffManager.AddAndUpdateBuff(enemyHero, playerHero, BuffClass.CanUseActiveSkill);
            }
            if ((int)curTime % 30 == 0)
            {
                //BuffManager.AddAndUpdateBuff(enemyHero, playerHero, BuffClass.CanUsePassiveSkill);
            }
            //玩家行动条积攒(2.5f)
            if (playerHero.CurSliderValue < 1)
            {
                playerHero.CurSliderValue += 1 / ((500 / playerHero.CurSpeed) * 60);
            }
            else
            {
                //玩家获得行动点
                PlayerGetAP();
            }
            playerActionSlider.value = playerHero.CurSliderValue;
            //敌人行动条积攒(3f)
            if (enemyHero.CurSliderValue < 1)
            {
                enemyHero.CurSliderValue += 1 / ((500 / enemyHero.CurSpeed) * 60);
            }
            else
            {
                EnemyGetAP();
                if (enemyHero.CurApPoint >= curRandEnemyApCount)
                {
                    EnemyDrawCard(curRandEnemyApCount);
                }
            }
            enemyActionSlider.value = enemyHero.CurSliderValue;
            //当记录的行动点小于玩家当前行动点时
            while (playerSaveApCount < playerHero.CurApPoint)
            {
                //将首个未获得的行动点改变为已获得状态
                //Debug.Log(playerSaveApCount);
                GameObject gameObject = playerAPContent.transform.GetChild(playerSaveApCount).gameObject;
                gameObject.GetComponent<Image>().sprite = actionFinish;
                playerSaveApCount++;
            }
            //当记录的行动点大于玩家当前行动点时
            while (playerSaveApCount > playerHero.CurApPoint)
            {
                //将首个已获得的行动点改为未获得状态
                GameObject gameObject = playerAPContent.transform.GetChild(playerSaveApCount - 1).gameObject;
                gameObject.GetComponent<Image>().sprite = actionOrigin;
                playerSaveApCount--;
            }
            //当记录的行动点小于敌人当前行动点时
            while (enemySaveApCount < enemyHero.CurApPoint)
            {
                //将首个未获得的行动点改变为已获得状态
                GameObject gameObject = enemyAPContent.transform.GetChild(enemySaveApCount).gameObject;
                gameObject.GetComponent<Image>().sprite = actionFinish;
                enemySaveApCount++;
            }
            //当记录的行动点大于敌人当前行动点时
            while (enemySaveApCount > enemyHero.CurApPoint)
            {
                //将首个已获得的行动点改为未获得状态
                GameObject gameObject = enemyAPContent.transform.GetChild(enemySaveApCount - 1).gameObject;
                gameObject.GetComponent<Image>().sprite = actionOrigin;
                enemySaveApCount--;
            }
            //更新buff持续时间

            #region UpdateSkill
            if (playerHero.IsActiveSkill == true)
            {
                ActionSkillEffect.SetActive(true);
                ActionSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(-7.4f, 0.74f, 0);
            }
            else
            {
                ActionSkillEffect.SetActive(false);
            }
            if (playerAssitGameObject != null && playerAssitHero != null)
            {
                if (timeCount >= 0.15f && playerAssitHero.heroCard.Staff_skill_self.isActionSkill == false && IsFirstAssistSkillEffect == true)
                {
                    IsFirstAssistSkillEffect = false;

                    GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/assist_Effect")) as GameObject;
                    effect.transform.SetParent(GameObject.Find("Effect").transform);
                    DestroySelf.isEnemy = false;

                    GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
                    spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
                    spell.GetComponent<RectTransform>().localPosition = new Vector3(-286, 0, 0);
                    spell.transform.localScale = new Vector3(100, 100, 1);
                }
                if (playerAssitHero.IsAssistSkill == true)
                {
                    if (playerAssitHero.heroCard.Staff_skill_self.isActionSkill == true)
                    {
                        AssistSkillEffect.SetActive(true);
                        AssistSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(-4.3f, 1.8f, 0);
                    }
                    else
                    {
                        AssistPassiveSkillEffect.SetActive(true);
                        AssistPassiveSkillEffect.GetComponent<RectTransform>().localPosition = new Vector3(-4.3f, 1.8f, 0);
                    }
                }
                else
                {
                    if (playerAssitHero.heroCard.Staff_skill_self.isActionSkill == true)
                    {
                        AssistSkillEffect.SetActive(false);
                    }
                    else
                    {
                        AssistPassiveSkillEffect.SetActive(false);
                    }
                }
            }
            if (playerHero.heroCard.Action_skill.Skill_enable == false)
            {
                playerActionSkill.GetComponent<Button>().interactable = false;
                if (playerHero.CurActiveSkillCoolingTime >= playerHero.MaxActiveSkillCoolingTime && playerHero.IsActiveSkill == true)
                {
                    playerHero.CurActiveSkillCoolingTime = 0;
                    playerHero.IsActiveSkill = false;
                }
                playerHero.CurActiveSkillCoolingTime += Time.fixedDeltaTime;
                if (playerHero.CurActiveSkillCoolingTime >= playerHero.MaxActiveSkillCoolingTime)
                {
                    playerHero.CurActiveSkillCoolingTime = playerHero.MaxActiveSkillCoolingTime;
                    playerActionSkill.GetComponent<Button>().interactable = true;
                    playerHero.heroCard.Action_skill.Skill_enable = true;
                    playerHero.IsActiveSkill = true;
                }
            }
            playerActionSkill.GetComponent<SkillCtrl>().UpdateCoolingTime(playerHero.CurActiveSkillCoolingTime, playerHero.MaxActiveSkillCoolingTime);

            if (playerAssitGameObject != null && playerAssitHero != null)
            {
                if (playerAssitHero.heroCard.Staff_skill_self.Skill_enable == false)
                {
                    playerAssistSkill.GetComponent<Button>().interactable = false;
                    if (playerAssitHero.CurAssistSkillCoolingTime >= playerAssitHero.MaxAssistSkillCoolingTime && playerAssitHero.IsAssistSkill == true)
                    {
                        playerAssitHero.CurAssistSkillCoolingTime = 0;
                        playerAssitHero.IsAssistSkill = false;
                    }
                    playerAssitHero.CurAssistSkillCoolingTime += Time.fixedDeltaTime;
                    if (playerAssitHero.CurAssistSkillCoolingTime >= playerAssitHero.MaxAssistSkillCoolingTime)
                    {
                        playerAssitHero.CurAssistSkillCoolingTime = playerAssitHero.MaxAssistSkillCoolingTime;
                        playerAssistSkill.GetComponent<Button>().interactable = true;
                        playerAssitHero.heroCard.Staff_skill_self.Skill_enable = true;
                        playerAssitHero.IsAssistSkill = true;
                    }
                }
                playerAssistSkill.GetComponent<AssistSkillCtrl>().UpdateCoolingTime(playerAssitHero.CurAssistSkillCoolingTime, playerAssitHero.MaxAssistSkillCoolingTime);
            }


            if (playerHero.heroCard.Prossive_skill_1.Skill_enable == false)
            {
                playerFirstPassiveSkill.GetComponent<Button>().interactable = false;
                if (playerHero.CurFirstPassiveSkillCoolingTime >= playerHero.MaxFirstPassiveSkillCoolingTime && playerHero.IsFirstPassiveSkill == true)
                {
                    playerHero.IsFirstPassiveSkill = false;
                    playerHero.CurFirstPassiveSkillCoolingTime = 0;
                }
                playerHero.CurFirstPassiveSkillCoolingTime += Time.fixedDeltaTime;
                if (playerHero.CurFirstPassiveSkillCoolingTime >= playerHero.MaxFirstPassiveSkillCoolingTime)
                {
                    playerHero.CurFirstPassiveSkillCoolingTime = playerHero.MaxFirstPassiveSkillCoolingTime;
                    playerHero.heroCard.Prossive_skill_1.Skill_enable = true;
                    playerHero.IsFirstPassiveSkill = true;
                    playerFirstPassiveSkill.GetComponent<Button>().interactable = true;
                }
            }
            playerFirstPassiveSkill.GetComponent<SkillCtrl>().UpdateCoolingTime(playerHero.CurFirstPassiveSkillCoolingTime, playerHero.MaxFirstPassiveSkillCoolingTime);

            if (playerHero.heroCard.Prossive_skill_2.Skill_enable == false)
            {
                playerSecondPassiveSkill.GetComponent<Button>().interactable = false;
                if (playerHero.CurSecondPassiveSkillCoolingTime >= playerHero.MaxSecondPassiveSkillCoolingTime && playerHero.IsSecondPassiveSkill == true)
                {
                    playerHero.CurSecondPassiveSkillCoolingTime = 0;
                    playerHero.IsSecondPassiveSkill = false;
                }
                playerHero.CurSecondPassiveSkillCoolingTime += Time.fixedDeltaTime;
                if (playerHero.CurSecondPassiveSkillCoolingTime >= playerHero.MaxSecondPassiveSkillCoolingTime)
                {
                    playerHero.CurSecondPassiveSkillCoolingTime = playerHero.MaxSecondPassiveSkillCoolingTime;
                    playerHero.heroCard.Prossive_skill_2.Skill_enable = true;
                    playerHero.IsSecondPassiveSkill = true;
                    playerSecondPassiveSkill.GetComponent<Button>().interactable = true;
                }
            }
            playerSecondPassiveSkill.GetComponent<SkillCtrl>().UpdateCoolingTime(playerHero.CurSecondPassiveSkillCoolingTime, playerHero.MaxSecondPassiveSkillCoolingTime);

            if (playerHero.heroCard.Prossive_skill_3.Skill_enable == false)
            {
                playerThirdPassiveSkill.GetComponent<Button>().interactable = false;
                if (playerHero.CurThirdPassiveSkillCoolingTime >= playerHero.MaxThirdPassiveSkillCoolingTime && playerHero.IsThirdPassiveSkill == true)
                {
                    playerHero.CurThirdPassiveSkillCoolingTime = 0;
                    playerHero.IsThirdPassiveSkill = false;
                }
                playerHero.CurThirdPassiveSkillCoolingTime += Time.fixedDeltaTime;
                if (playerHero.CurThirdPassiveSkillCoolingTime >= playerHero.MaxThirdPassiveSkillCoolingTime)
                {
                    playerHero.CurThirdPassiveSkillCoolingTime = playerHero.MaxThirdPassiveSkillCoolingTime;
                    playerHero.heroCard.Prossive_skill_3.Skill_enable = true;
                    playerHero.IsThirdPassiveSkill = true;
                    playerThirdPassiveSkill.GetComponent<Button>().interactable = true;
                }
            }
            playerThirdPassiveSkill.GetComponent<SkillCtrl>().UpdateCoolingTime(playerHero.CurThirdPassiveSkillCoolingTime, playerHero.MaxThirdPassiveSkillCoolingTime);

            #endregion

            BuffManager.UpdateBuffDurationTime(playerHero, enemyHero);
            BuffManager.UpdateBuffDurationTime(enemyHero, playerHero);
        }
    }

}
