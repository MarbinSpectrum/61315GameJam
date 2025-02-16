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
    public int MaxStageNumber { get; private set; } = 7;
    
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
        StageNumber = 1;
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
            var n = mapData.N;
            var m = mapData.M;
            var targetCameraPos = _cameraBasePos;
            targetCameraPos.x += 0.5f * m;
            targetCameraPos.z += -1.0f * m;
            targetCameraPos.y += -0.5f * n;
            Camera.main.transform.DOMove(targetCameraPos, 0.5f);
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

    public void StopGame()
    {
        //타이머가 멈춤
        if (_timeCor != null)
        {
            CoroutineHelper.StopCoroutine(_timeCor);
            _timeCor = null;
        }
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
