using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GuideCtrl;

public class MainSceneCtrl : BasePanel
{
    #region PanelRoot

    #region PanelUI

    private GameObject PanelUI;

    //中心关卡图片
    private GameObject ButtonCenterLevel;
    //轮换广告图
    private Sprite[] ShowSprites;
    //是否进行Update判断更新选择信息
    private bool isUpdateLevelIndex;
    //选择目标位置
    private Vector3 levelIndexDestination;

    #region PanelLeftButton

    private GameObject PanelLeftButton;

    //信箱按钮
    private Button ButtonLetterBox;
    //任务按钮
    private Button ButtonTask;
    //公告按钮
    private Button ButtonPub;
    //喇叭按钮
    private Button ButtonHorn;

    #endregion

    #region PanelLowButton

    private GameObject PanelLowButton;

    //滑动主线/竞技场按钮
    private GameObject Content;
    //存储按钮的列表
    private List<GameObject> ButtonList = new List<GameObject>();
    //Content先前的中心点
    private RectTransform ContentCenterPosPre;
    //Content当前的中心点
    private RectTransform ContentCenterPosNow;
    //开始位置
    private float StartPosX;
    //当前位置
    private float NowPosX;
    //位置偏移
    float offsetX;
    //当前处于选中状态的按钮的索引
    int LevelIndex;
    //先前索引
    int LevelPreIndex;

    #endregion

    #region PanelSecondary

    private GameObject PanelSecondary;

    //绘卷面板
    private GameObject PanelScroller;

    #region PanelCard

    //卡牌面板
    private GameObject PanelCard;
    //卡牌按钮
    private Button CardButton;
    //素材按钮
    private Button GoodsButton;
    //天赋按钮
    private Button TalentButton;
    //卡牌显示
    private GameObject CardDisplay;
    //素材显示
    private GameObject GoodsDisplay;
    //天赋显示
    private GameObject TalentDisplay;

    #endregion

    #region PanelGetCard

    //抽卡面板
    private GameObject PanelGetCard;

    //抽卡显示图片
    private Image ImageIntro;
    //赋魂几率按钮
    private Button ButtonProbability;
    //单抽按钮
    private Button ButtonOnce;
    //十连按钮
    private Button ButtonTenTimes;
    //滚动列表面板
    private GameObject PanelSelect;
    //滚动列表
    private GameObject ScrollViewGetCard;
    //日期提示
    private Text TextDate;

    #endregion

    #endregion

    #region PanelPlayerIcon

    private GameObject PanelPlayerIcon;

    //人物头像
    private GameObject ImagePlayerIcon;
    //人物头像框
    private GameObject ImagePlayerIconFrame;
    //等级文字
    private Text TextLevel;

    #endregion

    #region PanelGoods

    private GameObject PanelGoods;

    //金币文字
    private Text TextGold;
    //钻石文字
    private Text TextDiamond;
    //稀有货币文字
    private Text TextRare;

    #endregion

    #region PanelRightButton

    private GameObject PanelRightButton;

    //人物按钮
    private Button ButtonHeroCard;
    //绘卷按钮
    private Button ButtonScroller;
    //招募按钮
    private Button ButtonConscribe;
    //市集按钮
    private Button ButtonMarket;

    #endregion


    #endregion

    #region ButtonJourney

    private Button ButtonJourney;

    #endregion

    #region PanelUIDetail

    private GameObject PanelUIDetail;

    //遮罩
    private GameObject ImageMask;
    //信箱面板
    private GameObject PanelLetterBox;
    //任务面板
    private GameObject PanelTaskBox;
    //次级遮罩
    private GameObject ImageSeniorMask;
    //信箱详细面板
    private GameObject PanelLetterDetail;
    //任务详细面板
    private GameObject PanelTaskReward;
    //公告面板
    private GameObject PanelNotice;

    //已读
    private Image ReadToggle;
    //未读
    private Image UnReadToggle;
    //已读面板
    private GameObject ReadPanel;
    //未读面板
    private GameObject UnReadPanel;

    #endregion

    #region PanelPlayerInfo

    private GameObject PanelPlayerInfo;

    //玩家头像
    private Image PlayerIcon;
    //详情信息面板等级
    private Text PlayerInfoLevelText;
    //详情信息经验条
    private Slider PlayerExpSlider;
    //玩家名字
    private InputField PlayerName;
    //玩家先前名字
    private string PrePlayerName;
    //音量设置按钮
    private Button SetAudioVolumnButton;
    //返回大厅按钮
    private Button CancelButton;
    //退出游戏按钮
    private Button ExitButton;
    //升级按钮
    private Button LevelUpButton;

    //音量设置面板
    private GameObject PanelVolumnSetting;
    //确认按钮
    private Button ButtonCertain;
    //取消按钮
    private Button ButtonCancel;
    //音乐设置滑动条
    private Slider SliderMusic;
    //音乐滑动头图片
    private Image SliderMusicImage;
    //先前音乐音量
    private float PreSliderMusicVolumn;
    //音效设置滑动条
    private Slider SliderSoundEffect;
    //音效滑动条图片
    private Image SliderSoundEffectImage;
    //先前音效音量
    private float PreSliderSoundEffectVolumn;

    #endregion

    #region PanelCardInfo

    private GameObject PanelCardInfo;

    //注魂面板
    private GameObject UpgradePanel;
    //共鸣面板
    private GameObject UpstarPanel;
    //注魂按钮
    private Button UpgradeBtn;
    //共鸣按钮
    private Button UpstarBtn;

    //角色面板
    private GameObject RolePanel;
    //属性面板
    private GameObject AttributePanel;
    //技能面板
    private GameObject SkillPanel;

    //角色按钮
    private Button RoleButton;
    //属性按钮
    private Button AttributeButton;
    //技能按钮
    private Button SkillButton;

    #endregion

    #region PanelGuide

    private GameObject PanelGuide;

    //手指
    private GameObject GuideFinger;
    //引导文字
    private Text GuideText;

    #endregion

    #region PanelProbability

    //概率详情面板
    private GameObject PanelProbability;
    //取消概率详情按钮
    private Button ButtonExit;

    #endregion

    #endregion

    #region MainScene

    //信箱按钮
    private void OnLetterBoxButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelUIDetail.SetActive(true);
        PanelUIDetail.transform.Find("ImageMask").gameObject.SetActive(true);
        PanelLetterBox.SetActive(true);
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    //任务按钮
    private void OnTaskPanelButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelUIDetail.SetActive(true);
        PanelUIDetail.transform.Find("ImageMask").gameObject.SetActive(true);
        PanelTaskBox.SetActive(true);
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    //公告按钮
    private void OnNoticePanelButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelUIDetail.SetActive(true);
        PanelUIDetail.transform.Find("ImageMask").gameObject.SetActive(true);
        PanelNotice.SetActive(true);
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    //选择已读取
    public void OnReadToggleValueChange(bool isOn)
    {
        facade.PlayUIAudio("open");
        if (isOn)
        {
            ReadToggle.enabled = true;
            UnReadToggle.enabled = false;
            ReadToggle.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
            UnReadToggle.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            ReadPanel.SetActive(true);
            UnReadPanel.SetActive(false);
        }

    }

    //选择未读取
    public void OnUnReadToggleValueChange(bool isOn)
    {
        facade.PlayUIAudio("open");
        if (isOn)
        {
            ReadToggle.enabled = false;
            UnReadToggle.enabled = true;
            UnReadToggle.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
            ReadToggle.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            ReadPanel.SetActive(false);
            UnReadPanel.SetActive(true);
        }

    }

    //关闭信箱界面
    public void OnCloseButtonClick()
    {
        facade.PlayUIAudio("close");
        PanelUIDetail.SetActive(false);
        PanelLetterBox.SetActive(false);
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
    }

    //关闭任务界面
    public void OnCloseTaskPanelButtonClick()
    {
        facade.PlayUIAudio("close");
        PanelTaskBox.SetActive(false);
        PanelUIDetail.SetActive(false);
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
    }

    //关闭公告界面
    public void OnCloseNoticePanelButtonClick()
    {
        facade.PlayUIAudio("close");
        PanelNotice.SetActive(false);
        PanelUIDetail.SetActive(false);
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
    }

    //点击具体邮件
    public void OnMailItemClick()
    {
        facade.PlayUIAudio("open");
        ImageSeniorMask.gameObject.SetActive(true);
        PanelLetterDetail.SetActive(true);
    }

    //点击具体任务
    public void OnTaskItemClick()
    {
        facade.PlayUIAudio("open");
        ImageSeniorMask.gameObject.SetActive(true);
        PanelTaskReward.SetActive(true);
    }

    //确认具体邮件
    public void OnSureButtonClick()
    {
        facade.PlayUIAudio("close");
        ImageSeniorMask.gameObject.SetActive(false);
        PanelLetterDetail.SetActive(false);
        PanelUIDetail.transform.Find("ImageMask").gameObject.SetActive(true);
    }

    //确认奖励
    public void OnSureTaskRewardClick()
    {
        facade.PlayUIAudio("close");
        ImageSeniorMask.gameObject.SetActive(false);
        PanelTaskReward.SetActive(false);
        PanelUIDetail.transform.Find("ImageMask").gameObject.SetActive(true);
    }

    /// <summary>
    /// 往左按钮
    /// </summary>
    private void TurnToLeft()
    {
        levelIndexDestination = new Vector3(245, 0, 0);
        isUpdateLevelIndex = false;
    }

