using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    /// <summary>
    /// 定义：
    /// uiMng：构建UIManager对象，用于调用
    /// 方法：
    /// UIMng：用于UIManager的注入吧，使自身持有UIManager
    /// OnEnter：控制界面被显示出来
    /// OnPause：控制界面暂停
    /// OnResume：控制界面继续
    /// OnExit：控制界面不显示,退出这个界面，界面被关系
    /// 用于：
    /// 所有面板的基类，使各个面板都可以重写显示、暂停、继续、消失的操作
    /// </summary>
    protected UIManager _uiMng;
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

    protected UIManager uiMng
    {
        get
        {
            return facade.getUIManger();
        }
    }

    //控制界面被显示出来
    public virtual void OnEnter()
    {

    }

    //控制界面暂停
    public virtual void OnPause()
    {

    }

    //控制界面继续
    public virtual void OnResume()
    {

    }

    //控制界面不显示,退出这个界面，界面被关系
    public virtual void OnExit()
    {

    }
}
