using System.Collections;
using UnityEngine;

public class GameManager
{
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public int Score { get; private set; } = 0;
    public int Timer { get; private set; } = 0;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    public int stageNum = 1;
    private Coroutine timeCor;
    private Vector3 cameraBasePos;
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init()
    {
        Score = 0;
        Timer = 0;
        stageNum = 1;
        cameraBasePos = Camera.main.transform.position;
    }

    public void CreateStage()
    {
        Debug.Log($"[GameManager] CreateStage : {stageNum}");
        Managers.Map.CreateMap(stageNum);
        Managers.Chocolate.SetChocolates(stageNum);
        Managers.Eater.SetEaters(stageNum);
        
        var mapData = Managers.Data.GetMapData(stageNum);
        Timer = mapData.limitTime;

        if (Camera.main != null)
        {
            Camera.main.transform.position = cameraBasePos;
            Camera.main.transform.position += new Vector3(mapData.M / 2f + 1f, -0.5f,0f);
        }

        if (timeCor != null)
        {
            CoroutineHelper.StopCoroutine(timeCor);
            timeCor = null;
        }

        timeCor = CoroutineHelper.StartCoroutine(LimitTimeCor());
    }
    
    public void IncreaseScore(int num = 1)
    {
        Score += num;
    }
    
    private IEnumerator LimitTimeCor()
    {
        //타이머가 1초마다 줄어들게 만듬
        while (Timer > 0)
        {
            Timer--;
            yield return new WaitForSeconds(1);
        }
        
        //타이머가 끝남 게임오버임
        Managers.UI.ShowPopupUI<UI_Popup>("FailPopup");
    }
}