    /// <summary>
    /// 往右按钮
    /// </summary>
    private void TurnToRight()
    {
        levelIndexDestination = new Vector3(355, 0, 0);
        isUpdateLevelIndex = false;
    }

    /// <summary>
    /// 更新选择信息
    /// </summary>
    private void UpdateLevelIndex()
    {
        ContentCenterPosNow = Content.GetComponent<RectTransform>();
        NowPosX = ContentCenterPosNow.anchoredPosition.x;
        //根据Content的偏移值确定选中的按钮（一个按钮宽度110）
        offsetX = StartPosX - NowPosX;
        LevelIndex = (int)(offsetX / 109);
        LevelIndex = LevelIndex >= ButtonList.Count - 1 ? ButtonList.Count - 1 : LevelIndex;
        LevelIndex = LevelIndex < 0 ? 0 : LevelIndex;

        for (int i = 0; i < ButtonList.Count; i++)
        {
            //现将全部按钮设置Image为未选中
            ButtonList[i].SendMessage("ChangeSpriteToSmall");
            ButtonList[i].GetComponent<HightLight>().isSelected = false;
        }
        //再将选中按钮设置Image为选中Image
        ButtonList[LevelIndex].SendMessage("ChangeSpriteToBig");
        ButtonList[LevelIndex].GetComponent<HightLight>().isSelected = true;
        ChangeSprite();
    }

    //点击主线
    public void OnMainChapterClick()
    {
        facade.PlayUIAudio("button");
        if (ButtonList[0].GetComponent<HightLight>().isSelected)
        {
            LoadAsyncScene.nextSceneName = "world_scene";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
        }
    }

    //点击竞技场
    public void OnPVPClick()
    {
        facade.PlayUIAudio("button");
        if (ButtonList[1].GetComponent<HightLight>().isSelected)
        {
            LoadAsyncScene.nextSceneName = "PVP_scene";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
        }
    }

    //点击旅途
    public void OnJourneyButtonClick()
    {
        facade.PlayUIAudio("button");
        if (PanelSecondary.activeSelf == true)
        {
            PanelSecondary.SetActive(false);
            PanelCard.SetActive(false);
            PanelScroller.SetActive(false);
            PanelGetCard.SetActive(false);
            transform.Find("ButtonRight").gameObject.SetActive(true);
            transform.Find("ButtonLeft").gameObject.SetActive(true);
            return;
        }
        switch (LevelIndex)
        {
            case 0:
                OnMainChapterClick();
                break;
            case 1:
                OnPVPClick();
                break;
        }
    }

    //更换背景图片
    public void ChangeSprite()
    {
        int nowIndex = LevelIndex;
        //选择按钮的索引发生变化时
        if (nowIndex != LevelPreIndex)
        {
            ButtonCenterLevel.GetComponent<Image>().sprite = ShowSprites[nowIndex];
        }
        LevelPreIndex = nowIndex;
    }

