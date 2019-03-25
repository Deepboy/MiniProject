using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class TakeDamageRequest : BaseRequest {

    public PVPFightPanel pvpFightPanel;
    public override void Start()
    {
        actionCode = ActionCode.TakeDamage;
        base.Start();
    }


    public override void OnResponse(string data)
    {
        //显示伤害
        pvpFightPanel.OnTakeDamageResponse(data);
    }
}
