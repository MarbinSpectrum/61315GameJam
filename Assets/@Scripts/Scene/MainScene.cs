using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField] private GameObject background;

    private bool getBackgroundbasePos = false;
    private Vector3 backgroundBasePos;
    
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

        int stageNumber = Managers.Game.StageNumber;
        var mapData = Managers.Data.GetMapData(stageNumber);
        
        if (background == null)
        {
            Debug.LogError($"[MainScene] Awake() : 배경 오브젝트가 없습니다.");
            return;
        }
        
        if (getBackgroundbasePos == false)
        {
            getBackgroundbasePos = true;
            backgroundBasePos = background.transform.position;
        }

        background.transform.position = backgroundBasePos + new Vector3(mapData.M / 2f + 1f, -0.5f,0);
    }
}
