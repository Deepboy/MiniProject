using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class StartMatchingRequest : BaseRequest
{
    public PVPChoiceFightPanel pvpChoiceFightPanel;
    public override void Start()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.StartMatching;
        base.Start();
    }

    public void SendRequest(string data )
    {
        Debug.Log(data);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split('@');
        ReturnCode returnCode = (ReturnCode)Enum.Parse(typeof(ReturnCode), strs[0]);
        if (returnCode == ReturnCode.Success) {
            string self = strs[1];
            string other = strs[2];
            string cardInfo = strs[3];
            string orgHp = strs[4];
            string orgSpeed = strs[5];
            pvpChoiceFightPanel.OnMatchingResponse(string.Format("{0}@{1}@{2}@{3}@{4}", self,other,cardInfo,orgHp,orgSpeed));
        }
    }

}
