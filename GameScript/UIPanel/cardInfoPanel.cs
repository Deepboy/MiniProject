using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Model;
using GuideCtrl;

public class cardInfoPanel : BasePanel
{
    private GameObject showHero;

    private Dictionary<int, HeroCard> cardInfoDict = new Dictionary<int, HeroCard>();//所有卡牌信息
    private Dictionary<int, Goods> goodsInfoDict = new Dictionary<int, Goods>();//所有物品信息

    private List<HeroCard> userCardList = new List<HeroCard>();//玩家持有卡牌信息
    private Dictionary<int, Goods> userGoodsDict = new Dictionary<int, Goods>();//玩家持有物品信息

    private List<GameObject> userCardGOList = new List<GameObject>();
    private List<GameObject> userGoodsGOList = new List<GameObject>();

    //cardPanel中含有的面板
    private Transform CardPanel;//角色卡、物品、天赋树面板的父物体
    private Transform CardLeftPanel;//cardPanel的左边面板
    private Transform CardCenterPanel;//cardPanel中间的面板（角色卡、物品、天赋树）
    private Transform CardTopPanel;//cardPanel上方面板
    private Transform Card_card;//玩家角色卡面板
    private Transform Card_goods;//玩家物品面板
    private Transform Card_lineup;//天赋树面板

    //HeroInfoPanel中含有的面板
    private Transform HeroInfoPanel;//角色详细信息面板
    private Transform Info_LeftPanel;//角色信息左面板（立绘）
    private Transform Info_RightPanel;//角色信息右面板（信息）
    private Transform Info_role;//角色信息
    private Transform Info_attribute;//角色属性
    private Transform Info_skill;//角色技能
    private Transform Info_Role_UpgradePanel;
    private Transform Info_Role_UpstarPanel;

    //绘卷按钮
    private GameObject ScrollerButton;
    //绘卷面板
    private GameObject ScrollerPanel;
    //引导面板
    private GameObject GuidePanel;


    //人物信息面板
    private GameObject playerInfoPanel;
    //等级文字
    private Text LevelText;
    //人物头像
    private Image playerIcon;
    //详情信息面板等级
    private Text playerInfoLevelText;
    //详情信息经验条
    private Slider playerExpSlider;
    //玩家名字
    private InputField playerName;
    //音量设置按钮
    private Button setAudioVolumnButton;
    //返回大厅按钮
    private Button cancelButton;
    //退出游戏按钮
    private Button exitButton;

    private int now_Exp;
    private int need_Exp;
    private float rate_Exp;
    private float target_rate_Exp;
    private bool isUpgrade = false;

