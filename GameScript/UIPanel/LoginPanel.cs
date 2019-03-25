using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

public class LoginPanel : BasePanel
{

    /// <summary>
    /// 定义：
    /// closeButton：建立对象，用于获得面板上关闭按钮
    /// usernameIF：建立对象，用于获得面板上用户名的内容
    /// passworldIF：建立对象，用于获得面板上密码的内容
    /// loginButton：建立对象，用于获得面板上登录按钮
    /// registerButton：建立对象，用于获得面板上注册按钮
    /// loginRequest：建立对象，用于获得面板上loginRequest脚本， 
    /// 方法：
    /// OnEnter：
    /// OnCloseClick：点击关闭按钮时响应的函数
    /// OnLoginClick：点击登录按钮时响应的函数
    /// OnLoginResponse：处理登录响应（即点击登录按钮之后，服务器返回的消息的处理）
    /// OnRegisterClick：点击注册按钮时响应的函数
    /// OnExit：控制面板的退出
    /// 用于：
    /// 登录面板上所有功能操作及响应在此发起
    /// </summary>

    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    
    private LoginRequest loginRequest;
    private bool isFighting = false;

    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        usernameIF = transform.Find("username_Text/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("pwd_Text/InputField").GetComponent<InputField>();
        closeButton = transform.Find("ExitGame_Button").GetComponent<Button>();
        closeButton.onClick.AddListener(OnExitGameButtonClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }

    private void Update()
    {
        if (isFighting) {
            isFighting = false;
            LoadAsyncScene.nextSceneName = "PVPFight_scene";
            uiMng.PushSenceSync(UISenceType.Middle_Scene);
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }


    public override void OnPause()//对于登录面板，若有新的面板入栈，则需要隐藏自身，所以需要gameObject.SetActive(false)
    {
        base.OnPause();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
    private void OnExitGameButtonClick()
    {
        facade.PlayUIAudio("close");
        uiMng.PushPanel(UIPanelType.ExitGame);
    }

    private void OnLoginClick()
    {
        facade.PlayUIAudio("button");
        //uiMng.PushSenceSync(UISenceType.Main_Scene);
        string msg = "";
        LoadAsyncScene.nextSceneName = "Main_Scene";
        uiMng.PushSenceSync(UISenceType.Middle_Scene);
        return;
        if (usernameIF.text.Contains(",") || passwordIF.text.Contains(",")) {
            msg += "用户名、密码不能含有非法字符 ！";
        }
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空 !";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空 !";
        }
        if (msg != "")
        {
            //Debug.Log(msg);
            uiMng.ShowMessageSync(msg);
            return;
        }
        //TODO 发送到服务器端进行验证
        Debug.Log("账号：" + usernameIF.text + ",密码：" + passwordIF.text);
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    public void OnLoginResponse(ReturnCode returnCode,string playerInfo,string pvpInfo,string data)
    {
        if (returnCode == ReturnCode.Success)
        {
            Debug.Log("验证结果：正确");
            //跳转界面
            //TODO
            //uiMng.PushSenceSync(UISenceType.Main_Scene);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Guild");
            LoadAsyncScene.nextSceneName = "Main_Scene";
            uiMng.PushSenceSync(UISenceType.Middle_Scene);
        }
        else if (returnCode == ReturnCode.Fail)
        {
            Debug.Log("验证结果：错误");
            uiMng.ShowMessageSync(data);
        }
        else {
            facade.SetPVPInfo(pvpInfo);
            facade.SetPVPPlayerInfo(playerInfo);
            facade.SetPVPState(true);
            isFighting = true;
        }
    }

    private void OnRegisterClick()
    {
        facade.PlayUIAudio("button");
        uiMng.PushPanel(UIPanelType.Register);
    }


   
}
