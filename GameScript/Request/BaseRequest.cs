using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour
{
    /// <summary>
    /// 定义：
    /// requestCode：各个request中的默认请求码（None）
    /// actionCode：各个request中的默认执行函数码（None）
    /// facade：持有facade，方便调用其他Manager
    /// 方法：
    /// Awake：unity启动函数，优先于start，用于赋值。获得facade的AddRequest方法，将自身写入字典里
    /// SendRequest(string data)：通过facade发送请求
    /// SendRequest()：使所有子类持有发送请求的函数
    /// OnResponse：使所有子类持有发送响应的函数
    /// OnDestroy：unity销毁函数，用于移除字典中的数据。
    /// 用于：
    /// 所有Request的基类，使得所有Request持有RequestCode、ActionCode、facade以及发送信息的函数等。
    /// </summary>
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade _facade;

    protected GameFacade facade
    {
        get
        {
            if (_facade == null)
            {
                _facade = GameFacade.Instance;
            }
            return _facade;
        }
    }

    // Use this for initialization
    public virtual void Start()
    {
        facade.AddRequest(actionCode, this);
    }

    protected void SendRequest(string data)
    {
        facade.SendRequest(requestCode, actionCode, data);
    }

    //请求的发起
    public virtual void SendRequest() { }

    //请求的响应
    public virtual void OnResponse(string data) { }

    //销毁
    public virtual void OnDestroy()
    {
        if (facade != null)
            facade.RemoveRequest(actionCode);
    }

}
