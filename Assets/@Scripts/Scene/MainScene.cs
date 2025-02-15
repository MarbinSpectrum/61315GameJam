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
        Managers.Sound.Init();
        
        Managers.Game.CreateStage();
        
        Managers.Sound.Play("CB_Bgm",Define.ESoundType.BGM);
    }
}
