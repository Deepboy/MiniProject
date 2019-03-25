using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest
{
    /// <summary>
    /// 定义：
    /// loginPanel：登录面板，用于后面的调用
    /// 方法：
    /// Awake：初始化赋值
    /// SendRequest：通过BaseRequest转facade发送请求
    /// OnResponse：响应函数，对服务器传回来的对应的数据进行响应
    /// 用于：
    /// 用于发起登录的请求
    /// </summary>


    private LoginPanel loginPanel;

    // Use this for initialization

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Start();
        facade.setUserInfo("");
    }

    public void SendRequest(string username, string password)
    {
        string data = username + ',' + password;
        //Debug.Log(data);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        Debug.Log(data);
        string[] strs = data.Split('#');
        string pvpInfo = "";
        string playerInfo = "";
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success) {
            Debug.Log(strs[0] + "|" + strs[1]);
            facade.setUserInfo(strs[0] + "|" + strs[1]);
        }
        if (returnCode == ReturnCode.Waiting) {
            string info = strs[0] + "|" + strs[1];
            facade.setUserInfo(info);
            pvpInfo = strs[3];
            playerInfo = strs[2];
        }
        loginPanel.OnLoginResponse(returnCode, playerInfo, pvpInfo, strs[1]);
    }

}
