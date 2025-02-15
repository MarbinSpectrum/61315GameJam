
using UnityEngine;

public class GameManager
{
    public int Score { get; private set; } = 0;
    public int limitTime = 0;
    public int stageNum = 1;

    public void Init()
    {
        Score = 0;
        limitTime = 0;
        stageNum = 1;
    }

    public void CreateStage()
    {
        Debug.Log($"[GameManager] CreateStage : {stageNum}");
        Managers.Map.CreateMap(stageNum);
        Managers.Chocolate.SetChocolates(stageNum);
        Managers.Eater.SetEaters(stageNum);
        var mapData = Managers.Data.GetMapData(stageNum);

        if (Camera.main != null)
            Camera.main.transform.position += new Vector3(mapData.M / 2f + 1f, -0.5f,0f);
    }
    
    public void IncreaseScore(int num = 1)
    {
        Score += num;
    }
}
