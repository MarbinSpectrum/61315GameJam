public class SuccessPopup : UI_Popup
{
    public override void Init()
    {
        base.Init();

        OnAfterCloseAction += NextStage;
        Managers.Sound.Play("Cb_Success",Define.ESoundType.EFFECT);

        var mapData = Managers.Data.GetMapData(Managers.Game.StageNumber + 1);
        if (mapData == null)
        {
            //다음스테이지가 없는듯? 버튼 비활성화
            buttonClose.gameObject.SetActive(false);
        }
    }

    private void NextStage()
    {
        Managers.Game.StageNumber++;
        Managers.Game.CreateStage();
    }
}