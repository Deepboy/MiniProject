using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

public class MapPanel : BasePanel
{
    /// <summary>
    /// 地图
    /// 关卡的容器
    /// </summary>
    private RectTransform Map;
    private Transform Yun;
    private Transform content;
    private Transform canvas;
    private Transform choiceFightPabel;
    private Button ExitBtn;
    private Button ChoiceHeroBtn;
    /// <summary>
    /// 将地图移动到该地点的坐标
    /// 选中关卡目标的位置
    /// content的初始位置
    /// </summary>
    private int MapSpeed = 2;
    private int ItemSpeed = 3;
    private int YunSpeed = 1;
    private Vector3 targetPosition;
    private Vector3 challengeTarget;
    private Vector3 startContentPosition;
    private Vector3 startMapPosition;
    private Vector3 targetItemPosition;
    private Vector2 content_size;
    private Vector2 content_space;
    private Vector3 startScale = new Vector3(0.9f, 0.9f, 0.8f);
    private Color startColor= new Color(210, 210, 210, 255) / 255;
    private Vector3 startChallengeItemPosition = new Vector3(-500, 0, 0);
    private Vector3 startRewardItemPosition = new Vector3(-450, -50, 0);
    /// <summary>
    /// content下的物体，关卡
    /// 关卡的初始位置
    /// 地图中对应关卡的位置
    /// </summary>
    private List<RectTransform> ItemList = new List<RectTransform>();
    private List<Vector3> challengeTargetList = new List<Vector3>();
    private List<Transform> challengeTransformList = new List<Transform>();
    private List<Transform> YunList = new List<Transform>();
    /// <summary>
    /// ScrollView中的参数值
    /// </summary>
    Vector3 _scale;
    Vector3 _challengeNamePosition;
    Vector3 _rewardPosition;
    Color _color;
    int ChoiceItem = 0;
    bool isChoice = false;

    private Sprite target;
    private Sprite untarget;

    private GameObject kong_stage;
    private GameObject stage;
    private List<Stage> userStageList;
    private Dictionary<int, Goods> all_goods_dict=new Dictionary<int, Goods>();

    private int StageIndex = 0;
    //新手指引相关变量
    private int GuideIndex = 0;
    private Text GuideText;
    private GameObject GuidePanel;
    private GameObject Finger;
    //public static bool IsNew = false;
    private GameObject Guide_mask;




    private void Awake()
    {
        //新手指引相关变量初始化
        GuidePanel = GameObject.Find("Canvas").transform.Find("MapPanel/GuidePanel").gameObject;
        GuideText = GuidePanel.transform.Find("Guide_Text").GetComponent<Text>();
        Finger = GuidePanel.transform.Find("Finger").gameObject;


        userStageList = facade.getUserStageList();
        all_goods_dict = facade.getGoodsInfoDict();

        kong_stage = Resources.Load<GameObject>("Prefab/kong_stage");
        stage = Resources.Load<GameObject>("Prefab/stage");
        target = Resources.Load<Sprite>("UI/Map/LightPoint");
        untarget = Resources.Load<Sprite>("UI/Map/UnlightPoint");
        Map = transform.Find("Map").GetComponent<RectTransform>();
        Yun = transform.Find("Map/yun").GetComponent<RectTransform>();
        content = transform.Find("CheckPoint/Viewport/Content").transform;
        targetPosition = transform.Find("targetPosition").transform.position;

    }
  
        
     