    private GameObject textGold;
    private GameObject textDiamond;
    private Button GongMingBtn;
    private int index = 0;
    void Awake()
    {
        HideOtherLeftPanelImage();

        textGold = GameObject.Find("Canvas/cardInfoPanel/CardPanel/TopPanel/TextGold");
        textDiamond = GameObject.Find("Canvas/cardInfoPanel/CardPanel/TopPanel/TextDiamond");


        CardPanel = transform.Find("CardPanel").transform;
        CardLeftPanel = CardPanel.Find("LeftPanel").transform;
        CardCenterPanel = CardPanel.Find("CenterPanel").transform;
        CardTopPanel = CardPanel.Find("TopPanel").transform;
        Card_card = CardCenterPanel.Find("card").transform;
        Card_goods = CardCenterPanel.Find("goods").transform;
        Card_lineup = CardCenterPanel.Find("lineup").transform;

        CardLeftPanel.Find("roleButton").GetComponent<Button>().onClick.AddListener(OnRoleButtonClick);
        CardLeftPanel.Find("goodsButton").GetComponent<Button>().onClick.AddListener(OnGoodsButtonClick);
        CardLeftPanel.Find("lineupButton").GetComponent<Button>().onClick.AddListener(OnGiftButtonClick);
        CardPanel.Find("ExitButton").transform.GetComponent<Button>().onClick.AddListener(OnExitScenceButtonClick);

        HeroInfoPanel = transform.Find("HeroInfoPanel").transform;
        Info_LeftPanel = HeroInfoPanel.Find("LeftPanel").transform;
        Info_RightPanel = HeroInfoPanel.Find("RightPanel").transform;
        Info_role = Info_RightPanel.Find("role").transform;
        Info_attribute = Info_RightPanel.Find("attribute").transform;
        Info_skill = Info_RightPanel.Find("skill").transform;
        Info_Role_UpgradePanel = Info_role.Find("ButtonPanel/UpgradePanel").transform;
        Info_Role_UpstarPanel = Info_role.Find("ButtonPanel/UpstarPanel").transform;

        Info_LeftPanel.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        Info_LeftPanel.Find("LeftButton").GetComponent<Button>().onClick.AddListener(OnLeftInfoButtonClick);
        Info_LeftPanel.Find("RightButton").GetComponent<Button>().onClick.AddListener(OnRightInfoButtonClick);
        Info_RightPanel.Find("TopPanel/role").GetComponent<Button>().onClick.AddListener(OnRoleInfoButtonClick);
        Info_RightPanel.Find("TopPanel/skill").GetComponent<Button>().onClick.AddListener(OnSkillInfoButtonClick);
        Info_RightPanel.Find("TopPanel/attribute").GetComponent<Button>().onClick.AddListener(OnAttributeInfoButtonClick);

        Info_role.Find("ButtonPanel/UpgradeBtn").GetComponent<Button>().onClick.AddListener(OnShowUpgradeBtnClick);
        Info_role.Find("ButtonPanel/UpstarBtn").GetComponent<Button>().onClick.AddListener(OnShowUpstarBtnClick);
        Info_role.Find("ButtonPanel/UpgradePanel/UpButton").GetComponent<Button>().onClick.AddListener(OnUpgradeClick);

        Info_skill.Find("ButtonPanel/passive_skill1").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_1_Click);
        Info_skill.Find("ButtonPanel/passive_skill2").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_2_Click);
        Info_skill.Find("ButtonPanel/passive_skill3").GetComponent<Button>().onClick.AddListener(OnSkillInfo_passive_skill_3_Click);
        Info_skill.Find("ButtonPanel/active_skill").GetComponent<Button>().onClick.AddListener(OnSkillInfo_active_skill_Click);
        Info_skill.Find("ButtonPanel/staff_skill").GetComponent<Button>().onClick.AddListener(OnSkillInfo_staff_skill_Click);

        ScrollerPanel = transform.Find("PanelScroller").gameObject;
        ScrollerPanel.SetActive(false);
        ScrollerButton = CardPanel.transform.Find("RightPanel").Find("gonghui_button").gameObject;
        ScrollerButton.GetComponent<Button>().onClick.AddListener(ScrollerButtonOnClick);
        ScrollerPanel.transform.Find("RightPanel/xiake_button").GetComponent<Button>().onClick.AddListener(HeroInfoButtonOnClick);
        ScrollerPanel.transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitScenceButtonClick);
        CardPanel.transform.Find("RightPanel").Find("xiake_button").GetComponent<Button>().onClick.AddListener(HeroInfoButtonOnClick);
        GuidePanel = transform.Find("PanelGuide").gameObject;

        GongMingBtn = Info_Role_UpstarPanel.GetChild(0).GetComponent<Button>();
        GongMingBtn.onClick.AddListener(OnGongMingButtonClick);

        LevelText = transform.Find("CardPanel/LeftPanel/player_icon/TextLevel").GetComponent<Text>();

        playerInfoPanel = transform.Find("PanelPlayerInfo").gameObject;
        playerInfoPanel.SetActive(false);
        playerInfoPanel.transform.Find("ButtonReturn").GetComponent<Button>().onClick.AddListener(CancelButtonOnClick);
        playerIcon = playerInfoPanel.transform.Find("PanelDetailedInfo/ImagePlayerIcon").GetComponent<Image>();
        playerInfoLevelText = playerInfoPanel.transform.Find("PanelDetailedInfo/TextLevel").GetComponent<Text>();
        playerExpSlider = playerInfoPanel.transform.Find("PanelDetailedInfo/SliderExp").GetComponent<Slider>();
        playerName = playerInfoPanel.transform.Find("InputField").GetComponent<InputField>();
        playerName.onEndEdit.AddListener((param) => { changedName(param); });
        setAudioVolumnButton = playerInfoPanel.transform.Find("PanelDetailedInfo/ButtonAudio").GetComponent<Button>();
        cancelButton = playerInfoPanel.transform.Find("PanelDetailedInfo/ButtonCancel").GetComponent<Button>();
        cancelButton.onClick.AddListener(CancelButtonOnClick);
        exitButton = playerInfoPanel.transform.Find("PanelDetailedInfo/ButtonExit").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButtonOnClick);

        transform.Find("CardPanel/LeftPanel/player_icon").GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);
        transform.Find("CardPanel/LeftPanel/player_icon/player_iconBorder").GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);

        transform.Find("PanelScroller/PanelScroller/player_icon").GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);
        transform.Find("PanelScroller/PanelScroller/player_icon/player_iconBorder").GetComponent<Button>().onClick.AddListener(PlayerImageOnClick);
    }

    void OnEnable()
    {
        StartGuide();

        LevelText.text = facade.getUserInfo().User_level.ToString();
        playerInfoLevelText.text = facade.getUserInfo().User_level.ToString();
        playerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / (float)(facade.getUserInfo().User_level * 300);
        playerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();
        playerName.text = facade.getUserInfo().Username;

        ScrollerPanel.transform.Find("TopPanel/TextDiamond").GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
        ScrollerPanel.transform.Find("TopPanel/TextGold").GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();
        transform.Find("PanelScroller/PanelScroller/player_icon/TextLevel").GetComponent<Text>().text = facade.getUserInfo().User_level.ToString();

        textDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
        textGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();

        userCardList = facade.getUserCardList();
        userGoodsDict = facade.GetUserGoodsDict();
        foreach (Transform c in Card_card.Find("Scroll View/Viewport/Content").transform)
        {
            Destroy(c.gameObject);
            userCardGOList.Remove(c.gameObject);
        }
        foreach (Transform g in Card_goods.Find("Scroll View/Viewport/Content").transform)
        {
            Destroy(g.gameObject);
            userGoodsGOList.Remove(g.gameObject);
        }
        for (int i = 0; i < userCardList.Count; i++)
        {
            int count = i;
            HeroCard c = userCardList[i];
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HeroList"), Card_card.Find("Scroll View/Viewport/Content").transform);
            Sprite img = Resources.Load<Sprite>(c.Card_path);
            go.transform.Find("Image").GetComponent<Image>().sprite = img;
            //做第一个人物的引导
            if (i == 0)
                GuidePanel.transform.Find("HeroList").GetComponent<Image>().sprite = img;
            go.transform.Find("Text").GetComponent<Text>().text = c.Hero_level.ToString();
            go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            go.transform.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OnHeroListClick(count);
            });
            //做第一个人物的引导
            if (i == 0)
            {
                GuidePanel.transform.Find("HeroList").transform.GetComponent<Button>().onClick.AddListener(delegate ()
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
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/goods"), Card_goods.Find("Scroll View/Viewport/Content").transform);
                Sprite img = Resources.Load<Sprite>(userGoodsDict[key].Goods_path);
                go.transform.Find("Image").GetComponent<Image>().sprite = img;
                go.transform.Find("Image/Text").GetComponent<Text>().text = "x" + userGoodsDict[key].Goods_has.ToString();
                userGoodsGOList.Add(go);
            }
        }

    }

    void Start()
    {
        CardLeftPanel.Find("roleButton").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Update()
    {

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
                HeroCard c = userCardList[index];
                Info_role.Find("ButtonPanel/UpgradePanel/Level/Text").GetComponent<Text>().text = c.Hero_level.ToString();
                facade.SaveUserInfo();
                isUpgrade = false;
            }
        }
    }

    //激活玩家所持有的卡牌面板
    private void OnRoleButtonClick()
    {
        HideOtherLeftPanelImage();
        CardLeftPanel.Find("roleButton").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Card_card.gameObject.SetActive(true);
        Card_goods.gameObject.SetActive(false);
        Card_lineup.gameObject.SetActive(false);
    }
    //激活玩家所持有的物品面板
    private void OnGoodsButtonClick()
    {
        HideOtherLeftPanelImage();
        CardLeftPanel.Find("goodsButton").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Card_card.gameObject.SetActive(false);
        Card_goods.gameObject.SetActive(true);
        Card_lineup.gameObject.SetActive(false);
    }
    //激活天赋面板
    private void OnGiftButtonClick()
    {
        HideOtherLeftPanelImage();
        CardLeftPanel.Find("lineupButton").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Card_card.gameObject.SetActive(false);
        Card_goods.gameObject.SetActive(false);
        Card_lineup.gameObject.SetActive(true);
    }
    //退出场景按钮
    private void OnExitScenceButtonClick()
    {
        uiMng.PushSenceSync(UISenceType.Main_Scene);
    }
    //激活角色详细信息面板
    private void OnRoleInfoButtonClick()
    {
        Debug.Log("OnRoleInfoButtonClick");
        Info_role.gameObject.SetActive(true);
        Info_skill.gameObject.SetActive(false);
        Info_attribute.gameObject.SetActive(false);
    }
    //激活角色技能信息面板
    private void OnSkillInfoButtonClick()
    {
        Debug.Log("OnSkillInfoButtonClick");
        Info_role.gameObject.SetActive(false);
        Info_skill.gameObject.SetActive(true);
        Info_attribute.gameObject.SetActive(false);
    }
    //激活角色属性面板
    private void OnAttributeInfoButtonClick()
    {
        Debug.Log("OnAttributeInfoButtonClick");
        Info_role.gameObject.SetActive(false);
        Info_skill.gameObject.SetActive(false);
        Info_attribute.gameObject.SetActive(true);
    }
    //角色详细信息面板退出事件
    private void OnExitButtonClick()
    {
        HeroInfoPanel.gameObject.SetActive(false);
        CardPanel.gameObject.SetActive(true);
    }
    //向左切换角色
    private void OnLeftInfoButtonClick()
    {

        if (index > 0)
        {
            SetHeroInfoShow(index - 1);
            index -= 1;
        }
        else
        {
            SetHeroInfoShow(userCardList.Count - 1);
            index = userCardList.Count - 1;
        }
        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + index));
        GameObject BG = GameObject.Find("Canvas/cardInfoPanel").transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
    }
    //向右切换角色
    private void OnRightInfoButtonClick()
    {
        if (index < userCardList.Count - 1)
        {
            SetHeroInfoShow(index + 1);
            index += 1;
        }
        else
        {
            SetHeroInfoShow(0);
            index = 0;
        }
        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + index));
        GameObject BG = GameObject.Find("Canvas/cardInfoPanel").transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
    }
    //进入角色详细信息面板
    public void OnHeroListClick(int index)
    {
        this.index = index;
        HeroInfoPanel.gameObject.SetActive(true);
        CardPanel.gameObject.SetActive(false);
        SetHeroInfoShow(index);

        Sprite bg = Instantiate(Resources.Load<Sprite>("MainSceneUI/" + index));
        GameObject BG = GameObject.Find("Canvas/cardInfoPanel").transform.Find("HeroInfoPanel/BG").gameObject;
        BG.GetComponent<Image>().sprite = bg;
        if (GuideIndex.isGuide == true && GuideIndex.mainGuideIndex != MainGuideIndex.FinishGuide)
        {
            GuideIndex.mainGuideIndex = MainGuideIndex.LevelUpGuide;
            StartGuide();
        }
    }
    //技能信息面板被动技能1显示事件
    public void OnSkillInfo_passive_skill_1_Click()
    {
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/passive_skill1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_1.Skill_describe;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_1.Skill_name;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }
    //技能信息面板被动技能2显示事件
    public void OnSkillInfo_passive_skill_2_Click()
    {
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/passive_skill2").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_2.Skill_describe;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_2.Skill_name;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }
    //技能信息面板被动技能3显示事件
    public void OnSkillInfo_passive_skill_3_Click()
    {
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/passive_skill3").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_3.Skill_describe;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_3.Skill_name;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[被动技能]";
    }
    //技能信息面板主动技能显示事件
    public void OnSkillInfo_active_skill_Click()
    {
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/active_skill").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Action_skill.Skill_describe;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Action_skill.Skill_name;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[主动技能]";
    }
    //技能信息面板助战技能显示事件
    public void OnSkillInfo_staff_skill_Click()
    {
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/staff_skill").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Staff_skill_self.Skill_describe;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Staff_skill_self.Skill_name;
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Type").GetComponent<Text>().text = "[助战技能]";
    }


    public void HideOtherSkillImage()
    {
        GameObject ButtonPanel = GameObject.Find("Canvas/cardInfoPanel").transform.Find("HeroInfoPanel/RightPanel/skill/ButtonPanel").gameObject;
        for (int i = 0; i < ButtonPanel.transform.childCount; i++)
        {
            ButtonPanel.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

    }
    public void HideOtherLeftPanelImage()
    {
        GameObject LeftPanel = GameObject.Find("Canvas/cardInfoPanel/CardPanel/LeftPanel").gameObject;
        for (int i = 0; i < LeftPanel.transform.childCount - 1; i++)
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
        GameObject.Find("Canvas").transform.Find("cardInfoPanel/HeroInfoPanel/RightPanel/role/ButtonPanel/UpgradePanel/Experience/Now_Exp").GetComponent<Text>().text = now_Exp.ToString();
        need_Exp = c.Hero_needEXP;
        GameObject.Find("Canvas").transform.Find("cardInfoPanel/HeroInfoPanel/RightPanel/role/ButtonPanel/UpgradePanel/Experience/Next_Level_Need").GetComponent<Text>().text = need_Exp.ToString();
        rate_Exp = ((float)now_Exp) / (float)need_Exp;
        Info_LeftPanel.Find("texture").GetComponent<Image>().sprite = Resources.Load<Sprite>(c.Texture_path);
        GameObject go = GameObject.Instantiate(Resources.Load(c.Model_path) as GameObject, Info_role.Find("MiddlePanel/Fox_Image").transform.position, Info_role.Find("MiddlePanel/Fox_Image").transform.localRotation, Info_role.Find("MiddlePanel/Fox_Image").transform);
        Info_role.Find("MiddlePanel/Fox_Image").transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        showHero = go;
        Info_role.Find("MiddlePanel/CardName/Text").GetComponent<Text>().text = c.Hero_name;
        //Info_role.Find("MiddlePanel/Star/Text").GetComponent<Text>().text = c.Hero_starLevel.ToString();
        Info_skill.Find("MiddlePanel/skillInfo/Skill_Name").GetComponent<Text>().text = c.Prossive_skill_1.Skill_name;
        ScrollerPanel.transform.Find("TopPanel/TextDiamond").GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
        ScrollerPanel.transform.Find("TopPanel/TextGold").GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();

        textDiamond.GetComponent<Text>().text = facade.GetUserGoodsDict()[9002].Goods_has.ToString();
        textGold.GetComponent<Text>().text = facade.GetUserGoodsDict()[9001].Goods_has.ToString();

        playerExpSlider.value = (float)facade.getUserInfo().User_ReserveExp / (float)(facade.getUserInfo().User_level * 300);
        playerExpSlider.transform.Find("Text").GetComponent<Text>().text = facade.getUserInfo().User_ReserveExp.ToString() + "/" + (facade.getUserInfo().User_level * 300).ToString();

        switch (c.Hero_starLevel)
        {
            case 4:
                Info_Role_UpstarPanel.GetChild(1).GetChild(0).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(2).GetChild(0).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(2).GetChild(1).gameObject.SetActive(true);
                Info_role.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star4");
                Info_Role_UpstarPanel.GetChild(1).GetChild(1).GetComponent<Text>().text = "40";
                Info_Role_UpstarPanel.GetChild(2).GetChild(0).GetComponent<Text>().text = "2000";
                break;
            case 5:
                Info_Role_UpstarPanel.GetChild(1).GetChild(0).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(2).GetChild(0).gameObject.SetActive(true);
                Info_Role_UpstarPanel.GetChild(2).GetChild(1).gameObject.SetActive(true);
                Info_role.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star5");
                Info_Role_UpstarPanel.GetChild(1).GetChild(1).GetComponent<Text>().text = "50";
                Info_Role_UpstarPanel.GetChild(2).GetChild(0).GetComponent<Text>().text = "2500";
                break;
            case 6:
                Info_role.Find("MiddlePanel/Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/star6");
                Info_Role_UpstarPanel.GetChild(1).GetChild(0).gameObject.SetActive(false);
                Info_Role_UpstarPanel.GetChild(1).GetChild(1).gameObject.SetActive(false);
                Info_Role_UpstarPanel.GetChild(2).GetChild(0).gameObject.SetActive(false);
                Info_Role_UpstarPanel.GetChild(2).GetChild(1).gameObject.SetActive(false);
                
                break;
        }
        Info_role.Find("MiddlePanel/Attribute/Text").GetComponent<Text>().text = "火";
        Info_role.Find("ButtonPanel/UpgradePanel/Level/Text").GetComponent<Text>().text = c.Hero_level.ToString();
        SetRadarDrawInfo(c);
        SetSkillInfoShow(index);
        SetUpgradeRate(rate_Exp);
        HideOtherSkillImage();
        Info_skill.Find("ButtonPanel/passive_skill1").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    //设置角色技能详细信息界面
    private void SetSkillInfoShow(int index)
    {
        HeroCard c = userCardList[index];
        Info_skill.Find("MiddlePanel/skillInfo/Text").GetComponent<Text>().text = c.Prossive_skill_1.Skill_describe;
    }

    private void OnShowUpgradeBtnClick()
    {
        Info_Role_UpgradePanel.gameObject.SetActive(true);
        Info_Role_UpstarPanel.gameObject.SetActive(false);
    }

    private void OnShowUpstarBtnClick()
    {
        Info_Role_UpgradePanel.gameObject.SetActive(false);
        Info_Role_UpstarPanel.gameObject.SetActive(true);
    }

    private void OnUpgradeClick()
    {
        HeroCard c = userCardList[index];
        UserInfo ui = facade.getUserInfo();
        if (ui.User_ReserveExp < c.Hero_needEXP * Mathf.Pow(c.Hero_level, 2))
        {
            GameObject Tips1 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
            Tips1.transform.SetParent(Info_Role_UpgradePanel);
            Tips1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips1.GetComponent<Text>().text = "储备经验不足！";
            return;
        }
        if (c.Hero_level >= ui.Level_limit)
        {
            GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
            Tips2.transform.SetParent(Info_Role_UpgradePanel);
            Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Tips2.GetComponent<Text>().text = "已达等级上限！";
            return;
        }
        GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
        Tips3.transform.SetParent(Info_Role_UpgradePanel);
        Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Tips3.GetComponent<Text>().text = "Level Up！";
        ui.User_ReserveExp -= c.Hero_needEXP * (int)Mathf.Pow(c.Hero_level, 2);
        target_rate_Exp = 1;
        GameObject go = userCardGOList[index];
        c.Hero_level += 1;
        go.transform.Find("Text").GetComponent<Text>().text = c.Hero_level.ToString();

        c.Fire = (int)(c.OrgFire + c.Hero_level * c.Fire_growth * 2.3f);
        c.Water = (int)(c.OrgWater + c.Hero_level * c.Water_growth * 2.3f);
        c.Soil = (int)(c.OrgSoil + c.Hero_level * c.Soil_growth * 2.3f);
        c.Wind = (int)(c.OrgWind + c.Hero_level * c.Wind_growth * 2.3f);
        c.Thunder = (int)(c.OrgThunder + c.Hero_level * c.Thunder_growth * 2.3f);
        c.Dark = (int)(c.OrgDark + c.Hero_level * c.Dark_growth * 2.3f);

        c.Hero_hp = (int)(c.OrgHp + c.Hero_level * c.Hero_hp_growth * 2.3f);
        c.Hero_speed = (int)(c.OrgSpeed + c.Hero_level * c.Hero_speed_growth * 2.3f);

        SetRadarDrawInfo(c);
        SetHeroInfoShow(index);
        isUpgrade = true;

    }

    private void SetUpgradeRate(float rate)
    {
        Info_role.Find("ButtonPanel/UpgradePanel/Experience/rate_left").transform.GetComponent<Image>().fillAmount = rate;
        Info_role.Find("ButtonPanel/UpgradePanel/Experience/rate_right").transform.GetComponent<Image>().fillAmount = rate;
    }

    private void SetRadarDrawInfo(HeroCard c)
    {
        RadarDraw rd = Info_attribute.Find("MiddlePanel/attribute/RadarSprite").transform.GetComponent<RadarDraw>();
        rd.fire = c.Fire;
        rd.water = c.Water;
        rd.earth = c.Soil;
        rd.wind = c.Wind;
        rd.thunder = c.Thunder;
        rd.dark = c.Dark;

        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/fire").transform.GetComponent<Text>().text = "(" + c.Fire + ")火";
        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/water").transform.GetComponent<Text>().text = "(" + c.Water + ")水";
        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/earth").transform.GetComponent<Text>().text = "(" + c.Soil + ")土";
        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/wind").transform.GetComponent<Text>().text = "风(" + c.Wind + ")";
        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/thunder").transform.GetComponent<Text>().text = "雷(" + c.Thunder + ")";
        Info_attribute.Find("MiddlePanel/attribute/AttributeInfo/dark").transform.GetComponent<Text>().text = "暗(" + c.Dark + ")";

        Info_attribute.Find("MiddlePanel/otherAttribute/Hp/Text").transform.GetComponent<Text>().text = c.Hero_hp.ToString();
        Info_attribute.Find("MiddlePanel/otherAttribute/Speed/Text").transform.GetComponent<Text>().text = c.Hero_speed.ToString();
    }

    #region 点击事件

    /// <summary>
    /// 绘卷按钮点击
    /// </summary>
    private void ScrollerButtonOnClick()
    {
        ScrollerPanel.SetActive(true);
    }

    /// <summary>
    /// 人物按钮点击
    /// </summary>
    private void HeroInfoButtonOnClick()
    {
        ScrollerPanel.SetActive(false);
    }

    /// <summary>
    /// 人物头像点击
    /// </summary>
    private void PlayerImageOnClick()
    {
        playerName.text = facade.getUserInfo().Username;
        playerInfoPanel.SetActive(true);
        CardPanel.transform.Find("ExitButton").gameObject.SetActive(false);
        ScrollerPanel.transform.Find("ExitButton").gameObject.SetActive(false);
    }

    /// <summary>
    /// 返回大厅按钮点击
    /// </summary>
    private void CancelButtonOnClick()
    {
        playerInfoPanel.SetActive(false);
        CardPanel.transform.Find("ExitButton").gameObject.SetActive(true);
        ScrollerPanel.transform.Find("ExitButton").gameObject.SetActive(true);
    }

    /// <summary>
    /// 退出游戏按钮点击
    /// </summary>
    private void ExitButtonOnClick()
    {
        Application.Quit();
    }

    #endregion

    /// <summary>
    /// 玩家改名
    /// </summary>
    private void changedName(string name)
    {
        facade.getUserInfo().Username = name;
        facade.SaveUserInfo();
        playerName.text = name;
    }

    #region 新手指导

    /// <summary>
    /// 开启新手引导
    /// </summary>
    private void StartGuide()
    {
        if (GuideIndex.isGuide == true && GuideIndex.mainGuideIndex != MainGuideIndex.FinishGuide)
        {
            GuidePanel.SetActive(true);
            switch (GuideIndex.mainGuideIndex)
            {
                case MainGuideIndex.HeroCardGuide:
                    CardPanel.transform.Find("ExitButton").gameObject.SetActive(false);
                    GuidePanel.transform.Find("HeroList").gameObject.SetActive(true);
                    GuidePanel.transform.Find("finger").GetComponent<Animator>().SetTrigger("HeroCardGuide");
                    break;
                case MainGuideIndex.LevelUpGuide:
                    GuidePanel.transform.Find("finger").GetComponent<Animator>().SetTrigger("Idle");
                    GuidePanel.transform.Find("finger").GetComponent<Animator>().SetTrigger("LevelUpGuide");
                    GuidePanel.transform.Find("HeroList").gameObject.SetActive(false);
                    GuidePanel.transform.Find("ButtonPanel").gameObject.SetActive(true);
                    GuidePanel.transform.Find("ButtonPanel/UpgradePanel/UpButton").GetComponent<Button>().onClick.AddListener(FinishGuideOnClick);
                    GuidePanel.transform.Find("ainuomiya").gameObject.SetActive(true);
                    GuidePanel.transform.Find("Text").gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            CardPanel.transform.Find("ExitButton").gameObject.SetActive(true);
            GuidePanel.SetActive(false);
        }
    }

    /// <summary>
    /// 完成新手引导
    /// </summary>
    private void FinishGuideOnClick()
    {
        GuideIndex.mainGuideIndex = MainGuideIndex.FinishGuide;
        StartGuide();
    }
    /// <summary>
    /// 共鸣按钮
    /// </summary>
    public void OnGongMingButtonClick()
    {
        Debug.Log("GMBTN");
        HeroCard c = userCardList[index];
        UserInfo ui = facade.getUserInfo();
        switch (c.Hero_starLevel)
        {
            case 4:
                if (facade.GetUserGoodsDict()[9002].Goods_has < 2000 || facade.GetUserGoodsDict()[9001].Goods_has < 40)
                {
                    GameObject Tips1 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips1.transform.SetParent(Info_Role_UpstarPanel);
                    Tips1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips1.GetComponent<Text>().text = "钻石或金币不足";
                    return;
                }
                else
                {
                    GameObject Tips2 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips2.transform.SetParent(Info_Role_UpstarPanel);
                    Tips2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips2.GetComponent<Text>().text = "Star Up！";
                    
                    c.Hero_id += 1;
                    facade.GetUserGoodsDict()[9002].Goods_has -= 2000;
                    facade.GetUserGoodsDict()[9001].Goods_has -= 40;

                    facade.SaveUserInfo();
                }
                    break;
            case 5:
                if (facade.GetUserGoodsDict()[9002].Goods_has <2500 || facade.GetUserGoodsDict()[9001].Goods_has < 50)
                {
                    GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips3.transform.SetParent(Info_Role_UpstarPanel);
                    Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips3.GetComponent<Text>().text = "钻石或金币不足";
                    return;
                }
                else
                {
                    GameObject Tips3 = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                    Tips3.transform.SetParent(Info_Role_UpstarPanel);
                    Tips3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Tips3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    Tips3.GetComponent<Text>().text = "Star Up！";
                    c.Hero_id += 1;
                    facade.GetUserGoodsDict()[9002].Goods_has -= 2500;
                    facade.GetUserGoodsDict()[9001].Goods_has -= 50;
                    facade.SaveUserInfo();
                }
                break;
            case 6:
                GameObject Tips = Instantiate(Resources.Load<GameObject>("Prefab/UpStarTips_Text"));
                Tips.transform.SetParent(Info_Role_UpstarPanel);
                Tips.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Tips.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                Tips.GetComponent<Text>().text = "已达到最大星级！";
                break;
        }
        
        SetHeroInfoShow(index);
    }
    #endregion

}
