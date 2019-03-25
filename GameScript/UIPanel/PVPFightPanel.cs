using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using System;
using SpecialBuffManager;
using UnityEngine.SceneManagement;

public class PVPFightPanel : BasePanel {
    private Dictionary<BuffCode, Buff> buff_info_dict = new Dictionary<BuffCode, Buff>();
    private Dictionary<string, HeroCard> hero_info_dict = new Dictionary<string, HeroCard>();
    private Dictionary<string, SpecialBuff> card_info_dict = new Dictionary<string, SpecialBuff>();
    private Dictionary<int, Skill> skill_info_dict = new Dictionary<int, Skill>();

    private ShowTimerRequest showTimeRequest;
    private StartGameRequest startGameRequest;
    private OutCardRequest outCardRequest;
    private GameOverRequest gameOverRequest;
    private EffectOverRequest effectOverRequest;
    private UseSkillRequest useSkillRequest;

    private Transform playerInfo;
    private Slider playerHpSlider;
    private Slider playerActionPointSlider;
    private Transform playerActionScrollView;
    private Transform playerBuffScrollView;

    private Transform enemyInfo;
    private Slider enemyHpSlider;
    private Slider enemyActionPointSlider;
    private Transform enemyActionScrollView;
    private Transform enemyBuffScrollView;

    private Transform fightInfoPanel;
    private Transform cardPreview_Panel;

    private GameObject player_heroGO;
    private string player_name;
    private HeroCard player_hero;
    private HeroCard staff_hero = null;
    private string pvpPlayerInfo;
    private int playerHeroCardID;
    private float playerHp=-1;
    private float playerSpeed = -1;
    private float playerRateOfActionPoint;
    private string playerBuffInfo;
    private Dictionary<BuffCode,GameObject> playerBuffGODict=new Dictionary<BuffCode, GameObject>();
    private string playerCardInfo;
    private List<GameObject> choiceCardGOList = new List<GameObject>();
    private Dictionary<ElementType, int> cardInfoDict = new Dictionary<ElementType, int>();
    private Dictionary<ElementType, int> OutCardInfoDict = new Dictionary<ElementType, int>();
    private Animator playerAnimator;
    private float playerOrgHp;
    private float playerOrgSpeed;
    private string playerSkillInfo;
    private Transform ActionSkillTrigger;
    private Transform StaffSkillTrigger=null;
    private float playerShield = 0;
    //玩家黑洞
    private GameObject playerBlackHole;


    private GameObject enemy_heroGO;
    private string enemy_name;
    private HeroCard enemy_hero;
    private int enemyHeroCardID;
    private float enemyHp=-1;
    private float enemySpeed = -1;
    private float enemyRateOfActionPoint;
    private string enemyBuffInfo;
    private Dictionary<BuffCode, GameObject> enemyBuffGODict = new Dictionary<BuffCode, GameObject>();
    private Animator enemyAnimator;
    private float enemyOrgHp;
    private float enemyOrgSpeed;
    private float enemyShield = 0;
    //敌人黑洞
    private GameObject enemyBlackHole;


    private Sprite ActionPointIcon_Finish;
    private Sprite ActionPointIcon_Origin;
    private bool isAccept = false;
    private bool isSynchroCardInfo = true;

    private Transform PanelLost;
    private Transform PanelWin;
    private bool isOver = false;
    public bool isEffectOver = false;
    public string EffectInfo = "";
    private ReturnCode resultCode;


    private bool isChangePlayerHp = false;
    private bool isChangePlayerSpeed = false;
    private float player_old_Hp;
    private float player_new_Hp;

    private bool isChangeEnemyHp = false;
    private bool isChangeEnemySpeed = false;
    private float enemy_old_Hp;
    private float enemy_new_Hp;

    private string playerEffectKey;
    private string playerSkillKey;
    private bool isPlayerEffect;
    private bool isPlayerSkillEffect;
    private string enemyEffectKey;
    private string enemySkillKey;
    private bool isEnemyEffect;
    private bool isEnemySkillEffect;

    private string outCardData= "000000";
    private bool useCardPreview = true;


    private string timeInfo="";
    private bool isShowTimer = false;
    private Transform maskPanel;
    private Transform maskLeftPanel;
    private Transform maskRightPanel;
    private Transform showTimePanel;
    private Transform vsPanel;

    private float smoothSpeed = 5;
    private bool timer_animation = false;
    private bool isWhiteMoveEffect = true;
    private bool isDrawCardEffect = true;

