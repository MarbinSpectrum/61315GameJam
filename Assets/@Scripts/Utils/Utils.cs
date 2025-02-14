using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{
    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.EUIEvent type = Define.EUIEvent.Click)
    {
        var evt = go.GetOrAddComponent<UI_EventHandler>();
        switch(type)
        {
            case Define.EUIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
                
            case Define.EUIEvent.PointUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
                
            case Define.EUIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
                
            case Define.EUIEvent.BeginDrag:
                evt.OnBeginDragHandler -= action;
                evt.OnBeginDragHandler += action;
                break;

            case Define.EUIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
                
            case Define.EUIEvent.EndDrag:
                evt.OnEndDragHandler -= action;
                evt.OnEndDragHandler += action;
                break;
                
            case Define.EUIEvent.Drop:
                evt.OnDropHandler -= action;
                evt.OnDropHandler += action;
                break;
        }
    }
    
    public static void RemoveUIEvent(GameObject go, Action<PointerEventData> action, Define.EUIEvent type = Define.EUIEvent.Click)
    {
        var evt = go.GetOrAddComponent<UI_EventHandler>();
        switch(type)
        {
            case Define.EUIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                break;
                
            case Define.EUIEvent.PointUp:
                evt.OnPointerUpHandler -= action;
                break;
                
            case Define.EUIEvent.Click:
                evt.OnClickHandler -= action;
                break;
                
            case Define.EUIEvent.BeginDrag:
                evt.OnBeginDragHandler -= action;
                break;

            case Define.EUIEvent.Drag:
                evt.OnDragHandler -= action;
                break;
                
            case Define.EUIEvent.EndDrag:
                evt.OnEndDragHandler -= action;
                break;
                
            case Define.EUIEvent.Drop:
                evt.OnDropHandler -= action;
                break;
        }
    }
}
