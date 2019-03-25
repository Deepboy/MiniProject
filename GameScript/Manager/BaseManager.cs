using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    /// <summary>
    /// 定义：
    /// facade：构建facade对象，中介核心，Manager需要调用其他Manager时需通过facade调用
    /// 方法：
    /// BaseManager()：构造函数，持有facade，方便Manager之间的调用
    /// OnInit()：Manager初始化函数
    /// OnDestroy()：Manager销毁函数
    /// 用于：
    /// 所有Manager的基类，所有Manager必须持有的操作在此定义
    /// </summary>
    protected GameFacade facade;
    public BaseManager(GameFacade facade)
    {
        this.facade = facade;
    }

    public virtual void Updata() { }
    public virtual void OnInit() { }
    public virtual void OnDestroy() { }

}