    private bool isPlayerBlackHoleOver = true;
    private bool isEnemyBlackHoleOver = true;
    void Start () {

        facade.PlayBGM("pk");
        //初始化各种请求
        ActionPointIcon_Finish = Resources.Load<Sprite>("Image/Icon/actionFinish");
        ActionPointIcon_Origin = Resources.Load<Sprite>("Image/Icon/actionOrigin");
        showTimeRequest = transform.GetComponent<ShowTimerRequest>();
        startGameRequest = transform.GetComponent<StartGameRequest>();
        outCardRequest = transform.GetComponent<OutCardRequest>();
        gameOverRequest = transform.GetComponent<GameOverRequest>();
        effectOverRequest = transform.GetComponent<EffectOverRequest>();
        useSkillRequest = transform.GetComponent<UseSkillRequest>();
        showTimeRequest.pvpFightPanel = this;
        startGameRequest.pvpFightPanel = this;
        outCardRequest.pvpFightPanel = this;
        gameOverRequest.pvpFightPanel = this;
        useSkillRequest.pvpFightPanel = this;
        //初始化信息表
        pvpPlayerInfo = facade.GetPVPPlayerInfo();
        buff_info_dict = facade.GetBuffInfoDict();
        card_info_dict = facade.GetCardEffectDict();
        skill_info_dict = facade.GetAllSkillInfoDict();
        //初始化出牌信息字典
        OutCardInfoDict.Add(ElementType.Fire, 0);
        OutCardInfoDict.Add(ElementType.Water, 0);
        OutCardInfoDict.Add(ElementType.Earth, 0);
        OutCardInfoDict.Add(ElementType.Wind, 0);
        OutCardInfoDict.Add(ElementType.Thunder, 0);
        OutCardInfoDict.Add(ElementType.Dark, 0);
        SetGameInfo(facade.GetPVPInfo());
        //SynchroPlayerCard(playerCardInfo);
        //初始化显示卡牌列表
        cardInfoDict.Add(ElementType.Fire, 0);
        cardInfoDict.Add(ElementType.Water, 0);
        cardInfoDict.Add(ElementType.Earth, 0);
        cardInfoDict.Add(ElementType.Wind, 0);
        cardInfoDict.Add(ElementType.Thunder, 0);
        cardInfoDict.Add(ElementType.Dark, 0);

        //创建人物模型,设置技能
        string[] strs = pvpPlayerInfo.Split(',');
        player_hero = facade.getCardInfoDict()[playerHeroCardID];
        if (strs[2] != "00000")
        {
            staff_hero = facade.getCardInfoDict()[int.Parse(strs[2])];
            StaffSkillTrigger = staff_hero.Staff_skill_self.isActionSkill ?
                GameObject.Instantiate(Resources.Load<GameObject>("Prefab/AssistSkill"), transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").transform).transform :
                GameObject.Instantiate(Resources.Load<GameObject>("Prefab/AssistPassiveSkill"), transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").transform).transform;
            StaffSkillTrigger.transform.localPosition = new Vector3(0, 0, 0);
            transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").GetComponent<Image>().sprite = Resources.Load<Sprite>(staff_hero.SkillIcon_path);
            player_hero.Staff_skill_other = staff_hero.Staff_skill_self;
        }
        else {
            transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").gameObject.SetActive(false);
        }
        ActionSkillTrigger = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/ActionSkill"), transform.Find("PanelFight/PanelPlayerSkill/ActiveSkill").transform).transform;
        transform.Find("PanelFight/PanelPlayerSkill/ActiveSkill").GetComponent<Image>().sprite = Resources.Load<Sprite>(player_hero.SkillIcon_path);

        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillFirst").GetComponent<SkillCtrl>().skill = player_hero.Prossive_skill_1;
        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillSecond").GetComponent<SkillCtrl>().skill = player_hero.Prossive_skill_2;
        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillThird").GetComponent<SkillCtrl>().skill = player_hero.Prossive_skill_3;
        transform.Find("PanelFight/PanelPlayerSkill/ActiveSkill").GetComponent<SkillCtrl>().skill = player_hero.Action_skill;
        transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").GetComponent<SkillCtrl>().skill = player_hero.Staff_skill_other;
        transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").GetComponent<AssistSkillCtrl>().skill = player_hero.Staff_skill_other;

        enemy_hero = facade.getCardInfoDict()[enemyHeroCardID];
        player_heroGO = GameObject.Instantiate(Resources.Load<GameObject>(player_hero.FightModel_path), transform.Find("PanelFight/PanelFightInfo/player").transform);
        player_heroGO.GetComponent<AnimationCtrl>().isPVP = true;
        enemy_heroGO = GameObject.Instantiate(Resources.Load<GameObject>(enemy_hero.FightModel_path), transform.Find("PanelFight/PanelFightInfo/enemy").transform);
        enemy_heroGO.GetComponent<AnimationCtrl>().isPVP = true;
        enemy_heroGO.tag = "Enemy";


        //获取玩家信息面板
        playerInfo = transform.Find("PanelFight/PanelPlayerInfo").transform;
        //获取血量UI
        playerHpSlider = playerInfo.transform.Find("SliderPlayerHp").GetComponent<Slider>();
        //获取行动点UI
        playerActionPointSlider = playerInfo.transform.Find("SliderPlayer").GetComponent<Slider>();
        playerActionScrollView = playerInfo.transform.Find("ScrollViewPlayer/Viewport/Content").transform;
        //获取BUFF列表UI
        playerBuffScrollView = playerInfo.Find("ScrollViewPlayerBuff/Viewport/Content").transform;


        //获取敌人信息面板
        enemyInfo = transform.Find("PanelFight/PanelEnemyInfo").transform;
        //获取血量UI
        enemyHpSlider = enemyInfo.transform.Find("SliderEnemyHp").GetComponent<Slider>();
        //获取行动点UI
        enemyActionPointSlider = enemyInfo.transform.Find("SliderEnemy").GetComponent<Slider>();
        enemyActionScrollView = enemyInfo.transform.Find("ScrollViewEnemy/Viewport/Content").transform;
        //获取BUFF列表UI
        enemyBuffScrollView = enemyInfo.Find("ScrollViewEnemyBuff/Viewport/Content").transform;

        maskPanel = transform.Find("MaskPanel").transform;
        maskLeftPanel = maskPanel.transform.Find("LeftMask").transform;
        maskRightPanel = maskPanel.transform.Find("RightMask").transform;
        showTimePanel = maskPanel.transform.Find("showtime").transform;
        vsPanel = maskPanel.transform.Find("VS").transform;



        //播放卡牌动画
        transform.Find("white_image").transform.GetComponent<Animation>().Play();
        transform.Find("DrawCard").transform.GetComponent<Animation>().Play();
        transform.Find("PanelFight/PanelCardInfo/ScrollViewCard").transform.GetComponent<Animation>().Play();

        //出牌按钮事件
        transform.Find("DrawCard").GetComponent<Button>().onClick.AddListener(OnOutCardClick);
        transform.Find("white_image").GetComponent<Button>().onClick.AddListener(OnDownCardClick);


        //获取战斗结算面板
        PanelLost = transform.Find("PanelLost").transform;
        PanelWin = transform.Find("PanelWin").transform;
        //战斗结算跳转事件
        PanelLost.Find("Button (1)").GetComponent<Button>().onClick.AddListener(OnGameOverClick);
        PanelWin.Find("Button").GetComponent<Button>().onClick.AddListener(OnGameOverClick);

        //技能使用事件
        transform.Find("PanelFight/PanelPlayerSkill/ActiveSkill").GetComponent<Button>().onClick.AddListener(delegate () { OnSkillBtnClick(player_hero.Active_skill_id.ToString()); });
        transform.Find("PanelFight/PanelPlayerSkill/AssistSkill").GetComponent<Button>().onClick.AddListener(delegate () { OnSkillBtnClick(player_hero.Staff_skill_other.Skill_id.ToString()); });

        fightInfoPanel = transform.Find("PanelFight/PanelFightInfo").transform;
        cardPreview_Panel = transform.Find("CardPreview_Panel").transform;

        playerAnimator = player_heroGO.GetComponent<Animator>();
        enemyAnimator=enemy_heroGO.GetComponent<Animator>();


        transform.Find("Preview_Button").GetComponent<Button>().onClick.AddListener(OnCardPreviewClick);

        if (PlayerPrefs.GetInt("PreviewTopControlBool_Save") == 0)
        {
            useCardPreview = true;
            cardPreview_Panel.gameObject.SetActive(true);
            transform.Find("Preview_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/previewWait");     
        }
        else
        {
            useCardPreview = false;
            transform.Find("Preview_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/preview");
        }

        playerBlackHole = transform.Find("PanelFight/PanelCardEffect/PlayerCard").gameObject;
        enemyBlackHole = transform.Find("PanelFight/PanelCardEffect/EnemyCard").gameObject;

        SetStartInfo();


        if (facade.GetPVPState()) {
            maskPanel.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isAccept) {
            transform.Find("DrawCard").GetComponent<Button>().interactable = true;
            isAccept = false;
        }
        if (isChangePlayerHp && isPlayerBlackHoleOver) {
            ShowHpChange(player_old_Hp,player_new_Hp,player_heroGO,new Vector3(1,1,1));
            isChangePlayerHp = false;
        }
        if (isChangeEnemyHp && isEnemyBlackHoleOver)
        {
            ShowHpChange(enemy_old_Hp, enemy_new_Hp, enemy_heroGO,new Vector3(-1, 1, 1));
            isChangeEnemyHp = false;
        }

        
        if (isOver&&isEffectOver) {
            if (resultCode == ReturnCode.Success) {
                enemyAnimator.SetTrigger("die");
                PanelWin.gameObject.SetActive(true);
            }

            if (resultCode == ReturnCode.Fail) {
                playerAnimator.SetTrigger("die");
                PanelLost.gameObject.SetActive(true);
            }
            isOver = false;
        }

        if (playerInfo.transform.Find("SliderPlayer/Fill Area/Fill").GetComponent<Image>().color != new Color(42f / 255, 184f / 255, 18f / 255) && playerSpeed == playerOrgSpeed) {
            playerInfo.transform.Find("SliderPlayer/Fill Area/Fill").GetComponent<Image>().color = new Color(42f / 255, 184f / 255, 18f / 255);
        }
        if (playerSpeed != playerOrgSpeed && isChangePlayerSpeed) {
            playerInfo.transform.Find("SliderPlayer/Fill Area/Fill").GetComponent<Image>().color= playerSpeed < playerOrgSpeed ? new Color(4f / 255, 158f / 255, 248f / 255) : new Color(32f / 255, 227f / 255, 175f / 255);
            isChangePlayerSpeed = false;
        }

        if (enemyInfo.transform.Find("SliderEnemy/Fill Area/Fill").GetComponent<Image>().color != new Color(42f / 255, 184f / 255, 18f / 255) && enemySpeed==enemyOrgSpeed) {
            enemyInfo.transform.Find("SliderEnemy/Fill Area/Fill").GetComponent<Image>().color = new Color(42f / 255, 184f / 255, 18f / 255);
        }
        if (enemySpeed != enemyOrgSpeed && isChangeEnemySpeed)
        {
            enemyInfo.transform.Find("SliderEnemy/Fill Area/Fill").GetComponent<Image>().color = enemySpeed < enemyOrgSpeed? new Color(4f / 255, 158f / 255, 248f / 255) : new Color(32f / 255, 227f / 255, 175f / 255);
            isChangeEnemySpeed = false;
        }

        if (isPlayerEffect) {
            isPlayerEffect = false;
            facade.PlaySkillAudio(card_info_dict[playerEffectKey.Replace("|", "")].EffectSoundPath, float.Parse(card_info_dict[playerEffectKey.Replace("|", "")].EffectSoundDelay));
            EffectInfo = "player";
            //展示玩家出牌的元素
            playerBlackHole.GetComponent<BlackHoleCenter>().CreateElemBall(playerEffectKey.Replace("|", ""));
            if (card_info_dict.ContainsKey(playerEffectKey.Replace("|",""))) {
                StartCoroutine(WaitForCreateElemBall(true, card_info_dict[playerEffectKey.Replace("|", "")].isLongRange, card_info_dict[playerEffectKey.Replace("|", "")].Effect));
            }
            else {
                int count = int.Parse(playerEffectKey.Split('|')[0])+ int.Parse(playerEffectKey.Split('|')[1]) + int.Parse(playerEffectKey.Split('|')[2]) + int.Parse(playerEffectKey.Split('|')[3]) + int.Parse(playerEffectKey.Split('|')[4]) + int.Parse(playerEffectKey.Split('|')[5]);
                if (count == 4)
                    StartCoroutine(WaitForCreateElemBall(true,"1", "Chaos_ShengZheYiWu"));
                if (count == 5)
                    StartCoroutine(WaitForCreateElemBall(true, "1", "Chaos_ShengJian"));
            }
        }
        if (isEnemyEffect) {
            isEnemyEffect = false;
            facade.PlaySkillAudio(card_info_dict[enemyEffectKey.Replace("|", "")].EffectSoundPath, float.Parse(card_info_dict[enemyEffectKey.Replace("|", "")].EffectSoundDelay));
            EffectInfo = "enemy";
            enemyBlackHole.GetComponent<BlackHoleCenter>().CreateElemBall(enemyEffectKey.Replace("|", ""));
            if (card_info_dict.ContainsKey(enemyEffectKey.Replace("|", "")))
            {
                StartCoroutine(WaitForCreateElemBall(false, card_info_dict[enemyEffectKey.Replace("|", "")].isLongRange, card_info_dict[enemyEffectKey.Replace("|", "")].Effect));
            }
            else
            {
                int count = int.Parse(enemyEffectKey.Split('|')[0]) + int.Parse(enemyEffectKey.Split('|')[1]) + int.Parse(enemyEffectKey.Split('|')[2]) + int.Parse(enemyEffectKey.Split('|')[3]) + int.Parse(enemyEffectKey.Split('|')[4]) + int.Parse(enemyEffectKey.Split('|')[5]);
                if (count == 4)
                    StartCoroutine(WaitForCreateElemBall(false, "1", "Chaos_ShengZheYiWu"));
                if (count == 5)
                    StartCoroutine(WaitForCreateElemBall(false, "1", "Chaos_ShengJian"));
            }

        }
        if (isEffectOver) {
            effectOverRequest.SendRequest(EffectInfo);
            isEffectOver = false;
        }
        if (isPlayerSkillEffect) {
            isPlayerSkillEffect = false;
            EffectInfo = "player";
            if (player_hero.Action_skill.Skill_id==int.Parse(playerSkillKey))
                GenerateEffect("2",player_hero.Action_skill.Skill_effect_path);
        }
        if (isEnemySkillEffect) {
            isEnemySkillEffect = false;
            EffectInfo = "enemy";
            if (enemy_hero.Action_skill.Skill_id == int.Parse(enemySkillKey))
                GenerateEnemyEffect("2", enemy_hero.Action_skill.Skill_effect_path);
        }
        if (outCardData.Replace("|","") == "000000"|| !useCardPreview)
        {
            cardPreview_Panel.gameObject.SetActive(false);
        }
        if (isShowTimer) {
            SyncShowTmer(timeInfo);
            isShowTimer = false;
        }
        if (timer_animation) {
            maskLeftPanel.localPosition = Vector3.Lerp(maskLeftPanel.localPosition, new Vector3(-1900, 0, 0), Time.deltaTime * smoothSpeed);
            maskRightPanel.localPosition = Vector3.Lerp(maskRightPanel.localPosition, new Vector3(1900, 0, 0), Time.deltaTime * smoothSpeed);
            if (maskRightPanel.localPosition.x > 1800 && maskLeftPanel.localPosition.x < -1800) {
                maskPanel.gameObject.SetActive(false);
                timer_animation = false;
            }
        }

        if (!transform.Find("DrawCard").transform.GetComponent<Animation>().IsPlaying("blackMove") && isDrawCardEffect)
        {
            isDrawCardEffect = false;
            transform.Find("DrawCard").transform.GetComponent<Image>().sprite= Resources.Load<Sprite>("Image/Icon/black");

        }

        if (!transform.Find("white_image").transform.GetComponent<Animation>().IsPlaying("whiteMove") && isWhiteMoveEffect)
        {
            isWhiteMoveEffect = false;
            transform.Find("white_image").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/white"); ;
        }
    }

    void FixedUpdate () {
        //同步血量
        SynchroHp();
        if (isSynchroCardInfo)
        {
            SynchroPlayerCard(playerCardInfo);
            isSynchroCardInfo = false;
        }
        //同步行动点
        SynchroActionPoint();
        //同步技能CD
        SynchroSkillInfo();
        //同步护盾值
        player_heroGO.transform.Find("bubble2").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, playerShield);
        enemy_heroGO.transform.Find("bubble2").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, enemyShield);
        //同步BUFF列表
        SynchroBuffList(playerBuffInfo, playerBuffGODict, playerBuffScrollView, new Vector3(1, 1, 1));
        SynchroBuffList(enemyBuffInfo, enemyBuffGODict, enemyBuffScrollView, new Vector3(-1, 1, 1));
    }

