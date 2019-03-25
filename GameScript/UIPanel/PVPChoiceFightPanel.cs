using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using System.Text;
using UnityEngine.SceneManagement;

public class PVPChoiceFightPanel : BasePanel
{

    private Transform PVPPanel;

    private StartMatchingRequest startMatchingRequest;
    private QuitMatchingRequest quitMatchingRequest;

    private List<HeroCard> userHeroCardList = new List<HeroCard>();
    private List<GameObject> choiceCardList = new List<GameObject>();
    private Dictionary<string, bool> whichElementChoiced = new Dictionary<string, bool>();

    private HeroCard mainHero;
    private int mainHeroIndex = -1;
    private GameObject mainHeroGO;
    private HeroCard staffHero;
    private int staffHeroIndex = -1;
    private GameObject staffHeroGO;
    private int elementCount = 0;

    private skill_type skill_index;

    private Transform leftPanel;
    private Transform rightPanel;
    private Transform mainHeroPanel;
    private Transform staffHeroPanel;
    private Transform targetPanel;
    private Transform CenterPanel;
    private Transform TipsPanel;

    private Transform choiceCardPanel;
    private Transform skillDescribePanel;
    private Button FightBtn;

    private Transform MatchingMaskPanel;

    private Sprite icon_action;
    private Sprite icon_passive_1;
    private Sprite icon_passive_2;
    private Sprite icon_passive_3;
    private Sprite icon_staff;
    private GameObject mask;

    private Text CurChooseCardNum;
    private string pvpcardInfo;
    private bool isSuccessMatching = false;


    private bool isQuitMatching = false;

    private void Awake()
    {
        startMatchingRequest = transform.GetComponent<StartMatchingRequest>();
        startMatchingRequest.pvpChoiceFightPanel = this;
        quitMatchingRequest = transform.GetComponent<QuitMatchingRequest>();
        quitMatchingRequest.pvpChoiceFightPanel=this;

        CurChooseCardNum = GameObject.Find("Canvas/ChoiceFightPanel/CurChooseCardNum_Text").GetComponent<Text>();

        mask = GameObject.Find("Canvas").transform.Find("ChoiceFightPanel").transform.Find("mask").gameObject;
        mask.SetActive(false);
        PVPPanel = GameObject.Find("Canvas").transform.Find("PVPPanel").transform;
        CenterPanel = transform.Find("CenterPanel").transform;
        TipsPanel = transform.Find("CenterPanel/TipsPanel").transform;

        leftPanel = transform.Find("LeftPanel").transform;
        rightPanel = transform.Find("RightPanel").transform;

        mainHeroPanel = leftPanel.Find("MainHeroPanel").transform;
        mainHeroPanel.GetComponent<Button>().onClick.AddListener(OnMainHeroBtnClick);
        staffHeroPanel = leftPanel.Find("StaffHeroPanel").transform;
        staffHeroPanel.GetComponent<Button>().onClick.AddListener(OnStaffHeroBtnClick);
        choiceCardPanel = leftPanel.Find("ChoiceCardPanel").transform;
        skillDescribePanel = leftPanel.Find("SkillDescribe").transform;

        TipsPanel.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(OnConfirmBtnClick);

        targetPanel = mainHeroPanel;

        targetPanel.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        skill_index = skill_type.action;



        skillDescribePanel.Find("skillType").transform.GetComponent<Button>().onClick.AddListener(OnSkillTyprClick);


        leftPanel.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitBtnClick);

        FightBtn = rightPanel.Find("FightButton").transform.GetComponent<Button>();
        FightBtn.onClick.RemoveAllListeners();
        FightBtn.onClick.AddListener(OnFightBtnClick);

        whichElementChoiced.Add("Fire", false);
        whichElementChoiced.Add("Water", false);
        whichElementChoiced.Add("Earth", false);
        whichElementChoiced.Add("Wind", false);
        whichElementChoiced.Add("Thunder", false);
        whichElementChoiced.Add("Dark", false);

