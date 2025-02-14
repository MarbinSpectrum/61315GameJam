// ----- C#
using System;
using System.Collections;

// ----- Unity
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : MonoBehaviour
{
    // --------------------------------------------------
    // Components   
    // --------------------------------------------------
    [Header("[Option]")]
    [SerializeField] protected bool isCloseDim = true;
    [SerializeField] protected bool isPlaySound = true;
    [SerializeField] protected bool isPlayAnimation = true;
    
    [Space(1.5f)] [Header("[Components]")]
    [SerializeField] protected RectTransform frame = null;
    [SerializeField] protected Image imageDim = null;
    [SerializeField] protected Button buttonClose = null;
    
    // --------------------------------------------------
    // Event
    // --------------------------------------------------
    public event Action OnBeforeCloseAction = null;
    public event Action OnAfterCloseAction = null;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private bool _isClosing = false;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void Awake()
    {
        Managers.UI.SetCanvas(gameObject);
        BindBackGroundAction();
        BindButtonCloseAction();
        Init();
    }

    public virtual void Init()
    {
        PlayAnimation(true);
    }

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Public
    public virtual void ClosePopupUI()
    {
        OnBeforeCloseAction?.Invoke();
        if (isPlayAnimation && frame)
            PlayAnimation(false);
        else
            Managers.UI.ClosePopupUI(this);
        OnAfterCloseAction?.Invoke();
    }
    
    public virtual void Refresh()
    {
    
    }

    public virtual void PlayAnimation(bool isShow)
    {
        if (!isPlayAnimation || !frame) return;
        
        if (isShow)
        {
            frame.transform.localScale = Vector3.zero;            
            var sequence = DOTween.Sequence();
            sequence.Append(frame.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.2f))
                .Append(frame.transform.DOScale(Vector3.one, 0.05f)); 
            sequence.Play();
        }
        else
            StartCoroutine(Co_Close());
    }
    
    public void OnOffDim(bool isOn)
    {
        if (!imageDim)
            return;
        
        imageDim.gameObject.SetActive(isOn);
    }
    
    // ----- Private
    private void BindBackGroundAction()
    {
        if (isCloseDim == false || imageDim == null)
            return;

        
        Utils.AddUIEvent(imageDim?.gameObject, data =>
        {
            if (data.pointerCurrentRaycast.gameObject == imageDim.gameObject)
                ClosePopupUI();
        });
    }

    private void BindButtonCloseAction()
    {
        if (!buttonClose)
            return;
        buttonClose.onClick.AddListener(ClosePopupUI);
    }
    
    // --------------------------------------------------
    // Coroutine
    // --------------------------------------------------
    private IEnumerator Co_Close()
    {
        if (_isClosing) yield break;
        _isClosing = true;
        
        frame.transform.localScale = Vector3.one;            
        var sequence = DOTween.Sequence();
        sequence.Append(frame.transform.DOScale(new Vector3(1.075f, 1.075f, 1.075f), 0.05f))
            .Append(frame.transform.DOScale(Vector3.zero, 0.125f)); 
        sequence.Play();

        yield return new WaitForSeconds(0.175f);
    
        _isClosing = false;
        Managers.UI.ClosePopupUI(this);
    }
}