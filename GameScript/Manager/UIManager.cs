using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class UIManager : BaseManager
{
    /// <summary>
    /// 定义：
    /// canvasTransform：创建对象——画布
    /// panelPathDict：用于存储所有面板Prefab的路径
    /// panelDict：用于保存所有实例化面板的游戏物体身上的BasePanel组件
    /// panelStack：栈，控制面板的进栈显示，出栈消失等
    /// msgPanel：创建一个对象，用于持有MessagePanel面板，用于信息显示
    /// 方法：
    /// UIManager:base(facade)：自身的构造函数，继承基类，让自身持有facade
    /// OnInit：初始化函数，将枚举类型中对应的面板推进栈中
    /// CanvasTransform：get方法，使其他模块可以调用该画布
    /// PushPanel：把某个页面入栈，把某个面板显示在界面上
    /// PopPanel：出栈，把面板从界面上移除
    /// GetPanel：根据面板类型得到实例化的面板
    /// ParseUIPanelTypeJson：解析面板类型的Json，并将其加入panelPathDict中，用于存储所有面板Prefab的路径，便于查找
    /// InjectMsgPanel：信息面板的注入函数，使得UIMng持有信息面板，便于其他面板调用
    /// ShowMessage：本地消息的显示
    /// ShowMessageSync：异步消息的显示
    /// 用于：
    /// 所有面板的中介者，控制所有的面板，包括对应请求码的面板的显示等
    /// </summary>

    /// 
    /// 单例模式的核心
    /// 1，定义一个静态的对象 在外界访问 在内部构造
    /// 2，构造方法私有化

    //private static UIManager _instance;

    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new UIManager();
    //        }
    //        return _instance;
    //    }
    //}


    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack;
    private Transform canvasTransform;
    private UIPanelType panelTypeToPush = UIPanelType.None;
    private UISenceType senceType = UISenceType.None;
    private MessagePanel msgPanel;


    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    public UIManager(GameFacade facade) : base(facade)
    {
        ParseUIPanelTypeJson();
    }

    public override void OnInit()
    {
        base.OnInit();
        PushPanel(UIPanelType.BG);
        PushPanel(UIPanelType.Message);
    }

    public override void Updata()
    {
        if (panelTypeToPush != UIPanelType.None)
        {
            PushPanel(panelTypeToPush);
            panelTypeToPush = UIPanelType.None;
        }
        if (senceType != UISenceType.None)
        {
            SceneManager.LoadScene(Enum.GetName(typeof(UISenceType),senceType));
            senceType = UISenceType.None;
        }
        base.Updata();
    }
    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public BasePanel PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
        return panel;
    }

    public void PushPanelSync(UIPanelType panelType)
    {
        panelTypeToPush = panelType;
    }

    public void PushSenceSync(UISenceType senceType)
    {
        this.senceType = senceType;
    }

    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();

    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            //instPanel.GetComponent<BasePanel>().UIMng = this;
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    public void SetUIMsgPanel(MessagePanel msgPanel) {
        this.msgPanel = msgPanel;
    }

    public void ShowMessageSync(string msg) {
        msgPanel.ShowMessageSync(msg);
    }

    

}
