using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;




public class EffectOverRequest : BaseRequest {

    public override void Start()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.EffectOver;
        base.Start();
    }

    public void SendRequest(string data)
    {
        //Debug.Log(data);
        base.SendRequest(data);
    }



}
