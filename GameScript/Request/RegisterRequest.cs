using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RegisterRequest : BaseRequest
{

    private RegisterPanel registerPanel;

    // Use this for initialization

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Start();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + ',' + password;
        Debug.Log(data);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        Debug.Log(data);
        string[] strs = data.Split('|');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {
            facade.setUserInfo(data);
        }
        registerPanel.OnRegisterResponse(returnCode);
    }
}
