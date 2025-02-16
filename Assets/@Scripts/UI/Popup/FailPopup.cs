public class FailPopup : UI_Popup
{
    public override void Init()
    {
        base.Init();

        OnAfterCloseAction += Retry;
        Managers.Sound.Play("Cb_Fail");
    }

    private void Retry()
    {
        Managers.Game.CreateStage();
    }
}