    /// <summary>
    /// 人物按钮点击
    /// </summary>
    public void OnCardBtnClick()
    {
        facade.PlayUIAudio("open");
        PanelSecondary.SetActive(true);
        PanelCard.SetActive(true);
        PanelScroller.SetActive(false);
        PanelGetCard.SetActive(false);
        if (GuideIndex.mainGuideIndex == MainGuideIndex.HeroInfoGuide)
        {
            GuideIndex.mainGuideIndex = MainGuideIndex.HeroCardGuide;
            GuideFunc();
        }
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    /// <summary>
    /// 绘卷按钮点击
    /// </summary>
    private void ScrollerButtonOnClick()
    {
        facade.PlayUIAudio("open");
        PanelSecondary.SetActive(true);
        PanelCard.SetActive(false);
        PanelGetCard.SetActive(false);
        PanelScroller.SetActive(true);
        if(GuideIndex.mainGuideIndex == MainGuideIndex.ScrollerGuide)
        {
            GuideIndex.mainGuideIndex = MainGuideIndex.HeroInfoGuide;
            GuideFunc();
        }
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    /// <summary>
    /// 人物头像点击
    /// </summary>
    private void PlayerImageOnClick()
    {
        facade.PlayUIAudio("open");
        PanelPlayerInfo.SetActive(true);
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    /// <summary>
    /// 玩家改名
    /// </summary>
    private void ChangedName(string name)
    {
        if(PrePlayerName == name)
        {
            return;
        }
        GameObject nameTips = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/ChangeNameTips"));
        nameTips.transform.SetParent(PlayerName.gameObject.transform);
        nameTips.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        nameTips.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        if (name.Length > 12)
        {
            PlayerName.text = PrePlayerName;
            nameTips.GetComponent<Text>().text = "名字过长！";
            return;
        }
        nameTips.GetComponent<Text>().text = "修改成功！";
        PlayerName.text = name;
        PrePlayerName = PlayerName.text;
        facade.getUserInfo().Username = name;
        facade.SaveUserInfo();
    }

    /// <summary>
    /// 返回大厅按钮点击
    /// </summary>
    private void CancelButtonOnClick()
    {
        facade.PlayUIAudio("close");
        PanelPlayerInfo.SetActive(false);
        PanelSecondary.SetActive(false);
        PanelCard.SetActive(false);
        PanelScroller.SetActive(false);
        PanelGetCard.SetActive(false);
        PanelCardInfo.SetActive(false);
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
    }

    /// <summary>
    /// 取消玩家详细信息面板按钮点击
    /// </summary>
    private void ReturnButtonOnClick()
    {
        facade.PlayUIAudio("close");
        PanelPlayerInfo.SetActive(false);
        if (PanelSecondary.activeSelf)
        {
            return;
        }
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
    }

    /// <summary>
    /// 退出游戏按钮点击
    /// </summary>
    private void ExitButtonOnClick()
    {
        facade.PlayUIAudio("close");
        Application.Quit();
    }

    /// <summary>
    /// 音量设置按钮点击
    /// </summary>
    private void AudioButtonOnClick()
    {
        facade.PlayUIAudio("open");
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
        PanelVolumnSetting.SetActive(true);
        SliderMusic.value = facade.GetBGMVolumn();
        SliderSoundEffect.value = facade.GetUIAudioVolumn();
        PreSliderMusicVolumn = SliderMusic.value;
        PreSliderSoundEffectVolumn = SliderSoundEffect.value;
    }

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

    /// <summary>
    /// 音量设置确认按钮
    /// </summary>
    private void VolumnSettingCertainBtnOnClick()
    {
        facade.PlayUIAudio("open");
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
        PreSliderMusicVolumn = SliderMusic.value;
        PreSliderSoundEffectVolumn = SliderSoundEffect.value;
        PanelVolumnSetting.SetActive(false);
    }

    /// <summary>
    /// 音量设置取消按钮
    /// </summary>
    private void VolumnSettingCancelBtnOnClick()
    {
        facade.PlayUIAudio("close");
        transform.Find("ButtonRight").gameObject.SetActive(true);
        transform.Find("ButtonLeft").gameObject.SetActive(true);
        PanelVolumnSetting.SetActive(false);
        facade.SetBGMVolumn(PreSliderMusicVolumn);
        facade.SetDrawCardAudioVolumn(PreSliderSoundEffectVolumn);
        facade.SetSkillAudioVolumn(PreSliderSoundEffectVolumn);
        facade.SetUIAudioVolumn(PreSliderSoundEffectVolumn);
        SliderMusic.value = facade.GetBGMVolumn();
        SliderSoundEffect.value = facade.GetUIAudioVolumn();
    }

    #endregion

    #region CardInfo

    //展示卡牌
    private GameObject showHero;
    //所有卡牌信息
    private Dictionary<int, HeroCard> cardInfoDict = new Dictionary<int, HeroCard>();
    //所有物品信息
    private Dictionary<int, Goods> goodsInfoDict = new Dictionary<int, Goods>();
    //玩家持有卡牌信息
    private List<HeroCard> userCardList = new List<HeroCard>();
    //玩家持有物品信息
    private Dictionary<int, Goods> userGoodsDict = new Dictionary<int, Goods>();
    //玩家卡牌对象
    private List<GameObject> userCardGOList = new List<GameObject>();
    //玩家物品对象
    private List<GameObject> userGoodsGOList = new List<GameObject>();

    //当前经验
    private int now_Exp;
    //所需经验
    private int need_Exp;
    //经验比例
    private float rate_Exp;
    //目标经验比例
    private float target_rate_Exp;
    //是否更新经验
    private bool isUpgrade = false;
    //共鸣按钮
    private Button GongMingBtn;
    //卡牌索引
    private int HeroIndex = 0;

    //激活玩家所持有的卡牌面板
    private void OnRoleButtonClick()
    {
        facade.PlayUIAudio("open");
        HideOtherLeftPanelImage();
        CardButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        CardDisplay.gameObject.SetActive(true);
        GoodsDisplay.gameObject.SetActive(false);  
        TalentDisplay.gameObject.SetActive(false);
    }

    //激活玩家所持有的物品面板
    private void OnGoodsButtonClick()
    {
        facade.PlayUIAudio("open");
        HideOtherLeftPanelImage();
        GoodsButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        CardDisplay.gameObject.SetActive(false);
        GoodsDisplay.gameObject.SetActive(true);
        TalentDisplay.gameObject.SetActive(false);
    }

    //激活天赋面板
    private void OnGiftButtonClick()
    {
        facade.PlayUIAudio("open");
        HideOtherLeftPanelImage();
        TalentButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        CardDisplay.gameObject.SetActive(false);
        GoodsDisplay.gameObject.SetActive(false);
        TalentDisplay.gameObject.SetActive(true);
    }

    //激活角色详细信息面板
    private void OnRoleInfoButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelCardInfo.SetActive(true);
        RolePanel.SetActive(true);
        SkillPanel.gameObject.SetActive(false);
        AttributePanel.gameObject.SetActive(false);
    }

    //激活角色技能信息面板
    private void OnSkillInfoButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelCardInfo.SetActive(true);
        RolePanel.SetActive(false);
        SkillPanel.gameObject.SetActive(true);
        AttributePanel.gameObject.SetActive(false);
    }

    //激活角色属性面板
    private void OnAttributeInfoButtonClick()
    {
        facade.PlayUIAudio("open");
        PanelCardInfo.SetActive(true);
        RolePanel.SetActive(false);
        SkillPanel.gameObject.SetActive(false);
        AttributePanel.gameObject.SetActive(true);
    }

    //角色详细信息面板退出事件
    private void OnExitButtonClick()
    {
        facade.PlayUIAudio("close");
        PanelCardInfo.SetActive(false);
        PanelSecondary.SetActive(true);
        PanelCard.SetActive(true);
    }

    //向左切换角色
    private void OnLeftInfoButtonClick()
    {
        facade.PlayUIAudio("open");
        if (HeroIndex > 0)
        {
            SetHeroInfoShow(HeroIndex - 1);
            HeroIndex -= 1;
        }
        else
        {
            SetHeroInfoShow(userCardList.Count - 1);
            HeroIndex = userCardList.Count - 1;
        }
        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + HeroIndex));
        GameObject BG = PanelCardInfo.transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
    }

    //向右切换角色
    private void OnRightInfoButtonClick()
    {
        facade.PlayUIAudio("open");
        if (HeroIndex < userCardList.Count - 1)
        {
            SetHeroInfoShow(HeroIndex + 1);
            HeroIndex += 1;
        }
        else
        {
            SetHeroInfoShow(0);
            HeroIndex = 0;
        }
        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + HeroIndex));
        GameObject BG = PanelCardInfo.transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
    }

    //进入角色详细信息面板
    public void OnHeroListClick(int index)
    {
        facade.PlayUIAudio("open");
        this.HeroIndex = index;
        PanelCardInfo.transform.Find("HeroInfoPanel").gameObject.SetActive(true);
        PanelCard.gameObject.SetActive(false);
        PanelSecondary.gameObject.SetActive(false);
        PanelCardInfo.SetActive(true);

        SetHeroInfoShow(index);

        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + index));
        GameObject BG = PanelCardInfo.transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
        if(GuideIndex.mainGuideIndex == MainGuideIndex.HeroCardGuide)
        {
            GuideIndex.mainGuideIndex = MainGuideIndex.LevelUpGuide;
            GuideFunc();
        }
    }

    //技能信息面板被动技能1显示事件
    public void OnSkillInfo_passive_skill_1_Click()
    {
        facade.PlayUIAudio("open");
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/passive_skill1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[HeroIndex];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_1.Skill_describe;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_1.Skill_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }

    //技能信息面板被动技能2显示事件
    public void OnSkillInfo_passive_skill_2_Click()
    {
        facade.PlayUIAudio("open");
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/passive_skill2").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[HeroIndex];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_2.Skill_describe;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_2.Skill_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }

    //技能信息面板被动技能3显示事件
    public void OnSkillInfo_passive_skill_3_Click()
    {
        facade.PlayUIAudio("open");
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/passive_skill3").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[HeroIndex];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_3.Skill_describe;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_3.Skill_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }

    //技能信息面板主动技能显示事件
    public void OnSkillInfo_active_skill_Click()
    {
        facade.PlayUIAudio("open");
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/active_skill").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[HeroIndex];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Action_skill.Skill_describe;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Action_skill.Skill_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[主动技能]";
    }

    //技能信息面板助战技能显示事件
    public void OnSkillInfo_staff_skill_Click()
    {
        facade.PlayUIAudio("open");
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/staff_skill").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[HeroIndex];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Staff_skill_self.Skill_describe;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Staff_skill_self.Skill_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[助战技能]";
    }

    //将其他技能图片变灰
    public void HideOtherSkillImage()
    {
        GameObject ButtonPanel = SkillPanel.transform.Find("ButtonPanel").gameObject;
        for (int i = 0; i < ButtonPanel.transform.childCount; i++)
        {
            ButtonPanel.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

    }

    //将其他的按钮变灰
    public void HideOtherLeftPanelImage()
    {
        GameObject LeftPanel = PanelCard.transform.Find("LeftPanel").gameObject;
        for (int i = 0; i < LeftPanel.transform.childCount; i++)
        {
            LeftPanel.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    //设置角色详细信息界面
    private void SetHeroInfoShow(int index)
    {
        if (showHero != null)
        {
            Destroy(showHero);
        }
        HeroCard c = userCardList[index];
        now_Exp = c.Hero_exp;
        Sprite img = Resources.Load<Sprite>(c.Card_path);
        CardDisplay.transform.Find("Scroll View/Viewport/Content").GetChild(index).transform.Find("Image").GetComponent<Image>().sprite = img;
        UpgradePanel.transform.Find("Experience/Now_Exp").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString();
        need_Exp = c.Hero_needEXP;
        UpgradePanel.transform.Find("Experience/Next_Level_Need").GetComponent<Text>().text = (c.Hero_needEXP * Mathf.Pow(c.Hero_level, 1)).ToString();
        rate_Exp = ((float)now_Exp) / (float)need_Exp;
        PanelCardInfo.transform.Find("HeroInfoPanel/LeftPanel/texture").GetComponent<Image>().sprite = Resources.Load<Sprite>(c.Texture_path);
        GameObject go = GameObject.Instantiate(Resources.Load(c.Model_path) as GameObject, RolePanel.transform.Find("MiddlePanel/Fox_Image").transform.position, RolePanel.transform.Find("MiddlePanel/Fox_Image").transform.localRotation, RolePanel.transform.Find("MiddlePanel/Fox_Image").transform);
        RolePanel.transform.Find("MiddlePanel/Fox_Image").transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        showHero = go;
        RolePanel.transform.Find("MiddlePanel/CardName/Text").GetComponent<Text>().text = c.Hero_name;
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_1.Skill_name;

        TextDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
        TextGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
        TextLevel.text = facade.getUserInfo().User_level.ToString();
        PlayerInfoLevelText.text = facade.getUserInfo().User_level.ToString();

        PlayerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / (float)(facade.getUserInfo().User_level * 300);
        PlayerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();

        switch (c.Hero_starLevel)
        {
            case 4:
                UpstarPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                RolePanel.transform.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star4");
                UpstarPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "40";
                UpstarPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "2000";
                break;
            case 5:
                UpstarPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                UpstarPanel.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                RolePanel.transform.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star5");
                UpstarPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "50";
                UpstarPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "2500";
                break;
            case 6:
                RolePanel.transform.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star6");
                UpstarPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                UpstarPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                UpstarPanel.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                UpstarPanel.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);

                break;
        }
        RolePanel.transform.Find("MiddlePanel/Attribute/Text").GetComponent<Text>().text = "火";
        RolePanel.transform.Find("ButtonPanel/UpgradePanel/Level/Text").GetComponent<Text>().text = c.Hero_level.ToString();
        SetRadarDrawInfo(c);
        SetSkillInfoShow(index);
        SetUpgradeRate(rate_Exp);
        HideOtherSkillImage();
        SkillPanel.transform.Find("ButtonPanel/passive_skill1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    //设置角色技能详细信息界面
    private void SetSkillInfoShow(int index)
    {
        HeroCard c = userCardList[index];
        SkillPanel.transform.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_1.Skill_describe;
    }

    //注魂显示按钮
    private void OnShowUpgradeBtnClick()
    {
        facade.PlayUIAudio("open");
        UpgradePanel.gameObject.SetActive(true);
        UpstarPanel.gameObject.SetActive(false);
    }

    //共鸣显示按钮
    private void OnShowUpstarBtnClick()
    {
        facade.PlayUIAudio("open");
        UpgradePanel.gameObject.SetActive(false);
        UpstarPanel.gameObject.SetActive(true);
        UpstarBtn.transform.Find("Exclamation").gameObject.SetActive(false);
        GuideIndex.IsUpstarDisplayButton = false;
    }

    /// <summary>
    /// 升级按钮
    /// </summary>
    private void LevelUpBtnOnClick()
    {
        facade.PlayUIAudio("open");
        UserInfo ui = facade.getUserInfo();
        if (ui.User_ReserveExp < 300 * ui.User_level)
        {
            GameObject Tips1 = Instantiate(Resources.Load<GameObject>("Prefab/LevelUpTips"));
            Tips1.transform.SetParent(LevelUpButton.transform);
            Tips1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips1.GetComponent<Text>().text = "储备经验不足！";
            return;
        }
        if(ui.User_level >= 60)
        {
            GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/LevelUpTips"));
            Tips2.transform.SetParent(LevelUpButton.transform);
            Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips2.GetComponent<Text>().text = "已达当前角色等级上限！";
            return;
        }
        GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/LevelUpTips"));
        Tips3.transform.SetParent(LevelUpButton.transform);
        Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, -278, 0);
        Tips3.GetComponent<Text>().text = "Level Up！";
        ui.User_ReserveExp -= 300 * ui.User_level;
        ui.User_level++;
        facade.SaveUserInfo();
        //更新玩家等级和经验信息
        TextLevel.text = facade.getUserInfo().User_level.ToString();
        PlayerInfoLevelText.text = facade.getUserInfo().User_level.ToString();
        PlayerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / ((float)(facade.getUserInfo().User_level * 300));
        PlayerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();
    }

    /// <summary>
    /// 人物升级/升星特效
    /// </summary>
    private void StrengthenEffect()
    {

    }

    //注魂按钮
    private void OnUpgradeClick()
    {
        facade.PlayUIAudio("button");
        HeroCard c = userCardList[HeroIndex];
        UserInfo ui = facade.getUserInfo();
        if (ui.User_ReserveExp < c.Hero_needEXP * Mathf.Pow(c.Hero_level, 1))
        {
            GameObject Tips1 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
            Tips1.transform.SetParent(UpgradePanel.transform);
            Tips1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips1.GetComponent<Text>().text = "储备经验不足！";
            return;
        }
        if(c.Hero_level >= facade.getUserInfo().User_level)
        {
            GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
            Tips2.transform.SetParent(UpgradePanel.transform);
            Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips2.GetComponent<Text>().text = "已达当前角色等级上限！";
            return;
        }
        if (c.Hero_level >= ui.Level_limit)
        {
            GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
            Tips2.transform.SetParent(UpgradePanel.transform);
            Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips2.GetComponent<Text>().text = "已达等级上限！";
            return;
        }
        StrengthenEffect();
        GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
        Tips3.transform.SetParent(UpgradePanel.transform);
        Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Tips3.GetComponent<Text>().text = "Level Up！";
        ui.User_ReserveExp -= c.Hero_needEXP * (int)Mathf.Pow(c.Hero_level, 1);
        target_rate_Exp = 1;
        GameObject go = userCardGOList[HeroIndex];
        c.Hero_level += 1;
        go.transform.Find("Text").GetComponent<Text>().text = c.Hero_level.ToString();

        c.Fire = (int)(c.OrgFire + c.Hero_level * c.Fire_growth * 2.3f);
        c.Water = (int)(c.OrgDark + c.Hero_level * c.Water_growth * 2.3f);
        c.Soil = (int)(c.OrgSoil + c.Hero_level * c.Soil_growth * 2.3f);
        c.Wind = (int)(c.OrgWind + c.Hero_level * c.Wind_growth * 2.3f);
        c.Thunder = (int)(c.OrgThunder + c.Hero_level * c.Thunder_growth * 2.3f);
        c.Dark = (int)(c.OrgDark + c.Hero_level * c.Dark_growth * 2.3f);

        c.Hero_hp = (int)(c.OrgHp + c.Hero_level * c.Hero_hp_growth);
        c.Hero_speed = (int)(c.OrgSpeed + c.Hero_level * c.Hero_speed_growth);

        SetRadarDrawInfo(c);
        SetHeroInfoShow(HeroIndex);
        isUpgrade = true;
    }

    /// <summary>
    /// 共鸣按钮
    /// </summary>
    public void OnGongMingButtonClick()
    {
        facade.PlayUIAudio("button");
        GuideIndex.IsUpstarButton = false;
        UpstarPanel.transform.Find("UpButton/Exclamation").gameObject.SetActive(false);
        HeroCard c = userCardList[HeroIndex];
        UserInfo ui = facade.getUserInfo();
        switch (c.Hero_starLevel)
        {
            case 4:
                if (facade.GetUserGoodsDict()[9002].Goods_has < 2000 || facade.GetUserGoodsDict()[9001].Goods_has < 40)
                {
                    GameObject Tips1 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips1.transform.SetParent(UpstarPanel.transform);
                    Tips1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips1.GetComponent<Text>().text = "钻石或金币不足";
                    return;
                }
                else
                {
                    StrengthenEffect();
                    GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips2.transform.SetParent(UpstarPanel.transform);
                    Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips2.GetComponent<Text>().text = "Star Up！";
                    int new_cardid = c.Hero_id + 1;
                    int new_level = c.Hero_level;
                    c.Hero_starLevel=5;
                    HeroCard hc = facade.getCardInfoDict()[new_cardid];
                    HeroCard aa = new HeroCard(hc.ToString(),facade.GetAllSkillInfoDict());
                    userCardList[HeroIndex] = aa;
                    aa.Hero_level = new_level;
                    aa.Fire = (int)(aa.OrgFire + aa.Hero_level * aa.Fire_growth * 2.3f);
                    aa.Water = (int)(aa.OrgDark + aa.Hero_level * aa.Water_growth * 2.3f);
                    aa.Soil = (int)(aa.OrgSoil + aa.Hero_level * aa.Soil_growth * 2.3f);
                    aa.Wind = (int)(aa.OrgWind + aa.Hero_level * aa.Wind_growth * 2.3f);
                    aa.Thunder = (int)(aa.OrgThunder + aa.Hero_level * aa.Thunder_growth * 2.3f);
                    aa.Dark = (int)(aa.OrgDark + aa.Hero_level * aa.Dark_growth * 2.3f);

                    aa.Hero_hp = (int)(aa.OrgHp + aa.Hero_level * aa.Hero_hp_growth);
                    aa.Hero_speed = (int)(aa.OrgSpeed + aa.Hero_level * aa.Hero_speed_growth);
                    SetRadarDrawInfo(aa);
                    facade.GetUserGoodsDict()[9002].Goods_has -= 2000;
                    facade.GetUserGoodsDict()[9001].Goods_has -= 40;
                    facade.SaveUserInfo();
                }
                break;
            case 5:
                if (facade.GetUserGoodsDict()[9002].Goods_has < 2500 || facade.GetUserGoodsDict()[9001].Goods_has < 50)
                {
                    GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips3.transform.SetParent(UpstarPanel.transform);
                    Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips3.GetComponent<Text>().text = "钻石或金币不足";
                    return;
                }
                else
                {
                    StrengthenEffect();
                    GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips3.transform.SetParent(UpstarPanel.transform);
                    Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips3.GetComponent<Text>().text = "Star Up！";
                    int new_cardid = c.Hero_id + 1;
                    int new_level = c.Hero_level;
                    c.Hero_starLevel = 6;
                    HeroCard hc = facade.getCardInfoDict()[new_cardid];
                    HeroCard aa = new HeroCard(hc.ToString(), facade.GetAllSkillInfoDict());
                    userCardList[HeroIndex] = aa;
                    aa.Hero_level = new_level;
                    aa.Fire = (int)(aa.OrgFire + aa.Hero_level * aa.Fire_growth * 2.3f);
                    aa.Water = (int)(aa.OrgDark + aa.Hero_level * aa.Water_growth * 2.3f);
                    aa.Soil = (int)(aa.OrgSoil + aa.Hero_level * aa.Soil_growth * 2.3f);
                    aa.Wind = (int)(aa.OrgWind + aa.Hero_level * aa.Wind_growth * 2.3f);
                    aa.Thunder = (int)(aa.OrgThunder + aa.Hero_level * aa.Thunder_growth * 2.3f);
                    aa.Dark = (int)(aa.OrgDark + aa.Hero_level * aa.Dark_growth * 2.3f);

                    aa.Hero_hp = (int)(aa.OrgHp + aa.Hero_level * aa.Hero_hp_growth);
                    aa.Hero_speed = (int)(aa.OrgSpeed + aa.Hero_level * aa.Hero_speed_growth);
                    SetRadarDrawInfo(aa);
                    facade.GetUserGoodsDict()[9002].Goods_has -= 2500;
                    facade.GetUserGoodsDict()[9001].Goods_has -= 50;
                    facade.SaveUserInfo();
                }
                break;
            case 6:
                GameObject Tips = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                Tips.transform.SetParent(UpstarPanel.transform);
                Tips.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Tips.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                Tips.GetComponent<Text>().text = "已达到最大星级！";
                break;
        }
        
        SetHeroInfoShow(HeroIndex);
    }

    //设置注魂显示
    private void SetUpgradeRate(float rate)
    {
        RolePanel.transform.Find("ButtonPanel/UpgradePanel/Experience/rate_left").transform.GetComponent<Image>().fillAmount = rate;
        RolePanel.transform.Find("ButtonPanel/UpgradePanel/Experience/rate_right").transform.GetComponent<Image>().fillAmount = rate;
    }

    //更新雷达图
    private void SetRadarDrawInfo(HeroCard c)
    {
        RadarDraw rd = AttributePanel.transform.Find("MiddlePanel/attribute/RadarSprite").transform.GetComponent<RadarDraw>();
        rd.fire = c.Fire;
        rd.water = c.Water;
        rd.earth = c.Soil;
        rd.wind = c.Wind;
        rd.thunder = c.Thunder;
        rd.dark = c.Dark;

        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/fire").transform.GetComponent<Text>().text = "(" + c.Fire + ")火";
        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/water").transform.GetComponent<Text>().text = "(" + c.Water + ")水";
        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/earth").transform.GetComponent<Text>().text = "(" + c.Soil + ")土";
        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/wind").transform.GetComponent<Text>().text = "风(" + c.Wind + ")";
        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/thunder").transform.GetComponent<Text>().text = "雷(" + c.Thunder + ")";
        AttributePanel.transform.Find("MiddlePanel/attribute/AttributeInfo/dark").transform.GetComponent<Text>().text = "暗(" + c.Dark + ")";

        AttributePanel.transform.Find("MiddlePanel/otherAttribute/Hp/Text").transform.GetComponent<Text>().text = c.Hero_hp.ToString();
        AttributePanel.transform.Find("MiddlePanel/otherAttribute/Speed/Text").transform.GetComponent<Text>().text = c.Hero_speed.ToString();
    }

    /// <summary>
    /// 更新角色牌显示
    /// </summary>
    private void UpdateHeroCard()
    {
        //更新玩家等级和经验信息
        PlayerInfoLevelText.text = facade.getUserInfo().User_level.ToString();
        PlayerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / (float)(facade.getUserInfo().User_level * 300);
        PlayerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();

        TextDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
        TextGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();

        userCardList = facade.getUserCardList();
        userGoodsDict = facade.GetUserGoodsDict();
        foreach (Transform c in PanelCard.transform.Find("CenterPanel/card/Scroll View/Viewport/Content").transform)
        {
            Destroy(c.gameObject);
            userCardGOList.Remove(c.gameObject);
        }
        foreach (Transform g in PanelCard.transform.Find("CenterPanel/goods/Scroll View/Viewport/Content").transform)
        {
            Destroy(g.gameObject);
            userGoodsGOList.Remove(g.gameObject);
        }
        for (int i = 0; i < userCardList.Count; i++)
        {
            int count = i;
            HeroCard c = userCardList[i];
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HeroList"), PanelCard.transform.Find("CenterPanel/card/Scroll View/Viewport/Content").transform);
            Sprite img = Resources.Load<Sprite>(c.Card_path);
            go.transform.Find("Image").GetComponent<Image>().sprite = img;
            //做第一个人物的引导
            if (i == 0)
            {
                PanelGuide.transform.Find("HeroList/Image").GetComponent<Image>().sprite = img;
                PanelGuide.transform.Find("HeroList/Text").GetComponent<Text>().text = c.Hero_level.ToString();
            }
            go.transform.Find("Text").GetComponent<Text>().text = c.Hero_level.ToString();
            go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            go.transform.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OnHeroListClick(count);
            });
            //做第一个人物的引导
            if (i == 0)
            {
                PanelGuide.transform.Find("HeroList").transform.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    OnHeroListClick(count);
                });
            }
            userCardGOList.Add(go);
        }
        foreach (var key in userGoodsDict.Keys)
        {
            if (userGoodsDict[key].Goods_has != 0)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/goods"), PanelCard.transform.Find("CenterPanel/goods/Scroll View/Viewport/Content").transform);
                Sprite img = Resources.Load<Sprite>(userGoodsDict[key].Goods_path);
                go.transform.Find("Image").GetComponent<Image>().sprite = img;
                go.transform.Find("Image/Text").GetComponent<Text>().text = "x" + userGoodsDict[key].Goods_has.ToString();
                userGoodsGOList.Add(go);
            }
        }
    }

    #endregion

    #region Guide

    /// <summary>
    /// 引导函数
    /// </summary>
    private void GuideFunc()
    {
        if (GuideIndex.isGuide == true && GuideIndex.mainGuideIndex != MainGuideIndex.FinishGuide)
        {
            PanelGuide.SetActive(true);
            switch (GuideIndex.mainGuideIndex)
            {
                case MainGuideIndex.LevelGudie:
                    PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(true);
                    PanelGuide.transform.Find("ButtonJourney").gameObject.SetActive(true);
                    PanelGuide.transform.Find("ButtonJourney").GetComponent<Button>().onClick.AddListener(OnMainChapterClick);
                    GuideFinger.GetComponent<RectTransform>().localPosition = new Vector3(459, 2, 0);
                    GuideFinger.GetComponent<Animator>().SetTrigger("LevelGuide");
                    GuideText.text = "欢迎来到卡其亚大陆，我是秩序女神欧诺米亚，王都发生了异变，情况紧急，让我们去查看情况！";
                    GuideIndex.mainGuideIndex = MainGuideIndex.ScrollerGuide;
                    break;
                case MainGuideIndex.ScrollerGuide:
                    PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(true);
                    PanelGuide.transform.Find("ButtonJourney").gameObject.SetActive(false);
                    PanelGuide.transform.Find("ScrollerButton").gameObject.SetActive(true);
                    PanelGuide.transform.Find("ScrollerButton").GetComponent<Button>().onClick.AddListener(ScrollerButtonOnClick);
                    GuideText.text = "现在我们已经解决了王都的异变，接下来让我们来看一下绘卷的力量吧！";
                    GuideFinger.GetComponent<Animator>().SetTrigger("ScrollerGuide");
                    break;
                case MainGuideIndex.HeroInfoGuide:
                    GuideFinger.GetComponent<Animator>().SetTrigger("Idle");
                    PanelGuide.transform.Find("ScrollerButton").gameObject.SetActive(false);
                    PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(false);
                    PanelGuide.transform.Find("Text").gameObject.SetActive(false);
                    PanelGuide.transform.Find("ImageMask").gameObject.SetActive(false);
                    PanelGuide.transform.Find("ImageAlphaMask").gameObject.SetActive(true);
                    StartCoroutine("WaitForGuide");
                    break;
                case MainGuideIndex.HeroCardGuide:
                    GuideFinger.GetComponent<Animator>().SetTrigger("Idle");
                    PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(false);
                    PanelGuide.transform.Find("Text").gameObject.SetActive(false);
                    PanelGuide.transform.Find("HeroList").gameObject.SetActive(true);
                    PanelGuide.transform.Find("HeroInfoButton").gameObject.SetActive(false);
                    GuideFinger.GetComponent<Animator>().SetTrigger("HeroCardGuide");
                    break;
                case MainGuideIndex.LevelUpGuide:
                    GuideFinger.GetComponent<Animator>().SetTrigger("Idle");
                    GuideFinger.GetComponent<Animator>().SetTrigger("LevelUpGuide");
                    PanelGuide.transform.Find("HeroList").gameObject.SetActive(false);
                    PanelGuide.transform.Find("ButtonPanel").gameObject.SetActive(true);
                    PanelGuide.transform.Find("ButtonPanel/UpgradePanel/UpButton").GetComponent<Button>().onClick.AddListener(FinishGuideOnClick);
                    PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(true);
                    PanelGuide.transform.Find("Text").gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            PanelGuide.SetActive(false);
        }
    }

    /// <summary>
    /// 新手指引等待协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForGuide()
    {
        yield return new WaitForSeconds(5.0f);
        PanelGuide.transform.Find("ainuomiya").gameObject.SetActive(true);
        PanelGuide.transform.Find("Text").gameObject.SetActive(true);
        PanelGuide.transform.Find("ImageMask").gameObject.SetActive(true);
        PanelGuide.transform.Find("ImageAlphaMask").gameObject.SetActive(true);
        PanelGuide.transform.Find("HeroInfoButton").gameObject.SetActive(true);
        GuideText.text = "你现在的经验已经足够升级你的角色卡牌了，快来升级角色卡牌吧！";
        GuideFinger.GetComponent<Animator>().SetTrigger("HeroInfoGuide");
        PanelGuide.transform.Find("HeroInfoButton").GetComponent<Button>().onClick.AddListener(OnCardBtnClick);
    }

    /// <summary>
    /// 完成新手引导
    /// </summary>
    private void FinishGuideOnClick()
    {
        GuideIndex.mainGuideIndex = MainGuideIndex.FinishGuide;
        GuideIndex.isGuide = false;
        facade.getUserInfo().Minecraft = 0;
        facade.SaveUserInfo();
        PanelGuide.SetActive(false);
    }

    #endregion

    #region GetCard

    //抽卡特效面板
    private GameObject PanelGetCardEffect;
    //抽卡特效父节点
    private GameObject Animation;
    //单抽图片
    private GameObject GetCardOnceGo;
    //十连图片
    private GameObject[] GetCardTenTimesGo;
    //奖励面板
    private GameObject PanelReward;
    //单抽父节点
    private GameObject GoGetCardOnce;
    //十连父节点
    private GameObject GoGetCardTimes;
    //退出抽卡动画
    private GameObject ButtonExitAnim;
    //是否为接受单抽数据
    private bool isGetCardOnce;
    //是否为接受十连数据
    private bool isGetCardTenTimes;
    //是否接受抽卡失败数据
    private bool isGetCardFailed;
    //抽牌数据
    private string[] data;
    //跳过按钮
    private Button ButtonJump;
    //是否为单抽动画
    private bool isOnceAnimation;

    //获得钻石按钮
    private Button ButtonGetGold;
    //获得金币按钮
    private Button ButtonGetDiamond;
    //获得经验按钮
    private Button ButtonGetExp;

    /// <summary>
    /// 获得金币点击
    /// </summary>
    private void GetGoldBtnOnClick()
    {
        facade.GetUserGoodsDict()[9002].Goods_has += 10000;
        facade.SaveUserInfo();
        UpdateHeroCard();
    }

    /// <summary>
    /// 获得钻石点击
    /// </summary>
    private void GetDiamondBtnOnClick()
    {
        facade.GetUserGoodsDict()[9001].Goods_has += 5000;
        facade.SaveUserInfo();
        UpdateHeroCard();
    }

    /// <summary>
    /// 获得经验点击
    /// </summary>
    private void GetExpBtnOnClick()
    {
        UserInfo ui = facade.getUserInfo();
        ui.User_ReserveExp += 10000;
        facade.SaveUserInfo();
        UpdateHeroCard();
    }

    /// <summary>
    /// 赋魂按钮点击
    /// </summary>
    private void GetCardBtnOnClick()
    {
        facade.PlayUIAudio("open");
        PanelSecondary.SetActive(true);
        PanelScroller.SetActive(false);
        PanelCard.SetActive(false);
        PanelGetCard.SetActive(true);
        transform.Find("ButtonRight").gameObject.SetActive(false);
        transform.Find("ButtonLeft").gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入概率详情面板
    /// </summary>
    private void EnterProbaBtnOnClick()
    {
        facade.PlayUIAudio("open");
        PanelProbability.SetActive(true);
    }

    /// <summary>
    /// 退出概率详情面板
    /// </summary>
    private void ExitProbaBtnOnClick()
    {
        facade.PlayUIAudio("close");
        PanelProbability.SetActive(false);
    }

    /// <summary>
    /// 单抽
    /// </summary>
    private void GetCardOnce()
    {
        ButtonOnce.interactable = false;
        GetComponent<GetCardRequest>().SendRequest("Once", facade.GetUserGoodsDict()[9001].Goods_has.ToString());
    }

    /// <summary>
    /// 十连
    /// </summary>
    private void GetCardTenTimes()
    {
        ButtonTenTimes.interactable = false;
        GetComponent<GetCardRequest>().SendRequest("TenTimes", facade.GetUserGoodsDict()[9001].Goods_has.ToString());
    }

    /// <summary>
    /// 抽卡处理函数
    /// </summary>
    private void GetCardFunc(string []data, bool isOnce)
    {
        if(isOnce)
        {
            GameObject go;
            if (data[0].Contains("SSR"))
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/SSR"));
            }
            else if (data[0].Contains("SR"))
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/SR"));
            }
            else if (data[0].Contains("R"))
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/R"));
            }
            else if (data[0].Contains("Diamond"))
            {
                string[] strs = data[0].Split(':');
                go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Diamond"));
                go.transform.GetChild(0).GetComponent<Text>().text = strs[1];
            }
            else
            {
                string[] strs = data[0].Split(':');
                go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Gold"));
                go.transform.GetChild(0).GetComponent<Text>().text = strs[1];
            }
            go.transform.SetParent(GoGetCardOnce.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            int count = 0;
            foreach(string str in data)
            {
                GameObject go;
                if (str.Contains("SSR"))
                {
                    go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/SSR"));
                }
                else if (str.Contains("SR"))
                {
                    go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/SR"));
                }
                else if (str.Contains("R"))
                {
                    go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/R"));
                }
                else if (str.Contains("Diamond"))
                {
                    string[] strs = str.Split(':');
                    go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Diamond"));
                    go.transform.GetChild(0).GetComponent<Text>().text = strs[1];
                }
                else
                {
                    string[] strs = str.Split(':');
                    go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Gold"));
                    go.transform.GetChild(0).GetComponent<Text>().text = strs[1];
                }
                go.transform.SetParent(GoGetCardTimes.transform.GetChild(count++));
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    /// <summary>
    /// 单抽
    /// </summary>
    public void GetCardOnceResponse(string[] data)
    {
        this.data = data;
        isOnceAnimation = true;
        isGetCardOnce = true;
    }

    /// <summary>
    /// 等待单抽动画协程
    /// </summary>
    private IEnumerator WaitForGetCardOnce()
    {
        yield return new WaitForSeconds(3.0f);
        facade.PlayUIAudio("ui_str");
        yield return new WaitForSeconds(4.0f);
        ButtonJump.gameObject.SetActive(false);
        PanelReward.SetActive(true);
        GoGetCardOnce.SetActive(true);
        ButtonExitAnim.SetActive(true);
        ButtonOnce.interactable = true;
    }

    /// <summary>
    /// 十连
    /// </summary>
    public void GetCardTenTimesResponse(string[] data)
    {
        this.data = data;
        isOnceAnimation = false;
        isGetCardTenTimes = true;
    }

    /// <summary>
    /// 等待十连动画协程
    /// </summary>
    private IEnumerator WaitForGetCardTenTimes()
    {
        yield return new WaitForSeconds(3.0f);
        facade.PlayUIAudio("ui_str");
        yield return new WaitForSeconds(4.0f);
        ButtonJump.gameObject.SetActive(false);
        ButtonExitAnim.SetActive(true);
        PanelReward.SetActive(true);
        GoGetCardTimes.SetActive(true);
        ButtonTenTimes.interactable = true;
    }

    /// <summary>
    /// 抽卡失败
    /// </summary>
    public void GetCardFailed()
    {
        isGetCardFailed = true;
    }

    /// <summary>
    /// 退出抽卡动画
    /// </summary>
    private void ExitGetCardAnim()
    {
        PanelGetCardEffect.SetActive(false);
        PanelReward.SetActive(false);
        GoGetCardTimes.SetActive(false);
        GoGetCardOnce.SetActive(false);
        ButtonExitAnim.SetActive(false);
        for (int i = GoGetCardOnce.transform.childCount - 1; i >= 0; --i)
        {
            Destroy(GoGetCardOnce.transform.GetChild(i).gameObject);
        }
        for (int i = GoGetCardTimes.transform.childCount - 1; i >= 0; --i)
        {
            for (int j = GoGetCardTimes.transform.GetChild(i).childCount - 1; j >= 0; --j)
            {
                Destroy(GoGetCardTimes.transform.GetChild(i).GetChild(j).gameObject);
            }
        }
        ButtonJump.gameObject.SetActive(false);
        UpdateHeroCard();
    }

    /// <summary>
    /// 跳过抽卡动画
    /// </summary>
    private void JumpAnimation()
    {
        StopAllCoroutines();
        for (int i = Animation.transform.childCount - 1; i >= 0; --i)
        {
            Destroy(Animation.transform.GetChild(i).gameObject);
        }
        if (isOnceAnimation)
        {
            PanelReward.SetActive(true);
            GoGetCardOnce.SetActive(true);
            ButtonExitAnim.SetActive(true);
            ButtonOnce.interactable = true;
        }
        else
        {
            ButtonExitAnim.SetActive(true);
            PanelReward.SetActive(true);
            GoGetCardTimes.SetActive(true);
            ButtonTenTimes.interactable = true;
        }
        ButtonJump.gameObject.SetActive(false);
    }

    #endregion

    private void Awake()
    {
        Debug.Log(facade.getUserInfo().Minecraft);
        if (facade.getUserInfo().Minecraft != 1)
        {
            GuideCtrl.GuideIndex.isGuide = false;
            GuideCtrl.GuideIndex.isNew = false;
            GuideIndex.mainGuideIndex = MainGuideIndex.FinishGuide;
        }
        if (facade.getUserInfo().Minecraft == 1 && GuideIndex.IsFirstGuide == true)
        {
            GuideCtrl.GuideIndex.isGuide = true;
            GuideCtrl.GuideIndex.isNew = true;
            GuideIndex.IsUpstarDisplayButton = true;
            GuideIndex.IsUpstarButton = true;
            GuideIndex.mainGuideIndex = MainGuideIndex.LevelGudie;
            GuideIndex.IsFirstGuide = false;
        }

        isUpdateLevelIndex = true;
        levelIndexDestination = Vector3.zero;

        PanelUI = transform.Find("PanelUI").gameObject;

        transform.Find("ButtonRight").GetComponent<Button>().onClick.AddListener(TurnToLeft);
        transform.Find("ButtonLeft").GetComponent<Button>().onClick.AddListener(TurnToRight);

        ButtonCenterLevel = PanelUI.transform.Find("ButtonCenterLevel").gameObject;
        ButtonCenterLevel.GetComponent<Button>().onClick.AddListener(OnJourneyButtonClick);
        ShowSprites = new Sprite[2];
        ShowSprites[0] = Resources.Load<Sprite>("UI/FirstCenterLevel");
        ShowSprites[1] = Resources.Load<Sprite>("UI/SecondCenterLevel");

        PanelLeftButton = PanelUI.transform.Find("PanelLeftButton").gameObject;
        ButtonLetterBox = PanelLeftButton.transform.Find("ButtonLetterBox").GetComponent<Button>();
        ButtonLetterBox.onClick.AddListener(OnLetterBoxButtonClick);
        ButtonTask = PanelLeftButton.transform.Find("ButtonTask").GetComponent<Button>();
        ButtonTask.onClick.AddListener(OnTaskPanelButtonClick);
        ButtonPub = PanelLeftButton.transform.Find("ButtonPub").GetComponent<Button>();
        ButtonPub.onClick.AddListener(OnNoticePanelButtonClick);
        ButtonHorn = PanelLeftButton.transform.Find("ButtonHorn").GetComponent<Button>();

        PanelLowButton = PanelUI.transform.Find("PanelLowButton").gameObject;
        Content = PanelLowButton.transform.Find("Viewport/Content").gameObject;
        ButtonList = new List<GameObject>();
        //主线
        ButtonList.Add(Content.transform.Find("mainChapter").gameObject);
        //竞技场
        ButtonList.Add(Content.transform.Find("Area").gameObject);
        ButtonList[0].GetComponent<Button>().onClick.AddListener(OnMainChapterClick);
        ButtonList[1].GetComponent<Button>().onClick.AddListener(OnPVPClick);
        ContentCenterPosPre = Content.GetComponent<RectTransform>();
        ContentCenterPosNow = Content.GetComponent<RectTransform>();
        StartPosX = ContentCenterPosPre.anchoredPosition.x;
        NowPosX = ContentCenterPosNow.anchoredPosition.x;
        //根据Content的偏移值确定选中的按钮（一个按钮宽度110）
        offsetX = StartPosX - NowPosX;
        LevelIndex = 0;
        LevelPreIndex = 0;

        PanelSecondary = PanelUI.transform.Find("PanelSecondary").gameObject;
        PanelScroller = PanelSecondary.transform.Find("PanelScroller").gameObject;
        PanelCard = PanelSecondary.transform.Find("PanelCard").gameObject;
        CardButton = PanelCard.transform.Find("LeftPanel/roleButton").GetComponent<Button>();
        GoodsButton = PanelCard.transform.Find("LeftPanel/goodsButton").GetComponent<Button>();
        TalentButton = PanelCard.transform.Find("LeftPanel/lineupButton").GetComponent<Button>();
        CardDisplay = PanelCard.transform.Find("CenterPanel/card").gameObject;
        GoodsDisplay = PanelCard.transform.Find("CenterPanel/goods").gameObject;
        TalentDisplay = PanelCard.transform.Find("CenterPanel/lineup").gameObject;
        //按钮事件
        CardButton.onClick.AddListener(OnRoleButtonClick);
        GoodsButton.onClick.AddListener(OnGoodsButtonClick);
        TalentButton.onClick.AddListener(OnGiftButtonClick);

        PanelGetCard = PanelSecondary.transform.Find("PanelGetCard").gameObject;
        ImageIntro = PanelGetCard.transform.Find("ImageIntro").GetComponent<Image>();
        ButtonProbability = PanelGetCard.transform.Find("ButtonProbability").GetComponent<Button>();
        ButtonProbability.onClick.AddListener(EnterProbaBtnOnClick);
        ButtonOnce = PanelGetCard.transform.Find("PanelRight/PanelButton/ButtonOnce").GetComponent<Button>();
        ButtonOnce.onClick.AddListener(GetCardOnce);
        ButtonTenTimes = PanelGetCard.transform.Find("PanelRight/PanelButton/ButtonTenTimes").GetComponent<Button>();
        ButtonTenTimes.onClick.AddListener(GetCardTenTimes);
        PanelSelect = PanelGetCard.transform.Find("PanelRight/PanelSelect").gameObject;
        ScrollViewGetCard = PanelSelect.transform.Find("Scroll View").gameObject;
        TextDate = PanelGetCard.transform.Find("PanelIntro/TextDate").GetComponent<Text>();
        PanelGetCardEffect = transform.Find("PanelGetCardEffect").gameObject;
        ButtonExitAnim = PanelGetCardEffect.transform.Find("ButtonExit").gameObject;
        ButtonExitAnim.GetComponent<Button>().onClick.AddListener(ExitGetCardAnim);
        ButtonJump = PanelGetCardEffect.transform.Find("ButtonJump").GetComponent<Button>();
        ButtonJump.onClick.AddListener(JumpAnimation);
        isOnceAnimation = false;
        isGetCardOnce = false;
        isGetCardTenTimes = false;
        isGetCardFailed = false;
        data = new string[10];
        ButtonGetGold = PanelGetCard.transform.Find("ButtonGetGold").GetComponent<Button>();
        ButtonGetGold.onClick.AddListener(GetGoldBtnOnClick);
        ButtonGetDiamond = PanelGetCard.transform.Find("ButtonGetDiamond").GetComponent<Button>();
        ButtonGetDiamond.onClick.AddListener(GetDiamondBtnOnClick);
        ButtonGetExp = PanelGetCard.transform.Find("ButtonGetExp").GetComponent<Button>();
        ButtonGetExp.onClick.AddListener(GetExpBtnOnClick);
        Animation = PanelGetCardEffect.transform.Find("Animation").gameObject;
        GetCardOnceGo = PanelGetCardEffect.transform.Find("PanelReward/ImageOnce").gameObject;
        GetCardTenTimesGo = new GameObject[10];
        for (int i = 0; i < 10; ++i)
        {
            GetCardTenTimesGo[i] = PanelGetCardEffect.transform.Find("PanelReward/PanelTenTimes").GetChild(i).gameObject;
        }
        PanelReward = PanelGetCardEffect.transform.Find("PanelReward").gameObject;
        GoGetCardOnce = PanelReward.transform.Find("ImageOnce").gameObject;
        GoGetCardTimes = PanelReward.transform.Find("PanelTenTimes").gameObject;

        PanelPlayerIcon = PanelUI.transform.Find("PanelPlayerIcon").gameObject;
        ImagePlayerIcon = PanelPlayerIcon.transform.Find("ImagePlayerIcon").gameObject;
        ImagePlayerIcon.GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);
        ImagePlayerIconFrame = PanelPlayerIcon.transform.Find("ImagePlayerIconFrame").gameObject;
        ImagePlayerIconFrame.GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);
        TextLevel = PanelPlayerIcon.transform.Find("TextLevel").GetComponent<Text>();
        TextLevel.text = facade.getUserInfo().User_level.ToString();

        PanelGoods = PanelUI.transform.Find("PanelGoods").gameObject;
        TextGold = PanelGoods.transform.Find("TextGold").GetComponent<Text>();
        TextDiamond = PanelGoods.transform.Find("TextDiamond").GetComponent<Text>();
        TextRare = PanelGoods.transform.Find("TextRare").GetComponent<Text>();
        TextDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
        TextGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();

        PanelRightButton = PanelUI.transform.Find("PanelRightButton").gameObject;
        ButtonHeroCard = PanelRightButton.transform.Find("ButtonHeroCard").GetComponent<Button>();
        ButtonHeroCard.onClick.AddListener(OnCardBtnClick);
        ButtonScroller = PanelRightButton.transform.Find("ButtonScroller").GetComponent<Button>();
        ButtonScroller.onClick.AddListener(ScrollerButtonOnClick);
        ButtonConscribe = PanelRightButton.transform.Find("ButtonConscribe").GetComponent<Button>();
        ButtonConscribe.onClick.AddListener(GetCardBtnOnClick);
        ButtonMarket = PanelRightButton.transform.Find("ButtonMarket").GetComponent<Button>();

        ButtonJourney = transform.Find("ButtonJourney").GetComponent<Button>();
        ButtonJourney.onClick.AddListener(OnJourneyButtonClick);

        PanelUIDetail = transform.Find("PanelUIDetail").gameObject;
        ImageMask = PanelUIDetail.transform.Find("ImageMask").gameObject;
        PanelLetterBox = PanelUIDetail.transform.Find("PanelLetterBox").gameObject;
        PanelLetterBox.transform.Find("ButtonExit").GetComponent<Button>().onClick.AddListener(OnCloseButtonClick);
        foreach(Button button in PanelLetterBox.transform.Find("Read_Scroll View/Viewport/Content").GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(OnMailItemClick);
        }
        PanelTaskBox = PanelUIDetail.transform.Find("PanelTaskBox").gameObject;
        PanelTaskBox.transform.Find("ButtonExit").GetComponent<Button>().onClick.AddListener(OnCloseTaskPanelButtonClick);
        PanelTaskBox.transform.Find("Read_Scroll View/Viewport/Content/bg").GetComponent<Button>().onClick.AddListener(OnTaskItemClick);
        ImageSeniorMask = PanelUIDetail.transform.Find("ImageSeniorMask").gameObject;
        PanelLetterDetail = PanelUIDetail.transform.Find("PanelLetterDetail").gameObject;
        PanelLetterDetail.transform.Find("PanelUnion/Sure_Button").GetComponent<Button>().onClick.AddListener(OnSureButtonClick);
        PanelTaskReward = PanelUIDetail.transform.Find("PanelTaskReward").gameObject;
        PanelTaskReward.transform.Find("PanelChest/Sure_Button").GetComponent<Button>().onClick.AddListener(OnSureTaskRewardClick);
        PanelNotice = PanelUIDetail.transform.Find("PanelNotice").gameObject;
        PanelNotice.transform.Find("Close_Button").GetComponent<Button>().onClick.AddListener(OnCloseNoticePanelButtonClick);

        ReadToggle = PanelLetterBox.transform.Find("ReadOrUnread/Read").GetComponent<Image>();
        UnReadToggle = PanelLetterBox.transform.Find("ReadOrUnread/UnRead").GetComponent<Image>();
        ReadPanel = PanelLetterBox.transform.Find("Read_Scroll View").gameObject;
        UnReadPanel = PanelLetterBox.transform.Find("UnReadBG").gameObject;

        PanelPlayerInfo = transform.Find("PanelPlayerInfo").gameObject;
        PanelPlayerInfo.transform.Find("ButtonReturn").GetComponent<Button>().onClick.AddListener(ReturnButtonOnClick);
        PlayerIcon = PanelPlayerInfo.transform.Find("PanelDetailedInfo/ImagePlayerIcon").GetComponent<Image>();
        PlayerInfoLevelText= PanelPlayerInfo.transform.Find("PanelDetailedInfo/TextLevel").GetComponent<Text>();
        PlayerExpSlider = PanelPlayerInfo.transform.Find("PanelDetailedInfo/SliderExp").GetComponent<Slider>();
        PlayerName = PanelPlayerInfo.transform.Find("InputField").GetComponent<InputField>();
        PlayerName.text = facade.getUserInfo().Username;
        PlayerName.onEndEdit.AddListener((param) => { ChangedName(param); });
        PrePlayerName = PlayerName.text;
        SetAudioVolumnButton = PanelPlayerInfo.transform.Find("PanelDetailedInfo/ButtonAudio").GetComponent<Button>();
        SetAudioVolumnButton.onClick.AddListener(AudioButtonOnClick);
        CancelButton = PanelPlayerInfo.transform.Find("PanelDetailedInfo/ButtonCancel").GetComponent<Button>();
        CancelButton.onClick.AddListener(CancelButtonOnClick);
        ExitButton = PanelPlayerInfo.transform.Find("PanelDetailedInfo/ButtonExit").GetComponent<Button>();
        ExitButton.onClick.AddListener(ExitButtonOnClick);
        LevelUpButton = PanelPlayerInfo.transform.Find("ButtonLevelUp").GetComponent<Button>();
        LevelUpButton.onClick.AddListener(LevelUpBtnOnClick);
        //更新玩家等级和经验信息
        PlayerInfoLevelText.text = facade.getUserInfo().User_level.ToString();
        PlayerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / (float)(facade.getUserInfo().User_level * 300);
        PlayerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();

        PanelCardInfo = transform.Find("PanelCardInfo").gameObject;
        UpgradePanel = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/role/ButtonPanel/UpgradePanel").gameObject;
        UpstarPanel = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/role/ButtonPanel/UpstarPanel").gameObject;
        UpgradeBtn = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/role/ButtonPanel/UpgradeBtn").GetComponent<Button>();
        UpstarBtn = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/role/ButtonPanel/UpstarBtn").GetComponent<Button>();
        AttributePanel = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/attribute").gameObject;
        RolePanel = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/role").gameObject;
        SkillPanel = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/skill").gameObject;
        RoleButton = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/TopPanel/role").GetComponent<Button>();
        AttributeButton = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/TopPanel/attribute").GetComponent<Button>();
        SkillButton = PanelCardInfo.transform.Find("HeroInfoPanel/RightPanel/TopPanel/skill").GetComponent<Button>();

        PanelCardInfo.transform.Find("HeroInfoPanel/LeftPanel/ExitButton").GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        PanelCardInfo.transform.Find("HeroInfoPanel/LeftPanel/LeftButton").GetComponent<Button>().onClick.AddListener(OnLeftInfoButtonClick);
        PanelCardInfo.transform.Find("HeroInfoPanel/LeftPanel/RightButton").GetComponent<Button>().onClick.AddListener(OnRightInfoButtonClick);
        RoleButton.onClick.AddListener(OnRoleInfoButtonClick);
        SkillButton.onClick.AddListener(OnSkillInfoButtonClick);
        AttributeButton.onClick.AddListener(OnAttributeInfoButtonClick);

        RolePanel.transform.Find("ButtonPanel/UpgradeBtn").GetComponent<Button>().onClick.AddListener(OnShowUpgradeBtnClick);
        RolePanel.transform.Find("ButtonPanel/UpstarBtn").GetComponent<Button>().onClick.AddListener(OnShowUpstarBtnClick);
        RolePanel.transform.Find("ButtonPanel/UpgradePanel/UpButton").GetComponent<Button>().onClick.AddListener(OnUpgradeClick);

        SkillPanel.transform.Find("ButtonPanel/passive_skill1").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_1_Click);
        SkillPanel.transform.Find("ButtonPanel/passive_skill2").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_2_Click);
        SkillPanel.transform.Find("ButtonPanel/passive_skill3").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_3_Click);
        SkillPanel.transform.Find("ButtonPanel/active_skill").GetComponent<Button>().onClick.AddListener(OnSkillInfo_active_skill_Click);
        SkillPanel.transform.Find("ButtonPanel/staff_skill").GetComponent<Button>().onClick.AddListener(OnSkillInfo_staff_skill_Click);

        GongMingBtn = UpstarPanel.transform.GetChild(0).GetComponent<Button>();
        GongMingBtn.onClick.AddListener(OnGongMingButtonClick);

        PanelVolumnSetting = PanelPlayerInfo.transform.Find("PanelVolumnSetting").gameObject;
        ButtonCertain = PanelVolumnSetting.transform.Find("ButtonCertain").GetComponent<Button>();
        ButtonCertain.onClick.AddListener(VolumnSettingCertainBtnOnClick);
        ButtonCancel = PanelVolumnSetting.transform.Find("ButtonCancel").GetComponent<Button>();
        ButtonCancel.onClick.AddListener(VolumnSettingCancelBtnOnClick);
        SliderMusic = PanelVolumnSetting.transform.Find("SliderMusic").GetComponent<Slider>();
        SliderMusic.onValueChanged.AddListener(delegate (float volumn) { SliderMusicOnChange(); });
        SliderMusicImage = SliderMusic.gameObject.transform.Find("Handle Slide Area/Handle/Image").GetComponent<Image>();
        PreSliderMusicVolumn = SliderMusic.value;
        SliderSoundEffect = PanelVolumnSetting.transform.Find("SliderSoundEffect").GetComponent<Slider>();
        SliderSoundEffect.onValueChanged.AddListener(delegate (float volumn) { SliderSoundEffectOnChange(); });
        SliderSoundEffectImage = SliderSoundEffect.gameObject.transform.Find("Handle Slide Area/Handle/Image").GetComponent<Image>();
        PreSliderSoundEffectVolumn = SliderSoundEffect.value;

        PanelGuide = transform.Find("PanelGuide").gameObject;
        GuideFinger = PanelGuide.transform.Find("ImageFinger").gameObject;
        GuideText = PanelGuide.transform.Find("Text").gameObject.GetComponent<Text>();

        PanelProbability = transform.Find("PanelProbability").gameObject;
        ButtonExit = PanelProbability.transform.Find("PanelProbabilityDetail/ButtonExit").GetComponent<Button>();
        ButtonExit.onClick.AddListener(ExitProbaBtnOnClick);

        //播放BGM
        if (facade.BGMName() != "select")
        {
            facade.PlayBGM("select");
        }

    }

    // Use this for initialization
    void Start()
    {
        HideOtherLeftPanelImage();
        UpdateHeroCard();

        CardButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if(GuideIndex.IsUpstarDisplayButton == true)
        {
            UpstarBtn.transform.Find("Exclamation").gameObject.SetActive(true);
        }
        if(GuideIndex.IsUpstarButton == true)
        {
            UpstarPanel.transform.Find("UpButton/Exclamation").gameObject.SetActive(true);
        }
        GuideFunc();
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpdateLevelIndex)
        {
            UpdateLevelIndex();
        }
        else
        {
            if (Mathf.Abs(Content.transform.GetComponent<RectTransform>().anchoredPosition.x - levelIndexDestination.x) < 1f)
            {
                isUpdateLevelIndex = true;
            }
            Content.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(Content.transform.GetComponent<RectTransform>().anchoredPosition, levelIndexDestination, Time.deltaTime * 18);
        }

        if(isGetCardOnce)
        {
            facade.PlayUIAudio("open");
            ButtonJump.gameObject.SetActive(true);
            GameObject animation = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Animation"));
            animation.transform.SetParent(Animation.transform);
            animation.transform.localScale = new Vector3(1, 1, 1);
            animation.transform.localPosition = new Vector3(0, 0, 0);
            TextDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
            TextGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
            PanelGetCardEffect.SetActive(true);
            GetCardFunc(data, true);
            StartCoroutine(WaitForGetCardOnce());
            isGetCardOnce = false;
        }
        if(isGetCardTenTimes)
        {
            facade.PlayUIAudio("open");
            ButtonJump.gameObject.SetActive(true);
            GameObject animation = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Animation"));
            animation.transform.SetParent(Animation.transform);
            animation.transform.localScale = new Vector3(1, 1, 1);
            animation.transform.localPosition = new Vector3(0, 0, 0);
            TextDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
            TextGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
            PanelGetCardEffect.SetActive(true);
            GetCardFunc(data, false);
            StartCoroutine(WaitForGetCardTenTimes());
            isGetCardTenTimes = false;
        }
        if(isGetCardFailed)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GetHeroCard/Tips"));
            go.transform.SetParent(PanelGetCard.transform.Find("PanelRight/PanelButton").transform);
            go.GetComponent<Text>().text = "钻石不足！";
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            isGetCardFailed = false;
            ButtonOnce.interactable = true;
            ButtonTenTimes.interactable = true;
        }
    }

    private void FixedUpdate()
    {
        if (isUpgrade)
        {
            if (rate_Exp < target_rate_Exp)
            {
                rate_Exp += 1 * Time.deltaTime;
                if (rate_Exp >= 1)
                {
                    rate_Exp = 1;
                }
                SetUpgradeRate(rate_Exp);
            }
            else
            {
                if (rate_Exp == 1)
                {
                    rate_Exp = 0;
                }
                SetUpgradeRate(rate_Exp);
                HeroCard c = userCardList[HeroIndex];
                UpgradePanel.transform.Find("Level/Text").GetComponent<Text>().text = c.Hero_level.ToString();
                isUpgrade = false;
                facade.SaveUserInfo();
            }
        }
    }
}
