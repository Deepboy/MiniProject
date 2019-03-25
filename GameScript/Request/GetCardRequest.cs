using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetCardRequest : BaseRequest
{
    private MainSceneCtrl mainScene;

    public override void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.GetHeroCard;
        mainScene = GetComponent<MainSceneCtrl>();
        base.Start();
    }

    public void SendRequest(string times, string diamond)
    {
        string data = times + ',' + diamond;
        Debug.Log(data);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        Debug.Log(data);
        string[] strs = data.Split('@');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {
            string[] getCardData = strs[2].Split('*');
            facade.setUserInfo("test|" + strs[3]);
            if(strs[1] == "Once")
            {
                mainScene.GetCardOnceResponse(getCardData);
            }
            else
            {
                mainScene.GetCardTenTimesResponse(getCardData);
            }
        }
        else
        {
            mainScene.GetCardFailed();
        }
    }
}
