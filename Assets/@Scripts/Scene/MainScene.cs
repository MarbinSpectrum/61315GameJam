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
        Managers.Map.CreateMap(stageNum);
        Managers.Chocolate.SetChocolates(stageNum);
    }
}
