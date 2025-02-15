using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameManager
{
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public int Score { get; private set; } = 0;
    public int CurrentTimer { get; private set; } = 0;
    public int Timer { get; private set; } = 0;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Const
    private const float MELTING_TIME_PERCENT = 0.1f;

    // ----- Normal
    public int StageNumber = 1;
    
    private Vector3 _cameraBasePos;
    private bool _isPlaying = false;
    private float _meltingInterval = 0f;
    private WaitForSeconds _MeltingWaitTime = null;
    
    // ----- Coroutine
    private Coroutine _timeCor;
    private Coroutine _meltingCor;
    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public void Init()
    {
        Score = 0;
        Timer = 0;
        CurrentTimer = 0;
        StageNumber = 2;
        // 깃허브 액션 테스트
        _cameraBasePos = Camera.main.transform.position;
    }

    public void CreateStage()
    {
        Debug.Log($"[GameManager] CreateStage : {StageNumber}");
        Managers.Map.CreateMap(StageNumber);
        Managers.Chocolate.SetChocolates(StageNumber);
        Managers.Chocolate.ClearMelt();
        Managers.Eater.SetEaters(StageNumber);
        
        var mapData = Managers.Data.GetMapData(StageNumber);
        Timer = mapData.limitTime;
        CurrentTimer = Timer;
        _meltingInterval = (Timer * MELTING_TIME_PERCENT);
        _MeltingWaitTime = new WaitForSeconds(_meltingInterval);

        if (Camera.main != null)
        {
            Camera.main.transform.position = _cameraBasePos;
            float addZPos = Mathf.Max(mapData.M, mapData.N)*0.8f;
            Camera.main.transform.position += new Vector3(mapData.M / 2f + 1f, -0.5f,-addZPos);
        }
        
        SetCoroutines();
    }
    
    public void IncreaseScore(int num = 1)
    {
        Score += num;
    }

    private void SetCoroutines()
    {
        if (_timeCor != null)
        {
            CoroutineHelper.StopCoroutine(_timeCor);
            _timeCor = null;
        }
        
        if (_meltingCor != null)
        {
            CoroutineHelper.StopCoroutine(_meltingCor);
            _meltingCor = null;
        }

        _timeCor = CoroutineHelper.StartCoroutine(TimerCor());
        _meltingCor = CoroutineHelper.StartCoroutine(MeltingCor());
    }
    
    private IEnumerator TimerCor()
    {
        while (CurrentTimer > 0)
        {
            CurrentTimer--;
            yield return new WaitForSeconds(1);
        }
        
        Managers.UI.ShowPopupUI<UI_Popup>("FailPopup");
    }
    
    private IEnumerator MeltingCor()
    {
        var elapsedTime = 0f;

        while (elapsedTime < Timer)
        {
            yield return _MeltingWaitTime;
            
            Managers.Chocolate.OnMeltingChocolates();
            elapsedTime += _meltingInterval;
        }
    }
}