    public void SetGameInfo(string data) {
        string[] strs = data.Split('@');
        //设置玩家信息
        string[] playerData = strs[0].Split('|');
        //设置玩家名字
        player_name = playerData[1];
        //设置玩家角色信息
        playerHeroCardID = int.Parse(playerData[2]);
        //设置玩家血量信息
        if ((int)playerHp != int.Parse(playerData[3]) && (int)playerHp != -1)
        {
            player_old_Hp = playerHp;
            player_new_Hp = (float)int.Parse(playerData[3]);
            playerHp = (float)int.Parse(playerData[3]);
            isChangePlayerHp = true;
        }
        else {
            playerHp = (float)int.Parse(playerData[3]);
        }
        //设置当前速度
        if (playerSpeed != float.Parse(playerData[4])) {
            playerSpeed = float.Parse(playerData[4]);
            isChangePlayerSpeed = true;
        }

        //设置玩家行动条
        playerRateOfActionPoint = float.Parse(playerData[5]);
        //设置护盾值
        playerShield = float.Parse(playerData[6]);
        //设置玩家buff信息
        playerBuffInfo = playerData[7];
        //设置技能信息
        playerSkillInfo = playerData[8];

        //设置敌人信息
        string[] enemyData = strs[1].Split('|');
        //设置玩家名字
        enemy_name = enemyData[1];
        //设置敌人角色信息
        enemyHeroCardID = int.Parse(enemyData[2]);
        //设置敌人血量
        if (enemyHp != float.Parse(enemyData[3]) && enemyHp != -1)
        {
            enemy_old_Hp = enemyHp;
            enemy_new_Hp = float.Parse(enemyData[3]);
            enemyHp = float.Parse(enemyData[3]);
            isChangeEnemyHp = true;
        }
        else {
            enemyHp = float.Parse(enemyData[3]);
        }
        //设置当前速度
        if (enemySpeed != float.Parse(enemyData[4]))
        {
            enemySpeed = float.Parse(enemyData[4]);
            isChangeEnemySpeed = true;
        }

        //设置敌人行动条
        enemyRateOfActionPoint = float.Parse(enemyData[5]);

        //设置护盾值
        enemyShield = float.Parse(enemyData[6]);
        //设置敌人buff列表
        enemyBuffInfo = enemyData[7];
        

        //设置玩家初始信息
        if (strs.Length >= 3) {
            playerCardInfo = strs[2];
            playerOrgHp = float.Parse(strs[3].Split('|')[0]);
            enemyOrgHp = float.Parse(strs[3].Split('|')[1]);
            playerOrgSpeed = float.Parse(strs[4].Split('|')[0]);
            enemyOrgSpeed = float.Parse(strs[4].Split('|')[1]);
        }
            
    }



