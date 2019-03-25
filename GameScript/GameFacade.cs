using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Assets.Script.Model;
using SpecialBuffManager;

public class GameFacade : MonoBehaviour
{
    /// <summary>
    /// 定义：
    /// _instance：建立对象facade，给其他Manager进行调用
    /// uiMng：建立对象，使facade持有uiManager
    /// audioMng：建立对象，使facade持有audioMng
    /// playerMng：建立对象，使facade持有playerMng
    /// cameraMng：建立对象，使facade持有cameraMng
    /// requestMng：建立对象，使facade持有requestMng
    /// clientMng：建立对象，使facade持有clientMng
    /// 方法：
    /// Instance：注册，get方法，使各个Mng可以持有facade
    /// Awake：unity自带函数，最开始的唤醒函数，单例模式核心，即一个客户端的游戏中只存在一个facade
    /// Start：初始化函数
    /// Update：unity自带函数，每帧执行一次
    /// InitManager：注入Mng，即给各个参数进行赋值，赋予对象
    /// DestroyManager：移除各个Mng
    /// OnDestroy：unity自带函数
    /// AddRequest：向字典中加入所需的请求的请求码以及自身
    /// RemoveRequest：向字典中移除对应的请求码及value
    /// HandleResponse：根据字典里对应的Mng处理请求
    /// ShowMessage：显示信息
    /// SendRequest：发送请求
    /// 用于：
    /// 中介者模式核心，各个Mng只能从这里调用其他Mng的函数
    /// </summary>

