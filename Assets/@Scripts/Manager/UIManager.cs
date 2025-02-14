// ----- C#
using System.Collections.Generic;
using Unity.VisualScripting;

// ----- Unity
using UnityEngine;

public class UIManager
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private int _order = 10;
    private Stack<UI_Popup> _popupPool = new();

    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public GameObject GlobalUIRoot
    {
        get
        {
            var root = GameObject.Find("@UI_Global_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Global_Root" };
                Object.DontDestroyOnLoad(root);
            }
            return root;
        }
    }
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void SetCanvas(GameObject go, bool sort = true)
    {
        var canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
            canvas.sortingOrder = 0;
    }
    
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        var go = Resources.Load<UI_Popup>($"UI/Popup/{name}.prefab");

        if (_popupPool.Count != 0)
        {
            var prevPopup = _popupPool.Peek();
            prevPopup.OnOffDim(false);
        }
        
        var popup = go.GetOrAddComponent<T>();
        _popupPool.Push(popup);
        
        Debug.Log($"[UIManager.ShowPopupUI] {popup.name}를 생성하였습니다.");
        
        go.transform.SetParent(Root.transform);
        return popup;
    }
    

    public void ClosePopupUI(UI_Popup popup)
    {
        if(_popupPool.Count == 0)
            return;
        
        if(_popupPool.Peek() != popup)
        {
            Debug.Log($"[UIManager.ClosePopupUI] {popup.name}를 닫지 못했습니다.");
            return;
        }
        ClosePopupUI();
    }
    
    public void ClosePopupUI()
    {
        if(_popupPool.Count == 0)
            return;
        
        var popup = _popupPool.Pop();
        if (_popupPool.Count != 0)
        {
            var currPopup = _popupPool.Peek();
            currPopup.Refresh();
            currPopup.OnOffDim(true);
        }
    
        Object.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }
}