using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class QuitMatchingRequest : BaseRequest {

    public PVPChoiceFightPanel pvpChoiceFightPanel;
    public override void Start()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitMatching;
        base.Start();
    }

    public void SendRequest(string data)
    {
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            pvpChoiceFightPanel.QuitMatchingResponse();
        }
    }
}
