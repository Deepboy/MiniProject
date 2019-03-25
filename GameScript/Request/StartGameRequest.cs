using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class StartGameRequest : BaseRequest
{
    public PVPFightPanel pvpFightPanel;
    public override void Start()
    {
        actionCode = ActionCode.StartGame;
        base.Start();
    }


    public override void OnResponse(string data)
    {
        Debug.Log(data);
        pvpFightPanel.SetGameInfo(data);
    }

}
