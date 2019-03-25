using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShowTimerRequest : BaseRequest {

    public PVPFightPanel pvpFightPanel;
    public override void Start()
    {
        actionCode = ActionCode.ShowTimer;
        base.Start();
    }


    public override void OnResponse(string data)
    {
        Debug.Log(data);
        pvpFightPanel.ShowTmer(data);
    }
}
