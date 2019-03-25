using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : BasePanel
{
    /// <summary>
    /// 定义：
    /// 方法：
    /// 用于：
    /// </summary>

    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterRequest registerRequest;

    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();

        usernameIF = transform.Find("username_Text/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("pwd_Text/InputField").GetComponent<InputField>();
        rePasswordIF = transform.Find("pwd_Text2/InputField").GetComponent<InputField>();

        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("ReturnToLoginButton").GetComponent<Button>().onClick.AddListener(OnReturnToLoginClick);

    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }

    public override void OnPause()
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

    private void OnRegisterClick()
    {
        facade.PlayUIAudio("button");
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空！";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "\n密码不能为空！";
        }
        if (passwordIF.text != rePasswordIF.text)
        {
            msg += "\n两次输入的密码不一致！";
        }
        if (msg != "")
        {
            //Debug.Log(msg);
            uiMng.ShowMessageSync(msg);
            return;
        }
        registerRequest.SendRequest(usernameIF.text, passwordIF.text);
        //进行注册，发送给服务器端
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //uiMng.PushPanelSync(UIPanelType.Login);
            uiMng.ShowMessageSync("注册成功!");
            uiMng.PushSenceSync(UISenceType.Main_Scene);
        }
        else
        {
            uiMng.ShowMessageSync("用户名已存在！");
        }
    }


    private void OnReturnToLoginClick()
    {
        facade.PlayUIAudio("close");
        uiMng.PushPanel(UIPanelType.Login);
    }

}
