using UnityEngine;

public class MainScene : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        CoroutineHelper.Init();
        Managers.Init();
        Managers.Game.Init();
        Managers.Data.Init();
        Managers.Map.Init();
        Managers.Chocolate.Init();
        
        Managers.Game.CreateStage();
    }
}