        choiceCardPanel.Find("fire").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Fire"); });
        choiceCardPanel.Find("water").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Water"); });
        choiceCardPanel.Find("earth").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Earth"); });
        choiceCardPanel.Find("wind").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Wind"); });
        choiceCardPanel.Find("thunder").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Thunder"); });
        choiceCardPanel.Find("dark").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("Dark"); });

        icon_action = Resources.Load<Sprite>("UI/Skill_icon/icon_action");
        icon_passive_1 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_1");
        icon_passive_2 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_2");
        icon_passive_3 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_3");
        icon_staff = Resources.Load<Sprite>("UI/Skill_icon/icon_staff");

        MatchingMaskPanel = transform.Find("MatchingMask").transform;
        transform.Find("MatchingMask/ExitButton").transform.GetComponent<Button>().onClick.AddListener(OnQuitMatchingBtnClick);

    }

    private void OnEnable()
    {
        StartChoice();
        StartElementChoice();
        userHeroCardList = facade.getUserCardList();
        targetPanel = mainHeroPanel;
        CurChooseCardNum.text = elementCount + "/" + "6";
        CurChooseCardNum.color = Color.red;
        foreach (Transform c in rightPanel.Find("Scroll View/Viewport/Content").transform)
        {
            choiceCardList.Remove(c.gameObject);
            Destroy(c.gameObject);
        }
        //StartChoice();
        for (int i = 0; i < userHeroCardList.Count; i++)
        {
            int count = i;
            HeroCard c = userHeroCardList[i];
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/World_HeroCard"), rightPanel.Find("Scroll View/Viewport/Content").transform);
            go.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(c.Icon_path);
            go.transform.Find("Text").GetComponent<Text>().text = c.Hero_name;
            go.transform.GetComponent<Button>().onClick.RemoveAllListeners();
            go.transform.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OnHeroListClick(count);
            });
            choiceCardList.Add(go);
        }
    }

    private void Update()
    {
        if (isSuccessMatching)
        {
            facade.SetPVPInfo(pvpcardInfo);
            MatchingSuccess();
        }
        if (isQuitMatching) {
            MatchingMaskPanel.gameObject.SetActive(false);
            isQuitMatching = false;
        }
    }

    //从角色列表里选择出战、助战角色
    private void OnHeroListClick(int count)
    {
        facade.PlayUIAudio("open");
        if (targetPanel == mainHeroPanel)
        {
            if (mainHeroGO != null)
            {
                Destroy(mainHeroGO);
                mainHeroGO = null;
                mainHero = null;
            }
            if (count == mainHeroIndex)
            {
                choiceCardList[mainHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
                mainHeroIndex = -1;
                ShowSkillDescribe();
                return;
            }
            if (count == staffHeroIndex) StartChoice();
            HeroCard c = userHeroCardList[count];
            mainHero = c;
            GameObject go = GameObject.Instantiate(Resources.Load(c.Model_path) as GameObject, targetPanel.Find("Image").transform.position, targetPanel.Find("Image").transform.rotation, targetPanel.Find("Image").transform);
            mainHeroGO = go;
            if (mainHeroIndex != -1)
                choiceCardList[mainHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
            choiceCardList[count].transform.Find("target").GetComponent<Text>().text = "主战";
            mainHeroIndex = count;
            ShowSkillDescribe();
        }
        else if (targetPanel == staffHeroPanel)
        {
            if (staffHeroGO != null)
            {
                Destroy(staffHeroGO);
                staffHeroGO = null;
                staffHero = null;
            }
            if (count == staffHeroIndex)
            {
                choiceCardList[staffHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
                staffHeroIndex = -1;
                ShowSkillDescribe();
                return;
            }
            if (count == mainHeroIndex) StartChoice();
            HeroCard c = userHeroCardList[count];
            staffHero = c;
            GameObject go = GameObject.Instantiate(Resources.Load(c.Model_path) as GameObject, targetPanel.Find("Image").transform.position, targetPanel.Find("Image").transform.rotation, targetPanel.Find("Image").transform);
            staffHeroGO = go;
            targetPanel.Find("Image").transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            if (staffHeroIndex != -1)
                choiceCardList[staffHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
            choiceCardList[count].transform.Find("target").GetComponent<Text>().text = "助战";
            staffHeroIndex = count;
            ShowSkillDescribe();
        }

    }

    //初始化出战角色跟助战角色
    private void StartChoice()
    {
        if (mainHeroIndex != -1)
            choiceCardList[mainHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
        if (staffHeroIndex != -1)
            choiceCardList[staffHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
        if (mainHeroGO != null)
            Destroy(mainHeroGO);
        if (staffHeroGO != null)
            Destroy(staffHeroGO);
        mainHero = null;
        mainHeroIndex = -1;
        mainHeroGO = null;
        staffHero = null;
        staffHeroIndex = -1;
        staffHeroGO = null;
        ShowSkillDescribe();
    }

    //出战框事件
    private void OnMainHeroBtnClick()
    {
        facade.PlayUIAudio("open");
        if (targetPanel == mainHeroPanel)
        {
            if (mainHeroGO != null)
            {
                Destroy(mainHeroGO);
                mainHeroGO = null;
                mainHero = null;
            }
            if (mainHeroIndex != -1)
                choiceCardList[mainHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
            mainHeroIndex = -1;
            ShowSkillDescribe();
            return;
        }
        mainHeroPanel.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        if (staffHero == null)
        {
            staffHeroPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            staffHeroPanel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        targetPanel = mainHeroPanel;
        ShowSkillDescribe();
    }

    //助战框事件
    private void OnStaffHeroBtnClick()
    {
        facade.PlayUIAudio("open");
        if (targetPanel == staffHeroPanel)
        {
            if (staffHeroGO != null)
            {
                Destroy(staffHeroGO);
                staffHeroGO = null;
                staffHero = null;
            }
            if (staffHeroIndex != -1)
                choiceCardList[staffHeroIndex].transform.Find("target").GetComponent<Text>().text = "";
            staffHeroIndex = -1;
            ShowSkillDescribe();
            return;
        }
        if (mainHero == null)
        {
            mainHeroPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            mainHeroPanel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        staffHeroPanel.GetComponent<Image>().color = new Color(1, 1, 0, 1);
        targetPanel = staffHeroPanel;
        ShowSkillDescribe();
    }

    //技能显示事件
    private void OnSkillTyprClick()
    {
        facade.PlayUIAudio("open");
        if (skill_index == skill_type.staff)
            return;
        if (skill_index == skill_type.action)
        {
            skill_index = skill_type.passive_1;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_passive_1;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Prossive_skill_1.Skill_describe;
        }
        else if (skill_index == skill_type.passive_1)
        {
            skill_index = skill_type.passive_2;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_passive_2;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Prossive_skill_2.Skill_describe;
        }
        else if (skill_index == skill_type.passive_2)
        {
            skill_index = skill_type.passive_3;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_passive_3;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Prossive_skill_3.Skill_describe;
        }
        else if (skill_index == skill_type.passive_3)
        {
            skill_index = skill_type.action;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_action;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Action_skill.Skill_describe;
        }
    }

    //初始显示所选角色技能描述
    private void ShowSkillDescribe()
    {
        if (targetPanel == mainHeroPanel && mainHero != null)
        {
            skillDescribePanel.gameObject.SetActive(true);
            skill_index = skill_type.action;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Action_skill.Skill_describe;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_action;
        }
        else if (targetPanel == staffHeroPanel && staffHero != null)
        {
            skillDescribePanel.gameObject.SetActive(true);
            skill_index = skill_type.staff;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = staffHero.Staff_skill_self.Skill_describe;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_staff;
        }
        else
            skillDescribePanel.gameObject.SetActive(false);
    }

    //开始战斗事件
    private void OnFightBtnClick()
    {
        facade.PlayUIAudio("button");
        string msg = "";
        if (mainHero == null)
        {
            msg = "未选择出战角色，";
            CurChooseCardNum.gameObject.SetActive(false);
        }
        if (elementCount < 3)
        {
            msg += "至少选择3中出战属性牌";
            CurChooseCardNum.gameObject.SetActive(false);
        }
        if (msg != "")
        {
            CenterPanel.gameObject.SetActive(true);
            mask.SetActive(true);
            TipsPanel.Find("Text").GetComponent<Text>().text = msg;
            return;
        }
        facade.SetPlayerFightInfo(mainHero, staffHero, whichElementChoiced);
        StringBuilder elementChoice = new StringBuilder();
        foreach (var key in whichElementChoiced.Keys) {
            if (whichElementChoiced[key])
                elementChoice.Append(key + ",");
        }
        elementChoice.Remove(elementChoice.Length - 1, 1);
        string pvpData = "";
        if (staffHero == null)
        {
            pvpData = string.Format("[{0},{1},{2},{3}]", mainHero.Hero_id, mainHero.Hero_level, "00000", elementChoice.ToString());
        }
        else {
            pvpData = string.Format("[{0},{1},{2},{3}]", mainHero.Hero_id, mainHero.Hero_level, staffHero.Hero_id, elementChoice.ToString());
        }

        facade.SetPVPPlayerInfo(pvpData);
        transform.GetComponent<StartMatchingRequest>().SendRequest(pvpData);
        MatchingMaskPanel.gameObject.SetActive(true);

    }

    private void OnExitBtnClick()
    {
        facade.PlayUIAudio("close");
        this.PVPPanel.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnElementClick(string element)
    {
        facade.PlayUIAudio("open");
        if (choiceCardPanel.Find(element.ToLower() + "/Image").gameObject.activeSelf == false)
        {
            whichElementChoiced[element] = true;
            choiceCardPanel.Find(element.ToLower() + "/Image").gameObject.SetActive(true);
            elementCount++;
        }
        else
        {
            whichElementChoiced[element] = false;
            choiceCardPanel.Find(element.ToLower() + "/Image").gameObject.SetActive(false);
            elementCount--;
        }
        UpdateChooseCardNum();
    }

    private void StartElementChoice()
    {
        elementCount = 0;
        string[] element = { "Fire", "Water", "Earth", "Wind", "Thunder", "Dark" };
        elementCount = 0;
        foreach (string str in element)
        {
            if (whichElementChoiced[str])
            {
                whichElementChoiced[str] = false;
                choiceCardPanel.Find(str.ToLower() + "/Image").gameObject.SetActive(false);
            }
        }
    }

    private void OnConfirmBtnClick()
    {
        facade.PlayUIAudio("close");
        CenterPanel.gameObject.SetActive(false);
        mask.SetActive(false);
        CurChooseCardNum.gameObject.SetActive(true);
    }
    //更新选择卡牌数目提示文本
    public void UpdateChooseCardNum()
    {
        CurChooseCardNum.gameObject.SetActive(true);
        CurChooseCardNum.text = elementCount + "/" + "6";
        if (elementCount >= 3)
        {
            CurChooseCardNum.color = Color.green;
        }
        else
        {
            CurChooseCardNum.color = Color.red;
        }
    }

    public void OnMatchingResponse(string pvpcardInfo)
    {
        this.pvpcardInfo = pvpcardInfo;
        isSuccessMatching = true;
    }
    private void MatchingSuccess()
    {
        facade.PlayBGM("fight");
        isSuccessMatching = false;
        SceneManager.LoadScene("PVPFight_scene");
    }

    public void QuitMatchingResponse() {
        isQuitMatching = true;
    }

    private void OnQuitMatchingBtnClick() {
        facade.PlayUIAudio("button");
        quitMatchingRequest.SendRequest("Quit");
    }
}