    //同步血量
    private void SynchroHp() {
        playerHpSlider.value = playerHp / playerOrgHp;
        playerHpSlider.transform.Find("PlayerHP_Text").GetComponent<Text>().text = ((int)playerHp).ToString();
        enemyHpSlider.value = enemyHp / enemyOrgHp;
        enemyHpSlider.transform.Find("EnemyHP_Text").GetComponent<Text>().text = ((int)enemyHp).ToString();
    }
    //同步行动点
    private void SynchroActionPoint()
    {
        
        playerActionPointSlider.value = playerRateOfActionPoint==5 ? 1 : playerRateOfActionPoint % 1;
        for (int i = 0; i < 5; i++)
            playerActionScrollView.GetChild(i).GetComponent<Image>().sprite = i+1 <= playerRateOfActionPoint?ActionPointIcon_Finish: ActionPointIcon_Origin;

        enemyActionPointSlider.value = enemyRateOfActionPoint==5 ? 1 : enemyRateOfActionPoint %1;
        for (int i = 0; i < 5; i++)
            enemyActionScrollView.GetChild(i).GetComponent<Image>().sprite = i+1 <= enemyRateOfActionPoint ? ActionPointIcon_Finish : ActionPointIcon_Origin;
    }
    //同步buff列表
    private void SynchroBuffList(string buffInfo, Dictionary<BuffCode, GameObject> targetBuffDict, Transform buffScrollView, Vector3 scale)
    {
        List<BuffCode> DictHasBuffList = new List<BuffCode>();
        DictHasBuffList.AddRange(targetBuffDict.Keys);
        if (buffInfo == "{}") {
            if (DictHasBuffList.Count==0) return;
            foreach (BuffCode bc in DictHasBuffList)
            {
                    GameObject go = targetBuffDict[bc];
                    targetBuffDict.Remove(bc);
                    Destroy(go);
            }
            return;
        }
        List<BuffCode> targetHasBuffList = new List<BuffCode>();
        string data = buffInfo.Replace("{", "");
        data = data.Replace("}", "");
        string[] dataList = data.Split(';');
        foreach (string s in dataList) {
            string info = s.Replace("[", "");
            info = info.Replace("]", "");
            BuffCode buffcode = (BuffCode)Enum.Parse(typeof(BuffCode), info.Split(',')[0]);
            int tier = int.Parse(info.Split(',')[1]);
            float time = float.Parse(info.Split(',')[2]);
            targetHasBuffList.Add(buffcode);
            if (targetBuffDict.ContainsKey(buffcode))
            {
                targetBuffDict[buffcode].transform.Find("ImageFill").GetComponent<Image>().fillAmount = 1 - time / float.Parse(buff_info_dict[buffcode].BuffDuration);
                targetBuffDict[buffcode].transform.Find("ImageFill/Text").GetComponent<Text>().text = tier.ToString();
            }
            else {
                GameObject buffGO = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/buff"), buffScrollView);
                //Destroy(buffGO.GetComponent<BuffCtrl>());
                buffGO.GetComponent<BuffCtrl>().isPVP = true;
                buffGO.GetComponent<BuffCtrl>().BuffName = buff_info_dict[buffcode].BuffName;
                buffGO.GetComponent<BuffCtrl>().BuffDescription= buff_info_dict[buffcode].BuffDes;
                buffGO.transform.localScale = scale;
                if(scale.x<0)
                    buffGO.transform.Find("Text").transform.localPosition = new Vector3(buffGO.transform.Find("Text").transform.localPosition.x*-1, buffGO.transform.Find("Text").transform.localPosition.y, buffGO.transform.Find("Text").transform.localPosition.z);
                buffGO.transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/"+buffcode.ToString()+"Buff");
                buffGO.transform.Find("ImageFill").GetComponent<Image>().fillAmount = 1 - time / float.Parse(buff_info_dict[buffcode].BuffDuration);
                buffGO.transform.Find("ImageFill/Text").GetComponent<Text>().text = tier.ToString();
                //buffGO.transform.Find("Text").GetComponent<Text>().text = buff_info_dict[buffcode].BuffDes;
                targetBuffDict.Add(buffcode, buffGO);
            }
        }
        
        foreach (BuffCode bc in DictHasBuffList) {
            if (!targetHasBuffList.Contains(bc)) {
                GameObject go = targetBuffDict[bc];
                targetBuffDict.Remove(bc);
                Destroy(go);
            } 
        }

    }

