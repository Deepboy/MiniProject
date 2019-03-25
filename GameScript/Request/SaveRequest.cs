using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class SaveRequest : BaseRequest
{

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Save;
        base.Start();
    }

    public void SendRequest(UserInfo userInfo)
    {
        string data = userInfo.ToString();
        Debug.Log(data);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        
    }

}
