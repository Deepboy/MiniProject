using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(LayoutElement))]
public class EventTriggerListener : EventTrigger
{

    //默认封装的委托
    public delegate void VoidDelegate(GameObject gameObject);

    //指针点击
    public VoidDelegate onClick;
    //指针按下
    public VoidDelegate onDown;
    //指针抬起
    public VoidDelegate onUp;
    //指针移入
    public VoidDelegate onEnter;
    //指针移出
    public VoidDelegate onExit;
    //指针选择
    public VoidDelegate onSelect;
    //指针更新选择
    public VoidDelegate onUpdateSelect;

    /// <summary>
    /// 搜索所有的gameobject，并绑定EventTriggerListener组件
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static EventTriggerListener GetComponent(GameObject gameObject)
    {
        EventTriggerListener listener = gameObject.GetComponent<EventTriggerListener>();
        if (listener == null)
        {
            listener = gameObject.AddComponent<EventTriggerListener>();
        }
        return listener;
    }

    //卡牌指针点击事件
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    //卡牌指针按下事件
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }

    //卡牌指针弹起事件
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) ;//onUp(gameObject);
    }

    //卡牌指针移入事件
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }

    //卡牌指针移出事件
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }

    //卡牌指针选择事件
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
            onSelect(gameObject);
    }

    //卡牌指针更新选择事件
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
            onUpdateSelect(gameObject);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
