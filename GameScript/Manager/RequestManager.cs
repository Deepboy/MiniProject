using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManager : BaseManager
{
    /// <summary>
    /// 定义：
    /// requestDict：构造字典类型，<ActionCode,Baserequest>，即一个actionCode对应一个request的执行
    /// 方法：
    /// RequestManager:base(facade)：自身的构造函数，继承基类，让自身持有facade
    /// AddRequest：提供向字典中加入数据的方法
    /// RemoveRequest：提供移除字典中数据的方法
    /// HandleResponse：根据传入的ActionCode，找到对应的方法，并执行OnResponse方法（BaseRequest中的默认方法）
    /// 用于：
    /// 控制整体的响应，是所有request的中介，并提供方法给facade，方便其他Manager调用
    /// </summary>
    public RequestManager(GameFacade facade) : base(facade) { }


    public override void OnInit()
    {
        base.OnInit();
    }

    //构造字典
    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest request = requestDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if (request == null)
        {
            Debug.Log("无法得到ActionCode[" + actionCode + "]对应的Request类");
            return;
        }
        request.OnResponse(data);
    }

}