     void OnEnable()
    {
        foreach (RectTransform rtItem in content.transform)
        {
            ItemList.Remove(rtItem);
            Destroy(rtItem.gameObject);
        }
        GameObject kong_1 = GameObject.Instantiate(kong_stage, content.transform);
        GameObject kong_2 = GameObject.Instantiate(kong_stage, content.transform);
        ItemList.Add(kong_1.GetComponent<RectTransform>());
        ItemList.Add(kong_2.GetComponent<RectTransform>());
        for (int index = userStageList.Count - 1; index >= 0; index--)
        {
            GameObject go = GameObject.Instantiate(stage, content.transform);
            go.transform.Find("challengeName/Text").GetComponent<Text>().text = userStageList[index].Stage_Name;
            go.transform.Find("reward/reward_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(userStageList[index].Reward_1.Goods_path);
            go.transform.Find("reward/reward_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(userStageList[index].Reward_2.Goods_path);
            go.transform.Find("reward/reward_3").GetComponent<Image>().sprite = Resources.Load<Sprite>(userStageList[index].Reward_3.Goods_path);
            go.transform.Find("reward/reward_4").GetComponent<Image>().sprite = Resources.Load<Sprite>(userStageList[index].Reward_4.Goods_path);
            ItemList.Add(go.GetComponent<RectTransform>());
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(delegate (){OnItemClick(go);});

            go.transform.Find("reward").Find("reward_1").GetComponent<LevelRewardCtrl>().InitReward(userStageList[index].Reward_1);
            go.transform.Find("reward").Find("reward_2").GetComponent<LevelRewardCtrl>().InitReward(userStageList[index].Reward_2);
            go.transform.Find("reward").Find("reward_3").GetComponent<LevelRewardCtrl>().InitReward(userStageList[index].Reward_3);
            go.transform.Find("reward").Find("reward_4").GetComponent<LevelRewardCtrl>().InitReward(userStageList[index].Reward_4);

            foreach (Transform rtTarget in Map.transform)
            {
                if (rtTarget.gameObject.name == userStageList[index].Target_Name)
                {
                    challengeTransformList.Add(rtTarget);
                    challengeTargetList.Add(rtTarget.position);
                }
            }


        }
        GameObject kong_3 = GameObject.Instantiate(kong_stage, content.transform);
        ItemList.Add(kong_3.GetComponent<RectTransform>());
    }

    private void Start()
    {
        foreach (Transform rtTarget in Yun.transform)
        {
            YunList.Add(rtTarget);
        }
        startContentPosition = content.localPosition;
        startMapPosition = Map.position;
        content_size = content.GetComponent<GridLayoutGroup>().cellSize;
        content_space = content.GetComponent<GridLayoutGroup>().spacing;
        targetItemPosition = new Vector3(0, -2*(content_size.y + content_space.y), 0);

        ExitBtn = transform.Find("BG/ExitButton").GetComponent<Button>();
        ExitBtn.onClick.AddListener(OnExitBtnClick);
        ChoiceHeroBtn = transform.Find("BG/ChoiceHeroButton").GetComponent<Button>();
        //FightBtn.onClick.RemoveAllListeners();
        ChoiceHeroBtn.onClick.AddListener(OnChoiceHeroBtnClick);

        canvas = GameObject.Find("Canvas").transform;
        choiceFightPabel = canvas.Find("ChoiceFightPanel").transform;

        if (GuideCtrl.GuideIndex.isNew)
        {
            GuidePanel.SetActive(true);
            ExitBtn.interactable = false;
            for (int i = 2; i < content.childCount-1; i++)
            {
                if(content.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text == "1-1王都")
                {
                    content.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = "战斗指引";
                }
            }
        }
        //播放BGM
        if (facade.BGMName() != "select")
        {
            facade.PlayBGM("select");
        }
    }

    private void Update()
    {

        SetYun();
        setItem();
    }

    private void setItem()
    {
        //content往上移动了几个Item
        int count = ItemList.Count-1 - Convert.ToInt32((content.localPosition.y - startContentPosition.y) / (content_size.y + content_space.y));
        float radio = ItemList.Count - 2 - ((content.localPosition.y - startContentPosition.y) / (content_size.y + content_space.y));
        if (count < 3)
        {
            count = 3;
        }
        for (int i = ItemList.Count - 2; i >1; i--)
        {
            _scale = startScale - new Vector3(0.1f, 0.05f, 0) * Math.Abs(i - radio);
            _challengeNamePosition = startChallengeItemPosition;
            _rewardPosition = startRewardItemPosition;
            challengeTransformList[i - 2].transform.GetComponent<Image>().sprite = untarget;
            if (i > count)
            {
                _color = startColor - new Color(0, 0, 0, 0.5f) * Math.Abs(i - radio);
                ItemList[i].GetComponent<Button>().interactable = false;
            }
            else if (i == count)
            {
                _color = startColor - new Color(0, 0, 0, 0.25f) * Math.Abs(i - radio);
                ItemList[i].GetComponent<Button>().interactable = true;
            }
            else if (i == count - 1)
            {
                ItemList[i].GetComponent<Button>().interactable = true;
                _color = new Color(1, 1, 1, 1) - new Color(0, 0, 0, 1 / 255f) * Math.Abs(i - radio);
                _challengeNamePosition = startChallengeItemPosition;
                _rewardPosition = _challengeNamePosition - new Vector3( _challengeNamePosition.x- startRewardItemPosition.x , 170, 0) * (1 - 1.5f * Math.Abs(i - radio));
                challengeTransformList[i - 2].transform.GetComponent<Image>().sprite = target;
                Map.position = Vector3.Lerp(Map.position, startMapPosition + (targetPosition - challengeTargetList[i - 2]), Time.deltaTime * MapSpeed);
                if (!Input.anyKey && !isChoice)
                {
                    content.localPosition = Vector3.Lerp(content.localPosition, startContentPosition + new Vector3(0, (ItemList.Count-(i + 2)) * (content_size.y + content_space.y), 0), Time.deltaTime * 3 * ItemSpeed);
                }
                if (isChoice)
                {
                    StageIndex = ChoiceItem - 1;
                    content.localPosition = Vector3.Lerp(content.localPosition, startContentPosition + new Vector3(0, (ChoiceItem - 1) * (content_size.y + content_space.y), 0), Time.deltaTime * ItemSpeed);
                }else {
                    StageIndex = i;
                }
            }
            else if (i == count - 2)
            {
                ItemList[i].GetComponent<Button>().interactable = true;
                _color = startColor - new Color(0, 0, 0, 10 / 255f) * Math.Abs(i - radio);
            }
            else if (i == count - 3)
            {
                ItemList[i].GetComponent<Button>().interactable = true;
                _color = startColor - new Color(0, 0, 0, 0.25f) * Math.Abs(i - radio);
            }
            else if (i < count - 3)
            {
                _color = startColor - new Color(0, 0, 0, 0.5f) * Math.Abs(i - radio);
                ItemList[i].GetComponent<Button>().interactable = false;
            }
            setItemColorAndLayer(ItemList[i], _color, _scale, _challengeNamePosition, _rewardPosition);
        }
    }

    private void SetYun() {
        foreach (Transform tf in YunList) {
            if (tf.localPosition.x >= 5000) {
                tf.localPosition = new Vector3(-5000, tf.localPosition.y, tf.localPosition.z);
            }
            tf.localPosition = tf.localPosition + new Vector3(10, 0, 0);
            //tf.localPosition = Vector3.Lerp(tf.localPosition, new Vector3(3700, tf.localPosition.y, tf.localPosition.z), Time.deltaTime * YunSpeed);
        }
    }



    /// <summary>
    /// 设置ScrollView中Item参数
    /// </summary>
    /// <param name="item">目标物体</param>
    /// <param name="color">设置颜色</param>
    /// <param name="scale">设置大小</param>
    /// <param name="challengeNamePosition">设置挑战栏</param>
    /// <param name="rewardPosition">设置奖励栏</param>
    /// <param name="layer">设置层级</param>
    private void setItemColorAndLayer(Transform item,Color color,Vector3 scale,Vector3 challengeNamePosition,Vector3 rewardPosition) {
        item.localScale =scale;
        Transform challengeName=item.Find("challengeName").transform;
        Transform reward=item.Find("reward").transform;
        Transform  text = item.Find("challengeName/Text").transform;
        foreach (Transform tf in reward.transform) {
            tf.GetComponent<Image>().color = new Color(1,1,1,4*(color.a-1)+1);
        }
        reward.GetComponent<Image>().color = color;
        challengeName.GetComponent<Image>().color = color;
        text.GetComponent<Text>().color = new Color(0, 0, 0, color.a);
        challengeName.localPosition = challengeNamePosition;
        reward.localPosition = rewardPosition;
    }

    private void OnExitBtnClick() {
        facade.PlayUIAudio("button");
        LoadAsyncScene.nextSceneName = "Main_Scene";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Middle_Scene");
    }
    private void OnChoiceHeroBtnClick()
    {
        facade.PlayUIAudio("open");
        facade.SetChallengeStageIndex(userStageList[userStageList.Count+1- StageIndex].Stage_ID);
        Debug.Log(userStageList[userStageList.Count + 1 - StageIndex].Stage_Name);
        choiceFightPabel.gameObject.SetActive(true);
        //facade.SetChallengeStageIndex();
        //this.gameObject.SetActive(false);
        //this.gameObject.SetActive(false);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    private void OnItemClick(GameObject ItemGo) {
        if (isChoice) return;
        isChoice = true;
        if (!Input.anyKey)
        {
            int count = ItemList.Count-1;
            foreach (RectTransform rtItem in ItemList)
            {
                if (rtItem == ItemGo.GetComponent<RectTransform>())
                {
                    break;
                }
                count--;
            }
            ChoiceItem = count;
            //content.localPosition = Vector3.Lerp(content.localPosition, startContentPosition + new Vector3(0, (count) * (content_size.y + content_space.y), 0), Time.deltaTime);
        }
        StartCoroutine("isChoiceItem", ItemGo);
    }

    private IEnumerator isChoiceItem(GameObject ItemGo) {
        yield return new WaitForSeconds(2);
        isChoice = false;
    }


    private void OnPvpButtonClick() {
        transform.Find("PVPButton/Text").GetComponent<Text>().text = "退出匹配";
        facade.SetPVPPlayerInfo("[10003,25,00000," + ElementType.Fire.ToString() + "," + ElementType.Water.ToString() + "," + ElementType.Wind.ToString() + "]");
        transform.GetComponent<StartMatchingRequest>().SendRequest("[10003,25,00000,"+ElementType.Fire.ToString()+","+ElementType.Water.ToString()+","+ElementType.Wind.ToString()+"]");
    }


}