    private void SynchroSkillInfo() {
        string s = playerSkillInfo.Replace("{", "");
        s = s.Replace("}", "");
        string[] strs = s.Split(';');
        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillFirst/Image").GetComponent<Image>().fillAmount = 1-float.Parse(strs[0]);
        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillSecond/Image").GetComponent<Image>().fillAmount = 1-float.Parse(strs[1]);
        transform.Find("PanelFight/PanelPlayerSkill/PassiveSkillThird/Image").GetComponent<Image>().fillAmount = 1-float.Parse(strs[2]);
        transform.Find("PanelFight/PanelPlayerSkill/ActiveSkill/Image").GetComponent<Image>().fillAmount = 1-float.Parse(strs[3]);
        ActionSkillTrigger.GetComponent<Image>().color = 1 - float.Parse(strs[3]) <= 0 ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
        transform.Find("PanelFight/PanelPlayerSkill/AssistSkill/Image").GetComponent<Image>().fillAmount = 1-float.Parse(strs[4]);
        if (StaffSkillTrigger != null) 
            StaffSkillTrigger.GetComponent<Image>().color = 1 - float.Parse(strs[4]) <= 0 ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
    }

    //同步手牌
    private void SynchroPlayerCard(string data)
    {
        for (int i = choiceCardGOList.Count - 1; i >= 0; i--)
        {
            Destroy(choiceCardGOList[i]);
        }
        choiceCardGOList.Clear();
        Dictionary<ElementType, int> tmp = new Dictionary<ElementType, int>();
        tmp.Add(ElementType.Fire, 0);
        tmp.Add(ElementType.Water, 0);
        tmp.Add(ElementType.Earth, 0);
        tmp.Add(ElementType.Wind, 0);
        tmp.Add(ElementType.Thunder, 0);
        tmp.Add(ElementType.Dark, 0);
        string s = data.Replace("{", "");
        s = s.Replace("}", "");
        string[] playerCardInfo = s.Split(';');
        foreach (string s1 in playerCardInfo)
        {
            string i = s1.Replace("[", "");
            i = i.Replace("]", "");
            tmp[(ElementType)Enum.Parse(typeof(ElementType), i.Split(',')[0])] = int.Parse(i.Split(',')[1]);
        }
        List<ElementType> element_keys = new List<ElementType>();
        element_keys.AddRange(cardInfoDict.Keys);
        foreach (ElementType e in element_keys)
        {
            if (tmp[e] == cardInfoDict[e]) continue;
            for (int i = tmp[e] - cardInfoDict[e]; i > 0; i--)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/card"), transform.Find("PanelFight/PanelCardInfo/ScrollViewCard/Viewport/Content").transform);
                go.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/" + e.ToString().ToLower());
                go.transform.GetComponent<CardCtrl>().elemType = e;
                go.transform.GetComponent<Button>().onClick.AddListener(delegate () { OnChoiceCardClick(go); });
            }
            cardInfoDict[e] = tmp[e];
        }
       
    }


    //点卡事件
    private void OnChoiceCardClick(GameObject go) {
        CardCtrl cardCtrl = go.transform.GetComponent<CardCtrl>();
        if (cardCtrl.ctrl == true)
        {
            go.transform.Find("chooseBorder").gameObject.SetActive(false);
            //将选择的牌下移
            go.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            go.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            OutCardInfoDict[cardCtrl.elemType] -= 1;
            outCardData = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", OutCardInfoDict[ElementType.Fire], OutCardInfoDict[ElementType.Water], OutCardInfoDict[ElementType.Earth],
                                                             OutCardInfoDict[ElementType.Wind], OutCardInfoDict[ElementType.Thunder], OutCardInfoDict[ElementType.Dark]);
            cardInfoDict[cardCtrl.elemType] += 1;
            cardCtrl.ctrl = false;
            choiceCardGOList.Remove(go);
            SetCardPreviewInfo();
            return;
        }
        else {
            go.transform.Find("chooseBorder").gameObject.SetActive(true);
            //将选择的牌上移
            go.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
            go.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
            cardCtrl.ctrl = true;
            OutCardInfoDict[cardCtrl.elemType] += 1;
            outCardData = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", OutCardInfoDict[ElementType.Fire], OutCardInfoDict[ElementType.Water], OutCardInfoDict[ElementType.Earth],
                                                             OutCardInfoDict[ElementType.Wind], OutCardInfoDict[ElementType.Thunder], OutCardInfoDict[ElementType.Dark]);
            cardInfoDict[cardCtrl.elemType] -= 1;
            choiceCardGOList.Add(go);
            if(useCardPreview)
                cardPreview_Panel.gameObject.SetActive(true);
            SetCardPreviewInfo();
            return;
        }
    }

    //出牌事件
    private void OnOutCardClick() {

        int count = OutCardInfoDict[ElementType.Fire] + OutCardInfoDict[ElementType.Water] + OutCardInfoDict[ElementType.Earth] + OutCardInfoDict[ElementType.Wind] + OutCardInfoDict[ElementType.Thunder] + OutCardInfoDict[ElementType.Dark];
        if (count == 0) return;
        if (count > (int)playerRateOfActionPoint) return;
        string data = outCardData;
        outCardRequest.SendRequest(data);
        playerEffectKey = data;
        transform.Find("DrawCard").GetComponent<Button>().interactable=false;
    }


    //出牌请求回应
    public void OnOurCardResponse(string data) {
        string[] strs = data.Split('|');
        if ((ReturnCode)int.Parse(strs[0]) == ReturnCode.Success)
        {
            outCardData = "000000";
            playerCardInfo = strs[1];
            playerEffectKey = data.Split('#')[1];
            isSynchroCardInfo = true;
            OutCardInfoDict[ElementType.Fire] = 0;
            OutCardInfoDict[ElementType.Water] = 0;
            OutCardInfoDict[ElementType.Earth] = 0;
            OutCardInfoDict[ElementType.Wind] = 0;
            OutCardInfoDict[ElementType.Thunder] = 0;
            OutCardInfoDict[ElementType.Dark] = 0;
            isPlayerEffect = true;
        }
        
        isAccept = true;
    }
    public void OnUseSkillResponse(string data) {
        if ((ReturnCode)int.Parse(data) == ReturnCode.Success) {
            //调用技能特效
            isPlayerSkillEffect = true;
        }
    }

    //血量飘字
    private void ShowHpChange(float old_Hp,float new_Hp,GameObject target,Vector3 scale) {
            GameObject textGO = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/Damage_Text"), target.transform.parent.transform);
            textGO.GetComponent<Text>().text = new_Hp - old_Hp < 0 ?(new_Hp - old_Hp).ToString(): "+"+(new_Hp - old_Hp).ToString();
            textGO.GetComponent<Text>().color = new_Hp - old_Hp<0 ? new Color(1, 0, 0, 1): new Color(0, 1, 0, 1);
            textGO.transform.localScale = scale;
    }

    private void OnCardPreviewClick() { 
        if (PlayerPrefs.GetInt("PreviewTopControlBool_Save") == 0)
        {
            useCardPreview = false;
            transform.Find("Preview_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/preview");
            PlayerPrefs.SetInt("PreviewTopControlBool_Save", 1);
        }
        else
        {
            useCardPreview = true;
            cardPreview_Panel.gameObject.SetActive(true);
            transform.Find("Preview_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/previewWait");
            PlayerPrefs.SetInt("PreviewTopControlBool_Save", 0);
        }
    }


    public void GameOverResponse(string data) {
        resultCode = (ReturnCode)int.Parse(data);
        isOver = true;
    }

    private void OnGameOverClick() {
        SceneManager.LoadScene("Main_Scene");
    }

    public void OnEffectResponse(string data) {
        string[] strs = data.Split('|');
        enemyEffectKey = strs[1];
        isEnemyEffect = true;
    }

    public void OnSkillEffectResponse(string data)
    {
        string[] strs = data.Split('|');
        enemySkillKey = strs[1];
        isEnemySkillEffect = true;
    }

    private void OnSkillBtnClick(string data) {
        playerSkillKey = data;
        Debug.Log(data);
        useSkillRequest.SendRequest(data);
    }



    public void GenerateEffect(string isLongRange, string EffectName)
    {
        if (isLongRange == "0")
        {
            playerAnimator.SetTrigger("attack");
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
                DestroySelf.isEnemy = false;
            }
        }
        else if (isLongRange == "1")
        {
            playerAnimator.SetTrigger("spell");
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
                DestroySelf.isEnemy = false;
            }
            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(0).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(100, 100, 1);
        }
        else {
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(player_heroGO.transform);
                effect.transform.localScale = new Vector3(1, 1, 1);
                effect.transform.localPosition = new Vector3(0, 0, 0);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
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
            enemyAnimator.SetTrigger("attack");
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                effect.transform.localPosition = new Vector3(-effect.transform.localPosition.x, effect.transform.localPosition.y, effect.transform.localPosition.z);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
                DestroySelf.isEnemy = true;
            }
        }
        else if (isLongRange == "1")
        {
            enemyAnimator.SetTrigger("spell");
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(GameObject.Find("Effect").transform);
                effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                effect.transform.localPosition = new Vector3(-effect.transform.localPosition.x, effect.transform.localPosition.y, effect.transform.localPosition.z);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
                DestroySelf.isEnemy = true;
            }
            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(1).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(-100, 100, 1);
        }
        else {
            if (EffectName != "0")
            {
                EffectName = EffectName.Replace("Prefab/CardEffects/", "");
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/" + EffectName)) as GameObject;
                effect.transform.SetParent(enemy_heroGO.transform);
                effect.transform.localScale = new Vector3(1, 1, 1);
                effect.transform.localPosition = new Vector3(0, 0, 0);
                //effect.transform.localScale = new Vector3(-effect.transform.localScale.x, effect.transform.localScale.y, effect.transform.localScale.z);
                //effect.transform.localPosition = new Vector3(-effect.transform.localPosition.x, effect.transform.localPosition.y, effect.transform.localPosition.z);
                effect.transform.GetComponent<DestroySelf>().isPVP = true;
                effect.transform.GetComponent<DestroySelf>().pvpFightPanel = this;
                DestroySelf.isEnemy = true;
            }
            GameObject spell = Instantiate(Resources.Load<GameObject>("Prefab/CardEffects/SpellEffect")) as GameObject;
            spell.transform.SetParent(fightInfoPanel.transform.GetChild(1).transform);
            spell.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            spell.transform.localScale = new Vector3(-100, 100, 1);
        }
    }

    public void SetCardPreviewInfo() {
        //cardPreview_Panel.transform.Find("").GetComponent<Image>().sprite = "";
        string data = outCardData.Replace("|", "");
        int count = int.Parse(outCardData.Split('|')[0]) + int.Parse(outCardData.Split('|')[1]) + int.Parse(outCardData.Split('|')[2]) + int.Parse(outCardData.Split('|')[3]) + int.Parse(outCardData.Split('|')[4]) + int.Parse(outCardData.Split('|')[5]);
        SpecialBuff spBuff = new SpecialBuff();
        string combineName = "";
        string fixedDamage = "";
        if (card_info_dict.ContainsKey(data))
        {
            spBuff = card_info_dict[data];
            combineName = spBuff.CombineName;
            fixedDamage = spBuff.FixedDamage;
        }
        else {
            combineName = count == 4 ? "圣者遗物" : "圣剑";
            fixedDamage = count == 4 ? "45" : "60";
            cardPreview_Panel.transform.Find("ImageGrade").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/A");
        }
        cardPreview_Panel.transform.Find("CombineName_Text").GetComponent<Text>().text = combineName + "(造成" + fixedDamage + "点伤害)";
        cardPreview_Panel.transform.Find("CombineDes_Text").GetComponent<Text>().text = "";
        if (!card_info_dict.ContainsKey(data)) return;

        string combineDes = "";
        if (spBuff.buffID1 != "0") {
            BuffCode buffcode = (BuffCode)Enum.Parse(typeof(BuffCode), spBuff.buffID1);
            Buff buffInfo = buff_info_dict[buffcode];
            combineDes += buffInfo.BuffName + "(+" + spBuff.addNumber1 + "层)\n"+buffInfo.BuffDes;
        }
        if (spBuff.buffID2 != "0")
        {
            BuffCode buffcode = (BuffCode)Enum.Parse(typeof(BuffCode), spBuff.buffID2);
            Buff buffInfo = buff_info_dict[buffcode];
            combineDes += buffInfo.BuffName + "(+" + spBuff.addNumber2 + "层)\n" + buffInfo.BuffDes;
        }
        if (spBuff.buffID3 != "0")
        {
            BuffCode buffcode = (BuffCode)Enum.Parse(typeof(BuffCode), spBuff.buffID3);
            Buff buffInfo = buff_info_dict[buffcode];
            combineDes += buffInfo.BuffName + "(+" + spBuff.addNumber3 + "层)\n" + buffInfo.BuffDes;
        }
        if (spBuff.buffID4 != "0")
        {
            BuffCode buffcode = (BuffCode)Enum.Parse(typeof(BuffCode), spBuff.buffID4);
            Buff buffInfo = buff_info_dict[buffcode];
            combineDes += buffInfo.BuffName + "(+" + spBuff.addNumber4 + "层)\n" + buffInfo.BuffDes;
        }
        cardPreview_Panel.transform.Find("CombineDes_Text").GetComponent<Text>().text = combineDes;
        cardPreview_Panel.transform.Find("ImageGrade").transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Icon/"+spBuff.Grade);
    }


    private IEnumerator WaitForCreateElemBall(bool isPlayer, string islongRange = "", string effectName = "")
    {
        if (isPlayer)
        {
            isPlayerBlackHoleOver = false;
            while (!playerBlackHole.GetComponent<BlackHoleCenter>().isFinishElemBall)
            {
                yield return null;
            }
            isPlayerBlackHoleOver = true;
            GenerateEffect(islongRange, effectName);
        }
        else
        {
            isEnemyBlackHoleOver = false;
            while (!enemyBlackHole.GetComponent<BlackHoleCenter>().isFinishElemBall)
            {
                yield return null;
            }
            isEnemyBlackHoleOver = true;
            GenerateEnemyEffect(islongRange, effectName);
        }
    }

    public void ShowTmer(string data) {
        timeInfo = data;
        isShowTimer = true;
    }

    private void SyncShowTmer(string data)
    {
        showTimePanel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        showTimePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/showtime/" + data);
        if (data == "fight") {
            timer_animation = true;
        }
    }

    private void SetStartInfo() {
        maskPanel.gameObject.SetActive(true);
        maskLeftPanel.transform.Find("playerIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>(player_hero.Texture_path);
        maskLeftPanel.transform.Find("playerName/Text").GetComponent<Text>().text = player_name;
        //maskLeftPanel.transform.Find("playerPhase/Text").GetComponent<Text>().text = "";

        maskRightPanel.transform.Find("enemyIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>(enemy_hero.Texture_path);
        maskRightPanel.transform.Find("enemyName/Text").GetComponent<Text>().text = enemy_name;
        //maskRightPanel.transform.Find("enemyPhase/Text").GetComponent<Text>().text = "";

    }

    private void OnDownCardClick() {
        for (int i = choiceCardGOList.Count-1;i>=0;i--) {
            OnChoiceCardClick(choiceCardGOList[i]);
        }
    }

    public void OnTakeDamageResponse(string data) {

    }
}
