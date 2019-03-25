using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum skill_type
{
    action = 0,
    passive_1 = 1,
    passive_2 = 2,
    passive_3 = 3,
    staff = 4,
}
public class ChoiceFightPanel : BasePanel
{

    private Transform MapPanel;

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

    private Sprite icon_action;
    private Sprite icon_passive_1;
    private Sprite icon_passive_2;
    private Sprite icon_passive_3;
    private Sprite icon_staff;
    private GameObject mask;

    private Text CurChooseCardNum;
    //新手指引相关变量
    private int GuideIndex = 0;
    private Text GuideText;
    private GameObject GuidePanel;
    private GameObject Finger;
    public  static bool IsNew = false;
    private GameObject Guide_mask;

    private void Awake()
    {
        //新手指引变量初始化
        GuidePanel = GameObject.Find("Canvas").transform.Find("ChoiceFightPanel/LeftPanel/GuidePanel").gameObject;
        Finger = GameObject.Find("Canvas").transform.Find("Finger").gameObject;
        GuideText = GameObject.Find("Canvas").transform.Find("ChoiceFightPanel/LeftPanel/GuidePanel/Guide_Text").GetComponent<Text>();
        Guide_mask = GameObject.Find("Canvas").transform.Find("ChoiceFightPanel/LeftPanel/GuidePanel/mask").gameObject;

        CurChooseCardNum = GameObject.Find("Canvas/ChoiceFightPanel/CurChooseCardNum_Text").GetComponent<Text>();

        mask = GameObject.Find("Canvas").transform.Find("ChoiceFightPanel").transform.Find("mask").gameObject;
        mask.SetActive(false);
        MapPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform;
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

        FightBtn = rightPanel.Find("ChoiceHeroButton").transform.GetComponent<Button>();
        FightBtn.onClick.RemoveAllListeners();
        FightBtn.onClick.AddListener(OnFightBtnClick);

        whichElementChoiced.Add("fire", false);
        whichElementChoiced.Add("water", false);
        whichElementChoiced.Add("earth", false);
        whichElementChoiced.Add("wind", false);
        whichElementChoiced.Add("thunder", false);
        whichElementChoiced.Add("dark", false);

        choiceCardPanel.Find("fire").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("fire"); });
        choiceCardPanel.Find("water").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("water"); });
        choiceCardPanel.Find("earth").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("earth"); });
        choiceCardPanel.Find("wind").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("wind"); });
        choiceCardPanel.Find("thunder").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("thunder"); });
        choiceCardPanel.Find("dark").transform.GetComponent<Button>().onClick.AddListener(delegate () { OnElementClick("dark"); });

        icon_action = Resources.Load<Sprite>("UI/Skill_icon/icon_action");
        icon_passive_1 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_1");
        icon_passive_2 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_2");
        icon_passive_3 = Resources.Load<Sprite>("UI/Skill_icon/icon_passive_3");
        icon_staff = Resources.Load<Sprite>("UI/Skill_icon/icon_staff");




    }
    private void Start()
    {
        if (GuideCtrl.GuideIndex.isNew)
        {
            StartCoroutine("AllCommand");
        }
        
    }
    /// <summary>
    /// 新手指引的协程
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllCommand()
    {
        GameObject.Find("Canvas").transform.Find("ChoiceFightPanel/RightPanel/Scroll View").GetComponent<ScrollRect>().horizontal = false;
        GameObject.Find("Canvas").transform.Find("ChoiceFightPanel/RightPanel/Scroll View").GetComponent<ScrollRect>().vertical = false;
        for (int i = 0; i < rightPanel.Find("Scroll View/Viewport/Content").transform.childCount; i++)
        {
            rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        FightBtn.interactable = false;
        Finger.SetActive(true);
        GuidePanel.SetActive(true);
        mainHeroPanel.GetComponent<Button>().interactable = false;
        staffHeroPanel.GetComponent<Button>().interactable = false;
        leftPanel.Find("ExitButton").GetComponent<Button>().interactable = false;

        if (GuideIndex == 0)
        {
            GuideText.text = "请选择火元素卡牌";
            Guide_mask.GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/fire");
            for (int i = 0; i < choiceCardPanel.childCount; i++)
            {
                choiceCardPanel.GetChild(i).GetComponent<Button>().interactable = false;
            }
            choiceCardPanel.GetChild(0).GetComponent<Button>().interactable = true;
            
        }
        while (GuideIndex == 0)
        {
            yield return null;
        }
        if (GuideIndex == 1)
        {
            GuideText.text = "请选择风元素卡牌";
            for (int i = 0; i < choiceCardPanel.childCount; i++)
            {
                choiceCardPanel.GetChild(i).GetComponent<Button>().interactable = false;
            }
            choiceCardPanel.GetChild(3).GetComponent<Button>().interactable = true;
            Finger.GetComponent<Animator>().SetTrigger("step2");
            Guide_mask.GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/wind");
        }
        while (GuideIndex == 1)
        {
            yield return null;
        }
        if (GuideIndex == 2)
        {
            GuideText.text = "请选择雷元素卡牌";
            for (int i = 0; i < choiceCardPanel.childCount; i++)
            {
                choiceCardPanel.GetChild(i).GetComponent<Button>().interactable = false;
            }
            choiceCardPanel.GetChild(4).GetComponent<Button>().interactable = true;
            Finger.GetComponent<Animator>().SetTrigger("step3");
            Guide_mask.GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/thunder");
        }
        while (GuideIndex == 2)
        {
            yield return null;
        }
        if (GuideIndex == 3)
        {
            
            for (int i = 0; i < choiceCardPanel.childCount; i++)
            {
                choiceCardPanel.GetChild(i).GetComponent<Button>().interactable = false;
            }
            for (int i = 0; i < rightPanel.Find("Scroll View/Viewport/Content").transform.childCount; i++)
            {
                rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(0).GetComponent<Button>().interactable = true;

            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/HighLine"));
            effect.transform.SetParent(rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(0).transform);
            effect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            effect.GetComponent<RectTransform>().sizeDelta = new Vector2(175, 175);
            effect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            GuideText.text = "请选择主战角色";
            Finger.GetComponent<Animator>().SetTrigger("step4");
            Guide_mask.GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/big");
        }
        while (GuideIndex == 3)
        {
            yield return null;
        }
        if (GuideIndex == 4)
        {
            for (int i = 0; i < rightPanel.Find("Scroll View/Viewport/Content").transform.childCount; i++)
            {
                rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(i).GetComponent<Button>().interactable = false;
            }

            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefab/HighLine"));
            effect.transform.SetParent(rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(1).transform);
            effect.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            effect.GetComponent<RectTransform>().sizeDelta = new Vector2(175, 175);
            effect.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            Destroy(rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(0).GetChild(1).gameObject);
            staffHeroPanel.GetComponent<Button>().interactable = true;
           
            GuideText.text = "请点击助战框";
            Finger.GetComponent<Animator>().SetTrigger("step5");
            Guide_mask.GetComponent<Image>().sprite = Resources.Load<Sprite>("MainSceneUI/small");
        }
        while (GuideIndex == 4)
        {
            yield return null;
        }
        if (GuideIndex == 5)
        {
            Finger.GetComponent<Animator>().SetTrigger("step7");
            rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        while (GuideIndex == 5)
        {
            yield return null;
        }
        if (GuideIndex == 6)
        {
            skillDescribePanel.gameObject.SetActive(true);
            Destroy(rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(1).GetChild(1).gameObject);

            rightPanel.Find("Scroll View/Viewport/Content").transform.GetChild(1).GetComponent<Button>().interactable = false;
            staffHeroPanel.GetComponent<Button>().interactable = false;
            FightBtn.interactable = true;
            GuideText.text = "配置完毕，请点击出战按钮开始战斗吧！";
            Finger.GetComponent<Animator>().SetTrigger("step6");
        }
        while (GuideIndex == 6)
        {
            yield return null;
        }

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
            if (GuideCtrl.GuideIndex.isNew)
            {
                GuideIndex++;
                choiceCardPanel.GetChild(0).GetComponent<Button>().interactable = false;
            }
          
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
            if (GuideCtrl.GuideIndex.isNew)
            {
                GuideIndex++;
                choiceCardPanel.GetChild(1).GetComponent<Button>().interactable = false;
            }
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
        if (GuideCtrl.GuideIndex.isNew)
        {
            GuideText.text = "请选择助战角色";
            GuideIndex++;
        }
       
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
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text =mainHero.Prossive_skill_1.Skill_name+ ":\n" + mainHero.Prossive_skill_1.Skill_describe;
        }
        else if (skill_index == skill_type.passive_1)
        {
            skill_index = skill_type.passive_2;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_passive_2;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Prossive_skill_2.Skill_name + ":\n" + mainHero.Prossive_skill_2.Skill_describe;
        }
        else if (skill_index == skill_type.passive_2)
        {
            skill_index = skill_type.passive_3;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_passive_3;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Prossive_skill_3.Skill_name + ":\n" + mainHero.Prossive_skill_3.Skill_describe;
        }
        else if (skill_index == skill_type.passive_3)
        {
            skill_index = skill_type.action;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_action;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Action_skill.Skill_name+ ":\n" + mainHero.Action_skill.Skill_describe;
        }
    }

    //初始显示所选角色技能描述
    private void ShowSkillDescribe()
    {
        if (targetPanel == mainHeroPanel && mainHero != null)
        {
            skillDescribePanel.gameObject.SetActive(true);
            skill_index = skill_type.action;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = mainHero.Action_skill.Skill_name + ":\n" + mainHero.Action_skill.Skill_describe;
            skillDescribePanel.Find("skillType").transform.GetComponent<Image>().sprite = icon_action;
        }
        else if (targetPanel == staffHeroPanel && staffHero != null)
        {
            skillDescribePanel.gameObject.SetActive(true);
            skill_index = skill_type.staff;
            skillDescribePanel.Find("skill_describe").transform.GetComponent<Text>().text = staffHero.Staff_skill_self.Skill_name+ ":\n" + staffHero.Staff_skill_self.Skill_describe;
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
        //UnityEngine.SceneManagement.SceneManager.LoadScene("ExcelPrefab");
        LoadAsyncScene.nextSceneName = "ExcelPrefab";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");

    }

    private void OnExitBtnClick()
    {
        facade.PlayUIAudio("button");
        //MapPanel.gameObject.SetActive(true);
        MapPanel.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnElementClick(string element)
    {
        facade.PlayUIAudio("open");
        if (choiceCardPanel.Find(element + "/Image").gameObject.activeSelf == false)
        {
            whichElementChoiced[element] = true;
            choiceCardPanel.Find(element + "/Image").gameObject.SetActive(true);
            elementCount++;
        }
        else
        {
            whichElementChoiced[element] = false;
            choiceCardPanel.Find(element + "/Image").gameObject.SetActive(false);
            elementCount--;
        }
        UpdateChooseCardNum();
        if (GuideCtrl.GuideIndex.isNew)
        {
            GuideIndex++;
        }
        
    }

    private void StartElementChoice()
    {
        elementCount = 0;
        string[] element = { "fire", "water", "earth", "wind", "thunder", "dark" };
        elementCount = 0;
        foreach (string str in element)
        {
            if (whichElementChoiced[str])
            {
                whichElementChoiced[str] = false;
                choiceCardPanel.Find(str + "/Image").gameObject.SetActive(false);
            }
        }
    }

    private void OnConfirmBtnClick()
    {
        facade.PlayUIAudio("button");
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

}
