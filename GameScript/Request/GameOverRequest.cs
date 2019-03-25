using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameOverRequest : BaseRequest {

    public PVPFightPanel pvpFightPanel;
    public override void Start()
    {
        actionCode = ActionCode.GameOver;
        requestCode = RequestCode.Room;
        base.Start();
    }


    public override void OnResponse(string data)
    {
        pvpFightPanel.GameOverResponse(data);   
    }
}
