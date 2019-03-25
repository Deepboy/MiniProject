using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class OutCardRequest : BaseRequest {

    public PVPFightPanel pvpFightPanel;
    public override void Start()
    {
        actionCode = ActionCode.OutCard;
        requestCode = RequestCode.Room;
        base.Start();
    }

    public void SendRequest(string data)
    {
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        Debug.Log(strs[0]);
        if ((ReturnCode)int.Parse(strs[0]) == ReturnCode.Waiting)
        {
            pvpFightPanel.OnEffectResponse(data);
        }
        else {
            pvpFightPanel.OnOurCardResponse(data);
        }
    }
}