    private static GameFacade _instance;
    public static GameFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        }
    }


    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    private DataManager dataMng;
    private IOManager ioMng;

    private SaveRequest saveRequest;

    private bool isEnterPlaying = false;

    private void Awake()
    {
        Time.timeScale = 1;
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(_instance.gameObject);


        InitManager();
    }

    public UIManager getUIManger()
    {
        return uiMng;
    }

    
    void Start()
    {
        //InitManager();
        saveRequest = gameObject.GetComponent<SaveRequest>();
    }

    void Update()
    {
        UpdateManager();
    }

    private void UpdateManager()
    {
        uiMng.Updata();
        audioMng.Updata();
        playerMng.Updata();
        requestMng.Updata();
        clientMng.Updata();
        dataMng.Updata();
        ioMng.Updata();
    }
    //构造、初始化函数
    private void InitManager()
    {
        uiMng = new UIManager(this);
        audioMng = new AudioManager(this);
        playerMng = new PlayerManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);
        dataMng = new DataManager(this);
        ioMng = new IOManager(this);


        audioMng.OnInit();
        playerMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
        dataMng.OnInit();
        //ioMng.OnInit();
        uiMng.OnInit();
    }

    private void DestroyManager()
    {
        uiMng.OnDestroy();
        audioMng.OnDestroy();
        playerMng.OnDestroy();
        requestMng.OnDestroy();
        clientMng.OnDestroy();
        dataMng.OnDestroy();
        ioMng.OnDestroy();
    }

    private void OnDestroy()
    {
        DestroyManager();
    }

    //添加请求码
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }

    //移除请求
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }
    //处理请求
    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestMng.HandleResponse(actionCode, data);
    }
    //发送请求
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode, actionCode, data);
    }
    //设置玩家信息
    public void setUserInfo(string data)
    {
        dataMng.setUserInfo(data);
    }

    //获得所有卡牌信息
    public Dictionary<int,HeroCard> getCardInfoDict() {
        return dataMng.GetCardInfoDict();
    }

    //获得物品信息
    public Dictionary<int, Goods> getGoodsInfoDict()
    {
        return dataMng.GetGoodsInfoDict();
    }

    //获得所有关卡信息
    public Dictionary<int, Stage> getStageInfoDict() {
        return dataMng.GetStageInfoDict();
    }

    //获得玩家角色卡信息
    public List<HeroCard> getUserCardList() {
        return dataMng.GetUserCardList();
    }

    //获得玩家物品信息
    public Dictionary<int,Goods> GetUserGoodsDict()
    {
        return dataMng.GetUserGoodsDict();
    }

    //获取玩家当前关卡
    public List<Stage> getUserStageList() {
        return dataMng.GetUserStageList();
    }

    //设置玩家信息
    public UserInfo getUserInfo() {
        return dataMng.getUserInfo();
    }

    public void SetChallengeStageIndex(int index) {
        dataMng.SetChallengeStageIndex(index);
    }

    public Stage GetChallengeStage() {
        return dataMng.GetChallengeStage();
    }

    public void SetPlayerFightInfo(HeroCard mainHero, HeroCard staffHero, Dictionary<string, bool> useElement)
    {
        dataMng.SetPlayerFightInfo(mainHero, staffHero, useElement);
    }

    public void SetEnemyFightInfo(HeroCard mainHero, HeroCard staffHero, Dictionary<string, bool> useElement)
    {
        dataMng.SetEnemyFightInfo(mainHero, staffHero, useElement);
    }


    public HeroCard GetPlayerUseHero()
    {
        return dataMng.GetPlayerUseHero();
    }

    public HeroCard GetPlayerUseStaffHero()
    {
        return dataMng.GetPlayerUseStaffHero();
    }

    public HeroCard GetEnemyUseHero()
    {
        return dataMng.GetEnemyUseHero();
    }

    public Dictionary<string, bool> GetPlayerUseElement()
    {
        return dataMng.GetPlayerUseElement();
    }

    public Dictionary<string, bool> GetEnemyUseElement()
    {
        return dataMng.GetEnemyUseElement();
    }

    public void StageFightResult(Stage stage,ReturnCode returnCode) {
        dataMng.StageFightResult(stage, returnCode);
    }

    public void SaveUserInfo() {
        UserInfo userInfo = dataMng.SaveUserInfo();
        saveRequest.SendRequest(userInfo);
    }
    public Dictionary<int, Skill> GetAllSkillInfoDict()
    {
        return dataMng.GetAllSkillInfoDict();
    }
    #region AudioManager

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="loop">是否循环</param>
    public void PlayBGM(string path, bool loop = true)
    {
        audioMng.PlayBGM(path, loop);
    }

    /// <summary>
    /// 设置BGM音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetBGMVolumn(float volumn)
    {
        audioMng.SetBGMVolumn(volumn);
    }

    /// <summary>
    /// 获得BGM音量
    /// </summary>
    /// <returns></returns>
    public float GetBGMVolumn()
    {
        return audioMng.GetBGMVolumn();
    }

    /// <summary>
    /// 暂停BGM
    /// </summary>
    public void BGMPause()
    {
        audioMng.BGMPause();
    }

    /// <summary>
    /// BGM恢复播放
    /// </summary>
    public void BGMReplay()
    {
        audioMng.BGMReplay();
    }

    /// <summary>
    /// BGM停止播放
    /// </summary>
    public void BGMStop()
    {
        audioMng.BGMStop();
    }

    /// <summary>
    /// 返回正在播放的BGM
    /// </summary>
    /// <returns></returns>
    public string BGMName()
    {
        return audioMng.BGMName();
    }

    /// <summary>
    /// 播放技能音效
    /// </summary>
    /// <param name="path"></param>
    public void PlaySkillAudio(string path, float delayTime = 0f)
    {
        audioMng.PlaySkillAudio(path, delayTime);
    }

    /// <summary>
    /// 设置技能音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetSkillAudioVolumn(float volumn)
    {
        audioMng.SetSkillAudioVolumn(volumn);
    }

    /// <summary>
    /// 获得技能音效音量
    /// </summary>
    /// <returns></returns>
    public float GetSkillAudioVolumn()
    {
        return audioMng.GetSkillAudioVolumn();
    }

    /// <summary>
    /// 暂停技能音效
    /// </summary>
    public void SkillAudioPause()
    {
        audioMng.SkillAudioPause();
    }

    /// <summary>
    /// 技能音效恢复播放
    /// </summary>
    public void SkillAudioReplay()
    {
        audioMng.SkillAudioReplay();
    }

    /// <summary>
    /// 播放出牌特效音效
    /// </summary>
    /// <param name="path"></param>
    public void PlayerDrawCardAudio(string path)
    {
        audioMng.PlayerDrawCardAudio(path);
    }

    /// <summary>
    /// 设置出牌特效音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetDrawCardAudioVolumn(float volumn)
    {
        audioMng.SetDrawCardAudioVolumn(volumn);
    }

    /// <summary>
    /// 获得出牌特效音效音量
    /// </summary>
    /// <returns></returns>
    public float GetDrawCardAudioVolumn()
    {
        return audioMng.GetDrawCardAudioVolumn();
    }

    /// <summary>
    /// 暂停出牌特效音效
    /// </summary>
    public void DrawCardAudioPause()
    {
        audioMng.DrawCardAudioPause();
    }

    /// <summary>
    /// 出牌特效音效恢复播放
    /// </summary>
    public void DrawCardAudioReplay()
    {
        audioMng.DrawCardAudioReplay();
    }

    /// <summary>
    /// 播放UI音效
    /// </summary>
    /// <param name="path"></param>
    public void PlayUIAudio(string path)
    {
        audioMng.PlayUIAudio(path);
    }

    /// <summary>
    /// 设置UI音效音量
    /// </summary>
    /// <param name="volumn"></param>
    public void SetUIAudioVolumn(float volumn)
    {
        audioMng.SetUIAudioVolumn(volumn);
    }

    /// <summary>
    /// 获得UI音效音量
    /// </summary>
    /// <returns></returns>
    public float GetUIAudioVolumn()
    {
        return audioMng.GetUIAudioVolumn();
    }

    #endregion

    public void SetPVPInfo(string pvpcardInfo) {
        dataMng.SetPVPInfo(pvpcardInfo);
    }
    public string GetPVPInfo() {
        return dataMng.getPVPInfo();
    }

    public Dictionary<BuffCode, Buff> GetBuffInfoDict()
    {
        return dataMng.GetBuffInfoDict();
    }
    public Dictionary<string, SpecialBuff> GetCardEffectDict()
    {
        return dataMng.GetCardEffectDict();
    }

    public void SetPVPPlayerInfo(string pvpPlayerInfo) {
        dataMng.SetPVPPlayerInfo(pvpPlayerInfo);
    }
    public string GetPVPPlayerInfo() {
        return dataMng.GetPVPPlayerInfo();
    }
    public void SetPVPState(bool isFighting) {
        dataMng.SetPVPState(isFighting);
    }

    public bool GetPVPState() {
        return dataMng.GetPVPState();
    }

}
