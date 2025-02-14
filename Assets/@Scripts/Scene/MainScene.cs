using UnityEngine;

public class MainScene : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        Managers.Init();
        Managers.Game.Init();
        Managers.Data.Init();
        Managers.Map.Init();
        Managers.Chocolate.Init();
        
        var stageNum = Managers.Game.stageNum;
        var mapData = Managers.Data.GetMapData(stageNum);
        Managers.Map.CreateMap(stageNum);
        Managers.Chocolate.SetChocolates(stageNum);
        Camera.main.transform.position += new Vector3(mapData.M / 2f - 0.5f , mapData.N / 2f - 0.5f,0);
    }
}
